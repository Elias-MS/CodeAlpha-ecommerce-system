# Contributing to E-Commerce Store

Thank you for your interest in contributing to the E-Commerce Store project! This document provides guidelines for contributing.

## 🤝 How to Contribute

### Ways to Contribute

1. **Report Bugs** - Found a bug? Let us know!
2. **Suggest Features** - Have an idea? Share it!
3. **Improve Documentation** - Help make docs better
4. **Submit Code** - Fix bugs or add features
5. **Review Code** - Help review pull requests
6. **Share Feedback** - Tell us what you think

---

## 🐛 Reporting Bugs

### Before Reporting

- Check if the bug has already been reported
- Verify it's actually a bug and not expected behavior
- Test with the latest version

### Bug Report Template

```markdown
**Description:**
A clear description of the bug

**Steps to Reproduce:**
1. Go to '...'
2. Click on '...'
3. See error

**Expected Behavior:**
What should happen

**Actual Behavior:**
What actually happens

**Environment:**
- OS: [e.g., Windows 10]
- Python Version: [e.g., 3.14]
- Django Version: [e.g., 6.0.5]
- Browser: [e.g., Chrome 120]

**Screenshots:**
If applicable

**Additional Context:**
Any other relevant information
```

---

## 💡 Suggesting Features

### Feature Request Template

```markdown
**Feature Description:**
Clear description of the feature

**Problem it Solves:**
What problem does this solve?

**Proposed Solution:**
How should it work?

**Alternatives Considered:**
Other solutions you've thought about

**Additional Context:**
Any other relevant information
```

---

## 🔧 Development Setup

### Prerequisites

- Python 3.8+
- pip
- Git
- Virtual environment (recommended)

### Setup Steps

1. **Fork the Repository**
   ```bash
   # Fork on GitHub, then clone your fork
   git clone https://github.com/YOUR_USERNAME/ecommerce-store.git
   cd ecommerce-store
   ```

2. **Create Virtual Environment**
   ```bash
   python -m venv venv
   source venv/bin/activate  # Linux/Mac
   venv\Scripts\activate     # Windows
   ```

3. **Install Dependencies**
   ```bash
   pip install -r requirements.txt
   ```

4. **Setup Database**
   ```bash
   python manage.py migrate
   ```

5. **Create Superuser**
   ```bash
   python manage.py createsuperuser
   ```

6. **Run Development Server**
   ```bash
   python manage.py runserver
   ```

---

## 📝 Code Contribution Guidelines

### Before You Start

1. Check existing issues and pull requests
2. Create an issue to discuss major changes
3. Fork the repository
4. Create a feature branch

### Coding Standards

#### Python Code

- Follow PEP 8 style guide
- Use meaningful variable names
- Add docstrings to functions and classes
- Keep functions small and focused
- Write comments for complex logic

**Example:**
```python
def calculate_cart_total(cart_items):
    """
    Calculate the total price of all items in the cart.
    
    Args:
        cart_items: QuerySet of CartItem objects
        
    Returns:
        Decimal: Total price of all cart items
    """
    return sum(item.subtotal for item in cart_items)
```

#### HTML/Templates

- Use semantic HTML5 elements
- Maintain consistent indentation (2 spaces)
- Add comments for complex sections
- Follow Django template best practices

**Example:**
```html
<!-- Product Card -->
<div class="card product-card">
    <img src="{{ product.image.url }}" alt="{{ product.name }}">
    <div class="card-body">
        <h5 class="card-title">{{ product.name }}</h5>
        <p class="card-text">{{ product.description|truncatewords:15 }}</p>
    </div>
</div>
```

#### CSS

- Use meaningful class names
- Follow BEM naming convention when possible
- Group related styles together
- Add comments for sections

**Example:**
```css
/* Product Card Styles */
.product-card {
    transition: transform 0.3s ease;
    border: none;
    box-shadow: 0 2px 8px rgba(0,0,0,.1);
}

.product-card:hover {
    transform: translateY(-5px);
}
```

#### JavaScript

- Use ES6+ features
- Add JSDoc comments
- Use meaningful function names
- Handle errors appropriately

**Example:**
```javascript
/**
 * Add product to cart with animation
 * @param {number} productId - The product ID
 * @param {number} quantity - Quantity to add
 */
function addToCart(productId, quantity) {
    // Implementation
}
```

---

## 🔀 Pull Request Process

### Creating a Pull Request

1. **Create a Branch**
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. **Make Changes**
   - Write clean, documented code
   - Follow coding standards
   - Test your changes

3. **Commit Changes**
   ```bash
   git add .
   git commit -m "Add: Brief description of changes"
   ```

4. **Push to Fork**
   ```bash
   git push origin feature/your-feature-name
   ```

5. **Create Pull Request**
   - Go to GitHub
   - Click "New Pull Request"
   - Fill in the template
   - Submit

### Commit Message Guidelines

Use clear, descriptive commit messages:

**Format:**
```
Type: Brief description

Detailed explanation (if needed)
```

