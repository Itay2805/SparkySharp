using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Graphics.Shaders
{
    public abstract class ShaderUniformDeclaration
    {

        public abstract string GetName();
        public abstract uint GetSize();
        public abstract uint GetCount();
        public abstract uint GetOffset();

        public abstract void SetOffset(uint offset);

    }

    public abstract class ShaderUniformBufferDeclaration
    {
        public abstract string GetName();
        public abstract uint GetRegister();
        public abstract uint GetShaderType();
        public abstract uint GetSize();
        public abstract ShaderUniformDeclaration FindUniform(string name);
    }

    public class ShaderStruct
    {
        private string name;
        private List<ShaderUniformDeclaration> fields = new List<ShaderUniformDeclaration>();
        private uint size;
        private uint offset;

        public ShaderStruct(string name)
        {
            this.name = name;
            this.size = 0;
            this.offset = 0;
        }

        public void AddField(ShaderUniformDeclaration field)
        {
            size += field.GetSize();
            uint offset = 0;
            if(fields.Count != 0)
            {
                ShaderUniformDeclaration previous = fields.Last();
                offset = previous.GetOffset() + previous.GetSize();
            }
            field.SetOffset(offset);
            fields.Add(field);
        }

        public void SetOffset(uint offset)
        {
            this.offset = offset;
        }

        public string GetName()
        {
            return name;
        }

        public uint GetSize()
        {
            return size;
        }

        public uint GetOffset()
        {
            return offset;
        }

        public List<ShaderUniformDeclaration> GetFields()
        {
            return fields;
        }
    }

}
