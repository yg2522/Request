using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace loctool.Client.Classes.Request
{
    public class RelayRequest<T> : IRequest<T>
    {
        readonly Predicate<object> _canExecute;

        public Func<object, object> FetchMethod { get; set; }

        public RelayRequest()
            : this(null)
        {
        }
        public RelayRequest(Predicate<object> canExecute)
            : base()
        {
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameters)
        {
            return _canExecute == null || _canExecute(parameters);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public T Execute(object parameters = null)
        {
            if (FetchMethod == null)
                return default(T);
            var item = FetchMethod(parameters);

            if (item == null)
                return default(T);

            return (T)item;
        }

    }
}
