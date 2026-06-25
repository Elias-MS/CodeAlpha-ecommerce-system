# 🚀 Deployment Guide - Simple E-commerce Store

## Overview
This guide explains how to deploy your Django e-commerce store online so it can be accessed via a public URL.

---

## 📋 Pre-Deployment Checklist

Before deploying, ensure:
- [ ] All features tested locally
- [ ] Database migrations applied
- [ ] Static files collected
- [ ] Admin account created
- [ ] Sample products added
- [ ] Secret key secured
- [ ] Debug mode turned off

---

## 🆓 Option 1: PythonAnywhere (Recommended for Beginners)

### **Why PythonAnywhere?**
- ✅ Free tier available
- ✅ Easy setup
- ✅ Python-friendly
- ✅ No credit card required
- ✅ Built-in database

### **Steps:**

#### 1. Sign Up
- Go to [pythonanywhere.com](https://www.pythonanywhere.com)
- Create free account
- Confirm email

#### 2. Upload Your Project
```bash
# Option A: Via Git
git init
git add .
git commit -m "Initial commit"
git remote add origin YOUR_GITHUB_REPO
git push -u origin main

# Option B: Upload ZIP
# Compress project folder
# Upload via PythonAnywhere Files tab
```

#### 3. Setup on PythonAnywhere

**In Bash console:**
```bash
cd ~
git clone YOUR_GITHUB_REPO_URL
cd Simple-E-commerce-Store

# Create virtual environment
mkvirtualenv --python=/usr/bin/python3.9 mysite
pip install -r requirements.txt

# Setup database
python manage.py migrate
python manage.py createsuperuser
python manage.py collectstatic
```

#### 4. Configure Web App
- Go to **Web** tab
- Click **Add a new web app**
- Choose **Manual configuration**
- Python version: 3.9
- Set source code path: `/home/yourusername/Simple-E-commerce-Store`
- Set working directory: `/home/yourusername/Simple-E-commerce-Store`

#### 5. Edit WSGI file
```python
import os
import sys

path = '/home/yourusername/Simple-E-commerce-Store'
if path not in sys.path:
    sys.path.append(path)

os.environ['DJANGO_SETTINGS_MODULE'] = 'ecommerce.settings'

from django.core.wsgi import get_wsgi_application
application = get_wsgi_application()
```

#### 6. Configure Static Files
In Web tab → Static files section:
- URL: `/static/`
- Directory: `/home/yourusername/Simple-E-commerce-Store/staticfiles`
- URL: `/media/`
- Directory: `/home/yourusername/Simple-E-commerce-Store/media`

#### 7. Update Settings
Edit `ecommerce/settings.py`:
```python
DEBUG = False
ALLOWED_HOSTS = ['yourusername.pythonanywhere.com']
```

#### 8. Reload Web App
- Click **Reload** button in Web tab
- Visit: `https://yourusername.pythonanywhere.com`

---

## 🚀 Option 2: Render.com (Auto-Deploy)

### **Why Render?**
- ✅ Free tier (750 hours/month)
- ✅ Auto-deploys from Git
- ✅ PostgreSQL included
- ✅ SSL certificate included

### **Steps:**

#### 1. Prepare Project

Create `render.yaml`:
```yaml
services:
  - type: web
    name: ecommerce-store
    env: python
    buildCommand: pip install -r requirements.txt && python manage.py migrate && python manage.py collectstatic --noinput
    startCommand: gunicorn ecommerce.wsgi:application
    envVars:
      - key: PYTHON_VERSION
        value: 3.9.13
      - key: SECRET_KEY
        generateValue: true
      - key: DEBUG
        value: False
```

Create `build.sh`:
```bash
#!/usr/bin/env bash
pip install -r requirements.txt
python manage.py migrate
python manage.py collectstatic --no-input
```

#### 2. Update Settings
```python
# ecommerce/settings.py
import os
import dj_database_url

DEBUG = os.getenv('DEBUG', 'False') == 'True'
SECRET_KEY = os.getenv('SECRET_KEY', 'your-secret-key')
ALLOWED_HOSTS = ['*']  # Update with your domain

# Database
DATABASES = {
    'default': dj_database_url.config(
        default='sqlite:///db.sqlite3',
        conn_max_age=600
    )
}
```

#### 3. Deploy
- Sign up at [render.com](https://render.com)
- Connect GitHub repository
- Click **New Web Service**
- Select repository
- Render auto-detects Django
- Click **Deploy**
- Get URL: `yourapp.onrender.com`

---

## ☁️ Option 3: Google Cloud Platform (Your Project is Ready!)

### **Why Google Cloud?**
- ✅ Your project has `app.yaml` configured
- ✅ Scalable
- ✅ Free $300 credit for new users
- ✅ Professional hosting

### **Steps:**

#### 1. Install Google Cloud SDK
Download from [cloud.google.com/sdk](https://cloud.google.com/sdk)

#### 2. Initialize
```bash
gcloud init
gcloud auth login
```

#### 3. Create Project
```bash
gcloud projects create your-project-id
gcloud config set project your-project-id
```

#### 4. Update Settings
```python
# ecommerce/settings.py
ALLOWED_HOSTS = ['your-project-id.appspot.com']
DEBUG = False
```

#### 5. Deploy
```bash
gcloud app deploy
```

#### 6. Access
Visit: `https://your-project-id.appspot.com`

---

## 🌊 Option 4: DigitalOcean (Full Control)

### **Why DigitalOcean?**
- ✅ Full server control
- ✅ Starting $5/month
- ✅ SSD storage
- ✅ Simple interface

### **Steps:**

#### 1. Create Droplet
- Sign up at [digitalocean.com](https://www.digitalocean.com)
- Create Droplet (Ubuntu 22.04)
- Choose $5/month plan

#### 2. Connect via SSH
```bash
ssh root@your_droplet_ip
```

#### 3. Install Dependencies
```bash
apt update
apt install python3-pip python3-dev nginx
pip3 install virtualenv
```

#### 4. Setup Project
```bash
cd /var/www
git clone YOUR_REPO_URL
cd Simple-E-commerce-Store
virtualenv venv
source venv/bin/activate
pip install -r requirements.txt
python manage.py migrate
python manage.py collectstatic
```

#### 5. Configure Gunicorn
```bash
gunicorn --bind 0.0.0.0:8000 ecommerce.wsgi:application
```

#### 6. Configure Nginx
Create `/etc/nginx/sites-available/ecommerce`:
```nginx
server {
    listen 80;
    server_name your_droplet_ip;

    location = /favicon.ico { access_log off; log_not_found off; }
    
    location /static/ {
        root /var/www/Simple-E-commerce-Store;
    }
    
    location /media/ {
        root /var/www/Simple-E-commerce-Store;
    }

    location / {
        proxy_pass http://127.0.0.1:8000;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
    }
}
```

#### 7. Enable Site
```bash
ln -s /etc/nginx/sites-available/ecommerce /etc/nginx/sites-enabled/
nginx -t
systemctl restart nginx
```

#### 8. Access
Visit: `http://your_droplet_ip`

---

## 🏠 Option 5: Local Network Access (Free - WiFi Only)

### **Access from Your Network:**

#### 1. Find Your IP Address
```bash
ipconfig  # Windows
# Look for IPv4 Address (e.g., 192.168.1.100)
```

#### 2. Update Settings
```python
# ecommerce/settings.py
ALLOWED_HOSTS = ['localhost', '127.0.0.1', '192.168.1.100', '*']
```

#### 3. Run Server on Network
```bash
python manage.py runserver 0.0.0.0:8000
```

#### 4. Access from Other Devices
On same WiFi network:
- `http://192.168.1.100:8000`

**Note**: This only works on your local network, not internet.

---

## 🔐 Production Security Settings

### **Update settings.py:**

```python
import os
from pathlib import Path

BASE_DIR = Path(__file__).resolve().parent.parent

# Security
SECRET_KEY = os.environ.get('SECRET_KEY', 'generate-new-key')
DEBUG = False
ALLOWED_HOSTS = ['yourdomain.com', 'www.yourdomain.com']

# HTTPS Settings (for production)
SECURE_SSL_REDIRECT = True
SESSION_COOKIE_SECURE = True
CSRF_COOKIE_SECURE = True
SECURE_BROWSER_XSS_FILTER = True
SECURE_CONTENT_TYPE_NOSNIFF = True
X_FRAME_OPTIONS = 'DENY'

# Database (use environment variable)
DATABASES = {
    'default': {
        'ENGINE': 'django.db.backends.postgresql',
        'NAME': os.environ.get('DB_NAME'),
        'USER': os.environ.get('DB_USER'),
        'PASSWORD': os.environ.get('DB_PASSWORD'),
        'HOST': os.environ.get('DB_HOST'),
        'PORT': '5432',
    }
}

# Static files
STATIC_URL = '/static/'
STATIC_ROOT = BASE_DIR / 'staticfiles'
STATICFILES_STORAGE = 'whitenoise.storage.CompressedManifestStaticFilesStorage'

# Media files
MEDIA_URL = '/media/'
MEDIA_ROOT = BASE_DIR / 'media'

# Email (update for production)
EMAIL_BACKEND = 'django.core.mail.backends.smtp.EmailBackend'
EMAIL_HOST = 'smtp.gmail.com'
EMAIL_PORT = 587
EMAIL_USE_TLS = True
EMAIL_HOST_USER = os.environ.get('EMAIL_USER')
EMAIL_HOST_PASSWORD = os.environ.get('EMAIL_PASSWORD')
```

---

## 📊 Cost Comparison

| Platform | Cost | Best For | Pros | Cons |
|----------|------|----------|------|------|
| **PythonAnywhere** | Free - $5/mo | Learning | Easy setup | Limited resources |
| **Render** | Free - $7/mo | Small apps | Auto-deploy | Sleeps on free tier |
| **Google Cloud** | $10-50/mo | Scalable apps | Professional | Complex setup |
| **DigitalOcean** | $5-10/mo | Full control | Cheap, powerful | Manual setup |
| **Heroku** | $7/mo | Quick deploy | Simple | Expensive for scale |
| **AWS** | $5-20/mo | Enterprise | Scalable | Complex pricing |

---

## 🎯 Recommended Path

### **For Learning/Testing:**
1. Start with **PythonAnywhere** (free, easy)
2. Try **Render** for auto-deployment experience

### **For Production:**
1. **DigitalOcean** if you want control ($5/mo)
2. **Google Cloud** if you need to scale
3. **Heroku** if you want simplicity

---

## ✅ Post-Deployment Checklist

After deployment:
- [ ] Test all pages load correctly
- [ ] Test user registration/login
- [ ] Test adding products to cart
- [ ] Test checkout process
- [ ] Test admin panel access
- [ ] Verify static files (CSS/images) load
- [ ] Test on mobile devices
- [ ] Setup monitoring/logging
- [ ] Configure backup system
- [ ] Setup custom domain (optional)
- [ ] Enable SSL certificate

---

## 🆘 Troubleshooting

### **Static Files Not Loading**
```bash
python manage.py collectstatic --noinput
```

### **Database Errors**
```bash
python manage.py migrate
```

### **Permission Errors**
```bash
chmod 755 -R /path/to/project
```

### **500 Error**
- Check DEBUG = False
- Check ALLOWED_HOSTS
- Check database connection
- Check logs

---

## 📞 Support Resources

- **Django Docs**: [docs.djangoproject.com](https://docs.djangoproject.com)
- **PythonAnywhere Help**: [help.pythonanywhere.com](https://help.pythonanywhere.com)
- **Render Docs**: [render.com/docs](https://render.com/docs)
- **Stack Overflow**: [stackoverflow.com/questions/tagged/django](https://stackoverflow.com/questions/tagged/django)

---

**Good luck with your deployment! 🚀**
