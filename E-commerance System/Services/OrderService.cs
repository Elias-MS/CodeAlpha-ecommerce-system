using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using E_commerance_System.Data;
using E_commerance_System.Models;

namespace E_commerance_System.Services
{
    public class OrderService
    {
        public static bool CreateOrder(Order order)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string orderQuery = @"
                            INSERT INTO Orders (OrderNumber, UserId, SubTotal, TaxAmount, ShippingCost, DiscountAmount, TotalAmount, PaymentMethod, PaymentReference, Phone, EstimatedDelivery, ShippingAddress, BillingAddress, CurrencyCode, PaymentProofImage) 
                            OUTPUT INSERTED.OrderId
                            VALUES (@orderNumber, @userId, @subTotal, @taxAmount, @shippingCost, @discountAmount, @totalAmount, @paymentMethod, @paymentRef, @phone, @estDelivery, @shippingAddress, @billingAddress, @currencyCode, @proof)";
                        
                        int orderId;
                        using (var cmd = new SqlCommand(orderQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@orderNumber", order.OrderNumber);
                            cmd.Parameters.AddWithValue("@userId", order.UserId);
                            cmd.Parameters.AddWithValue("@subTotal", order.SubTotal);
                            cmd.Parameters.AddWithValue("@taxAmount", order.TaxAmount);
                            cmd.Parameters.AddWithValue("@shippingCost", order.ShippingCost);
                            cmd.Parameters.AddWithValue("@discountAmount", order.DiscountAmount);
                            cmd.Parameters.AddWithValue("@totalAmount", order.TotalAmount);
                            cmd.Parameters.AddWithValue("@paymentMethod", order.PaymentMethod ?? "");
                            cmd.Parameters.AddWithValue("@paymentRef", order.PaymentReference ?? "");
                            cmd.Parameters.AddWithValue("@phone", order.Phone ?? "");
                            cmd.Parameters.AddWithValue("@estDelivery", (object)order.EstimatedDelivery ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@shippingAddress", order.ShippingAddress ?? "");
                            cmd.Parameters.AddWithValue("@billingAddress", order.BillingAddress ?? "");
                            cmd.Parameters.AddWithValue("@currencyCode", order.CurrencyCode ?? "ETB");
                            cmd.Parameters.AddWithValue("@proof", order.PaymentProofImage ?? "");

                            orderId = (int)cmd.ExecuteScalar();
                        }

                        foreach (var item in order.OrderDetails)
                        {
                            string itemQuery = @"
                                INSERT INTO OrderDetails (OrderId, ProductId, ProductName, ProductCode, Quantity, UnitPrice, DiscountAmount, TotalPrice) 
                                VALUES (@orderId, @productId, @productName, @productCode, @quantity, @unitPrice, @discountAmount, @totalPrice)";
                            
                            using (var cmd = new SqlCommand(itemQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@orderId", orderId);
                                cmd.Parameters.AddWithValue("@productId", item.ProductId);
                                cmd.Parameters.AddWithValue("@productName", item.ProductName);
                                cmd.Parameters.AddWithValue("@productCode", item.ProductCode ?? "");
                                cmd.Parameters.AddWithValue("@quantity", item.Quantity);
                                cmd.Parameters.AddWithValue("@unitPrice", item.UnitPrice);
                                cmd.Parameters.AddWithValue("@discountAmount", item.DiscountAmount);
                                cmd.Parameters.AddWithValue("@totalPrice", item.TotalPrice);
                                cmd.ExecuteNonQuery();
                            }

                            // Verify enough stock exists before the DB trigger handles reduction
                            string checkStockQuery = "SELECT Stock FROM Products WHERE ProductId = @productId";
                            using (var cmd = new SqlCommand(checkStockQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@productId", item.ProductId);
                                int currentStock = (int)(cmd.ExecuteScalar() ?? 0);
                                if (currentStock < item.Quantity)
                                {
                                    throw new Exception($"Insufficient stock for product: {item.ProductName}. Available: {currentStock}");
                                }
                            }
                            // Note: Stock reduction is handled automatically by the DB Trigger 'TR_OrderDetails_UpdateInventory'
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        private static Order MapOrderFromReader(SqlDataReader reader)
        {
            return new Order
            {
                OrderId = Convert.ToInt32(reader["OrderId"]),
                OrderNumber = reader["OrderNumber"].ToString(),
                UserId = Convert.ToInt32(reader["UserId"]),
                OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                Status = reader["Status"].ToString(),
                SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                TaxAmount = Convert.ToDecimal(reader["TaxAmount"]),
                ShippingCost = Convert.ToDecimal(reader["ShippingCost"]),
                DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]),
                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                PaymentMethod = reader["PaymentMethod"]?.ToString(),
                PaymentStatus = reader["PaymentStatus"]?.ToString(),
                ShippingAddress = reader["ShippingAddress"]?.ToString(),
                BillingAddress = reader["BillingAddress"]?.ToString(),
                Phone = reader["Phone"]?.ToString(),
                PaymentReference = reader["PaymentReference"]?.ToString(),
                CurrencyCode = reader["CurrencyCode"]?.ToString() ?? "ETB",
                PaymentProofImage = reader["PaymentProofImage"]?.ToString(),
                EstimatedDelivery = reader["EstimatedDelivery"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["EstimatedDelivery"]) : null
            };
        }

        public static List<Order> GetOrdersByUserId(int userId)
        {
            var orders = new List<Order>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Orders WHERE UserId = @userId ORDER BY OrderDate DESC";
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) orders.Add(MapOrderFromReader(reader));
                    }
                }
            }
            return orders;
        }

