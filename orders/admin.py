from django.contrib import admin
from .models import Order, OrderItem


class OrderItemInline(admin.TabularInline):
    model = OrderItem
    extra = 0
    readonly_fields = ['product', 'quantity', 'price', 'get_subtotal']
    
    def get_subtotal(self, obj):
        if obj.price is not None and obj.quantity is not None:
            return f'${obj.price * obj.quantity:.2f}'
        return '$0.00'
    get_subtotal.short_description = 'Subtotal'


@admin.register(Order)
class OrderAdmin(admin.ModelAdmin):
    # Disable sidebar navigation
    enable_nav_sidebar = False
    
    list_display = ['order_id', 'user', 'total_price', 'payment_method', 'payment_reference', 'order_status', 'payment_status', 'created_at']
    list_filter = ['order_status', 'payment_method', 'payment_status', 'created_at']
    search_fields = ['order_id', 'user__username', 'email', 'phone', 'payment_reference']
    readonly_fields = ['order_id', 'created_at', 'updated_at']
    list_editable = ['order_status', 'payment_status']
    inlines = [OrderItemInline]
    
    fieldsets = (
        ('Order Information', {
            'fields': ('order_id', 'user', 'total_price', 'order_status', 'created_at', 'updated_at')
        }),
        ('Shipping Information', {
            'fields': ('full_name', 'email', 'phone', 'shipping_address', 'city', 'state', 'zipcode')
        }),
        ('Payment Information', {
            'fields': ('payment_method', 'payment_reference', 'payment_status')
        }),
    )


@admin.register(OrderItem)
class OrderItemAdmin(admin.ModelAdmin):
    # Disable sidebar navigation
    enable_nav_sidebar = False
    
    list_display = ['order', 'product', 'quantity', 'price', 'get_subtotal']
    search_fields = ['order__order_id', 'product__name']
    
    def get_subtotal(self, obj):
        if obj.price is not None and obj.quantity is not None:
            return f'${obj.price * obj.quantity:.2f}'
        return '$0.00'
    get_subtotal.short_description = 'Subtotal'
