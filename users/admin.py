from django.contrib import admin
from django.contrib.auth.models import User
from django.contrib.auth.admin import UserAdmin as BaseUserAdmin
from django.utils.html import format_html
from django.utils import timezone
from django.contrib import messages
from .models import UserProfile, ContactMessage, UserReport, Complaint, Announcement, Notification


# Unregister the default User admin
admin.site.unregister(User)


@admin.register(User)
class CustomUserAdmin(BaseUserAdmin):
    """Enhanced User Admin with Management Features"""
    list_display = ['username', 'email', 'first_name', 'last_name', 'is_active_status', 'is_staff', 'date_joined', 'last_login']
    list_filter = ['is_active', 'is_staff', 'is_superuser', 'date_joined', 'last_login']
    search_fields = ['username', 'email', 'first_name', 'last_name']
    list_editable = []
    ordering = ['-date_joined']
    
    # Custom actions
    actions = ['activate_users', 'deactivate_users', 'make_staff', 'remove_staff']
    
    # Fieldsets for user edit page
    fieldsets = (
        ('User Information', {
            'fields': ('username', 'password')
        }),
        ('Personal Info', {
            'fields': ('first_name', 'last_name', 'email')
        }),
        ('Permissions', {
            'fields': ('is_active', 'is_staff', 'is_superuser', 'groups', 'user_permissions'),
            'classes': ('collapse',)
        }),
        ('Important Dates', {
            'fields': ('last_login', 'date_joined'),
            'classes': ('collapse',)
        }),
    )
    
    # Fieldsets for add user page
    add_fieldsets = (
        ('User Information', {
            'classes': ('wide',),
            'fields': ('username', 'email', 'password1', 'password2'),
        }),
        ('Permissions', {
            'fields': ('is_active', 'is_staff'),
        }),
    )
    
    def is_active_status(self, obj):
        """Display active status with color"""
        if obj.is_active:
            return format_html('<span style="color: {}; font-weight: bold;">✓ Active</span>', 'green')
        else:
            return format_html('<span style="color: {}; font-weight: bold;">✗ Inactive</span>', 'red')
    is_active_status.short_description = 'Status'
    is_active_status.admin_order_field = 'is_active'
    
    def activate_users(self, request, queryset):
        """Activate selected users"""
        updated = queryset.update(is_active=True)
        self.message_user(request, f'{updated} user(s) activated successfully.', messages.SUCCESS)
    activate_users.short_description = '✓ Activate selected users'
    
    def deactivate_users(self, request, queryset):
        """Deactivate selected users"""
        # Prevent deactivating superusers
        superusers = queryset.filter(is_superuser=True)
        if superusers.exists():
            self.message_user(request, 'Cannot deactivate superuser accounts!', messages.ERROR)
            queryset = queryset.exclude(is_superuser=True)
        
        updated = queryset.update(is_active=False)
        if updated > 0:
            self.message_user(request, f'{updated} user(s) deactivated successfully.', messages.SUCCESS)
    deactivate_users.short_description = '✗ Deactivate selected users'
    
    def make_staff(self, request, queryset):
        """Make selected users staff"""
        updated = queryset.update(is_staff=True)
        self.message_user(request, f'{updated} user(s) made staff successfully.', messages.SUCCESS)
    make_staff.short_description = '⭐ Make staff'
    
    def remove_staff(self, request, queryset):
        """Remove staff status from selected users"""
        # Prevent removing staff from superusers
        superusers = queryset.filter(is_superuser=True)
        if superusers.exists():
            self.message_user(request, 'Cannot remove staff status from superuser accounts!', messages.ERROR)
            queryset = queryset.exclude(is_superuser=True)
        
        updated = queryset.update(is_staff=False)
        if updated > 0:
            self.message_user(request, f'{updated} user(s) removed from staff successfully.', messages.SUCCESS)
    remove_staff.short_description = '✗ Remove staff status'


@admin.register(UserProfile)
class UserProfileAdmin(admin.ModelAdmin):
    # Disable sidebar navigation
    enable_nav_sidebar = False
    
    list_display = ['user', 'phone', 'city', 'state', 'created_at']
    search_fields = ['user__username', 'user__email', 'phone']
    list_filter = ['city', 'state', 'created_at']
    readonly_fields = ['created_at']
    
    fieldsets = (
        ('User', {
            'fields': ('user',)
        }),
        ('Contact Information', {
            'fields': ('phone', 'address')
        }),
        ('Location', {
            'fields': ('city', 'state', 'zipcode')
        }),
        ('Metadata', {
            'fields': ('created_at',),
            'classes': ('collapse',)
        }),
    )


