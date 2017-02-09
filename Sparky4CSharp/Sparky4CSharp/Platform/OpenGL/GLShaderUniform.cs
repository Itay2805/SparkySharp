using SP.Graphics.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Platform.OpenGL
{
    public class GLShaderUniformDeclaration : ShaderUniformDeclaration
    {

        public enum Type
        {
            NONE,
            FLOAT32,
            VEC2,
            VEC3,
            VEC4,
            MAT3,
            MAT4,
            INT32,
            STRUCT
        }

        private string name;
        private uint size;
        private uint count;
        private uint offset;

        private Type type;
        private ShaderStruct @struct;
        private int location;

        public GLShaderUniformDeclaration(Type type, string name, uint count = 1)
        {
            this.type = type;
            this.@struct = null;

            this.name = name;
            this.count = count;
            this.size = SizeOfUniformType(type) * count;
        }

        public GLShaderUniformDeclaration(ShaderStruct uniformStruct, string name, uint count = 1)
        {
            this.@struct = uniformStruct;
            this.type = Type.STRUCT;

            this.name = name;
            this.count = count;
            this.size = @struct.GetSize() * count;
        }

        public override string GetName()
        {
            return name;
        }

        public override uint GetSize()
        {
            return size;
        }

        public override uint GetCount()
        {
            return count;
        }

        public override uint GetOffset()
        {
            return offset;
        }

        public uint GetAbsoluteOffset()
        {
            return @struct != null ? @struct.GetOffset() + offset : offset;
        }

        public int GetLocation()
        {
            return location;
        }

        public new Type GetType()
        {
            return type;
        }

        public ShaderStruct GetShaderUniformStruct()
        {
            return @struct;
        }

        public override void SetOffset(uint offset)
        {
            if (type == Type.STRUCT)
                @struct.SetOffset(offset);

            this.offset = offset;
        }

        public static uint SizeOfUniformType(Type type)
        {
            switch(type)
            {
                case Type.INT32: return 4;
                case Type.FLOAT32: return 4;
                case Type.VEC2: return 4 * 2;
                case Type.VEC3: return 4 * 3;
                case Type.VEC4: return 4 * 4;
                case Type.MAT3: return 4 * 3 * 3;
                case Type.MAT4: return 4 * 4 * 4;
            }
            return 0;
        }

        public static Type StringToType(string type)
        {
            if (type == "int32") return Type.INT32;
            if (type == "float") return Type.FLOAT32;
            if (type == "vec2") return Type.VEC2;
            if (type == "vec3") return Type.VEC3;
            if (type == "vec4") return Type.VEC4;
            if (type == "mat3") return Type.MAT3;
            if (type == "mat4") return Type.MAT4;

            return Type.NONE;
        }

        public static string TypeToString(Type type)
        {
            switch(type)
            {
                case Type.INT32: return "int32";
                case Type.FLOAT32: return "float";
                case Type.VEC2: return "vec2";
                case Type.VEC3: return "vec3";
                case Type.VEC4: return "vec4";
                case Type.MAT3: return "mat3";
                case Type.MAT4: return "mat4";
            }

            return "Invalid Type";
        }

    }

    public struct GLShaderUniformField
    {
        public GLShaderUniformDeclaration.Type type;
        public string name;
        public uint count;
        public uint size;
        public int location;
    }

    public class GLShaderUniformBufferDeclaration : ShaderUniformBufferDeclaration
    {
        private string name;
        private List<ShaderUniformDeclaration> uniforms = new List<ShaderUniformDeclaration>();
        private uint register;
        private uint size;
        private uint shaderType; // 0 = VS, 1 = PS

        public GLShaderUniformBufferDeclaration(string name, uint shaderType)
        {
            this.name = name;
            this.shaderType = shaderType;
            this.size = 0;
            this.register = 0;
        }

        public void PushUniform(GLShaderUniformDeclaration uniform)
        {
            uint offset = 0;
            if(uniforms.Count != 0)
            {
                GLShaderUniformDeclaration previous = (GLShaderUniformDeclaration)uniforms.Last();
                offset = previous.GetOffset() + previous.GetSize();
            }
            uniform.SetOffset(offset);
            size += uniform.GetSize();
            uniforms.Add(uniform);
        }

        public override string GetName()
        {
            return name;
        }

        public override uint GetRegister()
        {
            return register;
        }

        public override uint GetShaderType()
        {
            return shaderType;
        }

        public override uint GetSize()
        {
            return size;
        }

        public List<ShaderUniformDeclaration> GetUniformDeclarations()
        {
            return uniforms;
        }

        public override ShaderUniformDeclaration FindUniform(string name)
        {
            foreach(ShaderUniformDeclaration uniform in uniforms)
            {
                if (uniform.GetName() == name)
                    return uniform;
            }
            return null;
        }
    }
}
