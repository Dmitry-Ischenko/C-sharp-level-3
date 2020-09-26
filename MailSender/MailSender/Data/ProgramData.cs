using MailSender.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MailSender.Data
{
    static class ProgramData
    {
        public static ObservableCollection<Sender> SendersCollection { get; set; } = new ObservableCollection<Sender>()
        {
            new Sender
            {
                Name = "Валера",
                Address = "valera@gmail.ru",
                Password = "test",
                Port = 587,
                Server = "smtp.gmail.ru",
                UseSSl = true
            },
            new Sender
            {
                Name = "Дмитрий",
                Address = "dmitry@test.ru",
                Password = "test",
                Port = 456,
                Server = "smtp.yandex.ru",
                UseSSl = true
            }
        };

        public static ObservableCollection<Recipient> RecipientsCollection { get; set; }

        public static ObservableCollection<Message> MessagesCollection { get; set; }

    }
}
