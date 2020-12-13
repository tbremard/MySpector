using MailKit.Net.Smtp;
using MimeKit;
using NLog;
using MailKit.Security;
using System;

namespace MySpector
{
    public class MailNotifier : INotifier
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        public void Notify(string mesg)
        {
            _log.Debug("Notification: " + mesg);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Thierry", "t.bremard@gmail.com"));
            message.To.Add(new MailboxAddress("Thierry", "t.bremard@gmail.com"));
            message.Subject = "MySpectorNotification: "+ mesg;
            message.Body = new TextPart("plain")
            {
                Text = @"Your notification trigered,
                        -- MySpector" + mesg
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp-relay.gmail.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate("xxx@gmail.com", "xxxxxxx");
                    client.Send(message);
                    client.Disconnect(true);
                }
                catch(Exception ex)
                {
                    _log.Error(ex);
                }
            }
        }
    }

}
