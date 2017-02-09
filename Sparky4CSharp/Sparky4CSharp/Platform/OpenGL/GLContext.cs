using OpenGL;
using SP.App;
using SP.Graphics.API;
using SP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIN32;

namespace SP.Platform.OpenGL
{
    public class GLContext : Context
    {

        private static IntPtr hDc;

        public GLContext(WindowProperties properties, object deviceContext)
        {
            hDc = WinUser.GetDC((IntPtr)deviceContext);
            IntPtr hrc = WinGDI.wglCreateContext(hDc);
            if(hrc.ToInt32() != 0)
            {
                if(!WinGDI.wglMakeCurrent(hDc, hrc))
                {
                    Log.Error("Failed setting OpenGL context!");
                    Log.Assert(() => false);
                }
            }
            else
            {
                Log.Error("Failed creating OpenGL context!");
                Log.Assert(() => false);
            }
        }

        public void Present()
        {
            WinGDI.SwapBuffers(hDc);
        }

        public static GLContext Get()
        {
            return (GLContext) context;
        }

    }
}
