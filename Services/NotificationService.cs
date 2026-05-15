using System;
using System.Data.SqlClient;
using E_commerance_System.Data;

namespace E_commerance_System.Services
{
    public class NotificationService
    {
        public static bool SendNotification(int? userId, string type, string message)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO Notifications (UserId, Type, Message) VALUES (@uid, @type, @msg)";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@uid", (object)userId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@type", type ?? "General");
                        cmd.Parameters.AddWithValue("@msg", message);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch { return false; }
        }

        public static bool SendReportToAdmin(int? userId, string type, string message)
        {
            return SendNotification(null, type, $"Report from User ID {userId}: {message}");
        }
    }
}
