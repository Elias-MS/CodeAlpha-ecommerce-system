@echo off
echo.
echo ========================================
echo   PUSH TO GITHUB - AUTOMATED
echo ========================================
echo.

REM Check if git is configured
git config user.name >nul 2>&1
if errorlevel 1 (
    echo [!] Git not configured. Please configure first:
    echo.
    set /p USERNAME="Enter your name: "
    set /p EMAIL="Enter your email: "
    git config --global user.name "%USERNAME%"
    git config --global user.email "%EMAIL%"
    echo.
    echo [+] Git configured successfully!
    echo.
)

echo Current git user:
git config user.name
echo.

echo [1/5] Checking status...
git status
echo.

echo [2/5] Adding all files...
git add .
echo.

echo [3/5] Creating commit...
git commit -m "Initial commit: Complete Django e-commerce platform with full features"
echo.

echo [4/5] Ready to push!
echo.
echo IMPORTANT: You need to create a GitHub repository first!
echo.
echo 1. Go to https://github.com/new
echo 2. Repository name: simple-ecommerce-store
echo 3. Do NOT add README, .gitignore, or license
echo 4. Click Create repository
echo.
set /p GITHUB_USERNAME="Enter your GitHub username: "
set /p REPO_NAME="Enter repository name [simple-ecommerce-store]: "
if "%REPO_NAME%"=="" set REPO_NAME=simple-ecommerce-store

echo.
echo [5/5] Connecting to GitHub...
git remote remove origin 2>nul
git remote add origin https://github.com/%GITHUB_USERNAME%/%REPO_NAME%.git
git branch -M main

echo.
echo Pushing to GitHub...
echo You will be asked for password - use PERSONAL ACCESS TOKEN!
echo.
git push -u origin main

if errorlevel 1 (
    echo.
    echo [!] Push failed! Common issues:
    echo     - Wrong username/password
    echo     - Need Personal Access Token instead of password
    echo     - Repository doesn't exist on GitHub
    echo.
    echo To create token: GitHub → Settings → Developer settings → Personal access tokens
    echo.
) else (
    echo.
    echo ========================================
    echo   SUCCESS! Project pushed to GitHub!
    echo ========================================
    echo.
    echo View at: https://github.com/%GITHUB_USERNAME%/%REPO_NAME%
    echo.
)

pause
