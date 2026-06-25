from django.urls import path
from . import views

app_name = 'users'

urlpatterns = [
    path('register/', views.register, name='register'),
    path('login/', views.user_login, name='login'),
    path('logout/', views.user_logout, name='logout'),
    path('profile/', views.profile, name='profile'),
    path('dashboard/', views.dashboard, name='dashboard'),
    path('about/', views.about_us, name='about_us'),
    path('contact/', views.contact_us, name='contact_us'),
    path('submit-report/', views.submit_report, name='submit_report'),
    path('password-reset/', views.password_reset_request, name='password_reset'),
    path('password-reset/<uidb64>/<token>/', views.password_reset_confirm, name='password_reset_confirm'),
    path('complaints/submit/', views.submit_complaint, name='submit_complaint'),
    path('complaints/', views.my_complaints, name='my_complaints'),
    path('complaints/<int:complaint_id>/', views.complaint_detail, name='complaint_detail'),
    path('announcements/', views.announcements, name='announcements'),
    
    # User Management (Staff Only)
    path('manage/', views.manage_users, name='manage_users'),
    path('toggle-status/<int:user_id>/', views.toggle_user_status, name='toggle_user_status'),
    path('complaints/manage/', views.manage_complaints, name='manage_complaints'),
]
