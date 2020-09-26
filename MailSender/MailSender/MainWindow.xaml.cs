using System;
using System.Windows;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Xml.Schema;
using System.Windows.Media;
using MailSender.Data;
using MailSender.Models;

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
            ProgramData.SendersCollection.Add(new Sender
            {
                Name = "Дмитрий",
                Address = "dmitry@test.ru",
                Password = "test",
                Port = 456,
                Server = "smtp.yandex.ru",
                UseSSl = true
            });
            ProgramData.SendersCollection.Add(new Sender
            {
                Name = "Валера",
                Address = "valera@gmail.ru",
                Password = "test",
                Port = 587,
                Server = "smtp.gmail.ru",
                UseSSl = true
            });
        }        
    }
}
