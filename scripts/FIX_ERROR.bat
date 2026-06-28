@echo off
echo ========================================
echo   FIXING DATABASE ERROR
echo ========================================
echo.

cd /d "%~dp0"

echo Running database fix...
C:\Users\User\AppData\Local\Python\bin\python.exe fix_database.py

echo.
echo ========================================
echo   DONE!
echo ========================================
echo.
echo Press any key to close...
pause > nul
