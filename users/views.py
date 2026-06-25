from django.shortcuts import render, redirect, get_object_or_404
from django.contrib.auth import login, authenticate, logout
from django.contrib.auth.decorators import login_required, user_passes_test
from django.contrib.auth.models import User
from django.contrib import messages
from django.core.mail import send_mail
from django.conf import settings
from django.contrib.auth.tokens import default_token_generator
from django.utils.http import urlsafe_base64_encode, urlsafe_base64_decode
from django.utils.encoding import force_bytes, force_str
from django.template.loader import render_to_string
from .models import UserProfile, ContactMessage, UserReport, Complaint, Announcement
from .forms import UserRegistrationForm, UserProfileForm
from orders.models import Order


def is_staff_user(user):
    """Check if user is staff or superuser"""
    return user.is_staff or user.is_superuser


def register(request):
    """User registration view"""
    if request.user.is_authenticated:
        return redirect('products:home')
    
    if request.method == 'POST':
        form = UserRegistrationForm(request.POST)
        if form.is_valid():
            username = form.cleaned_data['username']
            email = form.cleaned_data['email']
            password = form.cleaned_data['password']
            
            # Create user
            user = User.objects.create_user(username=username, email=email, password=password)
            
            # Create user profile
            UserProfile.objects.create(user=user)
            
            messages.success(request, 'Registration successful! Please login.')
            return redirect('users:login')
    else:
        form = UserRegistrationForm()
    
    return render(request, 'users/register.html', {'form': form})


def user_login(request):
    """User login view with deactivated account handling"""
    if request.user.is_authenticated:
        return redirect('users:dashboard')
    
    if request.method == 'POST':
        username = request.POST.get('username')
        password = request.POST.get('password')
        
        # Check if user exists and is active
        try:
            user_obj = User.objects.get(username=username)
            if not user_obj.is_active:
                messages.error(request, 'Your account has been deactivated. Please submit a report below.')
                return redirect('users:submit_report')
        except User.DoesNotExist:
            pass
        
        user = authenticate(request, username=username, password=password)
        
        if user is not None:
            login(request, user)
            messages.success(request, f'Welcome back, {username}!')
            # Redirect to dashboard after login
            next_url = request.GET.get('next', 'users:dashboard')
            return redirect(next_url)
        else:
            messages.error(request, 'Invalid username or password.')
    
    return render(request, 'users/login.html')


@login_required
def user_logout(request):
    """User logout view"""
    logout(request)
    messages.success(request, 'You have been logged out successfully.')
    return redirect('products:home')


@login_required
def profile(request):
    """User profile view"""
    user_profile, created = UserProfile.objects.get_or_create(user=request.user)
    
    if request.method == 'POST':
        form = UserProfileForm(request.POST, instance=user_profile)
        if form.is_valid():
            form.save()
            messages.success(request, 'Profile updated successfully!')
            return redirect('users:profile')
    else:
        form = UserProfileForm(instance=user_profile)
    
    return render(request, 'users/profile.html', {'form': form})


@login_required
def dashboard(request):
    """Dashboard - Different for Admin and Users"""
    # Admin Dashboard
    if request.user.is_staff or request.user.is_superuser:
        from products.models import Product, Category
        from django.contrib.auth.models import User
        
        total_products = Product.objects.count()
        total_categories = Category.objects.count()
        total_users = User.objects.filter(is_staff=False, is_superuser=False).count()
        total_orders = Order.objects.count()
        
        # Get pending complaints count safely
        try:
            pending_complaints = Complaint.objects.filter(status='pending').count()
        except:
            pending_complaints = 0
        
        recent_orders = Order.objects.all()[:5]
        
        # Get recent complaints safely
        try:
            recent_complaints = Complaint.objects.all()[:5]
        except:
            recent_complaints = []
        
        context = {
            'is_admin': True,
            'total_products': total_products,
            'total_categories': total_categories,
            'total_users': total_users,
            'total_orders': total_orders,
            'pending_complaints': pending_complaints,
            'recent_orders': recent_orders,
            'recent_complaints': recent_complaints,
        }
        return render(request, 'users/admin_dashboard.html', context)
    
    # User Dashboard
    else:
        user_profile = UserProfile.objects.get_or_create(user=request.user)[0]
        recent_orders = Order.objects.filter(user=request.user)[:5]
        total_orders = Order.objects.filter(user=request.user).count()
        total_spent = sum(order.total_price for order in Order.objects.filter(user=request.user))
        
        # Get user complaints safely
        try:
            my_complaints = Complaint.objects.filter(user=request.user)[:5]
        except:
            my_complaints = []
        
        context = {
            'is_admin': False,
            'user_profile': user_profile,
            'recent_orders': recent_orders,
            'total_orders': total_orders,
            'total_spent': total_spent,
            'my_complaints': my_complaints,
        }
        return render(request, 'users/dashboard.html', context)


def about_us(request):
    """About Us page"""
    return render(request, 'users/about_us.html')


def contact_us(request):
    """Contact Us page"""
    if request.method == 'POST':
        name = request.POST.get('name')
        email = request.POST.get('email')
        subject = request.POST.get('subject')
        message = request.POST.get('message')
        
        if all([name, email, subject, message]):
            ContactMessage.objects.create(
                name=name,
                email=email,
                subject=subject,
                message=message
            )
            messages.success(request, 'Thank you for contacting us! We will get back to you soon.')
            return redirect('users:contact_us')
        else:
            messages.error(request, 'Please fill all fields.')
    
    return render(request, 'users/contact_us.html')


