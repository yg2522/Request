using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loctool.Client.Classes.Request
{
    public interface IRequest
    {
        Func<object, object> FetchMethod { get; set; }

        event EventHandler CanExecuteChanged;

        bool CanExecute(object parameters);
    }

    public interface IRequest<T> : IRequest
    {
        T Execute(object parameters = null);
    }
}
