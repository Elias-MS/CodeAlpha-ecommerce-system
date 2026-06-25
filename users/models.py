from django.db import models
from django.contrib.auth.models import User


class UserProfile(models.Model):
    """Extended User Profile"""
    user = models.OneToOneField(User, on_delete=models.CASCADE, related_name='profile')
    phone = models.CharField(max_length=15, blank=True)
    address = models.TextField(blank=True)
    city = models.CharField(max_length=100, blank=True)
    state = models.CharField(max_length=100, blank=True)
    zipcode = models.CharField(max_length=10, blank=True)
    created_at = models.DateTimeField(auto_now_add=True)

    def __str__(self):
        return f'{self.user.username} Profile'


class ContactMessage(models.Model):
    """Contact Us Messages"""
    name = models.CharField(max_length=200)
    email = models.EmailField()
    subject = models.CharField(max_length=200)
    message = models.TextField()
    created_at = models.DateTimeField(auto_now_add=True)
    is_read = models.BooleanField(default=False)

    class Meta:
        ordering = ['-created_at']

    def __str__(self):
        return f'{self.name} - {self.subject}'


class UserReport(models.Model):
    """User Reports for deactivated accounts"""
    REPORT_TYPES = [
        ('account_deactivated', 'Account Deactivated'),
        ('cannot_login', 'Cannot Login'),
        ('technical_issue', 'Technical Issue'),
        ('other', 'Other'),
    ]
    
    username = models.CharField(max_length=150)
    email = models.EmailField()
    report_type = models.CharField(max_length=50, choices=REPORT_TYPES)
    message = models.TextField()
    created_at = models.DateTimeField(auto_now_add=True)
    is_resolved = models.BooleanField(default=False)

    class Meta:
        ordering = ['-created_at']

    def __str__(self):
        return f'{self.username} - {self.report_type}'


class Complaint(models.Model):
    """User Complaints System - Simplified"""
    STATUS_CHOICES = [
        ('pending', 'Pending'),
        ('replied', 'Replied'),
        ('resolved', 'Resolved'),
    ]
    
    user = models.ForeignKey(User, on_delete=models.CASCADE, related_name='complaints')
    subject = models.CharField(max_length=200)
    message = models.TextField()
    image = models.ImageField(upload_to='complaints/', blank=True, null=True, help_text='Optional: Upload image')
    status = models.CharField(max_length=20, choices=STATUS_CHOICES, default='pending')
    is_urgent = models.BooleanField(default=False, help_text='Mark as urgent if needs public announcement')
    admin_reply = models.TextField(blank=True, help_text='Private reply to user')
    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)
    replied_at = models.DateTimeField(null=True, blank=True)

    class Meta:
        ordering = ['-created_at']

    def __str__(self):
        return f'{self.user.username} - {self.subject}'


class Announcement(models.Model):
    """Public Announcements"""
    CATEGORY_CHOICES = [
        ('general', 'General'),
        ('new_product', 'New Product'),
        ('order', 'Order Update'),
        ('promotion', 'Promotion'),
        ('maintenance', 'Maintenance'),
        ('important', 'Important'),
    ]
    
    title = models.CharField(max_length=200)
    content = models.TextField()
    category = models.CharField(max_length=20, choices=CATEGORY_CHOICES, default='general', help_text='Announcement category')
    created_at = models.DateTimeField(auto_now_add=True)
    is_active = models.BooleanField(default=True)

    class Meta:
        ordering = ['-created_at']

    def __str__(self):
        return self.title


class Notification(models.Model):
    """User Notifications"""
    NOTIFICATION_TYPES = [
        ('new_product', 'New Product'),
        ('order_update', 'Order Update'),
        ('complaint_reply', 'Complaint Reply'),
        ('general', 'General'),
    ]
    
    user = models.ForeignKey(User, on_delete=models.CASCADE, related_name='notifications')
    notification_type = models.CharField(max_length=20, choices=NOTIFICATION_TYPES, default='general')
    title = models.CharField(max_length=200)
    message = models.TextField()
    link = models.CharField(max_length=500, blank=True, help_text='Optional link to related page')
    is_read = models.BooleanField(default=False)
    created_at = models.DateTimeField(auto_now_add=True)

    class Meta:
        ordering = ['-created_at']

    def __str__(self):
        return f'{self.user.username} - {self.title}'
