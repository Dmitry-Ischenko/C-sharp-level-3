using MailSender.Models;
using MailSender.ViewModels.Base;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace MailSender.Data
{
    class ProgramData: ViewModel
    {
        private ObservableCollection<Sender> _SendersCollection;
        private ObservableCollection<Recipient> _RecipientsCollection;
        private ObservableCollection<Message> _MessagesCollection;

        private string _SendersCollectionPath;
        private string _RecipientsCollectionPath;
        private string _MessagesCollectionPath;

        public ObservableCollection<Sender> SendersCollection { 
            get=> _SendersCollection; 
            set=>Set(ref _SendersCollection,value); 
        }
        public ObservableCollection<Recipient> RecipientsCollection {
            get => _RecipientsCollection;
            set => Set(ref _RecipientsCollection, value);
        }
        public ObservableCollection<Message> MessagesCollection {
            get => _MessagesCollection;
            set => Set(ref _MessagesCollection, value);
        }

        

        public ProgramData ()
        {
            string _ProgramPath =  Environment.CurrentDirectory;
            _SendersCollectionPath = $"{_ProgramPath}\\SendersCollection.xml";
            _RecipientsCollectionPath = $"{_ProgramPath}\\RecipientsCollection.xml";
            _MessagesCollectionPath = $"{_ProgramPath}\\MessagesCollection.xml";
            LoadData();
            this.PropertyChanged += SaveToFile;
            
        }

        private void SaveToFile(object sender, PropertyChangedEventArgs e)
        {
            
            switch (e.PropertyName) {
                case nameof(SendersCollection):
                    {
                        SaveInFile(_SendersCollectionPath, SendersCollection);
                        break;
                    }
                case nameof(RecipientsCollection):
                    {
                        SaveInFile(_RecipientsCollectionPath, RecipientsCollection);
                        break;
                    }
                case nameof(MessagesCollection):
                    {
                        SaveInFile(_MessagesCollectionPath, MessagesCollection);
                        break;
                    }
            }

        }

        private void SaveInFile<T>(string path,T ObjectSerializer)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(T));
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    formatter.Serialize(fs, ObjectSerializer);
                    fs.Close();
                }
            }
            catch(Exception e) { Debug.WriteLine(e.ToString()); }
        }
        private void LoadFromFile<T>(string path, T ObjectSerializer)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(T));
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    
                    ObjectSerializer = (T)formatter.Deserialize(fs);
                    fs.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        private void LoadData()
        {
            if (File.Exists(_SendersCollectionPath))
            {
                LoadFromFile(_SendersCollectionPath, SendersCollection);
            } 
            else
            {
#if DEBUG
                SendersCollection = new ObservableCollection<Sender>()
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
#else
                SendersCollection = new ObservableCollection<Sender>();
#endif
            }
            if (File.Exists(_RecipientsCollectionPath))
            {
                LoadFromFile(_RecipientsCollectionPath, RecipientsCollection);
            }
            else
            {
#if DEBUG
                RecipientsCollection = new ObservableCollection<Recipient>()
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
#else
                RecipientsCollection = new ObservableCollection<Recipient>();
#endif
            }
            if (File.Exists(_MessagesCollectionPath))
            {
                LoadFromFile(_MessagesCollectionPath, MessagesCollection);
            }
            else
            {
#if DEBUG
                MessagesCollection = new ObservableCollection<Message>()
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
#else
                 MessagesCollection = new ObservableCollection<Message>();
#endif
            }
        }

    }
}
