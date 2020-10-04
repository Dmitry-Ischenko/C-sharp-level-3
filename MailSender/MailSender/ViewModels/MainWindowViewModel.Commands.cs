﻿using MailSender.Infrastructure.Commands;
using MailSender.Models;
using MailSender.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;

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
                    sendMSG
                );
            mailsender.Send(
                SelectSenderSettings.Address,
                "Проверка почты",
                "Это тестовое сообщение, для проверки почты"
                );
        } 
#endregion
    }
}
