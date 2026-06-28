#!/bin/bash

# Deployment script for production

echo "========================================="
echo "Starting Deployment Process"
echo "========================================="

# Install dependencies
echo "Installing dependencies..."
pip install -r requirements.txt

# Collect static files
echo "Collecting static files..."
python manage.py collectstatic --noinput

# Run migrations
echo "Running database migrations..."
python manage.py migrate

# Create superuser (optional - comment out if not needed)
# echo "Creating superuser..."
# python manage.py createsuperuser

echo "========================================="
echo "Deployment Complete!"
echo "========================================="
echo "To start the server, run:"
echo "gunicorn ecommerce.wsgi:application"
