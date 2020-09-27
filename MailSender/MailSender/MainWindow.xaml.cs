using System;
using System.Windows;
using MailSender.Data;
using MailSender.Models;
using System.Linq;
using System.Collections.Generic;

namespace MailSender
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

        private void OnDeleteSelectedSender(object sender, RoutedEventArgs e)
        {
            if (UserSelected.SelectedItem is Sender _sender)
            {
                int index = ProgramData.SendersCollection.IndexOf(_sender);
                ProgramData.SendersCollection.Remove(_sender);
                int count = ProgramData.SendersCollection.Count;
                if (index>= count) { 
                    index = count - 1; 
                }
                UserSelected.SelectedIndex = index;
            }
        }

        private void OnAddSenderInCollection(object sender, RoutedEventArgs e)
        {
            if (!Int32.TryParse(PortBox.Text, out int _port))
            {
                return;
            }
            try
            {
                var item = ProgramData.SendersCollection.Last(i => i.Address == AddressBox.Text);
                item.Name = NameBox.Text;
                item.Address = AddressBox.Text;
                if (PasswordBox.Password != null && PasswordBox.Password.Length >0)
                {
                    item.Password = PasswordBox.Password;
                }
                item.Port = _port;
                item.Server = ServerBox.Text;
                item.UseSSl = SslBox.IsEnabled;
                UserSelected.SelectedIndex = ProgramData.SendersCollection.IndexOf(item);
            }
            catch
            {
                Sender _sender = new Sender()
                {
                    Name = NameBox.Text,
                    Address = AddressBox.Text,
                    Password = PasswordBox.Password,
                    Port = _port,
                    Server = ServerBox.Text,
                    UseSSl = SslBox.IsEnabled
                };
                ProgramData.SendersCollection.Add(_sender);
                UserSelected.SelectedIndex = ProgramData.SendersCollection.Count - 1;
            }
            PasswordBox.Password = "";
        }

        private void OnDeleteSelectedMessage(object sender, RoutedEventArgs e)
        {
            if (MessagesList.SelectedItem is Message _message)
            {
                int index = MessagesList.SelectedIndex;
                ProgramData.MessagesCollection.Remove(_message);
                int count = ProgramData.MessagesCollection.Count;
                if (index >= count)
                {
                    index = count - 1;
                }
                MessagesList.SelectedIndex = index;
            }
        }

        private void OnAddMessage(object sender, RoutedEventArgs e)
        {
            ProgramData.MessagesCollection.Add(new Message());
            MessagesList.SelectedIndex = ProgramData.MessagesCollection.Count - 1;
        }

        private void OnTestMessage(object sender, RoutedEventArgs e)
        {
            MailClient.lib.MailSender mailSender = new MailClient.lib.MailSender();
            OnAddSenderInCollection(sender, e);
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

        private void onSendNow(object sender, RoutedEventArgs e)
        {

            List<Recipient> list = new List<Recipient>();
            foreach(var t in ProgramData.RecipientsCollection)
            {
                if (t.Active)
                {
                    list.Add(t);
                }                
            }
            SendWindow sendWindow = new SendWindow();
            sendWindow.RecipientList = list;
            sendWindow.SenderUr = TaskSenderSelected.SelectedItem as Sender;
            sendWindow.MessageUr = TaskMessageSelected.SelectedItem as Message;
            sendWindow.ShowDialog();
        }
    }
}
