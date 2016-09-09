using System;
using System.Diagnostics;
using System.Windows.Input;

namespace WpfCommon.Commands
{
    /// <summary>
    /// Responosible for relaying command functionality by invoking delegates
    /// </summary>
    /// <remarks>
    /// By default, this command can always execute.
    /// </remarks>
    public class RelayCommand : ICommand
    {
        #region FIELDS
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Creaets a new command that will always execute
        /// </summary>
        /// <param name="execute">the action to execute</param>
        public RelayCommand(Action<object> execute) : this(execute, null)
        { }

        /// <summary>
        /// Creates a new command that can only execute under a certain condition
        /// </summary>
        /// <param name="execute">the action to execute</param>
        /// <param name="canExecute">the condition under ehich the action can be executed</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion

        #region ICOMMAND IMPLEMENTATION
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        #endregion
    }
}
