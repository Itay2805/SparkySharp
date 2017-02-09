using OpenGL;
using SP.Graphics.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Platform.OpenGL
{
    public class GLVertexBuffer : VertexBuffer
    {

        private uint[] handle = new uint[1];
        private BufferUsage usage;
        private uint size;
        private BufferLayout layout;

        public GLVertexBuffer(BufferUsage usage)
        {
            Gl.GenBuffers(1, handle);
        }

        ~GLVertexBuffer()
        {
            Gl.DeleteBuffers(1, handle);
        }

        public override void Bind()
        {
            Gl.BindBuffer(BufferTarget.ArrayBuffer, handle[0]);
        }

        public override void ReleasePointer()
        {
            Gl.UnmapBuffer(BufferTarget.ArrayBuffer);
            SetLayout(layout);
        }

        public override void Resize(uint size)
        {
            this.size = size;

            Gl.BindBuffer(BufferTarget.ArrayBuffer, handle[0]);
            Gl.BufferData(BufferTarget.ArrayBuffer, new IntPtr(size), IntPtr.Zero, SPBufferUsageToOpenGL(usage));
        }

        public override void SetData(uint size, IntPtr data)
        {
            Gl.BindBuffer(BufferTarget.ArrayBuffer, handle[0]);
            Gl.BufferData(BufferTarget.ArrayBuffer, new IntPtr(size), data, SPBufferUsageToOpenGL(usage));
        }

        public override void SetLayout(BufferLayout bufferLayout)
        {
            this.layout = bufferLayout;
            List<BufferElement> layout = bufferLayout.GetLayout();
            for(int i = 0; i < layout.Count; i++)
            {
                BufferElement element = layout[i];
                Gl.EnableVertexAttribArray(i);
                Gl.VertexAttribPointer((uint)i, (int)element.count, (VertexAttribPointerType) element.type, element.normalized, (int)bufferLayout.GetStride(), new IntPtr(element.offset));
            }
        }

        public override void Unbind()
        {
            Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        protected override IntPtr GetPointerInternal()
        {
            return Gl.MapBuffer(BufferTarget.ArrayBuffer, BufferAccess.WriteOnly);
        }

        private static BufferUsageHint SPBufferUsageToOpenGL(BufferUsage usage)
        {
            switch(usage)
            {
                case BufferUsage.DYNAMIC: return BufferUsageHint.DynamicDraw;
                case BufferUsage.STATIC: return BufferUsageHint.StaticDraw;
            }
            return 0;
        }
    }
}
