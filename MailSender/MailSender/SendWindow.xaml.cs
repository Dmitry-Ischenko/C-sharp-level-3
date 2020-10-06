using MailSender.Models;
using System;
using System.Collections.Generic;
using System.Windows;


namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для SendWindow.xaml
    /// </summary>
    public partial class SendWindow : Window
    {
        public List<Recipient> RecipientList { get; set; }
        public Sender SenderUr { get; set; }
        public Message MessageUr { get; set; }

        public SendWindow()
        {
            InitializeComponent();           
        }

        private void LoadedWindow(object sender, RoutedEventArgs e)
        {
            if (SenderUr?.Name != null)
            {
                Name.Text = SenderUr.Name;
            }
            Address.Text = SenderUr.Address;
            Subject.Text = MessageUr.Subject;
            Body.Text = MessageUr.Body;
            RecipientCount.Text = $"{RecipientList.Count}";
            foreach (var t in RecipientList)
            {
                MailClient.lib.MailSender mailSender = new MailClient.lib.MailSender();
                mailSender.ServerAddress = SenderUr.Server;
                mailSender.ServerPort = SenderUr.Port;
                mailSender.UserLogin = SenderUr.Address;
                mailSender.UserPassword = SenderUr.Password;
                mailSender.UseSSL = SenderUr.UseSSl;
                mailSender.SendMsg = false; // это надо убрать, что бы сообщения отправлялись
                try
                {
                    mailSender.SendMessage(SenderUr.Address, t.Address, MessageUr.Subject, MessageUr.Body);
                    ViewListBox.Items.Add(t);
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.ToString());
                }

            }
        }
    }
}
