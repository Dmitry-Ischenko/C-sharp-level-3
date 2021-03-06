﻿using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace MailClient.lib
{
    public class MailSender
    {
        public string ServerAddress { get; set; }

        public int ServerPort { get; set; }

        public bool UseSSL { get; set; }

        public string UserLogin { get; set; }

        public string UserPassword { get; set; }

        //Для имитации отправки
        public bool SendMsg { get; set; } = true;

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
                message.Subject = Subject;
                message.Body = Body;
                using (var client = new SmtpClient(ServerAddress,ServerPort))
                {
                    client.EnableSsl = UseSSL;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential
                    {
                        UserName = UserLogin,
                        Password = UserPassword
                    };
                    try
                    { 
                        if (SendMsg)
                        {
                            client.Send(message);
                        } else
                        {
                            //Имитация отправки
                            Thread.Sleep(2000);
                        }                      
                        
                    }
                    catch (Exception e)
                    {
                        Trace.TraceError(e.ToString());
                        throw;
                    }
                }
            }
        }
    }
}
