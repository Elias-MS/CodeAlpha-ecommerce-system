using System;
using System.Collections.Generic;
using System.Linq;
using E_commerance_System.Models;

namespace E_commerance_System.Services
{
    public class ShoppingCartService
    {
        private static List<CartItem> cartItems = new List<CartItem>();

        public static void AddToCart(Product product, int quantity = 1)
        {
            var existingItem = cartItems.FirstOrDefault(x => x.ProductId == product.ProductId);
            
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cartItems.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.Name,
                    Price = product.EffectivePrice,
                    Quantity = quantity,
                    Product = product
                });
            }
        }

        public static void RemoveFromCart(int productId)
        {
            cartItems.RemoveAll(x => x.ProductId == productId);
        }

        public static void UpdateQuantity(int productId, int quantity)
        {
            var item = cartItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                if (quantity <= 0)
                {
                    RemoveFromCart(productId);
                }
                else
                {
                    item.Quantity = quantity;
                }
            }
        }

        public static List<CartItem> GetCartItems()
        {
            return cartItems.ToList();
        }

        public static decimal GetCartTotal()
        {
            return cartItems.Sum(x => x.EffectiveSubtotal);
        }

        public static decimal GetCartSubtotal()
        {
            return cartItems.Sum(x => x.Subtotal);
        }

        public static decimal GetCartDiscount()
        {
            return cartItems.Sum(x => x.DiscountAmount);
        }

        public static int GetCartItemCount()
        {
            return cartItems.Sum(x => x.Quantity);
        }

        public static void ClearCart()
        {
            cartItems.Clear();
        }

        public static bool IsProductInCart(int productId)
        {
            return cartItems.Any(x => x.ProductId == productId);
        }

        public static CartItem GetCartItem(int productId)
        {
            return cartItems.FirstOrDefault(x => x.ProductId == productId);
        }

        public static bool HasItems()
        {
            return cartItems.Count > 0;
        }

        public static Order CreateOrderFromCart(int userId, string shippingAddress, string billingAddress = null, string paymentMethod = "Cash on Delivery")
        {
            if (!HasItems())
                return null;

            var order = new Order
            {
                UserId = userId,
                SubTotal = GetCartSubtotal(),
                DiscountAmount = GetCartDiscount(),
                TaxAmount = 0, // Can be calculated based on business rules
                ShippingCost = 0, // Can be calculated based on location/weight
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress ?? shippingAddress,
                PaymentMethod = paymentMethod,
                OrderDetails = new List<OrderDetail>()
            };

            foreach (var cartItem in cartItems)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = cartItem.ProductId,
                    ProductName = cartItem.ProductName,
                    ProductCode = cartItem.Product?.ProductCode,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Price,
                    DiscountAmount = cartItem.HasDiscount ? (cartItem.Price - cartItem.EffectivePrice) : 0,
                    TotalPrice = cartItem.EffectiveSubtotal
                });
            }

            // Calculate total amount
            order.TotalAmount = order.SubTotal + order.TaxAmount + order.ShippingCost - order.DiscountAmount;

            return order;
        }

        public static bool ValidateCartStock()
        {
            foreach (var item in cartItems)
            {
                if (!item.IsQuantityValid)
                {
                    return false;
                }
            }
            return true;
        }

        public static List<CartItem> GetInvalidStockItems()
        {
            return cartItems.Where(x => !x.IsQuantityValid).ToList();
        }

        public static void UpdateCartItemPrices()
        {
            // Update prices from database in case of price changes
            foreach (var item in cartItems)
            {
                var product = ProductService.GetProductById(item.ProductId);
                if (product != null)
                {
                    item.Price = product.EffectivePrice;
                    item.Product = product;
                }
            }
        }
    }
}