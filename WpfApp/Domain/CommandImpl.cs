using System;
using System.Windows.Input;

namespace WpfApp.Domain
{
    public class CommandImpl : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly Func<object?, bool> _canExecute;
        private readonly Action<object?> _execute;

        public CommandImpl(Action<object?> execute)
            : this(execute, null)
        {
        }

        public CommandImpl(Action<object?> execute, Func<object?, bool>? canExecute)
        {
            if (execute is null) throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute ?? (x => true);
        }

        public bool CanExecute(object? parameter) => _canExecute(parameter);

        public void Execute(object? parameter) => _execute(parameter);

        public void RaiseCanExecuteChangedEvent()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
