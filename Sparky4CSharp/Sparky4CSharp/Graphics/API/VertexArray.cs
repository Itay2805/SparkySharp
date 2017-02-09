using SP.Platform.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Graphics.API
{
    public abstract class VertexArray
    {
        private uint ID;
        private List<VertexBuffer> buffers = new List<VertexBuffer>();

        public abstract VertexBuffer GetBuffer(int index = 0);
        public abstract void PushBuffer(VertexBuffer buffer);

        public abstract void Bind();
        public abstract void Unbind();

        public abstract void Draw(uint count);

        public static VertexArray Create()
        {
            switch(Context.GetRenderAPI())
            {
                case RenderAPI.OPENGL: return new GLVertexArray();
            }
            return null;
        }
    }
}
