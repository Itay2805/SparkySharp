using SP.App;
using SP.Platform.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Graphics.API
{

    public enum RenderAPI
    {
        NONE,
        OPENGL,
        DIRECT3D
    }

    public class Context
    {
        protected static Context context;
        protected static RenderAPI renderAPI;

        public static void Create(WindowProperties properties, object deviceContext)
        {
            switch(GetRenderAPI())
            {
                case RenderAPI.OPENGL: context = new GLContext(properties, deviceContext); break;
            }
        }

        public static RenderAPI GetRenderAPI()
        {
            return renderAPI;
        }

        public static void SetRenderAPI(RenderAPI api)
        {
            renderAPI = api;
        }
    }
}