@admin.register(ContactMessage)
class ContactMessageAdmin(admin.ModelAdmin):
    # Disable sidebar navigation
    enable_nav_sidebar = False
    
    list_display = ['name', 'email', 'subject', 'is_read', 'created_at']
    search_fields = ['name', 'email', 'subject', 'message']
    list_filter = ['is_read', 'created_at']
    list_editable = ['is_read']
    readonly_fields = ['created_at']


@admin.register(UserReport)
class UserReportAdmin(admin.ModelAdmin):
    # Disable sidebar navigation
    enable_nav_sidebar = False
    
    list_display = ['username', 'email', 'report_type', 'is_resolved', 'created_at']
    search_fields = ['username', 'email', 'message']
    list_filter = ['report_type', 'is_resolved', 'created_at']
    list_editable = ['is_resolved']
    readonly_fields = ['created_at']


@admin.register(Complaint)
class ComplaintAdmin(admin.ModelAdmin):
    # Disable sidebar navigation
    enable_nav_sidebar = False
    
    list_display = ['user', 'subject', 'status', 'is_urgent', 'has_image', 'created_at']
    search_fields = ['user__username', 'subject', 'message', 'admin_reply']
    list_filter = ['status', 'is_urgent', 'created_at']
    list_editable = ['status', 'is_urgent']
    readonly_fields = ['created_at', 'updated_at', 'replied_at', 'image_preview']
    
    fieldsets = (
        ('Complaint Information', {
            'fields': ('user', 'subject', 'message', 'image', 'image_preview', 'created_at')
        }),
        ('Admin Response', {
            'fields': ('status', 'admin_reply', 'replied_at'),
            'description': 'Reply privately to the user'
        }),
        ('Urgent Announcement', {
            'fields': ('is_urgent',),
            'description': 'Check "is_urgent" if this needs to be announced publicly. Announcement will be created automatically.'
        }),
    )
    
    def has_image(self, obj):
        if obj.image:
            return format_html('<span style="color: {};">✓ Yes</span>', 'green')
        return format_html('<span style="color: {};">✗ No</span>', 'red')
    has_image.short_description = 'Image'
    
    def image_preview(self, obj):
        if obj.image:
            return format_html('<img src="{}" style="max-width: 300px; max-height: 300px;" />', obj.image.url)
        return "No image uploaded"
    image_preview.short_description = 'Image Preview'
    
    def save_model(self, request, obj, form, change):
        # Set replied_at timestamp when admin_reply is added
        if obj.admin_reply and not obj.replied_at:
            obj.replied_at = timezone.now()
            if obj.status == 'pending':
                obj.status = 'replied'
        
        # Complaints are now private - no announcements created
        # Admin replies are sent directly to the user
        
        super().save_model(request, obj, form, change)


@admin.register(Announcement)
class AnnouncementAdmin(admin.ModelAdmin):
    # Disable sidebar navigation
    enable_nav_sidebar = False
    
    list_display = ['title', 'category', 'is_active', 'created_at']
    search_fields = ['title', 'content']
    list_filter = ['category', 'is_active', 'created_at']
    list_editable = ['is_active']
    readonly_fields = ['created_at']
    
    fieldsets = (
        ('Announcement Information', {
            'fields': ('title', 'content', 'category')
        }),
        ('Status', {
            'fields': ('is_active', 'created_at')
        }),
    )


@admin.register(Notification)
class NotificationAdmin(admin.ModelAdmin):
    # Disable sidebar navigation
    enable_nav_sidebar = False
    
    list_display = ['user', 'notification_type', 'title', 'is_read', 'created_at']
    search_fields = ['user__username', 'title', 'message']
    list_filter = ['notification_type', 'is_read', 'created_at']
    list_editable = ['is_read']
    readonly_fields = ['created_at']
    
    fieldsets = (
        ('Notification Details', {
            'fields': ('user', 'notification_type', 'title', 'message', 'link')
        }),
        ('Status', {
            'fields': ('is_read', 'created_at')
        }),
    )
