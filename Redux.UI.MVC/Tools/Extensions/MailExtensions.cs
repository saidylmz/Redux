using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Redux.UI.MVC.Tools
{
    public class MailExtensions
    {
        public static bool SendMail(string mail, string subject, string body)
        {
            MailMessage msg = new MailMessage("noreply@saityilmaz.com", mail, subject, body)
            {
                IsBodyHtml = true
            };
            msg.From = new MailAddress("noreply@saityilmaz.com", "REDUX");
            SmtpClient smtp = new SmtpClient("saityilmaz.com", 587)
            {
                EnableSsl = false
            };
            NetworkCredential cred = new NetworkCredential("noreply@saityilmaz.com", "Saidylmz34");
            smtp.Credentials = cred;
            try
            {
                smtp.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
        }
        public static bool SendNewPassword(string username, string mail, string npassword)
        {
            string subject = "REDUX.com Şifreniz Sıfırlandı";
            string body = string.Format("<!DOCTYPE html><html><body><h3>Selam, {0}</h3><p>Redux.com şifren sıfırlanmıştır.</p><label>Yeni şifren: <b>{1}</b></label><br/>Şifreni hesap ayarlarından istediğin zaman değiştirebilirsin.<br/><p> Teşekkürler! </p></body></html> ", username, npassword);

            return SendMail(mail, subject, body);
        }
    }
}