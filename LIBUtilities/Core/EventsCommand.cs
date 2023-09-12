using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace LIBUtilities.Core
{
    public class EventsCommand : ICommand
    {
        public Action<object> OnExecute;
        public Func<bool> CanExecuteMethod;
        public List<string> DependencyPropertyList { get; set; }
        public event EventHandler CanExecuteChanged;

        public EventsCommand(Action<object> onExecuteAction) :
            this(onExecuteAction, () => { return true; }, null)
        {

        }

        public EventsCommand(Action<object> onExecuteAction, Func<bool> canExecuteMethod, string propertyName)
        {
            OnExecute = onExecuteAction;
            CanExecuteMethod = canExecuteMethod;
            DependencyPropertyList = new List<string>();
            if (!string.IsNullOrEmpty(propertyName))
                DependencyPropertyList.Add(propertyName);
        }

        public void UpdateCanExecute()
        {
            if (CanExecuteChanged == null)
                return;
            CanExecuteChanged(this, new EventArgs());
        }

        public void UpdateCanExecute(string propertyName)
        {
            foreach (string prop in DependencyPropertyList)
            {
                if (prop == propertyName)
                {
                    UpdateCanExecute();
                    return;
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteMethod != null)
                return CanExecuteMethod();
            return true;
        }

        public void Execute(object parameter)
        {
            if (OnExecute == null)
                return;
            OnExecute(parameter);
        }
    }
}