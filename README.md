# 🛒 Simple E-commerce Store

A full-featured, production-ready e-commerce platform built with Django (Python), featuring customer shopping capabilities and comprehensive admin management tools.

![Django](https://img.shields.io/badge/Django-4.2.30-green.svg)
![Python](https://img.shields.io/badge/Python-3.9+-blue.svg)
![License](https://img.shields.io/badge/License-MIT-yellow.svg)

## 🌟 Features

### For Customers
- 🛍️ Browse and search products
- 🛒 Shopping cart management
- 💳 Multiple payment methods (COD, Card, UPI, Net Banking)
- ⭐ Product reviews and ratings
- 📦 Order tracking
- 👤 User profile management
- 🌍 Multi-language support (12 languages)
- 💱 Currency conversion

### For Admins
- 📊 Dashboard with statistics
- 📦 Product management (CRUD operations)
- 👥 User management
- 🛒 Order management with status tracking
- 💬 Complaint handling system
- 📢 Announcement system
- 🔐 Secure admin panel

## 🚀 Quick Start

### Prerequisites
- Python 3.9 or higher
- pip package manager

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/yourusername/simple-ecommerce-store.git
cd simple-ecommerce-store
```

2. **Create virtual environment**
```bash
python -m venv venv
# Windows
venv\Scripts\activate
# Mac/Linux
source venv/bin/activate
```

3. **Install dependencies**
```bash
pip install -r requirements.txt
```

4. **Run migrations**
```bash
python manage.py migrate
```

5. **Create superuser**
```bash
python manage.py createsuperuser
```

6. **Run development server**
```bash
python manage.py runserver
```

7. **Open in browser**
```
http://127.0.0.1:8000/
```

## 📁 Project Structure

```
simple-ecommerce-store/
├── ecommerce/          # Main project configuration
├── products/           # Product catalog app
├── users/              # User authentication & profiles
├── cart/               # Shopping cart functionality
├── orders/             # Order processing
├── templates/          # HTML templates
├── static/             # CSS, JavaScript, images
├── media/              # User-uploaded files
├── requirements.txt    # Python dependencies
└── manage.py          # Django management script
```

## 🛠️ Technology Stack

- **Backend**: Django 4.2.30
- **Database**: SQLite (development) / MySQL/PostgreSQL (production)
- **Frontend**: HTML5, CSS3, Bootstrap 5, JavaScript
- **Icons**: Font Awesome
- **Image Processing**: Pillow

## 📸 Screenshots

(Add your screenshots here)

## 🔐 Admin Access

After creating a superuser, access the admin panel:
- URL: `http://127.0.0.1:8000/admin/`
- Dashboard: `http://127.0.0.1:8000/users/dashboard/`

## 🌐 Deployment

For deployment instructions to various platforms (PythonAnywhere, Render, Google Cloud, etc.), see [DEPLOYMENT_GUIDE.md](DEPLOYMENT_GUIDE.md)

## 📚 Documentation

- [Project Description](PROJECT_DESCRIPTION.md) - Detailed project overview
- [Deployment Guide](DEPLOYMENT_GUIDE.md) - How to deploy online
- [Setup Instructions](SETUP_INSTRUCTIONS.md) - Complete setup guide
- [API Documentation](API_DOCUMENTATION.md) - API endpoints reference

## 🔧 Configuration

### Database
By default, the project uses SQLite. To use MySQL:
1. Uncomment MySQL configuration in `ecommerce/settings.py`
2. Create database in MySQL
3. Update credentials

### Email
Update email settings in `ecommerce/settings.py` for production:
```python
EMAIL_BACKEND = 'django.core.mail.backends.smtp.EmailBackend'
EMAIL_HOST = 'smtp.gmail.com'
EMAIL_PORT = 587
EMAIL_USE_TLS = True
EMAIL_HOST_USER = 'your-email@gmail.com'
EMAIL_HOST_PASSWORD = 'your-app-password'
```

## 🌍 Multi-language Support

Supported languages:
- English, Amharic, Tigrinya, Hindi, Arabic
- Spanish, French, German, Portuguese
- Chinese, Japanese, Korean

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 👨‍💻 Author

Your Name
- GitHub: [@yourusername](https://github.com/yourusername)
- Email: your.email@example.com

## 🙏 Acknowledgments

- Django Documentation
- Bootstrap Team
- Font Awesome
- All contributors

## 📞 Support

For support, email your.email@example.com or create an issue in this repository.

---

**⭐ If you find this project helpful, please consider giving it a star!**
