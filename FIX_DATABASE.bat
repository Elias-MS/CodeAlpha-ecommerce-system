@echo off
echo ========================================
echo   FIXING DATABASE - Adding is_active
echo ========================================
echo.

cd /d "c:\xampp\htdocs\Simple E-commerce Store"

C:\Users\User\AppData\Local\Python\bin\python.exe add_is_active_column.py

echo.
echo ========================================
echo   Starting Django Server...
echo ========================================
echo.

C:\Users\User\AppData\Local\Python\bin\python.exe manage.py runserver

pause
