using MailKit.Net.Smtp;
using MimeKit;
using NLog;
using MailKit.Security;
using System;

namespace MySpector.Models
{
    //securesmtp.t-online.de
    public class MailNotifier : Notify
    {
        //        static Logger _log = LogManager.GetCurrentClassLogger();
        protected override bool NotifySingle(string msg)
        {
            bool ret = false;
            _log.Debug($"Preparing to mail: '{msg}'");
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Thierry", "t.bremard@gmail.com"));
            message.To.Add(new MailboxAddress("Thierry", "t.bremard@gmail.com"));
            message.Subject = "MySpectorNotification: " + msg;
            message.Body = new TextPart("plain")
            {
                Text = @"Your notification trigered,
                        -- MySpector" + msg
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp-relay.gmail.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate("xxx@gmail.com", "xxxxxxx");
                    _log.Debug("client.IsConnected: " + client.IsConnected);
                    _log.Debug("client.IsAuthenticated: " + client.IsAuthenticated);
                    _log.Debug("client.IsSecure: " + client.IsSecure);
                    client.Send(message);
                    client.Disconnect(true);
                    ret = true;
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
            }
            return ret;
        }
    }
}
