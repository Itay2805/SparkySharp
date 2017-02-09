using SP.Platform.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Graphics.API
{

    public struct BufferElement
    {
        public string name;
        public uint type;
        public uint size;
        public uint count;
        public uint offset;
        public bool normalized;

        public BufferElement(string name, uint type, uint size, uint count, uint offset, bool normalized)
        {
            this.name = name;
            this.type = type;
            this.size = size;
            this.count = count;
            this.offset = offset;
            this.normalized = normalized;
        }
    }

    public class BufferLayout
    {

        private uint size;
        private List<BufferElement> layout = new List<BufferElement>();

        public BufferLayout()
        {
            
        }

        private void Push(string name, uint type, uint size, uint count, bool normalized)
        {
            layout.Add(new BufferElement(name, type, size, count, this.size, normalized));
            this.size += size * count;
        }

        public void PushFloat(string name, uint count = 1, bool normalized = false)
        {
            switch(Context.GetRenderAPI())
            {
                case RenderAPI.OPENGL:
                    Push(name, GLTypes.GL_FLOAT, sizeof(float), count, normalized);
                    break;
            }
        }

        public void PushUInt(string name, uint count = 1, bool normalized = false)
        {
            switch (Context.GetRenderAPI())
            {
                case RenderAPI.OPENGL:
                    Push(name, GLTypes.GL_UNSIGNED_INT, sizeof(uint), count, normalized);
                    break;
            }
        }

        public void PushByte(string name, uint count = 1, bool normalized = false)
        {
            switch (Context.GetRenderAPI())
            {
                case RenderAPI.OPENGL:
                    Push(name, GLTypes.GL_UNSIGNED_BYTE, sizeof(byte), count, normalized);
                    break;
            }
        }

        public void PushVector2(string name, uint count = 1, bool normalized = false)
        {
            switch (Context.GetRenderAPI())
            {
                case RenderAPI.OPENGL:
                    Push(name, GLTypes.GL_FLOAT, sizeof(float), 2, normalized);
                    break;
            }
        }

        public void PushVector3(string name, uint count = 1, bool normalized = false)
        {
            switch (Context.GetRenderAPI())
            {
                case RenderAPI.OPENGL:
                    Push(name, GLTypes.GL_FLOAT, sizeof(float), 3, normalized);
                    break;
            }
        }

        public void PushVector4(string name, uint count = 1, bool normalized = false)
        {
            switch (Context.GetRenderAPI())
            {
                case RenderAPI.OPENGL:
                    Push(name, GLTypes.GL_FLOAT, sizeof(float), 4, normalized);
                    break;
            }
        }

        public List<BufferElement> GetLayout()
        {
            return layout;
        }

        public uint GetStride()
        {
            return size;
        }

    }
}
