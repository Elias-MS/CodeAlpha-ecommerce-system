from django.urls import path
from . import views

app_name = 'orders'

urlpatterns = [
    path('checkout/', views.checkout, name='checkout'),
    path('place-order/', views.place_order, name='place_order'),
    path('success/<str:order_id>/', views.order_success, name='order_success'),
    path('history/', views.order_history, name='order_history'),
    path('detail/<str:order_id>/', views.order_detail, name='order_detail'),
    path('manage/', views.manage_orders, name='manage_orders'),
    path('update-status/<int:order_id>/', views.update_order_status, name='update_order_status'),
]
