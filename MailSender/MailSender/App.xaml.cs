using MailClient.lib.Interfaces;
using MailClient.lib.Service;
using MailSender.Data;
using MailSender.Infrastructure.Converters;
using MailSender.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MailSender
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost _Hosting;

        public static IHost Hosting => _Hosting
            ??= Host.CreateDefaultBuilder(Environment.GetCommandLineArgs())
               .ConfigureServices(ConfigureServices)
               .Build();

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<ProgramData>();
            services.AddTransient<IMailService, SmtpMailService>();
            services.AddSingleton<IEncryptorService, Rfc2898Encryptor>();
            string sql_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Database1.mdf;Integrated Security=True";
            services.AddDbContext<MailSenderDB>(opt => opt.UseSqlServer(sql_string));
        }

        public static IServiceProvider Services => Hosting.Services;

    }
}
