﻿using MailClient.lib.Interfaces;
using MailClient.lib.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace MailClient.lib.Service
{
    public class SmtpMailService : IMailService
    {
        public IMailSender GetSender(string ServerAddress, 
            int ServerPort,
            bool UseSSL, 
            string UserLogin, 
            string UserPassword, 
            string UserName = null, 
            bool SendMsg = true,
            Action<bool> action=null)
        {
            return new SmtpMailSender(
                ServerAddress,
                ServerPort,
                UseSSL,
                UserLogin,
                UserPassword,
                UserName,
                SendMsg,
                action
                );
        }

        public IMailSender GetSenderAndNotify(string ServerAddress,
            int ServerPort,
            bool UseSSL,
            string UserLogin,
            string UserPassword,
            string UserName = null,
            bool SendMsg = true,
            Action<Recipient,bool > action = null)
        {
                return new SmtpMailSender(
                    ServerAddress,
                    ServerPort,
                    UseSSL,
                    UserLogin,
                    UserPassword,
                    UserName,
                    SendMsg,
                    action
                    );
            }
    }
    public class SmtpMailSender : IMailSender
    {
        private readonly string _ServerAddress;

        private readonly int _ServerPort;

        private readonly bool _UseSSL;

        private readonly string _UserLogin;

        private readonly string _UserPassword;

        private readonly string _UserName;

        public event Action<bool> SendSuccess;
        public event Action<Recipient,bool> Notify;
        //Для имитации отправки
        private readonly bool _SendMsg;

        public SmtpMailSender(string ServerAddress, 
            int ServerPort, 
            bool UseSSL, 
            string UserLogin, 
            string UserPassword, 
            string UserName = null, 
            bool SendMsg=true,
            Action<bool> action=null)
        {
            _ServerAddress = ServerAddress;
            _ServerPort = ServerPort;
            _UseSSL = UseSSL;
            _UserLogin = UserLogin;
            _UserPassword = UserPassword;
            _UserName = UserName;
            _SendMsg = SendMsg;
            SendSuccess += action;
        }
        public SmtpMailSender(string ServerAddress,
            int ServerPort,
            bool UseSSL,
            string UserLogin,
            string UserPassword,
            string UserName = null,
            bool SendMsg = true,
            Action<Recipient,bool> action = null)
        {
            _ServerAddress = ServerAddress;
            _ServerPort = ServerPort;
            _UseSSL = UseSSL;
            _UserLogin = UserLogin;
            _UserPassword = UserPassword;
            _UserName = UserName;
            _SendMsg = SendMsg;
            Notify += action;
        }
        public void Send(string RecipientAddress, string Subject, string Body)
        {
            MailAddress from;
            if (_UserName is null)
            {
                from = new MailAddress(_UserLogin);
            }
            else
            {
                from = new MailAddress(_UserLogin, _UserName);
            }
            var to = new MailAddress(RecipientAddress);
            using (var message = new MailMessage(from, to))
            {
                message.Subject = Subject;
                message.Body = Body;
                using (var client = new SmtpClient(_ServerAddress, _ServerPort))
                {
                    client.EnableSsl = _UseSSL;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential
                    {
                        UserName = _UserLogin,
                        Password = _UserPassword
                    };
                    try
                    {
                        if (_SendMsg)
                        {
                            client.Send(message);
                            SendSuccess?.Invoke(true);
                        }
                        else
                        {
                            //Имитация отправки
                            Thread.Sleep(2000);
                            SendSuccess?.Invoke(true);
                        }

                    }
                    catch (Exception e)
                    {
                        Trace.TraceError(e.ToString());
                        SendSuccess?.Invoke(false);
                        throw;
                    }
                }
            }
        }

        public void SendThread(string RecipientAddress, string Subject, string Body)
        {
            new Thread(() => Send(RecipientAddress, Subject, Body)) { IsBackground = true }.Start();
        }

        public void SendAndNotify(Recipient recipient, Message message)
        {
            new Thread(() => OnSendAndNotify(recipient, message)) { IsBackground = true }.Start();
        }
        private void OnSendAndNotify(Recipient recipient, Message _message)
        {
            MailAddress from;
            if (_UserName is null)
            {
                from = new MailAddress(_UserLogin);
            }
            else
            {
                from = new MailAddress(_UserLogin, _UserName);
            }
            var to = new MailAddress(recipient.Address);
            using (var message = new MailMessage(from, to))
            {
                message.Subject = _message.Subject;
                message.Body = _message.Body;
                using (var client = new SmtpClient(_ServerAddress, _ServerPort))
                {
                    client.EnableSsl = _UseSSL;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential
                    {
                        UserName = _UserLogin,
                        Password = _UserPassword
                    };
                    try
                    {
                        if (_SendMsg)
                        {
                            client.Send(message);
                            Notify?.Invoke(recipient,true);
                        }
                        else
                        {
                            //Имитация отправки
                            Thread.Sleep(2000);
                            Notify?.Invoke(recipient, true);
                        }

                    }
                    catch (Exception e)
                    {
                        Notify?.Invoke(recipient, false);
                        Trace.TraceError(e.ToString());
                        throw;
                    }
                }
            }
        }
    }
}
