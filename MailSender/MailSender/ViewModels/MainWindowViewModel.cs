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
    class MainWindowViewModel: ViewModel
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

        private Sender _SelectSenderSettings;

        public Sender SelectSenderSettings
        {
            get => _SelectSenderSettings;
            set => Set(ref _SelectSenderSettings, value);
        }

        private Message _SelectedMessageInMessadgeList;

        public Message SelectedMessageInMessadgeList
        {
            get => _SelectedMessageInMessadgeList;
            set => Set(ref _SelectedMessageInMessadgeList, value);
        }

        private ICommand _DeleteSenderCommand;

        public ICommand DeleteSenderCommand => _DeleteSenderCommand
            ??= new LambdaCommand(OnDeleteSenderCommandExecuted, CanDeleteSenderCommandExecute);

        private bool CanDeleteSenderCommandExecute(object p) => p is Sender || SelectSenderSettings != null;

        private void OnDeleteSenderCommandExecuted(object p)
        {
            var sender = p as Sender ?? SelectSenderSettings;
            if (sender is null) return;

            SenderCollection.Remove(sender);
            SelectSenderSettings = SenderCollection.FirstOrDefault();
        }

        public MainWindowViewModel(ProgramData _ProgramData)
        {
            SenderCollection = _ProgramData.SendersCollection;
            RecipientCollection = _ProgramData.RecipientsCollection;
            MessageCollection = _ProgramData.MessagesCollection;
            if (SenderCollection.Count > 0) SelectSenderSettings = SenderCollection[0];
            //MessageBox.Show($"{ProgramData.GetCountInstances}");
        }
    }
}
