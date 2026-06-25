# 🚀 Deploy to Render - Step by Step Guide

## Why Render?
- ✅ **100% FREE** (no credit card required)
- ✅ **Easy to use**
- ✅ **Automatic deployments from GitHub**
- ✅ **Free SSL certificate**
- ✅ **Free PostgreSQL database**
- ✅ **Custom domain support**

---

## 📋 Prerequisites

1. ✅ GitHub account (create at https://github.com)
2. ✅ Render account (create at https://render.com)
3. ✅ Your Django project (this folder)

---

## 🎯 Step 1: Fix Database Error First

Before deploying, fix the database:

1. Double-click: `FIX_DATABASE_NOW.bat`
2. Wait for "SUCCESS!" message

---

## 🎯 Step 2: Update .gitignore

Create/update `.gitignore` file to exclude sensitive files:

```
*.pyc
__pycache__/
db.sqlite3
*.sqlite3
.env
.env.production
media/
staticfiles/
*.log
.DS_Store
```

---

## 🎯 Step 3: Push to GitHub

### Option A: Using GitHub Desktop (Easiest)

1. **Download GitHub Desktop**: https://desktop.github.com/
2. **Install and sign in** with your GitHub account
3. **Add your project**:
   - File → Add Local Repository
   - Choose your project folder
   - Click "Create Repository"
4. **Commit changes**:
   - Write commit message: "Initial commit"
   - Click "Commit to main"
5. **Publish to GitHub**:
   - Click "Publish repository"
   - Uncheck "Keep this code private" (or keep it private)
   - Click "Publish Repository"

### Option B: Using Command Line

```bash
# Initialize git
git init

# Add all files
git add .

# Commit
git commit -m "Initial commit"

# Create repository on GitHub first, then:
git remote add origin https://github.com/yourusername/your-repo-name.git
git branch -M main
git push -u origin main
```

---

## 🎯 Step 4: Create Render Account

1. Go to: https://render.com/
2. Click "Get Started for Free"
3. Sign up with your **GitHub account** (recommended)
4. Authorize Render to access your GitHub

---

## 🎯 Step 5: Create PostgreSQL Database

1. In Render dashboard, click **"New +"**
2. Select **"PostgreSQL"**
3. Fill in:
   - **Name**: `ecommerce-db`
   - **Database**: `ecommerce`
   - **User**: `ecommerce`
   - **Region**: Choose closest to you
   - **Plan**: **Free**
4. Click **"Create Database"**
5. Wait for database to be created (takes 1-2 minutes)
6. **Copy the "Internal Database URL"** (you'll need this)

---

## 🎯 Step 6: Create Web Service

1. Click **"New +"** again
2. Select **"Web Service"**
3. Click **"Connect a repository"**
4. Find and select your GitHub repository
5. Click **"Connect"**

---

## 🎯 Step 7: Configure Web Service

Fill in the following:

### Basic Settings:
- **Name**: `ecommerce-store` (or your preferred name)
- **Region**: Same as your database
- **Branch**: `main`
- **Root Directory**: (leave empty)
- **Runtime**: `Python 3`

### Build Settings:
- **Build Command**:
  ```
  pip install -r requirements.txt && python manage.py collectstatic --noinput && python manage.py migrate
  ```

- **Start Command**:
  ```
  gunicorn ecommerce.wsgi:application
  ```

### Plan:
- Select **"Free"**

---

## 🎯 Step 8: Add Environment Variables

Scroll down to **"Environment Variables"** section and add these:

Click **"Add Environment Variable"** for each:

1. **PYTHON_VERSION**
   - Value: `3.10.0`

2. **SECRET_KEY**
   - Value: Generate a new secret key
   - Use this site: https://djecrety.ir/
   - Or use: `django-insecure-$(openssl rand -base64 32)`

3. **DEBUG**
   - Value: `False`

4. **ALLOWED_HOSTS**
   - Value: `.onrender.com`

5. **DATABASE_URL**
   - Value: Paste the "Internal Database URL" from Step 5
   - Example: `postgresql://user:password@host/database`

6. **DJANGO_SETTINGS_MODULE**
   - Value: `ecommerce.production_settings`

7. **DISABLE_COLLECTSTATIC**
   - Value: `0`

---

## 🎯 Step 9: Deploy!

1. Click **"Create Web Service"**
2. Render will start building your app
3. Wait 5-10 minutes for first deployment
4. Watch the logs for any errors

---

## 🎯 Step 10: Create Superuser

After deployment succeeds:

1. Go to your web service dashboard
2. Click **"Shell"** tab
3. Run these commands:
   ```bash
   python manage.py createsuperuser
   ```
4. Follow prompts to create admin account

---

## 🎯 Step 11: Test Your Website

1. Your website URL will be: `https://ecommerce-store.onrender.com`
2. Test these pages:
   - Home: `https://your-app.onrender.com/`
   - Admin: `https://your-app.onrender.com/admin/`
   - Products: `https://your-app.onrender.com/products/`

---

## 🎯 Step 12: Add Sample Data (Optional)

In the Shell tab, run:
```bash
python create_sample_data.py
```

---

## 🔧 Troubleshooting

### Issue: Static files not loading

**Solution**: Make sure you have these in `production_settings.py`:
```python
STATIC_ROOT = BASE_DIR / 'staticfiles'
MIDDLEWARE.insert(1, 'whitenoise.middleware.WhiteNoiseMiddleware')
```

### Issue: Database connection error

**Solution**: 
1. Check DATABASE_URL is correct
2. Make sure database is in same region as web service
3. Use "Internal Database URL" not "External"

### Issue: Build fails

**Solution**:
1. Check `requirements.txt` is correct
2. Make sure all files are committed to GitHub
3. Check build logs for specific error

### Issue: Application error

**Solution**:
1. Check logs in Render dashboard
2. Make sure SECRET_KEY is set
3. Make sure DEBUG=False
4. Check ALLOWED_HOSTS includes `.onrender.com`

---

## 🎨 Custom Domain (Optional)

To use your own domain:

1. Buy a domain (from Namecheap, GoDaddy, etc.)
2. In Render dashboard, go to your web service
3. Click "Settings" → "Custom Domain"
4. Add your domain
5. Update DNS records as instructed

---

## 🔄 Automatic Deployments

Every time you push to GitHub:
1. Render automatically detects changes
2. Rebuilds your app
3. Deploys new version
4. Zero downtime!

To push updates:
```bash
git add .
git commit -m "Your update message"
git push
```

---

## 📊 Monitor Your App

In Render dashboard:
- **Logs**: View real-time logs
- **Metrics**: See CPU, memory usage
- **Events**: Deployment history
- **Shell**: Run commands

---

## 💰 Cost

**FREE TIER INCLUDES:**
- ✅ 750 hours/month (enough for 1 app running 24/7)
- ✅ 512 MB RAM
- ✅ Free SSL certificate
- ✅ Automatic deployments
- ✅ Custom domains
- ✅ Free PostgreSQL database (90 days, then $7/month)

**Note**: Free tier apps spin down after 15 minutes of inactivity. First request after inactivity takes 30-60 seconds.

---

## 🎉 Success!

Your e-commerce store is now live on the internet! 🚀

**Share your link**: `https://your-app-name.onrender.com`

---

## 📞 Need Help?

- Render Docs: https://render.com/docs
- Django Deployment: https://docs.djangoproject.com/en/stable/howto/deployment/
- Community: https://community.render.com/

---

## ✅ Checklist

- [ ] Fixed database error locally
- [ ] Created GitHub account
- [ ] Pushed code to GitHub
- [ ] Created Render account
- [ ] Created PostgreSQL database
- [ ] Created web service
- [ ] Added environment variables
- [ ] Deployed successfully
- [ ] Created superuser
- [ ] Tested website
- [ ] Added sample data
- [ ] Shared with friends! 🎉
