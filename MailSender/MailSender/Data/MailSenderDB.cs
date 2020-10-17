using MailClient.lib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Data
{
    class MailSenderDB: DbContext
    {
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public MailSenderDB(DbContextOptions<MailSenderDB> opt) : base(opt) { }
    }
}
