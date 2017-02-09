using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WIN32
{

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct PIXELFORMATDESCRIPTOR
    {
        public ushort nSize;
        public ushort nVersion;
        public uint dwFlags;
        public byte iPixelType;
        public byte cColorBits;
        public byte cRedBits;
        public byte cRedShift;
        public byte cGreenBits;
        public byte cGreenShift;
        public byte cBlueBits;
        public byte cBlueShift;
        public byte cAlphaBits;
        public byte cAlphaShift;
        public byte cAccumBits;
        public byte cAccumRedBits;
        public byte cAccumGreenBits;
        public byte cAccumBlueBits;
        public byte cAccumAlphaBits;
        public byte cDepthBits;
        public byte cStencilBits;
        public byte cAuxBuffers;
        public byte iLayerType;
        public byte bReserved;
        public uint dwLayerMask;
        public uint dwVisibleMask;
        public uint dwDamageMask;
    }

    public class WinGDI
    {

        public const uint PFD_DOUBLEBUFFER = 0x00000001;
        public const uint PFD_STEREO = 0x00000002;
        public const uint PFD_DRAW_TO_WINDOW = 0x00000004;
        public const uint PFD_DRAW_TO_BITMAP = 0x00000008;
        public const uint PFD_SUPPORT_GDI = 0x00000010;
        public const uint PFD_SUPPORT_OPENGL = 0x00000020;
        public const uint PFD_GENERIC_FORMAT = 0x00000040;
        public const uint PFD_NEED_PALETTE = 0x00000080;
        public const uint PFD_NEED_SYSTEM_PALETTE = 0x00000100;
        public const uint PFD_SWAP_EXCHANGE = 0x00000200;
        public const uint PFD_SWAP_COPY = 0x00000400;
        public const uint PFD_SWAP_LAYER_BUFFERS = 0x00000800;
        public const uint PFD_GENERIC_ACCELERATED = 0x00001000;
        public const uint PFD_SUPPORT_DIRECTDRAW = 0x00002000;
        public const uint PFD_DIRECT3D_ACCELERATED = 0x00004000;
        public const uint PFD_SUPPORT_COMPOSITION = 0x00008000;

        public const byte PFD_TYPE_RGBA = 0;
        public const byte PFD_TYPE_COLORINDEX = 1;

        public const byte PFD_MAIN_PLANE = 0;
        public const byte PFD_OVERLAY_PLANE = 1;

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern int ChoosePixelFormat(
            [In] IntPtr hDc,
            [In] ref PIXELFORMATDESCRIPTOR ppfd
            );

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool SetPixelFormat(
            [In] IntPtr hDc,
            [In] int format,
            [In] ref PIXELFORMATDESCRIPTOR ppfd
            );

        [DllImport("opengl32.dll", SetLastError = true)]
        public static extern IntPtr wglCreateContext(
            IntPtr hDc
            );

        [DllImport("opengl32.dll", SetLastError = true)]
        public static extern bool wglMakeCurrent(
            IntPtr hDc,
            IntPtr hrc
            );

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool SwapBuffers(
            IntPtr hDc
            );


    }
}
