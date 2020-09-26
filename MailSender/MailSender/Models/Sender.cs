using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Models
{
    public class Sender
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Password { get; set; }

        public string Server { get; set; }

        public int Port { get; set; }
        
        public bool UseSSl { get; set; }
    }
}
