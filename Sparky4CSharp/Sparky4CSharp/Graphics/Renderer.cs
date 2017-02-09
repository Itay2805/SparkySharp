using SP.Graphics.API;
using SP.Platform.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Graphics
{
    public enum RendererBufferType : uint
    {
        NONE = 0,
        COLOR = 1 << 0,
        DEPTH = 1 << 1,
        STENCIL = 1 << 2
    }

    public enum RendererBlendFunction
    {
        NONE,
        ZERO,
        ONE,
        SOURCE_ALPHA,
        DESTINATION_ALPHA,
        ONE_MINUS_SOURCE_ALPHA
    }

    public abstract class Renderer
    {

        private static Renderer instance;

        protected abstract void InitInternal();

        protected abstract void ClearInternal(uint buffer);
        protected abstract void PresentInternal();

        protected abstract void SetDepthTestingInternal(bool enabled);
        protected abstract void SetBlendInternal(bool enabled);
        protected abstract void SetViewportInternal(int x, int y, int width, int height);

        protected abstract void SetBlendFunctionInternal(RendererBlendFunction source, RendererBlendFunction destination);
        protected abstract void SetBlendEquationInternal(RendererBlendFunction blendEquation);

        protected abstract string GetTitleInternal();

        public static void Clear(uint buffer)
        {
            instance.ClearInternal(buffer);
        }

        public static void Present()
        {
            instance.PresentInternal();
        }

        public static void SetDepthTesting(bool enabled)
        {
            instance.SetDepthTestingInternal(enabled);
        }

        public static void SetBlend(bool enabled)
        {
            instance.SetBlendInternal(enabled);
        }

        public static void SetViewport(int x, int y, int width, int height)
        {
            instance.SetViewportInternal(x, y, width, height);
        }

        public static void SetBlendFunction(RendererBlendFunction source, RendererBlendFunction destination)
        {
            instance.SetBlendFunctionInternal(source, destination);
        }

        public static void SetBlendEquation(RendererBlendFunction blendEquation)
        {
            instance.SetBlendEquationInternal(blendEquation);
        }

        public static string GetTitle()
        {
            return instance.GetTitleInternal();
        }

        public static void Init()
        {
            switch(Context.GetRenderAPI())
            {
                case RenderAPI.OPENGL: instance = new GLRenderer(); break;
            }
            instance.InitInternal();
        }

        public static Renderer GetRenderer()
        {
            return instance;
        }

    }
}
