using Shop.DAL.Entites.Helper;
using System.Net;
using System.Net.Mail;

namespace Shop.PL.Helper
{
    public static class EmailSetting
    {
       public static void SendEmail(Email email)
        {
            var Client = new SmtpClient(" smtp.gmail.com", 587);
            Client.EnableSsl=true;
            Client.Credentials = new NetworkCredential("hemaabokhalil677@gmail.com", "dcevghkstaxytjfx");
            Client.Send("hemaabokhalil677@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
