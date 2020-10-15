using MailSender.Infrastructure.Commands;
using MailSender.ViewModels.Base;
using MailSender.Views.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows;
using MailClient.lib.Models;

namespace MailSender.ViewModels
{
    partial class MainWindowViewModel : ViewModel
    {
        #region Комманда для удаления отправителей
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
        #endregion

        #region Комманда для сохранения изменений в отправителе
        private ICommand _SaveSenderCommand;

        public ICommand SaveSenderCommand => _SaveSenderCommand
            ??= new LambdaCommand(OnSaveSenderCommandExecuted, CanSaveSenderCommandExecute);

        private bool CanSaveSenderCommandExecute(object p) => true;

        private void OnSaveSenderCommandExecuted(object p)
        {
            var value = (object[])p;
            var Name = (string)value[0];
            var Address = (string)value[1];
            var Server = (string)value[2];
            var Password = (string)value[3];
            var UseSSl = (bool)value[5];
            SelectSenderSettings.Name = Name;
            SelectSenderSettings.Server = Server;
            if (Int32.TryParse((string)value[4], out int Port))
            {
                SelectSenderSettings.Port = Port;
            }
            SelectSenderSettings.UseSSl = UseSSl;
            SelectSenderSettings.Address = Address;
            if (!(Password is null))
            {
                if (Password.Length > 0)
                {
                    SelectSenderSettings.Password = Password;
                }
            }
        }
        #endregion

        #region Добавить пустого отправителя
        private ICommand _AddSenderCommand;

        public ICommand AddSenderCommand => _AddSenderCommand
            ??= new LambdaCommand(OnAddSenderCommandExecuted, CanAddSenderCommandExecute);

        private bool CanAddSenderCommandExecute(object p) => true;

        private void OnAddSenderCommandExecuted(object p)
        {
            var sender = new Sender();
            SenderCollection.Add(sender);
            SelectSenderSettings = sender;
        }
        #endregion

        #region Добавить пустое письмо
        private ICommand _AddMessageCommand;

        public ICommand AddMessageCommand => _AddMessageCommand
            ??= new LambdaCommand(OnAddMessageCommandExecuted, CanAddMessageCommandExecute);

        private bool CanAddMessageCommandExecute(object p) => true;

        private void OnAddMessageCommandExecuted(object p)
        {
            var message = new Message();
            MessageCollection.Add(message);
            SelectedMessageInMessadgeList = message;
        }
        #endregion

        #region Удалить письмо
        private ICommand _DeleteMessageCommand;

        public ICommand DeleteMessageCommand => _DeleteMessageCommand
            ??= new LambdaCommand(OnDeleteMessageCommandExecuted, CanDeleteMessageCommandExecute);

        private bool CanDeleteMessageCommandExecute(object p) => true;

        private void OnDeleteMessageCommandExecuted(object p)
        {
            MessageCollection.Remove(SelectedMessageInMessadgeList);
            SelectedMessageInMessadgeList = MessageCollection.FirstOrDefault();
        }
        #endregion

        #region Проверить настройки SMTP
        private ICommand _TestSMTPSettingsCommand;

        public ICommand TestSMTPSettingsCommand => _TestSMTPSettingsCommand
            ??= new LambdaCommand(OnTestSMTPSettingsCommandExecuted, CanTestSMTPSettingsCommandExecute);

        private bool CanTestSMTPSettingsCommandExecute(object p) => true;

        private void OnTestSMTPSettingsCommandExecuted(object p)
        {
#if DEBUG
            bool sendMSG = false;
#else
            bool sendMSG = true;
#endif

            var mailsender = _MailService.GetSender(
                    SelectSenderSettings.Server,
                    SelectSenderSettings.Port,
                    SelectSenderSettings.UseSSl,
                    SelectSenderSettings.Address,
                    SelectSenderSettings.Password,
                    SelectSenderSettings.Name,
                    sendMSG,
                    SendSuccess
                );
            //mailsender.
            mailsender.SendThread(
                SelectSenderSettings.Address,
                "Проверка почты",
                "Это тестовое сообщение, для проверки почты"
                );
        }
        #endregion

        private void SendSuccess(bool a)
        {
            if (a) MessageBox.Show("Тестовое сообщение отправленно");
            else MessageBox.Show("Все пропало!");
        }

        #region Отправляем почту
        private SendWindow _Window;
        private ICommand _SendVindowShowCommand;

        public ICommand SendVindowShowCommand => _SendVindowShowCommand
            ??= new LambdaCommand(OnSendVindowShowCommand, CanSendVindowShowCommand);

        private bool CanSendVindowShowCommand(object p)
        {
            if (SelectMessageSend is null) return false;
            if (SelectSenderSend is null) return false;
            if (RecipientCollection.Count == 0) return false;
            return true;
        }

        private void OnSendVindowShowCommand(object p)
        {

            foreach (var item in RecipientCollection)
            {
                if (item.Active == true)
                {
                    var copyitem = new Recipient()
                    {
                        Active = false,
                        Address = item.Address,
                        Name = item.Name
                    };
                    _recipients.Add(copyitem);
                }
            }
            var window = new SendWindow
            {
                Owner = Application.Current.MainWindow
            };
            _Window = window;
            window.Closed += OnWindowClosed;
            window.Loaded += OnWindowLoaded;
            window.ShowDialog();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
#if DEBUG
            var sendMSG = false;
#else
var sendMSG = true;
#endif
            var mailsender = _MailService.GetSenderAndNotify(
                    SelectSenderSend.Server,
                    SelectSenderSend.Port,
                    SelectSenderSend.UseSSl,
                    SelectSenderSend.Address,
                    SelectSenderSend.Password,
                    SelectSenderSend.Name,
                    sendMSG,
                    Notify
                );
            foreach (var item in Recipients)
            {
                mailsender.SendAndNotify(item, SelectMessageSend);
                //item.Active = true;
            }
        }

        private void Notify(Recipient arg1, bool arg2)
        {
            if (arg2)
            {
                arg1.Active = true;
            } 
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            ((Window)sender).Closed -= OnWindowClosed;
            _Window = null;
            _recipients.Clear();
        } 
#endregion
    }
}
