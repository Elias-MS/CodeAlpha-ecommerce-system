using System;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace E_commerance_System.Services
{
    public class EmailService
    {
        public static bool SendVerificationCode(string toEmail, string code)
        {
            try
            {
                string senderEmail = ConfigurationManager.AppSettings["SmtpEmail"];
                string senderPassword = ConfigurationManager.AppSettings["SmtpPassword"];

                if (string.IsNullOrEmpty(senderEmail) || string.IsNullOrEmpty(senderPassword) || senderEmail == "YOUR_EMAIL@gmail.com")
                {
                    // Configuration not set, cannot send email
                    return false;
                }

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true,
                };

                string subject = "Your E-Commerce Speed Verification Code";
                string body = $@"
                    <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #e0e0e0; border-radius: 5px; overflow: hidden;'>
                        <div style='background-color: #1a237e; color: white; padding: 20px; text-align: center;'>
                            <h2 style='margin: 0;'>E-Commerce Speed Security</h2>
                        </div>
                        <div style='padding: 30px; background-color: #f9f9f9;'>
                            <p style='font-size: 16px; color: #333;'>Hello,</p>
                            <p style='font-size: 16px; color: #333;'>You have requested to reset your password. Please use the verification code below to complete the process:</p>
                            <div style='text-align: center; margin: 30px 0;'>
                                <span style='display: inline-block; font-size: 32px; font-weight: bold; letter-spacing: 5px; color: #00897b; background-color: white; padding: 15px 30px; border: 2px dashed #00897b; border-radius: 8px;'>{code}</span>
                            </div>
                            <p style='font-size: 14px; color: #666;'>If you did not request this, please ignore this email or contact support immediately.</p>
                        </div>
                        <div style='background-color: #eeeeee; padding: 15px; text-align: center; font-size: 12px; color: #999;'>
                            &copy; {DateTime.Now.Year} E-Commerce Speed. All rights reserved.
                        </div>
                    </div>";

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail, "E-Commerce Speed Security"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(toEmail);

                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception)
            {
                // In a real app, log the exception (e.g., auth failure, network issue)
                return false;
            }
        }
    }
}
