namespace MailClient.lib.Interfaces
{
    public interface IMailService
    {
        IMailSender GetSender(string ServerAddress, int ServerPort, bool UseSSL, string UserLogin, string UserPassword, string UserName = null, bool SendMsg = true);
    }

    public interface IMailSender
    {
        void Send(string RecipientAddress, string Subject, string Body);
    }
}
