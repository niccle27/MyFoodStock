﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Helper
{
    /// <summary>
    /// This class is from https://msdn.microsoft.com/en-us/magazine/dn237302.aspx It takes advantage of lambda function in order
    /// to restraint the number of implementing the ICommand interface
    /// </summary>
    class RelayCommand : ICommand
    {
        public delegate void ICommandOnExecute(object parameter);

        public delegate bool ICommandOnCanExecute(object
            parameter);

        private ICommandOnExecute _execute;
        private ICommandOnCanExecute _canExecute;

        public RelayCommand(ICommandOnExecute onExecuteMethod,
            ICommandOnCanExecute onCanExecuteMethod)
        {
            _execute = onExecuteMethod;
            _canExecute = onCanExecuteMethod;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}