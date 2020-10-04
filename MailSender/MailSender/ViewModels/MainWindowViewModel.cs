using MailClient.lib.Interfaces;
using MailSender.Data;
using MailSender.Infrastructure.Commands;
using MailSender.Models;
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

        private readonly IMailService _MailService;

        public MainWindowViewModel(ProgramData _ProgramData,IMailService MailService)
        {
            _MailService = MailService;
            SenderCollection = _ProgramData.SendersCollection;
            RecipientCollection = _ProgramData.RecipientsCollection;
            MessageCollection = _ProgramData.MessagesCollection;
            if (SenderCollection.Count > 0) SelectSenderSettings = SenderCollection[0];
            //MessageBox.Show($"{ProgramData.GetCountInstances}");
        }

        private List<Recipient> _recipients = new List<Recipient>();
        public List<Recipient> Recipients
        {
            get => _recipients;
            set => Set(ref _recipients, value);
        }
    }
}
