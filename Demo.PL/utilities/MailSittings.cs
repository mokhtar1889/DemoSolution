using System.Net;
using System.Net.Mail;

namespace Demo.PL.utilities
{
    public static class MailSittings
    {
        public static bool SendEmail(Email email) {

            try {
                var cleint = new SmtpClient("smtp.gmail.com", 587);
                cleint.EnableSsl = true;
                cleint.Credentials = new NetworkCredential("mohammedmokhtar272@gmail.com", "fyip rvkb pove pqct");
                cleint.Send("mohammedmokhtar272@gmail.com", email.To, email.Subject, email.Body);
                return true;

            } catch (Exception) {

                return false;
            }
            
        }
    }
}
