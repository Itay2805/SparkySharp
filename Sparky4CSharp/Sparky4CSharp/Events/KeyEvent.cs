using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Events
{
    public class KeyEvent : Event
    {

        protected uint keyCode;
        protected uint count;

        public KeyEvent(uint keyCode, Type type)
            :   base (type)
        {
            this.keyCode = keyCode;
        }

        public uint GetKeyCode()
        {
            return keyCode;
        }

        public static int GetStaticType()
        {
            return (int)Type.KEY_PRESSED | (int)Type.KEY_RELEASED;
        }

    }

    class KeyPressedEvent : KeyEvent
    {
        private uint repeat;
        private uint modifiers;

        public KeyPressedEvent(uint button, uint repeat, uint modifiers)
            : base(button, GetStaticType())
        {
            this.repeat = repeat;
            this.modifiers = modifiers;
        }

        public uint GetRepeat()
        {
            return repeat;
        }

        public uint GetModifiers()
        {
            return modifiers;
        }

        public bool IsModifier(uint modifier)
        {
            return (modifiers & modifier) != 0;
        }

        public static new Type GetStaticType()
        {
            return Type.KEY_PRESSED;
        }

    }


    class KeyReleasedEvent : KeyEvent
    {

        public KeyReleasedEvent(uint button)
            : base(button, GetStaticType())
        {
        }

        public static new Type GetStaticType()
        {
            return Type.KEY_RELEASED;
        }

    }
}