def submit_report(request):
    """Submit report for deactivated accounts"""
    if request.method == 'POST':
        username = request.POST.get('username')
        email = request.POST.get('email')
        report_type = request.POST.get('report_type')
        message = request.POST.get('message')
        
        if all([username, email, report_type, message]):
            UserReport.objects.create(
                username=username,
                email=email,
                report_type=report_type,
                message=message
            )
            messages.success(request, 'Your report has been submitted. Our team will review it shortly.')
            return redirect('users:login')
        else:
            messages.error(request, 'Please fill all fields.')
    
    return render(request, 'users/submit_report.html')


def password_reset_request(request):
    """Password reset request"""
    if request.method == 'POST':
        email = request.POST.get('email')
        try:
            user = User.objects.get(email=email)
            
            # Generate token
            token = default_token_generator.make_token(user)
            uid = urlsafe_base64_encode(force_bytes(user.pk))
            
            # Create reset link
            reset_link = request.build_absolute_uri(
                f'/users/password-reset/{uid}/{token}/'
            )
            
            # Send email (in development, it will print to console)
            subject = 'Password Reset Request'
            message = f'''
Hello {user.username},

You requested a password reset. Click the link below to reset your password:

{reset_link}

If you didn't request this, please ignore this email.

Best regards,
E-Shop Team
            '''
            
            send_mail(subject, message, settings.DEFAULT_FROM_EMAIL, [email])
            
            messages.success(request, 'Password reset link has been sent to your email.')
            return redirect('users:login')
        except User.DoesNotExist:
            messages.error(request, 'No account found with that email address.')
    
    return render(request, 'users/password_reset_request.html')


def password_reset_confirm(request, uidb64, token):
    """Password reset confirmation"""
    try:
        uid = force_str(urlsafe_base64_decode(uidb64))
        user = User.objects.get(pk=uid)
    except (TypeError, ValueError, OverflowError, User.DoesNotExist):
        user = None
    
    if user is not None and default_token_generator.check_token(user, token):
        if request.method == 'POST':
            password = request.POST.get('password')
            confirm_password = request.POST.get('confirm_password')
            
            if password == confirm_password:
                user.set_password(password)
                user.save()
                messages.success(request, 'Password reset successful! You can now login.')
                return redirect('users:login')
            else:
                messages.error(request, 'Passwords do not match.')
        
        return render(request, 'users/password_reset_confirm.html', {'validlink': True})
    else:
        messages.error(request, 'Invalid or expired reset link.')
        return render(request, 'users/password_reset_confirm.html', {'validlink': False})



@login_required
def submit_complaint(request):
    """Submit a complaint - Simplified"""
    if request.method == 'POST':
        subject = request.POST.get('subject')
        message = request.POST.get('message')
        image = request.FILES.get('image')
        
        if subject and message:
            Complaint.objects.create(
                user=request.user,
                subject=subject,
                message=message,
                image=image
            )
            messages.success(request, 'Your complaint has been submitted successfully. We will review it and respond privately.')
            return redirect('users:my_complaints')
        else:
            messages.error(request, 'Please fill all required fields.')
    
    return render(request, 'users/submit_complaint.html')


@login_required
def my_complaints(request):
    """View user's complaints and admin replies"""
    complaints = Complaint.objects.filter(user=request.user)
    return render(request, 'users/my_complaints.html', {'complaints': complaints})


@login_required
def complaint_detail(request, complaint_id):
    """View complaint details and admin reply"""
    complaint = get_object_or_404(Complaint, id=complaint_id, user=request.user)
    return render(request, 'users/complaint_detail.html', {'complaint': complaint})


def announcements(request):
    """View public announcements with category filter"""
    category = request.GET.get('category', 'all')
    
    if category == 'all':
        active_announcements = Announcement.objects.filter(is_active=True)
    else:
        active_announcements = Announcement.objects.filter(is_active=True, category=category)
    
    # Get all categories for filter dropdown
    categories = Announcement.CATEGORY_CHOICES
    
    context = {
        'announcements': active_announcements,
        'categories': categories,
        'selected_category': category,
    }
    
    return render(request, 'users/announcements.html', context)



@login_required
@user_passes_test(is_staff_user)
def manage_users(request):
    """Simple user management page for staff"""
    users = User.objects.all().order_by('-date_joined')
    context = {
        'users': users,
    }
    return render(request, 'users/manage_users.html', context)


@login_required
@user_passes_test(is_staff_user)
def toggle_user_status(request, user_id):
    """Toggle user active/inactive status"""
    if request.method == 'POST':
        user = get_object_or_404(User, id=user_id)
        action = request.POST.get('action')
        
        # Prevent deactivating superusers or staff
        if user.is_superuser or user.is_staff:
            messages.error(request, 'Cannot deactivate admin or staff users.')
            return redirect('users:manage_users')
        
        if action == 'deactivate':
            user.is_active = False
            user.save()
            messages.success(request, f'User {user.username} has been deactivated.')
        elif action == 'activate':
            user.is_active = True
            user.save()
            messages.success(request, f'User {user.username} has been activated.')
        
        return redirect('users:manage_users')
    
    return redirect('users:manage_users')


@login_required
@user_passes_test(is_staff_user)
def manage_complaints(request):
    """Simple complaint management page for staff"""
    complaints = Complaint.objects.all().order_by('-created_at')
    context = {
        'complaints': complaints,
    }
    return render(request, 'users/manage_complaints.html', context)
