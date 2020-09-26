using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace MailClient.lib
{
    public class MailSender
    {
        public string ServerAddress { get; set; }

        public int ServerPort { get; set; }

        public bool UseSSL { get; set; }

        public string UserLogin { get; set; }

        public string UserPassword { get; set; }

        public void SendMessage(string SenderAddress, string RecipientAddress, string Subject, string Body, string SenderName = null)
        {
            MailAddress from;
            if (SenderName == null)
            {
                from = new MailAddress(SenderAddress);
            } else
            {
                from = new MailAddress(SenderAddress, SenderName);
            }
            var to = new MailAddress(RecipientAddress); 
            using (var message = new MailMessage(from,to)) { 
                using (var client = new SmtpClient(ServerAddress,ServerPort))
                {
                    client.EnableSsl = UseSSL;

                    client.Credentials = new NetworkCredential
                    {
                        UserName = UserLogin,
                        Password = UserPassword
                    };

                    try
                    {
                        client.Send(message);
                    }
                    catch (SmtpException e)
                    {
                        Trace.TraceError(e.ToString());
                        throw;
                    }
                }
            }
        }

    }
}
