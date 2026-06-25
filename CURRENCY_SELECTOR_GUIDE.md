# 💰 Currency Selector - User Guide

## 🌍 Available Currencies (12 Popular Currencies)

Your e-commerce store now supports 12 major world currencies!

### 📋 Currency List:

1. **🇺🇸 USD** - US Dollar ($)
2. **🇪🇺 EUR** - Euro (€)
3. **🇬🇧 GBP** - British Pound (£)
4. **🇪🇹 ETB** - Ethiopian Birr (Br)
5. **🇦🇪 AED** - UAE Dirham (د.إ)
6. **🇸🇦 SAR** - Saudi Riyal (﷼)
7. **🇰🇼 KWD** - Kuwaiti Dinar (د.ك)
8. **🇰🇪 KES** - Kenyan Shilling (KSh)
9. **💰 USDT** - Tether (₮)
10. **🇮🇳 INR** - Indian Rupee (₹)
11. **🇯🇵 JPY** - Japanese Yen (¥)
12. **🇨🇳 CNY** - Chinese Yuan (¥)

---

## 🎯 How to Use

### For Users:

1. **Click** on the green currency button in the top-right corner
2. **Select** your preferred currency from the dropdown
3. **Done!** The currency will be saved and remembered

### Features:

✅ **Persistent Selection** - Your choice is saved in browser
✅ **Visual Feedback** - Selected currency is highlighted
✅ **Notification** - Confirmation message when changed
✅ **Flag Icons** - Easy identification with country flags
✅ **Smooth Animation** - Beautiful hover effects

---

## 📍 Location

The currency selector is located in the **navigation bar**, top-right corner:
- Before the Profile dropdown (logged-in users)
- Before Login/Register (guests)

---

## 🎨 Design Features

- **Green Gradient Button** - Money-themed color
- **Dropdown Menu** - Clean, organized list
- **Country Flags** - Visual identification
- **Hover Effects** - Items slide on hover
- **Active State** - Current currency highlighted
- **Responsive** - Works on mobile devices

---

## 💡 Technical Details

### Current Implementation:

- **Display Only** - Shows selected currency code
- **LocalStorage** - Saves user preference
- **No Conversion** - Prices remain in base currency

### For Real Currency Conversion:

To implement actual price conversion, you would need:

1. **Currency API** - Get real-time exchange rates
   - Example: https://exchangerate-api.com/
   - Example: https://openexchangerates.org/

2. **Backend Integration** - Convert prices server-side

3. **Update Views** - Pass converted prices to templates

---

## 🔧 Customization

### To Add More Currencies:

Edit `templates/base.html` and add:

```html
<li><a class="dropdown-item currency-item" href="#" data-currency="CODE" data-symbol="SYMBOL">
    <span class="currency-flag">FLAG</span> CODE - Name (SYMBOL)
</a></li>
```

### Example - Adding Nigerian Naira:

```html
<li><a class="dropdown-item currency-item" href="#" data-currency="NGN" data-symbol="₦">
    <span class="currency-flag">🇳🇬</span> NGN - Nigerian Naira (₦)
</a></li>
```

---

## 🌐 Supported Regions

- **North America** - USD
- **Europe** - EUR, GBP
- **Middle East** - AED, SAR, KWD
- **Africa** - ETB, KES
- **Asia** - INR, JPY, CNY
- **Crypto** - USDT

---

## 📱 Mobile Responsive

The currency selector works perfectly on:
- ✅ Desktop computers
- ✅ Tablets
- ✅ Mobile phones
- ✅ All screen sizes

---

## 🎉 Benefits

1. **Global Reach** - Support customers worldwide
2. **User Friendly** - Easy currency selection
3. **Professional** - Modern, polished interface
4. **Flexible** - Easy to add more currencies
5. **Fast** - Instant currency switching

---

## 🔄 Future Enhancements

Possible improvements:

1. **Real-time Conversion** - Integrate currency API
2. **Auto-detection** - Detect user location
3. **Price Display** - Show converted prices
4. **Admin Control** - Enable/disable currencies
5. **Exchange Rates** - Display current rates

---

## ✅ Testing

Test the currency selector:

1. Go to: http://127.0.0.1:8000/
2. Click the green currency button
3. Select different currencies
4. See the notification
5. Refresh page - selection is saved!

---

## 💰 Popular Currency Symbols

- **$** - Dollar (USD)
- **€** - Euro (EUR)
- **£** - Pound (GBP)
- **Br** - Birr (ETB)
- **د.إ** - Dirham (AED)
- **﷼** - Riyal (SAR)
- **د.ك** - Dinar (KWD)
- **KSh** - Shilling (KES)
- **₮** - Tether (USDT)
- **₹** - Rupee (INR)
- **¥** - Yen/Yuan (JPY/CNY)

---

**Your store now supports 12 major world currencies!** 🌍💰
