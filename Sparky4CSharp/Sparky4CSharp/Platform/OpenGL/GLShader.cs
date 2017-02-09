using SP.Graphics.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Platform.OpenGL
{

    public struct GLShaderErrorInfo
    {
        public uint shader;
        public string[] message;
        public uint[] line;
    }

    public class GLShader : Shader
    {

        private uint handle;
        private string name, path;
        private string source;
        private string vertexSource, fragmentSource;

        private List<ShaderUniformBufferDeclaration> VSUniformsBuffers = new List<ShaderUniformBufferDeclaration>();
        private List<ShaderUniformBufferDeclaration> PSUniformsBuffers = new List<ShaderUniformBufferDeclaration>();
        private GLShaderUniformBufferDeclaration VSUserUniformBuffer;
        private GLShaderUniformBufferDeclaration PSUserUniformBuffer;
        private List<ShaderResourceDeclaration> resources = new List<ShaderResourceDeclaration>();
        private List<ShaderStruct> structs = new List<ShaderStruct>();

        public GLShader(string name, string source)
        {

        }

        public void Init()
        {

        }

        public void Shutdown()
        {

        }

        public override void Bind()
        {
            throw new NotImplementedException();
        }

        public override void Unbind()
        {
            throw new NotImplementedException();
        }

        public override string GetFilePath()
        {
            return path;
        }

        public override string GetName()
        {
            return name;
        }

        public override void SetPSSystemUniformBuffer(byte[] data, uint size, uint slot = 0)
        {
            throw new NotImplementedException();
        }

        public override void SetPSUserUniformBuffer(byte[] data, uint size)
        {
            throw new NotImplementedException();
        }

        public override void SetVSSystemUniformBuffer(byte[] data, uint size, uint slot = 0)
        {
            throw new NotImplementedException();
        }

        public override void SetVSUserUniformBuffer(byte[] data, uint size)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, byte[] data)
        {

        }

        public void ResolveAndSetUniformField(GLShaderUniformDeclaration field, byte[] data, int offset)
        {

        }

        public override List<ShaderUniformBufferDeclaration> GetPSSystemUniforms()
        {
            return VSUniformsBuffers;
        }

        public override List<ShaderUniformBufferDeclaration> GetVSSystemUniforms()
        {
            return PSUniformsBuffers;
        }

        public override ShaderUniformBufferDeclaration GetVSUserUniform()
        {
            return VSUserUniformBuffer;
        }

        public override ShaderUniformBufferDeclaration GetPSUserUniform()
        {
            return PSUserUniformBuffer;
        }

        public override List<ShaderResourceDeclaration> GetResources()
        {
            return resources;
        }

        private static uint Compile(string[] shaders, out GLShaderErrorInfo info)
        {

        }

        private static void PreProcess(string shader, string[] shaders)
        {

        }

        private void Parse(string vertexSource, string fragmentSource)
        {

        }

        private void ParseUniform(string statement, uint shaderType)
        {

        }
    }
}
