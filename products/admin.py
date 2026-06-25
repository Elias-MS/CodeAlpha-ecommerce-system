from django.contrib import admin
from django.utils.html import format_html
from django.db.models import Count
from django.contrib import messages
from .models import Category, Product, ProductReview


@admin.register(Category)
class CategoryAdmin(admin.ModelAdmin):
    # Disable sidebar navigation
    enable_nav_sidebar = False
    
    """Enhanced Category Admin"""
    list_display = ['name', 'product_count', 'description_preview', 'created_at']
    search_fields = ['name', 'description']
    readonly_fields = ['created_at']
    
    fieldsets = (
        ('Category Information', {
            'fields': ('name', 'description')
        }),
        ('Metadata', {
            'fields': ('created_at',),
            'classes': ('collapse',)
        }),
    )
    
    def product_count(self, obj):
        """Display number of products in category"""
        count = obj.products.count()
        return format_html('<span style="background: {}; color: white; padding: 3px 10px; border-radius: 10px;">{}</span>', '#4CAF50', count)
    product_count.short_description = 'Products'
    
    def description_preview(self, obj):
        """Show preview of description"""
        if obj.description:
            return obj.description[:50] + '...' if len(obj.description) > 50 else obj.description
        return '-'
    description_preview.short_description = 'Description'


@admin.register(Product)
class ProductAdmin(admin.ModelAdmin):
    # Disable sidebar navigation
    enable_nav_sidebar = False
    
    """Enhanced Product Admin with Management Features"""
    list_display = ['image_preview', 'name', 'category', 'price_display', 'stock', 'availability_status', 'rating_display', 'created_at']
    list_filter = ['category', 'created_at', 'updated_at']
    search_fields = ['name', 'description', 'category__name']
    list_editable = ['stock']
    readonly_fields = ['created_at', 'updated_at', 'image_preview_large', 'rating']
    ordering = ['-created_at']
    list_per_page = 20
    
    # Bulk actions
    actions = ['activate_products', 'deactivate_products', 'set_out_of_stock', 'duplicate_product']
    
    fieldsets = (
        (None, {
            'fields': ('name', 'description', 'category', 'price', 'stock', 'image', 'image_preview_large', 'rating', 'created_at', 'updated_at')
        }),
    )
    
    def image_preview(self, obj):
        """Small image preview for list view"""
        if obj.image:
            return format_html('<img src="{}" style="width: 50px; height: 50px; object-fit: cover; border-radius: 5px;" />', obj.image.url)
        return "No image"
    image_preview.short_description = 'Image'
    
    def image_preview_large(self, obj):
        """Large image preview for detail view"""
        if obj.image:
            return format_html('<img src="{}" style="max-width: 400px; max-height: 400px; border-radius: 10px; box-shadow: 0 2px 8px rgba(0,0,0,0.1);" />', obj.image.url)
        return "No image uploaded"
    image_preview_large.short_description = 'Product Image'
    
    def price_display(self, obj):
        """Display price with currency symbol"""
        return format_html('<span style="color: #2196F3; font-weight: bold;">${}</span>', obj.price)
    price_display.short_description = 'Price'
    price_display.admin_order_field = 'price'
    
    def availability_status(self, obj):
        """Display availability status with color"""
        if obj.is_available:
            return format_html(
                '<span style="background: {}; color: white; padding: 5px 12px; border-radius: 12px; font-weight: bold;">✓ Available</span>',
                '#4CAF50'
            )
        else:
            return format_html(
                '<span style="background: {}; color: white; padding: 5px 12px; border-radius: 12px; font-weight: bold;">✗ Out of Stock</span>',
                '#f44336'
            )
    availability_status.short_description = 'Status'
    availability_status.admin_order_field = 'stock'
    
    def rating_display(self, obj):
        """Display rating with stars"""
        stars = '⭐' * int(obj.rating)
        return format_html('<span style="font-size: {};">{} {}</span>', '16px', stars, obj.rating)
    rating_display.short_description = 'Rating'
    rating_display.admin_order_field = 'rating'
    
    # Bulk Actions
    def activate_products(self, request, queryset):
        """Activate selected products by setting stock to 10"""
        updated = queryset.update(stock=10)
        self.message_user(request, f'{updated} product(s) activated successfully (stock set to 10).', messages.SUCCESS)
    activate_products.short_description = '✓ Activate selected products (set stock to 10)'
    
    def deactivate_products(self, request, queryset):
        """Deactivate selected products by setting stock to 0"""
        updated = queryset.update(stock=0)
        self.message_user(request, f'{updated} product(s) deactivated successfully (stock set to 0).', messages.WARNING)
    deactivate_products.short_description = '✗ Deactivate selected products (set stock to 0)'
    
    def set_out_of_stock(self, request, queryset):
        """Mark products as out of stock"""
        updated = queryset.update(stock=0)
        self.message_user(request, f'{updated} product(s) marked as out of stock.', messages.INFO)
    set_out_of_stock.short_description = '📦 Mark as out of stock'
    
    def duplicate_product(self, request, queryset):
        """Duplicate selected products"""
        count = 0
        for product in queryset:
            product.pk = None
            product.name = f"{product.name} (Copy)"
            product.save()
            count += 1
        self.message_user(request, f'{count} product(s) duplicated successfully.', messages.SUCCESS)
    duplicate_product.short_description = '📋 Duplicate selected products'


@admin.register(ProductReview)
class ProductReviewAdmin(admin.ModelAdmin):
    # Disable sidebar navigation
    enable_nav_sidebar = False
    
    """Enhanced Product Review Admin"""
    list_display = ['product', 'user', 'rating_display', 'comment_preview', 'created_at']
    list_filter = ['rating', 'created_at']
    search_fields = ['product__name', 'user__username', 'comment']
    readonly_fields = ['created_at']
    
    fieldsets = (
        ('Review Information', {
            'fields': ('product', 'user', 'rating')
        }),
        ('Comment', {
            'fields': ('comment',)
        }),
        ('Metadata', {
            'fields': ('created_at',),
            'classes': ('collapse',)
        }),
    )
    
    def rating_display(self, obj):
        """Display rating with stars"""
        stars = '⭐' * obj.rating
        return format_html('<span style="font-size: {};">{}</span>', '16px', stars)
    rating_display.short_description = 'Rating'
    rating_display.admin_order_field = 'rating'
    
    def comment_preview(self, obj):
        """Show preview of comment"""
        return obj.comment[:50] + '...' if len(obj.comment) > 50 else obj.comment
    comment_preview.short_description = 'Comment'
