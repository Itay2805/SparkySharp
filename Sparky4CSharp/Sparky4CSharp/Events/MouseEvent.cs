using SP.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Events
{
    public class MouseButtonEvent : Event
    {
        protected uint button;
        protected Vector2 position;

        protected MouseButtonEvent(uint button, float x, float y, Type type)
            : base(type)
        {
            this.button = button;
            this.position = new Vector2(x, y);
        }

        public uint GetButton()
        {
            return button;
        }

        public float GetX()
        {
            return position.x;
        }

        public float GetY()
        {
            return position.y;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public static int GetStaticType()
        {
            return (int)Type.MOUSE_PRESSED | (int)Type.MOUSE_RELEASED;
        }
    }

    class MousePressedEvent : MouseButtonEvent
    {
        public MousePressedEvent(uint button, float x, float y)
            : base(button, x, y, GetStaticType())
        {

        }

        public override string ToString()
        {
            return "MouseReleasedEvent: (" + GetButton() + ", " + GetX() + ", " + GetY() + ")";
        }

        public static new Type GetStaticType()
        {
            return Type.MOUSE_PRESSED;
        }
    }

    class MouseReleasedEvent : MouseButtonEvent
    {
        public MouseReleasedEvent(uint button, float x, float y)
            : base(button, x, y, GetStaticType())
        {

        }

        public static new Type GetStaticType()
        {
            return Type.MOUSE_RELEASED;
        }
    }

    class MouseMovedEvent : Event
    {
        private Vector2 position;
        private bool dragged;

        public MouseMovedEvent(float x, float y, bool dragged)
            :base(GetStaticType())
        {
            this.position = new Vector2(x, y);
            this.dragged = dragged;
        }

        public float GetX()
        {
            return position.x;
        }

        public float GetY()
        {
            return position.y;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public bool IsDragged()
        {
            return dragged;
        }

        public static Type GetStaticType()
        {
            return Type.MOUSE_MOVED;
        }
    }

}
