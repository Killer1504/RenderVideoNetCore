﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RenderVideo
{
    public class MyICommand : ICommand
    {
        private Action _TargetExecuteMethod;
        private Func<bool> _TargetCanExecuteMethod;

        public MyICommand(Action executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        public MyICommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            if (this != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return _TargetCanExecuteMethod != null ? _TargetCanExecuteMethod() : _TargetExecuteMethod != null;
        }

        // Beware - should use weak references if command instance lifetime
        //  is longer than lifetime of UI objects that get hooked up to command

        // Prism commands solve this in their implementation
        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            _TargetExecuteMethod?.Invoke();
        }
    }

    /// <summary>
    /// with paramter command
    /// </summary>
    public class MyICommandParamter : ICommand
    {
        private Action<object> _TargetExecuteMethod;
        private Func<bool> _TargetCanExecuteMethod;

        public MyICommandParamter(Action<object> executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        public MyICommandParamter(Action<object> executeMethod, Func<bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            if (this != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return _TargetCanExecuteMethod != null ? _TargetCanExecuteMethod() : _TargetExecuteMethod != null;
        }

        // Beware - should use weak references if command instance lifetime
        //  is longer than lifetime of UI objects that get hooked up to command

        // Prism commands solve this in their implementation
        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            _TargetExecuteMethod(parameter);
        }
    }
}