"""
Views for currency and other global features
"""
from django.http import JsonResponse
from django.views.decorators.http import require_POST
from django.views.decorators.csrf import csrf_exempt
import json


@require_POST
@csrf_exempt
def set_currency(request):
    """
    Set user's preferred currency in session
    """
    try:
        data = json.loads(request.body)
        currency_code = data.get('currency_code', 'USD')
        currency_symbol = data.get('currency_symbol', '$')
        
        # Save to session
        request.session['currency_code'] = currency_code
        request.session['currency_symbol'] = currency_symbol
        
        return JsonResponse({
            'status': 'success',
            'currency_code': currency_code,
            'currency_symbol': currency_symbol
        })
    except Exception as e:
        return JsonResponse({
            'status': 'error',
            'message': str(e)
        }, status=400)
