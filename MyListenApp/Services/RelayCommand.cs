using System;
using System.Windows.Input;

namespace MyListenApp.Services
{
    internal sealed class RelayCommand(Action execute, Func<bool>? canExecute = null) : ICommand
    {
        readonly Action execute = execute;
        readonly Func<bool>? canExecute = canExecute;

        public bool CanExecute(object? parameter)
        {
            return canExecute is null || canExecute.Invoke();
        }

        public void Execute(object? parameter)
        {
            execute.Invoke();
        }

        public event EventHandler? CanExecuteChanged;
        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

