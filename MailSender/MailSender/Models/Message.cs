using MailSender.Models.Base;

namespace MailSender.Models
{
    class Message: ModelBase
    {
        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
