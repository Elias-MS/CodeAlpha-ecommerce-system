@echo off
echo.
echo ============================================
echo   CLEANING UP UNNECESSARY DOCUMENTATION
echo ============================================
echo.
echo Removing extra .md files from repository...
echo.

REM Remove all the extra documentation files
git rm --cached "ADMIN_*.md" 2>nul
git rm --cached "ALIBABA_*.md" 2>nul
git rm --cached "ALL_WORKING_LINKS.md" 2>nul
git rm --cached "AMAZING_*.md" 2>nul
git rm --cached "APPLY_*.md" 2>nul
git rm --cached "BACK_TO_*.md" 2>nul
git rm --cached "COLOR_*.md" 2>nul
git rm --cached "COMPLAINT_*.md" 2>nul
git rm --cached "COMPLETE_*.md" 2>nul
git rm --cached "CURRENCY_*.md" 2>nul
git rm --cached "CURRENT_*.md" 2>nul
git rm --cached "DATABASE_*.md" 2>nul
git rm --cached "DEPLOY_*.md" 2>nul
git rm --cached "DESIGN_*.md" 2>nul
git rm --cached "DOCUMENTATION_*.md" 2>nul
git rm --cached "EASY_*.md" 2>nul
git rm --cached "ENHANCED_*.md" 2>nul
git rm --cached "FINAL_*.md" 2>nul
git rm --cached "FIX_*.md" 2>nul
git rm --cached "HOW_TO_*.md" 2>nul
git rm --cached "IMAGES_*.md" 2>nul
git rm --cached "IMPORT_*.md" 2>nul
git rm --cached "INSTANT_*.md" 2>nul
git rm --cached "LATEST_*.md" 2>nul
git rm --cached "MANAGE_*.md" 2>nul
git rm --cached "MANUAL_*.md" 2>nul
git rm --cached "MIGRATION_*.md" 2>nul
git rm --cached "MODERN_*.md" 2>nul
git rm --cached "MULTI_LANGUAGE_*.md" 2>nul
git rm --cached "NAVIGATION_*.md" 2>nul
git rm --cached "NEW_DESIGN_*.md" 2>nul
git rm --cached "NEXT_STEPS_*.md" 2>nul
git rm --cached "NOTIFICATION_*.md" 2>nul
git rm --cached "PAYMENT_*.md" 2>nul
git rm --cached "PRODUCTS_*.md" 2>nul
git rm --cached "PRODUCT_*.md" 2>nul
git rm --cached "PROJECT_SUMMARY.md" 2>nul
git rm --cached "QUICKSTART.md" 2>nul
git rm --cached "QUICK_*.md" 2>nul
git rm --cached "READY_TO_USE.md" 2>nul
git rm --cached "RUN_THIS_*.md" 2>nul
git rm --cached "SCREENSHOTS.md" 2>nul
git rm --cached "SETUP_COMPLETE.md" 2>nul
git rm --cached "SIDEBAR_*.md" 2>nul
git rm --cached "SIMPLE_*.md" 2>nul
git rm --cached "SOLUTION_*.md" 2>nul
git rm --cached "START_HERE.md" 2>nul
git rm --cached "SYSTEM_*.md" 2>nul
git rm --cached "TASK_*.md" 2>nul
git rm --cached "TEST_*.md" 2>nul
git rm --cached "TYPEERROR_*.md" 2>nul
git rm --cached "UPDATES_*.md" 2>nul
git rm --cached "UPLOAD_*.md" 2>nul
git rm --cached "USER_*.md" 2>nul
git rm --cached "VIEW_*.md" 2>nul
git rm --cached "PUSH_TO_GITHUB.md" 2>nul
git rm --cached "SUCCESS_GITHUB_PUSH.md" 2>nul

REM Remove HTML files
git rm --cached "*.html" 2>nul

REM Remove extra txt files
git rm --cached "*.txt" 2>nul

REM Remove extra bat files
git rm --cached "FIX_*.bat" 2>nul
git rm --cached "OPEN_*.bat" 2>nul
git rm --cached "RUN_FIX.bat" 2>nul
git rm --cached "PUSH_NOW.bat" 2>nul
git rm --cached "PUSH_TO_GITHUB.bat" 2>nul
git rm --cached "FINAL_PUSH_COMMAND.bat" 2>nul

echo.
echo Committing cleanup...
git commit -m "Cleanup: Remove unnecessary documentation files"

echo.
echo Pushing to GitHub...
git push origin main

echo.
echo ============================================
echo   CLEANUP COMPLETE!
echo ============================================
echo.
echo Repository cleaned up successfully!
echo View at: https://github.com/Elias-MS/E-commerance-system
echo.
pause
