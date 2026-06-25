from django.shortcuts import render, redirect, get_object_or_404
from django.contrib.auth.decorators import login_required, user_passes_test
from django.contrib import messages
from .models import Order, OrderItem
from cart.models import Cart
from users.models import UserProfile


def is_staff_user(user):
    """Check if user is staff or superuser"""
    return user.is_staff or user.is_superuser


@login_required
def checkout(request):
    """Checkout page"""
    # Prevent admin/staff from placing orders
    if request.user.is_staff or request.user.is_superuser:
        messages.error(request, 'Admin users cannot place orders. Please use a customer account.')
        return redirect('products:home')
    
    cart = get_object_or_404(Cart, user=request.user)
    cart_items = cart.items.all()
    
    if not cart_items:
        messages.error(request, 'Your cart is empty.')
        return redirect('cart:cart_view')
    
    # Get user profile for pre-filling form
    try:
        user_profile = UserProfile.objects.get(user=request.user)
    except UserProfile.DoesNotExist:
        user_profile = None
    
    context = {
        'cart': cart,
        'cart_items': cart_items,
        'user_profile': user_profile,
    }
    return render(request, 'orders/checkout.html', context)


@login_required
def place_order(request):
    """Place order"""
    # Prevent admin/staff from placing orders
    if request.user.is_staff or request.user.is_superuser:
        messages.error(request, 'Admin users cannot place orders. Please use a customer account.')
        return redirect('products:home')
    
    if request.method == 'POST':
        cart = get_object_or_404(Cart, user=request.user)
        cart_items = cart.items.all()
        
        if not cart_items:
            messages.error(request, 'Your cart is empty.')
            return redirect('cart:cart_view')
        
        # Get form data
        full_name = request.POST.get('full_name')
        email = request.POST.get('email')
        phone = request.POST.get('phone')
        shipping_address = request.POST.get('shipping_address')
        city = request.POST.get('city')
        state = request.POST.get('state')
        zipcode = request.POST.get('zipcode')
        payment_method = request.POST.get('payment_method')
        payment_reference = request.POST.get('payment_reference', '')
        
        # Validate required fields
        if not all([full_name, email, phone, shipping_address, city, state, zipcode, payment_method]):
            messages.error(request, 'Please fill all required fields.')
            return redirect('orders:checkout')
        
        # Validate payment reference for online payments
        if payment_method in ['card', 'upi', 'netbanking'] and not payment_reference:
            messages.error(request, 'Please enter payment reference/transaction number for online payment.')
            return redirect('orders:checkout')
        
        # Create order
        order = Order.objects.create(
            user=request.user,
            total_price=cart.total_price,
            full_name=full_name,
            email=email,
            phone=phone,
            shipping_address=shipping_address,
            city=city,
            state=state,
            zipcode=zipcode,
            payment_method=payment_method,
            payment_reference=payment_reference,
            payment_status=True if payment_method == 'cod' else False,
        )
        
        # Create order items and update stock
        for cart_item in cart_items:
            OrderItem.objects.create(
                order=order,
                product=cart_item.product,
                quantity=cart_item.quantity,
                price=cart_item.product.price,
            )
            
            # Update product stock
            product = cart_item.product
            product.stock -= cart_item.quantity
            product.save()
        
        # Create announcement to notify admin about new order
        from users.models import Announcement
        Announcement.objects.create(
            title=f"🛒 New Order #{order.id} from {request.user.username}",
            content=f"Customer {request.user.username} placed an order for ${order.total_price}. Order contains {cart_items.count()} item(s). Payment method: {order.get_payment_method_display()}. Please check the order management page.",
            category='order',
            is_active=True
        )
        
        # Clear cart
        cart_items.delete()
        
        messages.success(request, f'Order placed successfully! Order ID: {order.order_id}')
        return redirect('orders:order_success', order_id=order.order_id)
    
    return redirect('orders:checkout')


@login_required
def order_success(request, order_id):
    """Order success page"""
    order = get_object_or_404(Order, order_id=order_id, user=request.user)
    context = {'order': order}
    return render(request, 'orders/order_success.html', context)


@login_required
def order_history(request):
    """Order history page"""
    orders = Order.objects.filter(user=request.user)
    context = {'orders': orders}
    return render(request, 'orders/order_history.html', context)


@login_required
def order_detail(request, order_id):
    """Order detail page"""
    order = get_object_or_404(Order, order_id=order_id, user=request.user)
    context = {'order': order}
    return render(request, 'orders/order_detail.html', context)


@login_required
@user_passes_test(is_staff_user)
def manage_orders(request):
    """Simple order management page for staff"""
    orders = Order.objects.all().order_by('-created_at').prefetch_related('items__product__category')
    context = {
        'orders': orders,
    }
    return render(request, 'orders/manage_orders.html', context)


@login_required
@user_passes_test(is_staff_user)
def update_order_status(request, order_id):
    """Update order status"""
    if request.method == 'POST':
        order = get_object_or_404(Order, id=order_id)
        new_status = request.POST.get('status')
        
        if new_status in dict(Order.STATUS_CHOICES):
            order.order_status = new_status
            order.save()
            messages.success(request, f'Order #{order.id} status updated to {order.get_order_status_display()}')
        else:
            messages.error(request, 'Invalid status')
        
        return redirect('orders:manage_orders')
    
    return redirect('orders:manage_orders')
