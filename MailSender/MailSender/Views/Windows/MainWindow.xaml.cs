using System;
using System.Windows;
using MailSender.Data;
using MailSender.Models;
using System.Linq;
using System.Collections.Generic;

namespace MailSender.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnTestMessage(object sender, RoutedEventArgs e)
        {
            MailClient.lib.MailSender mailSender = new MailClient.lib.MailSender();
            if (UserSelected.SelectedItem is Sender _sender)
            {
                mailSender.ServerAddress = _sender.Server;
                mailSender.ServerPort = _sender.Port;
                mailSender.UserLogin = _sender.Address;
                mailSender.UserPassword = _sender.Password;
                mailSender.UseSSL = _sender.UseSSl;
                try
                {
                    mailSender.SendMessage(_sender.Address, _sender.Address, "Проверка настроек", "Если вы получили это сообщение, то все ок");
                    MessageBox.Show($"Тестовое сообщение отправленно на адрес {_sender.Address}. Проверьте, получили ли вы его.");

                }
                catch (Exception error)
                {
                    MessageBox.Show(error.ToString());
                }
            }
        }

    }
}
