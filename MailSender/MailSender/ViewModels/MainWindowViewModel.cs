using MailClient.lib.Interfaces;
using MailClient.lib.Models;
using MailSender.Data;
using MailSender.Infrastructure.Commands;
using MailSender.interfaces;
using MailSender.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MailSender.ViewModels
{
    partial class MainWindowViewModel: ViewModel
    {
        private ObservableCollection<Sender> _SenderCollection;
        private ObservableCollection<Recipient> _RecipientCollection;
        private ObservableCollection<Message> _MessageCollection;

        public ObservableCollection<Sender> SenderCollection
        {
            get => _SenderCollection;
            set => Set(ref _SenderCollection, value);
        }
        public ObservableCollection<Recipient> RecipientCollection
        {
            get => _RecipientCollection;
            set => Set(ref _RecipientCollection, value);
        }
        public ObservableCollection<Message> MessageCollection
        {
            get => _MessageCollection;
            set => Set(ref _MessageCollection, value);
        }

        #region Выбранный отправитель в окне настроек
        private Sender _SelectSenderSettings;

        public Sender SelectSenderSettings
        {
            get => _SelectSenderSettings;
            set => Set(ref _SelectSenderSettings, value);
        }
        #endregion

        #region Выбранное письмо в окне редактирования письм
        private Message _SelectedMessageInMessadgeList;

        public Message SelectedMessageInMessadgeList
        {
            get => _SelectedMessageInMessadgeList;
            set => Set(ref _SelectedMessageInMessadgeList, value);
        }
        #endregion

        #region Выбранные комбобокс на странице отпрваки
        private Sender _SelectSenderSend;

        public Sender SelectSenderSend
        {
            get => _SelectSenderSend;
            set => Set(ref _SelectSenderSend, value);
        }

        private Message _SelectMessageSend;

        public Message SelectMessageSend
        {
            get => _SelectMessageSend;
            set => Set(ref _SelectMessageSend, value);
        }

        #endregion
        private readonly IMailService _MailService;

        

        public MainWindowViewModel(IMailService MailService,
            IStore<Recipient> RecipientStore,
            IStore<Message> MessageStore,
            IStore<Sender> SenderStore)
        {
            _MailService = MailService;
            SenderCollection = new ObservableCollection<Sender>(SenderStore.GetAll());
            RecipientCollection = new ObservableCollection<Recipient>(RecipientStore.GetAll());
            MessageCollection = new ObservableCollection<Message>(MessageStore.GetAll());
            if (SenderCollection.Count > 0) SelectSenderSettings = SenderCollection[0];
            SelectMessageSend = MessageCollection.FirstOrDefault();
            SelectSenderSend = SenderCollection.FirstOrDefault();
            //MessageBox.Show($"{ProgramData.GetCountInstances}");
        }

        private ObservableCollection<Recipient> _recipients = new ObservableCollection<Recipient>();
        public ObservableCollection<Recipient> Recipients
        {
            get => _recipients;
            set => Set(ref _recipients, value);
        }
    }
}
