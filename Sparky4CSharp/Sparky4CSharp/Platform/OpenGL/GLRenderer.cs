using OpenGL;
using SP.Graphics;
using SP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Platform.OpenGL
{
    public class GLRenderer : Renderer
    {

        private GLContext context;
        private string rendererTitle;

        public GLRenderer()
        {
            context = GLContext.Get();
        }

        protected override void ClearInternal(uint buffer)
        {
            Gl.Clear(SPRendererBufferToGL(buffer));
        }

        protected override string GetTitleInternal()
        {
            return rendererTitle;
        }

        protected override void InitInternal()
        {
            SetDepthTesting(true);
            SetBlend(true);
            SetBlendFunction(RendererBlendFunction.SOURCE_ALPHA, RendererBlendFunction.ONE_MINUS_SOURCE_ALPHA);

            Log.Warn("----------------------------------");
            Log.Warn(" OpenGL:");
            Log.Warn("    ", Gl.GetString(StringName.Version));
            Log.Warn("    ", Gl.GetString(StringName.Vendor));
            Log.Warn("    ", Gl.GetString(StringName.Renderer));
            Log.Warn("----------------------------------");

            Gl.Enable(EnableCap.CullFace);
            Gl.FrontFace(FrontFaceDirection.Ccw);
            Gl.CullFace(CullFaceMode.Back);

            rendererTitle = "OpenGL";
        }

        protected override void PresentInternal()
        {
            context.Present();
        }

        protected override void SetBlendEquationInternal(RendererBlendFunction blendEquation)
        {
            throw new NotImplementedException();
        }

        protected override void SetBlendFunctionInternal(RendererBlendFunction source, RendererBlendFunction destination)
        {
            Gl.BlendFunc(SPRendererBlendFunctionToGLSrc(source), SPRendererBlendFunctionToGLDest(destination));
        }

        protected override void SetBlendInternal(bool enabled)
        {
            if(enabled)
            {
                Gl.Enable(EnableCap.Blend);
            }else
            {
                Gl.Disable(EnableCap.Blend);
            }
        }

        protected override void SetDepthTestingInternal(bool enabled)
        {
            if (enabled)
            {
                Gl.Enable(EnableCap.DepthTest);
            }
            else
            {
                Gl.Disable(EnableCap.DepthTest);
            }
        }

        protected override void SetViewportInternal(int x, int y, int width, int height)
        {
            Gl.Viewport(x, y, width, height);
        }

        private static ClearBufferMask SPRendererBufferToGL(uint buffer)
        {
            uint result = 0;
            if ((buffer & (uint)RendererBufferType.COLOR) != 0)
                result |= (uint)ClearBufferMask.ColorBufferBit;
            if ((buffer & (uint)RendererBufferType.DEPTH) != 0)
                result |= (uint)ClearBufferMask.DepthBufferBit;
            if ((buffer & (uint)RendererBufferType.STENCIL) != 0)
                result |= (uint)ClearBufferMask.StencilBufferBit;
            return (ClearBufferMask)result;
        }

        private static BlendingFactorSrc SPRendererBlendFunctionToGLSrc(RendererBlendFunction function)
        {
            switch(function)
            {
                case RendererBlendFunction.ZERO: return BlendingFactorSrc.Zero;
                case RendererBlendFunction.ONE: return BlendingFactorSrc.One;
                case RendererBlendFunction.SOURCE_ALPHA: return BlendingFactorSrc.SrcAlpha;
                case RendererBlendFunction.DESTINATION_ALPHA: return BlendingFactorSrc.DstAlpha;
                case RendererBlendFunction.ONE_MINUS_SOURCE_ALPHA: return BlendingFactorSrc.OneMinusSrcAlpha;
            }
            return 0;
        }

        private static BlendingFactorDest SPRendererBlendFunctionToGLDest(RendererBlendFunction function)
        {
            switch (function)
            {
                case RendererBlendFunction.ZERO: return BlendingFactorDest.Zero;
                case RendererBlendFunction.ONE: return BlendingFactorDest.One;
                case RendererBlendFunction.SOURCE_ALPHA: return BlendingFactorDest.SrcAlpha;
                case RendererBlendFunction.DESTINATION_ALPHA: return BlendingFactorDest.DstAlpha;
                case RendererBlendFunction.ONE_MINUS_SOURCE_ALPHA: return BlendingFactorDest.OneMinusSrcAlpha;
            }
            return 0;
        }

    }
}
