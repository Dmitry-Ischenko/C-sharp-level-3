﻿using System;
using System.Windows.Input;

namespace MailSender.Infrastructure.Commands.Base
{
    abstract class Command : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        bool ICommand.CanExecute(object parameter) => CanExecute(parameter);

        void ICommand.Execute(object parameter) => Execute(parameter);

        public virtual bool CanExecute(object p) => true;

        public abstract void Execute(object p);
    }
}
