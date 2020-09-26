using MailSender.Models.Base;

namespace MailSender.Models
{
    class Recipient: ModelBase
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public bool Active { get; set; }
    }
}
