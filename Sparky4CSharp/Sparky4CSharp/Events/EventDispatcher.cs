using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Events
{
    public class EventDispatcher
    {

        private Event e;

        public EventDispatcher(Event e)
        {
            this.e = e;
        }

        public void Dispatch<T>(Func<T, bool> func) where T : Event
        {
            T a = e as T;
            if (a != null) e.Handled = func.Invoke(a);
        }

    }
}
