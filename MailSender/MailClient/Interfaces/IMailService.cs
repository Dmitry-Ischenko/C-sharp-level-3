using MailClient.lib.Models;
using System;

namespace MailClient.lib.Interfaces
{
    public interface IMailService
    {
        IMailSender GetSender(string ServerAddress, 
            int ServerPort, 
            bool UseSSL, 
            string UserLogin, 
            string UserPassword, 
            string UserName = null, 
            bool SendMsg = true,
            Action<bool> action = null
            );
        IMailSender GetSenderAndNotify(string ServerAddress,
            int ServerPort,
            bool UseSSL,
            string UserLogin,
            string UserPassword,
            string UserName = null,
            bool SendMsg = true,
            Action<Recipient, bool> action = null
            );
    }

    public interface IMailSender
    {
        void Send(string RecipientAddress, string Subject, string Body);
        void SendThread(string RecipientAddress, string Subject, string Body);
        void SendAndNotify(Recipient recipient, Message message);
    }
}
