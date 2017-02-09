using SP.Graphics.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Platform.OpenGL
{
    public class GLShaderResourceDeclaration : ShaderResourceDeclaration
    {

        public enum Type
        {
            NONE,
            TEXTURE2D,
            TEXTURECUBE,
            TEXTURESHADOW
        }

        private string name;
        private uint register;
        private uint count;
        private Type type;

        public GLShaderResourceDeclaration(Type type, string name, uint count)
        {
            this.type = type;
            this.name = name;
            this.count = count;
        }

        public override uint GetCount()
        {
            return count;
        }

        public override string GetName()
        {
            return name;
        }

        public override uint GetRegister()
        {
            return register;
        }

        public new Type GetType()
        {
            return type;
        }

        public static Type StringToType(string type)
        {
            if (type == "sampler2D") return Type.TEXTURE2D;
            if (type == "samplerCube") return Type.TEXTURECUBE;
            if (type == "samplerShadow") return Type.TEXTURESHADOW;

            return Type.NONE;
        }

        public static string TypeToString(Type type)
        {
            switch(type)
            {
                case Type.TEXTURE2D: return "sampler2D";
                case Type.TEXTURECUBE: return "samplerCube";
                case Type.TEXTURESHADOW: return "samplerShadow";
            }

            return "Invalid Type";
        }

    }
}
