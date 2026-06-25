@echo off
echo ========================================
echo FIXING DATABASE - ADDING MISSING COLUMN
echo ========================================
echo.

cd /d "%~dp0"

echo Running database fix...
python SIMPLE_FIX.py

echo.
echo ========================================
echo DONE! Now refresh your browser (Ctrl+F5)
echo ========================================
echo.
pause
