from django.contrib import admin
from .models import Cart, CartItem


class CartItemInline(admin.TabularInline):
    model = CartItem
    extra = 0


@admin.register(Cart)
class CartAdmin(admin.ModelAdmin):
    # Disable sidebar navigation
    enable_nav_sidebar = False
    
    list_display = ['user', 'total_items', 'total_price', 'created_at']
    search_fields = ['user__username']
    inlines = [CartItemInline]


@admin.register(CartItem)
class CartItemAdmin(admin.ModelAdmin):
    # Disable sidebar navigation
    enable_nav_sidebar = False
    
    list_display = ['cart', 'product', 'quantity', 'subtotal', 'added_at']
    list_filter = ['added_at']
    search_fields = ['product__name', 'cart__user__username']
