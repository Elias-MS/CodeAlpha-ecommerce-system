"""
Context processors for currency and localization
"""

def currency(request):
    """
    Add currency information to all templates
    """
    # Get currency from session or use default
    currency_code = request.session.get('currency_code', 'USD')
    currency_symbol = request.session.get('currency_symbol', '$')
    
    return {
        'CURRENCY_CODE': currency_code,
        'CURRENCY_SYMBOL': currency_symbol,
    }
