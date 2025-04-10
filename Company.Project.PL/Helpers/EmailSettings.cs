using System.Net;
using System.Net.Mail;

namespace Company.Project.PL.Helpers
{
    public static class EmailSettings
    {
        public static bool SendEmail(Email email)
        {
            // Mail Server : Gmail
            // Protocol : SMTP

            try 
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;      // HTTPS

                //Do not forget to remove spaces from password
                client.Credentials = new NetworkCredential("sohaeid341@gmail.com", "");   // Sender , Gmail Password for this WebApp
                
                client.Send("sohaeid341@gmail.com", email.To, email.Subject, email.Body);

                return true;
            }
            catch (Exception e) 
            {
                return false;
            }
        }
    }
}
