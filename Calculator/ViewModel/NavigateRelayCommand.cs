﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.ViewModel
{
    public class NavigateRelayCommand : ICommand        //Базовый класс-комманда. Ввиду своей универсальности и
                                                        //простоты справляется с большинством задач в приложении.
    {
        private Action action;
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public NavigateRelayCommand(Action action)
        {
            this.action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}
