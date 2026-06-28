@echo off
color 0A
echo.
echo =============================================
echo   PUSH TO GITHUB NOW!
echo =============================================
echo.
echo Repository: https://github.com/Elias-MS/E-commerance-system
echo Files: 281 files ready (64,481 lines)
echo Status: Everything committed and ready
echo.
echo =============================================
echo   AUTHENTICATION REQUIRED
echo =============================================
echo.
echo When prompted:
echo   Username: Elias-MS
echo   Password: [Use your Personal Access Token]
echo.
echo Don't have a token? Create one:
echo   https://github.com/settings/tokens
echo   - Generate new token (classic)
echo   - Check: repo (full control)
echo   - Copy the token
echo.
echo =============================================
pause
echo.
echo Pushing to GitHub...
echo.

git push -u origin main

echo.
if %ERRORLEVEL% EQU 0 (
    echo =============================================
    echo   SUCCESS! PROJECT ON GITHUB!
    echo =============================================
    echo.
    echo View at: https://github.com/Elias-MS/E-commerance-system
    echo.
    start https://github.com/Elias-MS/E-commerance-system
) else (
    echo =============================================
    echo   AUTHENTICATION NEEDED
    echo =============================================
    echo.
    echo If you don't have a token, create one at:
    echo https://github.com/settings/tokens
    echo.
    echo Then run this file again
    echo.
)
echo.
pause
