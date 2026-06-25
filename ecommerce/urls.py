"""
URL configuration for ecommerce project.
"""
from django.contrib import admin
from django.urls import path, include
from django.conf import settings
from django.conf.urls.static import static
from django.conf.urls.i18n import i18n_patterns
from django.views.i18n import set_language
from . import views

urlpatterns = [
    # Language switcher (outside i18n_patterns so it works for all languages)
    path('i18n/setlang/', set_language, name='set_language'),
    # Currency switcher
    path('set-currency/', views.set_currency, name='set_currency'),
]

# Add language prefix to URLs
urlpatterns += i18n_patterns(
    path('admin/', admin.site.urls),
    path('', include('products.urls')),
    path('users/', include('users.urls')),
    path('cart/', include('cart.urls')),
    path('orders/', include('orders.urls')),
    prefix_default_language=False,  # Don't add /en/ for English
)

# Serve media files in development
if settings.DEBUG:
    urlpatterns += static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT)
    urlpatterns += static(settings.STATIC_URL, document_root=settings.STATIC_ROOT)