**Types:**
- `Add:` New feature
- `Fix:` Bug fix
- `Update:` Update existing feature
- `Remove:` Remove feature/code
- `Refactor:` Code refactoring
- `Docs:` Documentation changes
- `Style:` Code style changes
- `Test:` Add or update tests

**Examples:**
```
Add: User profile picture upload feature

Fix: Cart total calculation error when quantity is zero

Update: Improve product search performance

Docs: Add API documentation for order endpoints
```

### Pull Request Template

```markdown
**Description:**
Brief description of changes

**Type of Change:**
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

**Changes Made:**
- List of changes
- Another change

**Testing:**
- [ ] Tested locally
- [ ] All tests pass
- [ ] Added new tests

**Screenshots:**
If applicable

**Checklist:**
- [ ] Code follows style guidelines
- [ ] Self-review completed
- [ ] Comments added for complex code
- [ ] Documentation updated
- [ ] No new warnings generated
```

---

## 🧪 Testing

### Running Tests

```bash
# Run all tests
python manage.py test

# Run specific app tests
python manage.py test products

# Run with coverage
coverage run --source='.' manage.py test
coverage report
```

### Writing Tests

- Write tests for new features
- Update tests for modified features
- Ensure all tests pass before submitting PR

**Example:**
```python
from django.test import TestCase
from products.models import Product, Category

class ProductModelTest(TestCase):
    def setUp(self):
        self.category = Category.objects.create(name="Test Category")
        
    def test_product_creation(self):
        product = Product.objects.create(
            name="Test Product",
            price=99.99,
            category=self.category,
            stock=10
        )
        self.assertEqual(product.name, "Test Product")
        self.assertTrue(product.is_available)
```

---

## 📚 Documentation

### Updating Documentation

When making changes, update relevant documentation:

- **README.md** - Main documentation
- **API_DOCUMENTATION.md** - API changes
- **DATABASE_SCHEMA.md** - Database changes
- **FEATURES.md** - New features
- Code comments and docstrings

### Documentation Style

- Use clear, concise language
- Include code examples
- Add screenshots for UI changes
- Keep formatting consistent

---

## 🎨 Design Guidelines

### UI/UX Principles

- **Consistency** - Maintain consistent design
- **Simplicity** - Keep it simple and intuitive
- **Accessibility** - Ensure accessibility compliance
- **Responsiveness** - Test on multiple devices
- **Performance** - Optimize for speed

### Color Scheme

Maintain the existing color scheme:
- Primary: `#0d6efd`
- Success: `#198754`
- Danger: `#dc3545`
- Warning: `#ffc107`

---

## 🔒 Security

### Security Guidelines

- Never commit sensitive data (passwords, API keys)
- Use environment variables for secrets
- Validate all user inputs
- Sanitize data before display
- Follow OWASP security practices
- Report security issues privately

### Reporting Security Issues

**DO NOT** create public issues for security vulnerabilities.

Instead, email: security@eshop.com

Include:
- Description of vulnerability
- Steps to reproduce
- Potential impact
- Suggested fix (if any)

---

## 📋 Code Review Process

### For Contributors

- Be open to feedback
- Respond to review comments
- Make requested changes promptly
- Ask questions if unclear

### For Reviewers

- Be respectful and constructive
- Explain reasoning for suggestions
- Approve when ready
- Test changes locally if possible

---

## 🏆 Recognition

Contributors will be:
- Listed in CONTRIBUTORS.md
- Mentioned in release notes
- Credited in documentation

---

## 📞 Getting Help

### Resources

- **Documentation:** See docs folder
- **Issues:** GitHub Issues
- **Discussions:** GitHub Discussions
- **Email:** support@eshop.com

### Questions?

- Check existing documentation
- Search closed issues
- Ask in discussions
- Create a new issue

---

## 📜 Code of Conduct

### Our Pledge

We pledge to make participation in our project a harassment-free experience for everyone.

### Our Standards

**Positive behavior:**
- Using welcoming language
- Being respectful
- Accepting constructive criticism
- Focusing on what's best for the community

**Unacceptable behavior:**
- Harassment or discrimination
- Trolling or insulting comments
- Personal or political attacks
- Publishing private information

### Enforcement

Violations may result in:
- Warning
- Temporary ban
- Permanent ban

Report violations to: conduct@eshop.com

---

## 🎯 Priority Areas

We especially welcome contributions in:

1. **Testing** - Increase test coverage
2. **Documentation** - Improve docs
3. **Accessibility** - Enhance accessibility
4. **Performance** - Optimize performance
5. **Security** - Strengthen security
6. **Features** - Add new features
7. **Bug Fixes** - Fix reported bugs

---

## 📝 License

By contributing, you agree that your contributions will be licensed under the MIT License.

---

## 🙏 Thank You!

Thank you for contributing to E-Commerce Store! Your efforts help make this project better for everyone.

**Happy Contributing! 🚀**

---

*Last Updated: May 29, 2026*
