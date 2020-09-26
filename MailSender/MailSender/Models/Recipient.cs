using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Models
{
    public class Recipient
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public bool Active { get; set; }
    }
}