        public static List<Order> GetAllOrders()
        {
            var orders = new List<Order>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Orders ORDER BY OrderDate DESC";
                using (var cmd = new SqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) orders.Add(MapOrderFromReader(reader));
                }
            }
            return orders;
        }

        public static Order GetOrderById(int orderId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Orders WHERE OrderId = @orderId";
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var order = MapOrderFromReader(reader);
                            reader.Close();
                            order.OrderDetails = GetOrderDetails(orderId, connection);
                            return order;
                        }
                    }
                }
            }
            return null;
        }

        private static List<OrderDetail> GetOrderDetails(int orderId, SqlConnection connection)
        {
            var details = new List<OrderDetail>();
            string query = "SELECT * FROM OrderDetails WHERE OrderId = @orderId";
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@orderId", orderId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        details.Add(new OrderDetail {
                            OrderDetailId = Convert.ToInt32(reader["OrderDetailId"]),
                            OrderId = Convert.ToInt32(reader["OrderId"]),
                            ProductId = Convert.ToInt32(reader["ProductId"]),
                            ProductName = reader["ProductName"].ToString(),
                            ProductCode = reader["ProductCode"]?.ToString(),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                            DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]),
                            TotalPrice = Convert.ToDecimal(reader["TotalPrice"])
                        });
                    }
                }
            }
            return details;
        }

        public static List<OrderHistory> GetOrderHistory(int orderId)
        {
            var history = new List<OrderHistory>();
            using (var conn = DatabaseHelper.GetConnection()) {
                conn.Open();
                string query = "SELECT * FROM OrderHistory WHERE OrderId = @orderId ORDER BY UpdateDate DESC";
                using (var cmd = new SqlCommand(query, conn)) {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    using (var reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            history.Add(new OrderHistory {
                                HistoryId = Convert.ToInt32(reader["HistoryId"]),
                                Status = reader["Status"]?.ToString(),
                                UpdateNotes = reader["UpdateNotes"]?.ToString(),
                                UpdateDate = Convert.ToDateTime(reader["UpdateDate"])
                            });
                        }
                    }
                }
            }
            return history;
        }

        public static void LogOrderHistory(int orderId, string status, string paymentStatus, string notes, string updatedBy)
        {
            try {
                using (var conn = DatabaseHelper.GetConnection()) {
                    conn.Open();
                    string query = "INSERT INTO OrderHistory (OrderId, Status, PaymentStatus, UpdateNotes, UpdatedBy) VALUES (@orderId, @status, @payStatus, @notes, @updatedBy)";
                    using (var cmd = new SqlCommand(query, conn)) {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        cmd.Parameters.AddWithValue("@status", (object)status ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@payStatus", (object)paymentStatus ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@notes", (object)notes ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@updatedBy", updatedBy ?? "System");
                        cmd.ExecuteNonQuery();
                    }
                }
            } catch { }
        }

        public static bool UpdateOrderStatus(int orderId, string status)
        {
            try {
                using (var connection = DatabaseHelper.GetConnection()) {
                    connection.Open();
                    
                    // 1. Get User ID and Order Number for notification
                    int userId = 0;
                    string orderNum = "";
                    string getInfoSql = "SELECT UserId, OrderNumber FROM Orders WHERE OrderId = @id";
                    using (var cmdInfo = new SqlCommand(getInfoSql, connection))
                    {
                        cmdInfo.Parameters.AddWithValue("@id", orderId);
                        using (var reader = cmdInfo.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userId = Convert.ToInt32(reader["UserId"]);
                                orderNum = reader["OrderNumber"].ToString();
                            }
                        }
                    }

                    // 2. Update Order
                    string query = "UPDATE Orders SET Status = @status";
                    if (status == "Delivered")
                        query += ", PaymentStatus = 'Paid'";
                    query += " WHERE OrderId = @orderId";

                    using (var cmd = new SqlCommand(query, connection)) {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        cmd.Parameters.AddWithValue("@status", status);
                        bool success = cmd.ExecuteNonQuery() > 0;
                        if (success) 
                        {
                            LogOrderHistory(orderId, status, (status == "Delivered" ? "Paid" : null), "Status updated", "Admin");
                            
                            // 3. Send Notification to User
                            if (userId > 0)
                            {
                                string msg = $"Your Order #{orderNum} status has been updated to: {status}.";
                                NotificationService.SendNotification(userId, "Order Update", msg);
                            }
                        }
                        return success;
                    }
                }
            } catch { return false; }
        }

        public static bool ClearOrderHistory(int orderId)
        {
            try {
                using (var conn = DatabaseHelper.GetConnection()) {
                    conn.Open();
                    string query = "DELETE FROM OrderHistory WHERE OrderId = @orderId";
                    using (var cmd = new SqlCommand(query, conn)) {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        return cmd.ExecuteNonQuery() >= 0;
                    }
                }
            } catch { return false; }
        }

        public static bool DeleteOrder(int orderId)
        {
            using (var conn = DatabaseHelper.GetConnection()) {
                conn.Open();
                using (var trans = conn.BeginTransaction()) {
                    try {
                        ExecuteNonQuery(conn, "DELETE FROM OrderHistory WHERE OrderId = @orderId", trans, orderId);
                        ExecuteNonQuery(conn, "DELETE FROM OrderDetails WHERE OrderId = @orderId", trans, orderId);
                        ExecuteNonQuery(conn, "DELETE FROM Orders WHERE OrderId = @orderId", trans, orderId);
                        trans.Commit();
                        return true;
                    } catch { trans.Rollback(); return false; }
                }
            }
        }

        private static void ExecuteNonQuery(SqlConnection conn, string sql, SqlTransaction trans, int orderId)
        {
            using (var cmd = new SqlCommand(sql, conn, trans)) {
                cmd.Parameters.AddWithValue("@orderId", orderId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}