using SP.Graphics.API;
using SP.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Graphics.Shaders
{
    
    public abstract class Shader
    {

        public const int VERTEX_INDEX = 0;
        public const int UV_INDEX = 1;
        public const int MASK_UV_INDEX = 2;
        public const int TID_INDEX = 3;
        public const int MID_INDEX = 4;
        public const int COLOR_INDEX = 5;

        public const string UNIFORM_PROJETION_MATRIX_NAME = "sys_ProjectionMatrix";
        public const string UNIFORM_VIEW_MATRIX_NAME = "sys_ViewMatrix";
        public const string UNIFORM_MODEL_MATRIX_NAME = "sys_ModelMatrix";

        public static Shader currentlyBound;

        public abstract void Bind();
        public abstract void Unbind();

        public abstract void SetVSSystemUniformBuffer(byte[] data, uint size, uint slot = 0);
        public abstract void SetPSSystemUniformBuffer(byte[] data, uint size, uint slot = 0);

        public abstract void SetVSUserUniformBuffer(byte[] data, uint size);
        public abstract void SetPSUserUniformBuffer(byte[] data, uint size);

        public abstract List<ShaderUniformBufferDeclaration> GetVSSystemUniforms();
        public abstract List<ShaderUniformBufferDeclaration> GetPSSystemUniforms();
        public abstract ShaderUniformBufferDeclaration GetVSUserUniform();
        public abstract ShaderUniformBufferDeclaration GetPSUserUniform();

        public abstract List<ShaderResourceDeclaration> GetResources();

        public abstract string GetName();
        public abstract string GetFilePath();

        public static Shader CreateFromFile(string name, string filepath)
        {
            string source = VFS.Get.ReadTextFile(filepath);

            switch(Context.GetRenderAPI())
            {
                
            }

            return null;
        }

        public static Shader CreateFromSource(string name, string source)
        {
            switch(Context.GetRenderAPI())
            {

            }
            return null;
        }

        public static bool TryCompile(string source, out string error)
        {
            error = "Not Implemented";
            switch (Context.GetRenderAPI())
            {

            }
            return false;
        }

        public static bool TryCompileFromFile(string filepath, out string error)
        {
            string source = VFS.Get.ReadTextFile(filepath);
            return TryCompile(source, out error);
        }

    }
}
