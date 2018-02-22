using System;
using System.Net.Mail;

namespace SchoolToHomeBehaviorTracking_Server
{
    //class to send emails
    public class SendEmail
    { 
        public bool Send(string email, string accessCode)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("schooltohome.behaviortracking@gmail.com", "School-To-Home");
                mail.To.Add(email);
                mail.Subject = "School-To-Home Behavior Tracking Password Reset";
                mail.Body = "Use access code: " + accessCode + " to reset password";

                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Send Email");
                return false;
            }
        }
    }
}
