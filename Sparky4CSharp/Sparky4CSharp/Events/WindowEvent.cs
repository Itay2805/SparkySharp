using SP.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Events
{
    public class ResizeWindowEvent : Event
    {

        protected TVector2<uint> size;
        
        public ResizeWindowEvent(uint width, uint height) : base(GetStaticType())
        {
            this.size = new TVector2<uint>(width, height);
        }

        public TVector2<uint> GetSize()
        {
            return size;
        }

        public uint GetWidth()
        {
            return size.x;
        }

        public uint GetHeight()
        {
            return size.y;
        }

        public static Type GetStaticType()
        {
            return Type.WINDOW_RESIZE;
        }

    }
}
