using System;
using System.Windows;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Xml.Schema;
using System.Windows.Media;
using MailSender.Data;
using MailSender.Models;
using System.Linq;

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
                item.Password = PasswordBox.Password;
                item.Port = _port;
                item.Server = ServerBox.Text;
                item.UseSSl = SslBox.IsEnabled;
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
    }
}
