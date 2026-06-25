<div align="center">

# 🛒 Django E-Commerce Store

### A Full-Featured Online Shopping Platform

[![Django](https://img.shields.io/badge/Django-4.2.30-092E20?style=for-the-badge&logo=django&logoColor=white)](https://www.djangoproject.com/)
[![Python](https://img.shields.io/badge/Python-3.9+-3776AB?style=for-the-badge&logo=python&logoColor=white)](https://www.python.org/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.0-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)](https://getbootstrap.com/)
[![SQLite](https://img.shields.io/badge/SQLite-07405E?style=for-the-badge&logo=sqlite&logoColor=white)](https://www.sqlite.org/)
[![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)](LICENSE)

**[Live Demo](#) | [Documentation](API_DOCUMENTATION.md) | [Report Bug](https://github.com/Elias-MS/E-commerance-system/issues) | [Request Feature](https://github.com/Elias-MS/E-commerance-system/issues)**

</div>

---

## 📋 Table of Contents

- [About The Project](#-about-the-project)
- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [Usage](#-usage)
- [Screenshots](#-screenshots)
- [Deployment](#-deployment)
- [Contributing](#-contributing)
- [License](#-license)
- [Contact](#-contact)

---

## 🎯 About The Project

A comprehensive, production-ready e-commerce platform built with Django that provides a seamless online shopping experience. Features include user authentication, shopping cart, order management, multi-language support, and a powerful admin dashboard.

### ✨ Why This Project?

- 🚀 **Production Ready** - Fully functional with all essential e-commerce features
- 🎨 **Modern UI** - Responsive Bootstrap 5 design that works on all devices
- 🔒 **Secure** - Implements Django security best practices
- 🌍 **Multi-language** - Supports 12 different languages
- 📱 **Mobile Friendly** - Optimized for mobile shopping experience
- 🎓 **Learning Resource** - Well-documented code for learning Django

---

## 🌟 Features

<table>
<tr>
<td width="50%">

### 🛍️ Customer Features

- ✅ User registration & authentication
- ✅ Browse products with search & filters
- ✅ Detailed product pages with reviews
- ✅ Shopping cart management
- ✅ Secure checkout process
- ✅ Multiple payment methods
- ✅ Order tracking & history
- ✅ User profile management
- ✅ Product reviews & ratings
- ✅ Submit complaints with images
- ✅ View announcements

</td>
<td width="50%">

### 👨‍💼 Admin Features

- ✅ Comprehensive dashboard
- ✅ Product management (CRUD)
- ✅ Category management
- ✅ Order management
- ✅ User management
- ✅ Inventory tracking
- ✅ Complaint handling
- ✅ Announcement system
- ✅ Sales statistics
- ✅ Product activation/deactivation

</td>
</tr>
</table>

### 🌍 Additional Features

- 🔄 **Multi-Currency Support** - Display prices in different currencies
- 🌐 **12 Languages** - English, Amharic, Tigrinya, Hindi, Arabic, Spanish, French, German, Chinese, Japanese, Korean, Portuguese
- 📧 **Email Notifications** - Order confirmations and updates
- 🔐 **Secure Authentication** - Password hashing and session management
- 📱 **Responsive Design** - Works perfectly on desktop, tablet, and mobile
- 🎨 **Modern UI/UX** - Clean and intuitive interface

---

## 🛠️ Tech Stack

<div align="center">

### Backend
![Django](https://img.shields.io/badge/Django-092E20?style=for-the-badge&logo=django&logoColor=white)
![Python](https://img.shields.io/badge/Python-3776AB?style=for-the-badge&logo=python&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-07405E?style=for-the-badge&logo=sqlite&logoColor=white)

### Frontend
![HTML5](https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white)
![CSS3](https://img.shields.io/badge/CSS3-1572B6?style=for-the-badge&logo=css3&logoColor=white)
![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?style=for-the-badge&logo=javascript&logoColor=black)
![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)

### Tools & Libraries
![Git](https://img.shields.io/badge/Git-F05032?style=for-the-badge&logo=git&logoColor=white)
![Pillow](https://img.shields.io/badge/Pillow-Image_Processing-blue?style=for-the-badge)
![Font Awesome](https://img.shields.io/badge/Font_Awesome-339AF0?style=for-the-badge&logo=fontawesome&logoColor=white)

</div>

---

## 🚀 Getting Started

### 📋 Prerequisites

Before you begin, ensure you have the following installed:

- **Python** 3.9 or higher ([Download](https://www.python.org/downloads/))
- **pip** (comes with Python)
- **Git** ([Download](https://git-scm.com/downloads))
- **Virtual Environment** (recommended)

### 📥 Installation

Follow these steps to get your development environment running:

#### 1️⃣ Clone the Repository

```bash
git clone https://github.com/Elias-MS/E-commerance-system.git
cd E-commerance-system
```

#### 2️⃣ Create Virtual Environment

```bash
# Windows
python -m venv venv
venv\Scripts\activate

# Mac/Linux
python3 -m venv venv
source venv/bin/activate
```

#### 3️⃣ Install Dependencies

```bash
pip install -r requirements.txt
```

#### 4️⃣ Database Setup

```bash
# Run migrations
python manage.py migrate

# Create superuser for admin access
python manage.py createsuperuser
```

#### 5️⃣ Run Development Server

```bash
python manage.py runserver
```

#### 6️⃣ Access the Application

🌐 Open your browser and navigate to:
- **Main Site**: http://127.0.0.1:8000/
- **Admin Panel**: http://127.0.0.1:8000/admin/

---

## 📁 Project Structure

```
E-commerance-system/
│
├── 📂 ecommerce/                      # Main Project Configuration
│   ├── __init__.py
│   ├── settings.py                    # Project settings & configuration
│   ├── urls.py                        # Main URL routing
│   ├── wsgi.py                        # WSGI configuration for deployment
│   ├── asgi.py                        # ASGI configuration
│   ├── context_processors.py          # Custom context processors
│   └── views.py                       # Project-level views
│
├── 📂 products/                       # Products Management App
│   ├── 📂 migrations/                 # Database migrations
│   ├── __init__.py
│   ├── admin.py                       # Admin panel configuration
│   ├── apps.py                        # App configuration
│   ├── models.py                      # Product, Category, Review models
│   ├── views.py                       # Product views (list, detail, CRUD)
│   └── urls.py                        # Product URL routing
│
├── 📂 users/                          # User Management App
│   ├── 📂 migrations/                 # Database migrations
│   ├── __init__.py
│   ├── admin.py                       # User admin configuration
│   ├── apps.py                        # App configuration
│   ├── models.py                      # UserProfile, Complaint, Announcement models
│   ├── views.py                       # Authentication & user views
│   ├── urls.py                        # User URL routing
│   └── forms.py                       # User registration & profile forms
│
├── 📂 cart/                           # Shopping Cart App
│   ├── 📂 migrations/                 # Database migrations
│   ├── __init__.py
│   ├── admin.py                       # Cart admin configuration
│   ├── apps.py                        # App configuration
│   ├── models.py                      # Cart, CartItem models
│   ├── views.py                       # Cart management views
│   ├── urls.py                        # Cart URL routing
│   └── context_processors.py          # Cart count context processor
│
├── 📂 orders/                         # Order Management App
│   ├── 📂 migrations/                 # Database migrations
│   ├── __init__.py
│   ├── admin.py                       # Order admin configuration
│   ├── apps.py                        # App configuration
│   ├── models.py                      # Order, OrderItem models
│   ├── views.py                       # Checkout & order views
│   └── urls.py                        # Order URL routing
│
├── 📂 templates/                      # HTML Templates
│   ├── base.html                      # Base template (navbar, footer)
│   │
│   ├── 📂 products/                   # Product templates
│   │   ├── home.html                  # Homepage
│   │   ├── product_list.html          # Product listing page
│   │   ├── product_detail.html        # Product detail page
│   │   ├── add_product.html           # Add product (admin)
│   │   ├── edit_product.html          # Edit product (admin)
│   │   └── manage_products.html       # Product management (admin)
│   │
│   ├── 📂 users/                      # User templates
│   │   ├── register.html              # User registration
│   │   ├── login.html                 # User login
│   │   ├── profile.html               # User profile
│   │   ├── dashboard.html             # User dashboard
│   │   ├── admin_dashboard.html       # Admin dashboard
│   │   ├── about_us.html              # About page
│   │   ├── contact_us.html            # Contact page
│   │   ├── submit_complaint.html      # Submit complaint
│   │   ├── my_complaints.html         # View complaints
│   │   ├── manage_users.html          # User management (admin)
│   │   ├── manage_complaints.html     # Complaint management (admin)
│   │   └── announcements.html         # View announcements
│   │
│   ├── 📂 cart/                       # Cart templates
│   │   └── cart.html                  # Shopping cart page
│   │
│   └── 📂 orders/                     # Order templates
│       ├── checkout.html              # Checkout page
│       ├── order_success.html         # Order confirmation
│       ├── order_history.html         # Order history
│       ├── order_detail.html          # Order details
│       └── manage_orders.html         # Order management (admin)
│
├── 📂 static/                         # Static Files
│   ├── 📂 css/
│   │   └── style.css                  # Custom styles
│   ├── 📂 js/
│   │   ├── main.js                    # Main JavaScript
│   │   ├── currency.js                # Currency conversion
│   │   └── translations.js            # Multi-language support
│   └── 📂 admin/
│       └── css/
│           └── custom_admin.css       # Admin panel custom styles
│
├── 📂 media/                          # User Uploaded Files
│   ├── 📂 products/                   # Product images
│   └── 📂 complaints/                 # Complaint images
│
├── 📂 locale/                         # Translation Files
│   ├── 📂 am/                         # Amharic translations
│   ├── 📂 ti/                         # Tigrinya translations
│   ├── 📂 hi/                         # Hindi translations
│   ├── 📂 ar/                         # Arabic translations
│   └── ...                            # Other language translations
│
├── 📄 manage.py                       # Django management script
├── 📄 requirements.txt                # Python dependencies
├── 📄 runtime.txt                     # Python version for deployment
├── 📄 Procfile                        # Heroku deployment configuration
├── 📄 app.yaml                        # Google Cloud deployment
├── 📄 railway.toml                    # Railway deployment
├── 📄 render.yaml                     # Render deployment
│
├── 📄 README.md                       # Project documentation (this file)
├── 📄 LICENSE                         # MIT License
├── 📄 .gitignore                      # Git ignore rules
├── 📄 .env.example                    # Environment variables example
│
└── 📚 Documentation/                  # Additional Documentation
    ├── API_DOCUMENTATION.md           # API endpoints reference
    ├── DEPLOYMENT_GUIDE.md            # Deployment instructions
    ├── SETUP_INSTRUCTIONS.md          # Detailed setup guide
    ├── PROJECT_DESCRIPTION.md         # Project overview
    └── DATABASE_SCHEMA.md             # Database structure
```

### 📊 Database Models

<details>
<summary><b>Click to expand database structure</b></summary>

#### 🗃️ Products App
- **Product** - Product information (name, description, price, stock, category, image)
- **Category** - Product categories
- **ProductReview** - Customer reviews and ratings

#### 👤 Users App
- **UserProfile** - Extended user information (phone, address, city, state, zipcode)
- **ContactMessage** - Contact form submissions
- **UserReport** - User account reports
- **Complaint** - Customer complaints with admin replies
- **Announcement** - Public announcements
- **Notification** - User notifications

#### 🛒 Cart App
- **Cart** - Shopping cart for each user
- **CartItem** - Items in the cart

#### 📦 Orders App
- **Order** - Order information (customer details, payment, status)
- **OrderItem** - Products in each order

</details>

---

## 💻 Usage

### For Customers

1. **Browse Products**
   - Visit the homepage to see featured products
   - Use search and filters to find specific items
   - View detailed product information and reviews

2. **Shopping**
   - Add items to cart
   - Update quantities or remove items
   - Proceed to checkout

3. **Place Order**
   - Fill in shipping information
   - Choose payment method (COD, Card, UPI, Net Banking)
   - Confirm order and track status

4. **Manage Account**
   - Update profile information
   - View order history
   - Submit complaints or feedback

### For Administrators

1. **Access Admin Dashboard**
   - Login at `/admin/` or `/users/dashboard/`
   - View statistics and recent activity

2. **Manage Products**
   - Add, edit, or delete products
   - Update stock levels
   - Activate/deactivate products

3. **Handle Orders**
   - View all orders
   - Update order status (pending → processing → shipped → delivered)
   - Track customer orders

4. **User Management**
   - View all registered users
   - Activate/deactivate user accounts
   - Respond to customer complaints

---

## 📸 Screenshots

<div align="center">

### 🏠 Homepage
*Modern, responsive design with featured products*

### 🛍️ Product Listing
*Search, filter, and browse products easily*

### 📦 Product Details
*Detailed product information with reviews*

### 🛒 Shopping Cart
*Manage cart items before checkout*

### 💳 Checkout
*Secure checkout with multiple payment options*

### 👨‍💼 Admin Dashboard
*Comprehensive statistics and management tools*

**📷 *Add your screenshots here to showcase your project!***

</div>

---

## 🌐 Deployment

Ready to deploy your e-commerce store? We support multiple platforms:

<table>
<tr>
<td align="center" width="25%">
<img src="https://img.shields.io/badge/PythonAnywhere-1D9FD7?style=for-the-badge&logo=pythonanywhere&logoColor=white" /><br>
<b>PythonAnywhere</b><br>
Free hosting<br>
<a href="DEPLOYMENT_GUIDE.md#pythonanywhere">Guide →</a>
</td>
<td align="center" width="25%">
<img src="https://img.shields.io/badge/Render-46E3B7?style=for-the-badge&logo=render&logoColor=white" /><br>
<b>Render</b><br>
Auto-deploy from Git<br>
<a href="DEPLOYMENT_GUIDE.md#render">Guide →</a>
</td>
<td align="center" width="25%">
<img src="https://img.shields.io/badge/Google_Cloud-4285F4?style=for-the-badge&logo=google-cloud&logoColor=white" /><br>
<b>Google Cloud</b><br>
Scalable hosting<br>
<a href="DEPLOYMENT_GUIDE.md#google-cloud">Guide →</a>
</td>
<td align="center" width="25%">
<img src="https://img.shields.io/badge/DigitalOcean-0080FF?style=for-the-badge&logo=digitalocean&logoColor=white" /><br>
<b>DigitalOcean</b><br>
Full control VPS<br>
<a href="DEPLOYMENT_GUIDE.md#digitalocean">Guide →</a>
</td>
</tr>
</table>

📖 **Full deployment instructions**: [DEPLOYMENT_GUIDE.md](DEPLOYMENT_GUIDE.md)

---

## 🔧 Configuration

### Database Configuration

**Default: SQLite** (no setup required)

For **MySQL/PostgreSQL** in production:

```python
# ecommerce/settings.py

DATABASES = {
    'default': {
        'ENGINE': 'django.db.backends.mysql',  # or 'postgresql'
        'NAME': 'your_database_name',
        'USER': 'your_username',
        'PASSWORD': 'your_password',
        'HOST': 'localhost',
        'PORT': '3306',  # 5432 for PostgreSQL
    }
}
```

### Email Configuration

```python
# For production email notifications
EMAIL_BACKEND = 'django.core.mail.backends.smtp.EmailBackend'
EMAIL_HOST = 'smtp.gmail.com'
EMAIL_PORT = 587
EMAIL_USE_TLS = True
EMAIL_HOST_USER = 'your-email@gmail.com'
EMAIL_HOST_PASSWORD = 'your-app-specific-password'
DEFAULT_FROM_EMAIL = 'noreply@yourstore.com'
```

### Environment Variables

Create a `.env` file (see `.env.example`):

```bash
SECRET_KEY=your-secret-key-here
DEBUG=False
ALLOWED_HOSTS=yourdomain.com,www.yourdomain.com
DATABASE_URL=your-database-url
```

---

## 🌍 Multi-Language Support

<div align="center">

| Language | Code | Status |
|----------|------|--------|
| 🇬🇧 English | `en` | ✅ |
| 🇪🇹 Amharic | `am` | ✅ |
| 🇪🇷 Tigrinya | `ti` | ✅ |
| 🇮🇳 Hindi | `hi` | ✅ |
| 🇸🇦 Arabic | `ar` | ✅ |
| 🇪🇸 Spanish | `es` | ✅ |
| 🇫🇷 French | `fr` | ✅ |
| 🇩🇪 German | `de` | ✅ |
| 🇨🇳 Chinese | `zh-hans` | ✅ |
| 🇯🇵 Japanese | `ja` | ✅ |
| 🇰🇷 Korean | `ko` | ✅ |
| 🇵🇹 Portuguese | `pt` | ✅ |

</div>

To add a new language, run:
```bash
django-admin makemessages -l <language_code>
django-admin compilemessages
```

---

## 🤝 Contributing

Contributions make the open-source community an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**!

### How to Contribute

1. **Fork the Project**
2. **Create your Feature Branch**
   ```bash
   git checkout -b feature/AmazingFeature
   ```
3. **Commit your Changes**
   ```bash
   git commit -m 'Add some AmazingFeature'
   ```
4. **Push to the Branch**
   ```bash
   git push origin feature/AmazingFeature
   ```
5. **Open a Pull Request**

### Code of Conduct

Please read our [Code of Conduct](CODE_OF_CONDUCT.md) before contributing.

---

## 📝 License

Distributed under the MIT License. See `LICENSE` for more information.

```
MIT License - feel free to use this project for learning or commercial purposes!
```

---

## 👨‍💻 Author

<div align="center">

### **Elias M.S.**

[![GitHub](https://img.shields.io/badge/GitHub-Elias--MS-181717?style=for-the-badge&logo=github)](https://github.com/Elias-MS)
[![Email](https://img.shields.io/badge/Email-Contact-D14836?style=for-the-badge&logo=gmail&logoColor=white)](mailto:your.email@example.com)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Connect-0077B5?style=for-the-badge&logo=linkedin)](https://linkedin.com/in/your-profile)

</div>

---

## 🙏 Acknowledgments

Special thanks to:

- [Django](https://www.djangoproject.com/) - The web framework for perfectionists with deadlines
- [Bootstrap](https://getbootstrap.com/) - Frontend framework
- [Font Awesome](https://fontawesome.com/) - Icons
- [Stack Overflow](https://stackoverflow.com/) - Community support
- All contributors who helped improve this project

---

## 📚 Documentation

<div align="center">

| Document | Description |
|----------|-------------|
| [📖 API Documentation](API_DOCUMENTATION.md) | Complete API endpoints reference |
| [🚀 Deployment Guide](DEPLOYMENT_GUIDE.md) | How to deploy to production |
| [⚙️ Setup Instructions](SETUP_INSTRUCTIONS.md) | Detailed setup guide |
| [📋 Project Description](PROJECT_DESCRIPTION.md) | Comprehensive project overview |
| [🗃️ Database Schema](DATABASE_SCHEMA.md) | Database structure and models |

</div>

---

## 📞 Support

<div align="center">

### Need Help?

[![GitHub Issues](https://img.shields.io/github/issues/Elias-MS/E-commerance-system?style=for-the-badge)](https://github.com/Elias-MS/E-commerance-system/issues)
[![Documentation](https://img.shields.io/badge/Documentation-Read-blue?style=for-the-badge)](API_DOCUMENTATION.md)

**Found a bug?** [Report it](https://github.com/Elias-MS/E-commerance-system/issues)  
**Have a question?** [Ask in Discussions](https://github.com/Elias-MS/E-commerance-system/discussions)  
**Want to contribute?** [See Contributing](#-contributing)

</div>

---

## ⭐ Show Your Support

<div align="center">

If you found this project helpful, please give it a ⭐!

[![Star this repo](https://img.shields.io/github/stars/Elias-MS/E-commerance-system?style=social)](https://github.com/Elias-MS/E-commerance-system)
[![Fork this repo](https://img.shields.io/github/forks/Elias-MS/E-commerance-system?style=social)](https://github.com/Elias-MS/E-commerance-system/fork)
[![Watch this repo](https://img.shields.io/github/watchers/Elias-MS/E-commerance-system?style=social)](https://github.com/Elias-MS/E-commerance-system)

### Share This Project

Share this project with your friends and colleagues!

[![Twitter](https://img.shields.io/twitter/url?style=social&url=https%3A%2F%2Fgithub.com%2FElias-MS%2FE-commerance-system)](https://twitter.com/intent/tweet?text=Check%20out%20this%20awesome%20Django%20E-Commerce%20project!&url=https://github.com/Elias-MS/E-commerance-system)

</div>

---

<div align="center">

### 🌟 Featured Technologies

[![Django](https://img.shields.io/badge/Django-092E20?style=flat&logo=django&logoColor=white)](https://www.djangoproject.com/)
[![Python](https://img.shields.io/badge/Python-3776AB?style=flat&logo=python&logoColor=white)](https://www.python.org/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?style=flat&logo=bootstrap&logoColor=white)](https://getbootstrap.com/)
[![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?style=flat&logo=javascript&logoColor=black)](https://developer.mozilla.org/en-US/docs/Web/JavaScript)
[![HTML5](https://img.shields.io/badge/HTML5-E34F26?style=flat&logo=html5&logoColor=white)](https://developer.mozilla.org/en-US/docs/Web/HTML)
[![CSS3](https://img.shields.io/badge/CSS3-1572B6?style=flat&logo=css3&logoColor=white)](https://developer.mozilla.org/en-US/docs/Web/CSS)

**Made with ❤️ by [Elias M.S.](https://github.com/Elias-MS)**

*Last Updated: June 2026*

</div>
