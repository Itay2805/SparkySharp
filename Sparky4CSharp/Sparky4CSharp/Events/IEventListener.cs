using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Events
{
    public interface IEventListener
    {

        void OnEvent(Event e);

    }
}
