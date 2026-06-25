from django.shortcuts import render, get_object_or_404, redirect
from django.contrib.auth.decorators import login_required, user_passes_test
from django.contrib import messages
from django.db.models import Q
from .models import Product, Category, ProductReview
from users.models import Announcement


def is_staff_user(user):
    """Check if user is staff or superuser"""
    return user.is_staff or user.is_superuser


def home(request):
    """Home page with featured products"""
    products = Product.objects.all()[:8]
    categories = Category.objects.all()
    context = {
        'products': products,
        'categories': categories,
    }
    return render(request, 'products/home.html', context)


def product_list(request):
    """Product listing page with search and filter"""
    products = Product.objects.all()
    categories = Category.objects.all()
    
    # Search functionality
    search_query = request.GET.get('search', '')
    if search_query:
        products = products.filter(
            Q(name__icontains=search_query) | 
            Q(description__icontains=search_query)
        )
    
    # Category filter
    category_id = request.GET.get('category', '')
    if category_id:
        products = products.filter(category_id=category_id)
    
    context = {
        'products': products,
        'categories': categories,
        'search_query': search_query,
        'selected_category': category_id,
    }
    return render(request, 'products/product_list.html', context)


def product_detail(request, pk):
    """Product detail page"""
    product = get_object_or_404(Product, pk=pk)
    reviews = product.reviews.all()
    related_products = Product.objects.filter(category=product.category).exclude(pk=pk)[:4]
    
    context = {
        'product': product,
        'reviews': reviews,
        'related_products': related_products,
    }
    return render(request, 'products/product_detail.html', context)


@login_required
def add_review(request, pk):
    """Add product review"""
    product = get_object_or_404(Product, pk=pk)
    
    if request.method == 'POST':
        rating = request.POST.get('rating')
        comment = request.POST.get('comment')
        
        if rating and comment:
            ProductReview.objects.update_or_create(
                product=product,
                user=request.user,
                defaults={'rating': rating, 'comment': comment}
            )
            messages.success(request, 'Review added successfully!')
        else:
            messages.error(request, 'Please provide both rating and comment.')
    
    return redirect('products:product_detail', pk=pk)



@login_required
@user_passes_test(is_staff_user)
def manage_products(request):
    """Simple product management page for staff"""
    products = Product.objects.all().order_by('-created_at')
    context = {
        'products': products,
    }
    return render(request, 'products/manage_products.html', context)


@login_required
@user_passes_test(is_staff_user)
def toggle_product_status(request, pk):
    """Toggle product active/inactive status"""
    if request.method == 'POST':
        product = get_object_or_404(Product, pk=pk)
        action = request.POST.get('action')
        
        if action == 'deactivate':
            product.is_active = False
            product.save()
            messages.success(request, f'Product "{product.name}" has been deactivated. It will not be visible to customers.')
        elif action == 'activate':
            product.is_active = True
            product.save()
            messages.success(request, f'Product "{product.name}" has been activated. It is now visible to customers.')
        
        return redirect('products:manage_products')
    
    return redirect('products:manage_products')


@login_required
@user_passes_test(is_staff_user)
def add_product(request):
    """Add new product - simple form"""
    categories = Category.objects.all()
    
    if request.method == 'POST':
        name = request.POST.get('name')
        description = request.POST.get('description')
        category_id = request.POST.get('category')
        price = request.POST.get('price')
        stock = request.POST.get('stock')
        image = request.FILES.get('image')
        
        if name and description and category_id and price and stock and image:
            try:
                category = Category.objects.get(id=category_id)
                product = Product.objects.create(
                    name=name,
                    description=description,
                    category=category,
                    price=price,
                    stock=stock,
                    image=image
                )
                
                # Create announcement for new product
                Announcement.objects.create(
                    title=f"🎉 New Product: {product.name}",
                    content=f"Check out our new product in {category.name} category! {product.description[:150]}... Price: ${product.price}",
                    category='new_product',
                    is_active=True
                )
                
                messages.success(request, f'Product "{product.name}" added successfully! Announcement sent to all users.')
                return redirect('products:manage_products')
            except Exception as e:
                messages.error(request, f'Error adding product: {str(e)}')
        else:
            messages.error(request, 'Please fill in all required fields.')
    
    context = {
        'categories': categories,
    }
    return render(request, 'products/add_product.html', context)


@login_required
@user_passes_test(is_staff_user)
def edit_product(request, pk):
    """Edit existing product - simple form"""
    product = get_object_or_404(Product, pk=pk)
    categories = Category.objects.all()
    
    if request.method == 'POST':
        product.name = request.POST.get('name')
        product.description = request.POST.get('description')
        category_id = request.POST.get('category')
        product.price = request.POST.get('price')
        product.stock = request.POST.get('stock')
        
        # Update image only if new one is uploaded
        if request.FILES.get('image'):
            product.image = request.FILES.get('image')
        
        try:
            product.category = Category.objects.get(id=category_id)
            product.save()
            messages.success(request, f'Product "{product.name}" updated successfully!')
            return redirect('products:manage_products')
        except Exception as e:
            messages.error(request, f'Error updating product: {str(e)}')
    
    context = {
        'product': product,
        'categories': categories,
    }
    return render(request, 'products/edit_product.html', context)


@login_required
@user_passes_test(is_staff_user)
def delete_product(request, pk):
    """Delete product"""
    product = get_object_or_404(Product, pk=pk)
    
    if request.method == 'POST':
        product_name = product.name
        product.delete()
        messages.success(request, f'Product "{product_name}" deleted successfully!')
        return redirect('products:manage_products')
    
    return redirect('products:manage_products')
