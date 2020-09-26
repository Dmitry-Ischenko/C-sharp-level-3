using MailSender.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MailSender.Data
{
    static class ProgramData
    {
        public static ObservableCollection<Sender> SendersCollection { get; set; }

        public static ObservableCollection<Recipient> RecipientsCollection { get; set; }

        public static ObservableCollection<Message> MessagesCollection { get; set; }

    }
}
