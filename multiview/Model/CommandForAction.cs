using System;
using System.Windows.Input;

namespace multiview.Model
{
    public class CommandForAction : ICommand
    {
        private Action _action;

        public CommandForAction(Action action)
        {
            _action=action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;
        
        public void Execute(object? parameter)
        {
            _action();
        }
    }

    
}
