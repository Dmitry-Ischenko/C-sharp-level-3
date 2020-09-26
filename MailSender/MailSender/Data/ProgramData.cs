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

        public static ObservableCollection<Recipient> RecipientsCollection { get; set; } = new ObservableCollection<Recipient>()
        {
            new Recipient
            {
                 Active = true,
                  Address = "dmitry@yandex.ru",
                   Name = "Дмитрий Ищенко"
            },
            new Recipient
            {
                 Active = true,
                  Address = "v.zahudalov@yandex.ru",
                   Name = "Валера Захудалов"
            },
            new Recipient
            {
                 Active = true,
                  Address = "p.vasilev@yandex.ru",
                   Name = "Петя Васильев"
            }
        };

        public static ObservableCollection<Message> MessagesCollection { get; set; } = new ObservableCollection<Message>()
        {
            new Message
            {
                 Body = "Важное сообщение! в Челябинске осень!",
                  Subject = "Это важное сообщение"
            },
            new Message
            {
                 Body = "Тыр тыр тыр тыр",
                  Subject = "Это важное сообщение 2"
            },
            new Message
            {
                 Body = "Курлык мурлык",
                  Subject = "Это важное сообщение 3"
            }
        };

    }
}
