using MailClient.lib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailSender.Data
{
    class MailSenderDBInitializer
    {
        private readonly MailSenderDB _db;

        public MailSenderDBInitializer(MailSenderDB db) => _db = db;

        public void Initialize()
        {
            _db.Database.Migrate();
#if DEBUG
            InitializeMessage();
            InitializeRecipient();
            InitializeSender();
#endif
        }
        private void InitializeRecipient()
        {
            if (_db.Recipients.Any()) return;
            List<Recipient> recipients = Enumerable.Range(1, 10).Select(i => new Recipient
            {
                 Active = true,
                  Address = $"recipient_{i}@server.ru",
                    Name = $"Получатель {i}"
            }).ToList();
            _db.Recipients.AddRange(recipients);
            _db.SaveChanges();
        }
        private void InitializeMessage()
        {
            if (_db.Messages.Any()) return;
            List<Message> messages = Enumerable.Range(1, 3).Select(i => new Message
            {
                 Body = $"Сообщение {i}",
                  Subject = $"Заголовок {i}"
            }).ToList();
            _db.Messages.AddRange(messages);
            _db.SaveChanges();
        }
        private void InitializeSender()
        {
            if (_db.Senders.Any()) return;
            List<Sender> senders = Enumerable.Range(1, 3).Select(i => new Sender
            {
                Name = $"Отправитель {i}",
                 Password = $"Password{i}",
                  Address = $"Sender_{i}@server.ru",
                   Server = "server.ru",
                    Port = 24,
                     UseSSl = true
            }).ToList();
            _db.Senders.AddRange(senders);
            _db.SaveChanges();
        }
    }
}
