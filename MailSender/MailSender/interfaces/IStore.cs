using MailClient.lib.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.interfaces
{
    public interface IStore<T> where T : ModelBase
    {
        IEnumerable<T> GetAll();

        T GetById(int Id);

        T Add(T Item);

        void Update(T Item);

        void Delete(int Id);
    }
}
