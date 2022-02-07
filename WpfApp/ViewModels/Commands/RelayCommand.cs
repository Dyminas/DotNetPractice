using System;
using System.Windows.Input;

namespace WpfApp.ViewModels.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object?, bool> _canExecute;
        private readonly Action<object?> _execute;

        public RelayCommand(Action<object?> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute)
        {
            if (execute is null) throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute ?? (x => true);
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter) => _canExecute(parameter);

        public void Execute(object? parameter) => _execute(parameter);

        public static void NotifyCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
    }
}
