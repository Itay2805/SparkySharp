using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Events
{
    public class Event
    {

        public enum Type : int
        {
            KEY_PRESSED = 1 << 0,
            KEY_RELEASED = 1 << 1,
            
            MOUSE_PRESSED = 1 << 2,
            MOUSE_RELEASED = 1 << 3,
            MOUSE_MOVED = 1 << 4,

            WINDOW_RESIZE = 1 << 5,
        }

        public bool Handled { get; set; }
        public Type EventType { get; protected set; }

        protected Event(Type type)
        {
            Handled = false;
            EventType = type;
        }

        public override string ToString()
        {
            return "Event: " + EventType;
        }

    }
}
