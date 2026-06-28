@echo off
echo.
echo =============================================
echo   READY TO PUSH TO GITHUB!
echo =============================================
echo.
echo Repository: https://github.com/Elias-MS/E-commerance-system
echo Branch: main
echo Files ready: 281 files (64,481 lines of code)
echo.
echo =============================================
echo   IMPORTANT: When prompted for password
echo   Use your PERSONAL ACCESS TOKEN (not password)
echo =============================================
echo.
echo To create token:
echo 1. Go to: https://github.com/settings/tokens
echo 2. Generate new token (classic)
echo 3. Select 'repo' scope
echo 4. Copy the token
echo.
pause
echo.
echo Pushing to GitHub...
echo.

git push -u origin main

if errorlevel 1 (
    echo.
    echo ==========================================
    echo   PUSH FAILED!
    echo ==========================================
    echo.
    echo Common issues:
    echo - Wrong username/token
    echo - Repository doesn't exist
    echo - Need to use Personal Access Token
    echo.
    echo Try manual command:
    echo git push -u origin main
    echo.
) else (
    echo.
    echo ==========================================
    echo   SUCCESS! PROJECT PUSHED TO GITHUB!
    echo ==========================================
    echo.
    echo View your project at:
    echo https://github.com/Elias-MS/E-commerance-system
    echo.
    echo Next steps:
    echo 1. Add repository description on GitHub
    echo 2. Add topics: django, python, ecommerce
    echo 3. Enable GitHub Pages (optional)
    echo 4. Deploy to PythonAnywhere or Render
    echo.
)

pause
