﻿using MailSender.Models;
using MailSender.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

        private Dictionary<Type, XmlSerializer> _SerializerDictionary = new Dictionary<Type, XmlSerializer>();


        public ObservableCollection<Sender> SendersCollection
        {
            get => _SendersCollection;
            set => Set(ref _SendersCollection, value);
        }
        public ObservableCollection<Recipient> RecipientsCollection
        {
            get => _RecipientsCollection;
            set => Set(ref _RecipientsCollection, value);
        }
        public ObservableCollection<Message> MessagesCollection
        {
            get => _MessagesCollection;
            set => Set(ref _MessagesCollection, value);
        } 


        public ProgramData ()
        {
            
            string _ProgramPath = Environment.CurrentDirectory;
            _SendersCollectionPath = $"{_ProgramPath}\\SendersCollection.xml";
            _RecipientsCollectionPath = $"{_ProgramPath}\\RecipientsCollection.xml";
            _MessagesCollectionPath = $"{_ProgramPath}\\MessagesCollection.xml";
            //System.Xml.Serialization.XmlSerializer formatter = new System.Xml.Serialization.XmlSerializer(typeof(List<Sender>));
            _SerializerDictionary.Add(typeof(ObservableCollection<Sender>), new XmlSerializer(typeof(ObservableCollection<Sender>)));
            _SerializerDictionary.Add(typeof(ObservableCollection<Recipient>), new XmlSerializer(typeof(ObservableCollection<Recipient>)));
            _SerializerDictionary.Add(typeof(ObservableCollection<Message>), new XmlSerializer(typeof(ObservableCollection<Message>)));
            //_SerializerDictionary.Add(typeof(Sender), new XmlSerializer(typeof(Sender)));
            //_SerializerDictionary.Add(typeof(Recipient), new XmlSerializer(typeof(Recipient)));
            //_SerializerDictionary.Add(typeof(Message), new XmlSerializer(typeof(Message)));
            LoadData();
            SendersCollection.CollectionChanged += SaveData2;

        }

        private void SaveData2(object sender, NotifyCollectionChangedEventArgs e)
        {

            var name = nameof(sender);
            switch (name)
            {
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

        private void SaveData(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine("WTF?");
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

            XmlSerializer formatter = _SerializerDictionary[typeof(T)];
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
        //private void LoadFromFile<T>(string path, ObservableCollection<T> ObjectSerializer,Type type)
        //{
        //    XmlSerializer formatter = _SerializerDictionary[type];
        //    try
        //    {
        //        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
        //        {
                    
        //            switch($"{type.GetType()}")
        //            {
        //                case "Sender":
        //                    {                               
        //                        foreach (var item in (Sender[])formatter.Deserialize(fs))
        //                        {
        //                            ObjectSerializer.Add(item);
        //                        }
        //                        break;
        //                    }
        //            }

        //            fs.Close();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e.ToString());
        //    }
        //}

        private void LoadData()
        {
            if (File.Exists(_SendersCollectionPath))
            {
                SendersCollection = new ObservableCollection<Sender>();
                XmlSerializer formatter = new XmlSerializer(typeof(Sender[]));
                try
                {
                    using (FileStream fs = new FileStream(_SendersCollectionPath, FileMode.OpenOrCreate))
                    {

                        foreach (var item in (Sender[])formatter.Deserialize(fs))
                        {
                            SendersCollection.Add(item);
                        }
                        fs.Close();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
                //LoadFromFile(_SendersCollectionPath, SendersCollection,typeof(Sender));
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
                RecipientsCollection = new ObservableCollection<Recipient>();
                XmlSerializer formatter = new XmlSerializer(typeof(Recipient[]));
                try
                {
                    using (FileStream fs = new FileStream(_RecipientsCollectionPath, FileMode.OpenOrCreate))
                    {

                        foreach (var item in (Recipient[])formatter.Deserialize(fs))
                        {
                            RecipientsCollection.Add(item);
                        }
                        fs.Close();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
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
                MessagesCollection = new ObservableCollection<Message>();
                XmlSerializer formatter = new XmlSerializer(typeof(Message[]));
                try
                {
                    using (FileStream fs = new FileStream(_MessagesCollectionPath, FileMode.OpenOrCreate))
                    {

                        foreach (var item in (Message[])formatter.Deserialize(fs))
                        {
                            MessagesCollection.Add(item);
                        }
                        fs.Close();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
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
