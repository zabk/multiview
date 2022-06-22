using System;
using System.Windows.Input;

namespace multiview.Model
{
    public class ProcessingCommand : ICommand
    {
        private Action<Guid> _action;

        public ProcessingCommand(Action<Guid> action)
        {
            _action = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            _action((Guid)parameter);
        }
    }
}
