using System;
using System.Windows;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Xml.Schema;
using System.Windows.Media;

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
        private void StatusBarSendMessage(string StrginMessage, Brush color)
        {
            StatusBarMessage.Text = StrginMessage;
            StatusBarMessage.Foreground = color;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Server.Text.Length == 0)
            {
                StatusBarSendMessage("Не указан адрес сервера SMTP", Brushes.Red);
                return;
            }
            if (Login.Text.Length == 0)
            {
                StatusBarSendMessage("Не указан логин", Brushes.Red);
                return;
            }
            if (port.Text.Length == 0)
            {                
                StatusBarSendMessage("Не указан порт SMTP Сервера", Brushes.Red);
                return;
            }
            if (SendTo.Text.Length == 0)
            {
                StatusBarSendMessage("Не указано имя получателя сообщения", Brushes.Red);
                return;
            }
            int _port;
            if (!Int32.TryParse(port.Text, out _port))
            {
                StatusBarSendMessage("Не верно указан порт SMTP сервера", Brushes.Red);
                return;
            }
            var client = new SmtpClient(Server.Text,_port);
            var from = new MailAddress(Login.Text);
            var to = new MailAddress(SendTo.Text);
            var message = new MailMessage(from, to);
            message.Subject = Subject.Text;
            message.Body = Message.Text;
            switch (SecureType.SelectedIndex)
            {
                case 0:
                    {
                        client.EnableSsl = true;
                        break;
                    }
            }
            client.Credentials = new NetworkCredential
            {
                UserName = Login.Text,
                SecurePassword = Password.SecurePassword
            };
            try
            {
                client.Send(message);
                StatusBarSendMessage("Сообщение вроде отправленно", Brushes.Black);
                SendTo.Text = "";
                Message.Text = "";
                Subject.Text = "";
            } catch
            {
                StatusBarSendMessage("Все сломалось, обратитесь к разработчику", Brushes.Red);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Login.Text = "";
            //Server.Text = "";
            //port.Text = "";
            //Password.Clear();
            SendTo.Text = "";
            Message.Text = "";
            Subject.Text = "";
        }
    }
}
