using OpenGL;
using SP.Graphics.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Platform.OpenGL
{
    public class GLVertexArray : VertexArray
    {

        private uint[] handle = new uint[1];
        private List<VertexBuffer> buffers = new List<VertexBuffer>();

        public GLVertexArray()
        {

        }

        ~GLVertexArray()
        {

        }

        public override VertexBuffer GetBuffer(int index = 0)
        {
            return buffers[index];
        }

        public override void PushBuffer(VertexBuffer buffer)
        {
            buffers.Add(buffer);
        }

        public override void Bind()
        {
            buffers.First().Bind();
        }

        public override void Unbind()
        {
            buffers.First().Unbind();
        }

        public override void Draw(uint count)
        {
            Gl.DrawElements(BeginMode.Triangles, (int)count, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}
