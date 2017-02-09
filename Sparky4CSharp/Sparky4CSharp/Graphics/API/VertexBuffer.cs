using SP.Platform.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SP.Graphics.API
{

    public enum BufferUsage
    {
        STATIC,
        DYNAMIC
    }

    public abstract class VertexBuffer
    {

        public abstract void Resize(uint size);
        public abstract void SetLayout(BufferLayout layout);
        public abstract void SetData(uint size, IntPtr data);
        public void SetData<T>(T str) where T : struct
        {
            int size = Marshal.SizeOf(str);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(str, ptr, true);
            SetData((uint)size, ptr);
            Marshal.FreeHGlobal(ptr);
        } 

        public abstract void ReleasePointer();

        public abstract void Bind();
        public abstract void Unbind();

        public T GetPointer<T>() where T : struct
        {
            T str = new T();

            IntPtr ptr = GetPointerInternal();
            str = (T)Marshal.PtrToStructure(ptr, str.GetType());

            return str;
        }

        protected abstract IntPtr GetPointerInternal();

        public static VertexBuffer Create(BufferUsage usage = BufferUsage.STATIC)
        {
            switch(Context.GetRenderAPI())
            {
                case RenderAPI.OPENGL: return new GLVertexBuffer(usage);
            }
            return null;
        }


    }
}
