using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WIN32
{

    public delegate IntPtr WNDPROC(IntPtr hwnd, uint message, IntPtr wParam, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WNDCLASS
    {
        public uint style;
        public IntPtr lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszMenuName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszClassName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct MSG
    {
        public IntPtr hwnd;
        public uint message;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public POINT pt;
    }

    public class WinUser
    {

        public static readonly IntPtr IDC_ARROW = new IntPtr(32512);
        public static readonly IntPtr IDC_IBEAM = new IntPtr(32513);
        public static readonly IntPtr IDC_WAIT = new IntPtr(32514);
        public static readonly IntPtr IDC_CROSS = new IntPtr(32515);
        public static readonly IntPtr IDC_UPARROW = new IntPtr(32516);
        public static readonly IntPtr IDC_SIZENWSE = new IntPtr(32642);
        public static readonly IntPtr IDC_SIZENESW = new IntPtr(32643);
        public static readonly IntPtr IDC_SIZEWE = new IntPtr(32644);
        public static readonly IntPtr IDC_SIZENS = new IntPtr(32645);
        public static readonly IntPtr IDC_SIZEALL = new IntPtr(32646);
        public static readonly IntPtr IDC_NO = new IntPtr(32648);
        public static readonly IntPtr IDC_HAND = new IntPtr(32649);
        public static readonly IntPtr IDC_APPSTARTING = new IntPtr(32650);
        public static readonly IntPtr IDC_HELP = new IntPtr(32651);

        public static readonly IntPtr IDI_APPLICATION = new IntPtr(32512);
        public static readonly IntPtr IDI_HAND = new IntPtr(32513);
        public static readonly IntPtr IDI_QUESTION = new IntPtr(32514);
        public static readonly IntPtr IDI_EXCLAMATION = new IntPtr(32515);
        public static readonly IntPtr IDI_ASTERISK = new IntPtr(32516);
        public static readonly IntPtr IDI_WINLOGO = new IntPtr(32517);
        public static readonly IntPtr IDI_SHIELD = new IntPtr(32518);
        public static readonly IntPtr IDI_WARNING = IDI_EXCLAMATION;
        public static readonly IntPtr IDI_ERROR = IDI_HAND;
        public static readonly IntPtr IDI_INFORMATION = IDI_ASTERISK;

        public const uint WS_OVERLAPPED = 0x00000000;
        public const uint WS_POPUP = 0x80000000;
        public const uint WS_CHILD = 0x40000000;
        public const uint WS_MINIMIZE = 0x20000000;
        public const uint WS_VISIBLE = 0x10000000;
        public const uint WS_DISABLED = 0x08000000;
        public const uint WS_CLIPSIBLINGS = 0x04000000;
        public const uint WS_CLIPCHILDREN = 0x02000000;
        public const uint WS_MAXIMIZE = 0x01000000;
        public const uint WS_CAPTION = 0x00C00000;
        public const uint WS_BORDER = 0x00800000;
        public const uint WS_DLGFRAME = 0x00400000;
        public const uint WS_VSCROLL = 0x00200000;
        public const uint WS_HSCROLL = 0x00100000;
        public const uint WS_SYSMENU = 0x00080000;
        public const uint WS_THICKFRAME = 0x00040000;
        public const uint WS_GROUP = 0x00020000;
        public const uint WS_TABSTOP = 0x00010000;

        public const uint WS_MINIMIZEBOX = 0x00020000;
        public const uint WS_MAXIMIZEBOX = 0x00010000;
        
        public const uint WS_TILED = WS_OVERLAPPED;
        public const uint WS_ICONIC = WS_MINIMIZE;
        public const uint WS_SIZEBOX = WS_THICKFRAME;

        public const uint WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED |
                                                WS_CAPTION |
                                                WS_SYSMENU |
                                                WS_THICKFRAME |
                                                WS_MINIMIZEBOX |
                                                WS_MAXIMIZEBOX);

        public const uint WS_POPUPWINDOW = (WS_POPUP | 
                                            WS_BORDER | 
                                            WS_SYSMENU);

        public const uint WS_CHILDWINDOW = (WS_CHILD);

        public const uint WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW;

        public const uint WS_EX_DLGMODALFRAME = 0x00000001;
        public const uint WS_EX_NOPARENTNOTIFY = 0x00000004;
        public const uint WS_EX_TOPMOST = 0x00000008;
        public const uint WS_EX_ACCEPTFILES = 0x00000010;
        public const uint WS_EX_TRANSPARENT = 0x00000020;
        public const uint WS_EX_MDICHILD = 0x00000040;
        public const uint WS_EX_TOOLWINDOW = 0x00000080;
        public const uint WS_EX_WINDOWEDGE = 0x00000100;
        public const uint WS_EX_CLIENTEDGE = 0x00000200;
        public const uint WS_EX_CONTEXTHELP = 0x00000400;
        public const uint WS_EX_RIGHT = 0x00001000;
        public const uint WS_EX_LEFT = 0x00000000;
        public const uint WS_EX_RTLREADING = 0x00002000;
        public const uint WS_EX_LTRREADING = 0x00000000;
        public const uint WS_EX_LEFTSCROLLBAR = 0x00004000;
        public const uint WS_EX_RIGHTSCROLLBAR = 0x00000000;

        public const uint WS_EX_CONTROLPARENT = 0x00010000;
        public const uint WS_EX_STATICEDGE = 0x00020000;
        public const uint WS_EX_APPWINDOW = 0x00040000;
        
        public const uint WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
        public const uint WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);

        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;
        public const int SM_CXVSCROLL = 2;
        public const int SM_CYHSCROLL = 3;
        public const int SM_CYCAPTION = 4;
        public const int SM_CXBORDER = 5;
        public const int SM_CYBORDER = 6;
        public const int SM_CXDLGFRAME = 7;
        public const int SM_CYDLGFRAME = 8;
        public const int SM_CYVTHUMB = 9;
        public const int SM_CXHTHUMB = 10;
        public const int SM_CXICON = 11;
        public const int SM_CYICON = 12;
        public const int SM_CXCURSOR = 13;
        public const int SM_CYCURSOR = 14;
        public const int SM_CYMENU = 15;
        public const int SM_CXFULLSCREEN = 16;
        public const int SM_CYFULLSCREEN = 17;
        public const int SM_CYKANJIWINDOW = 18;
        public const int SM_MOUSEPRESENT = 19;
        public const int SM_CYVSCROLL = 20;
        public const int SM_CXHSCROLL = 21;
        public const int SM_DEBUG = 22;
        public const int SM_SWAPBUTTON = 23;
        public const int SM_RESERVED1 = 24;
        public const int SM_RESERVED2 = 25;
        public const int SM_RESERVED3 = 26;
        public const int SM_RESERVED4 = 27;
        public const int SM_CXMIN = 28;
        public const int SM_CYMIN = 29;
        public const int SM_CXSIZE = 30;
        public const int SM_CYSIZE = 31;
        public const int SM_CXFRAME = 32;
        public const int SM_CYFRAME = 33;
        public const int SM_CXMINTRACK = 34;
        public const int SM_CYMINTRACK = 35;
        public const int SM_CXDOUBLECLK = 36;
        public const int SM_CYDOUBLECLK = 37;
        public const int SM_CXICONSPACING = 38;
        public const int SM_CYICONSPACING = 39;
        public const int SM_MENUDROPALIGNMENT = 40;
        public const int SM_PENWINDOWS = 41;
        public const int SM_DBCSENABLED = 42;
        public const int SM_CMOUSEBUTTONS = 43;
        public const int SM_CXFIXEDFRAME = SM_CXDLGFRAME;
        public const int SM_CYFIXEDFRAME = SM_CYDLGFRAME;
        public const int SM_CXSIZEFRAME = SM_CXFRAME;
        public const int SM_CYSIZEFRAME = SM_CYFRAME;
        public const int SM_SECURE = 44;
        public const int SM_CXEDGE = 45;
        public const int SM_CYEDGE = 46;
        public const int SM_CXMINSPACING = 47;
        public const int SM_CYMINSPACING = 48;
        public const int SM_CXSMICON = 49;
        public const int SM_CYSMICON = 50;
        public const int SM_CYSMCAPTION = 51;
        public const int SM_CXSMSIZE = 52;
        public const int SM_CYSMSIZE = 53;
        public const int SM_CXMENUSIZE = 54;
        public const int SM_CYMENUSIZE = 55;
        public const int SM_ARRANGE = 56;
        public const int SM_CXMINIMIZED = 57;
        public const int SM_CYMINIMIZED = 58;
        public const int SM_CXMAXTRACK = 59;
        public const int SM_CYMAXTRACK = 60;
        public const int SM_CXMAXIMIZED = 61;
        public const int SM_CYMAXIMIZED = 62;
        public const int SM_NETWORK = 63;
        public const int SM_CLEANBOOT = 67;
        public const int SM_CXDRAG = 68;
        public const int SM_CYDRAG = 69;
        public const int SM_SHOWSOUNDS = 70;
        public const int SM_CXMENUCHECK = 71;
        public const int SM_CYMENUCHECK = 72;
        public const int SM_SLOWMACHINE = 73;
        public const int SM_MIDEASTENABLED = 74;
        public const int SM_MOUSEWHEELPRESENT = 75;
        public const int SM_XVIRTUALSCREEN = 76;
        public const int SM_YVIRTUALSCREEN = 77;
        public const int SM_CXVIRTUALSCREEN = 78;
        public const int SM_CYVIRTUALSCREEN = 79;
        public const int SM_CMONITORS = 80;
        public const int SM_SAMEDISPLAYFORMAT = 81;
        public const int SM_IMMENABLED = 82;
        public const int SM_CXFOCUSBORDER = 83;
        public const int SM_CYFOCUSBORDER = 84;
        public const int SM_TABLETPC = 86;
        public const int SM_MEDIACENTER = 87;
        public const int SM_STARTER = 88;
        public const int SM_SERVERR2 = 89;
        public const int SM_MOUSEHORIZONTALWHEELPRESENT = 91;
        public const int SM_CXPADDEDBORDER = 92;
        public const int SM_DIGITIZER = 94;
        public const int SM_MAXIMUMTOUCHES = 95;

        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_NORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_FORCEMINIMIZE = 11;
        public const int SW_MAX = 11;

        public const int CS_VREDRAW = 0x0001;
        public const int CS_HREDRAW = 0x0002;
        public const int CS_DBLCLKS = 0x0008;
        public const int CS_OWNDC = 0x0020;
        public const int CS_CLASSDC = 0x0040;
        public const int CS_PARENTDC = 0x0080;
        public const int CS_NOCLOSE = 0x0200;
        public const int CS_SAVEBITS = 0x0800;
        public const int CS_BYTEALIGNCLIENT = 0x1000;
        public const int CS_BYTEALIGNWINDOW = 0x2000;
        public const int CS_GLOBALCLASS = 0x4000;
        public const int CS_IME = 0x00010000;
        public const int CS_DROPSHADOW = 0x00020000;

        public const uint WM_NULL = 0x0000;
        public const uint WM_CREATE = 0x0001;
        public const uint WM_DESTROY = 0x0002;
        public const uint WM_MOVE = 0x0003;
        public const uint WM_SIZE = 0x0005;

        public const uint WM_ACTIVATE = 0x0006;
        public const uint WA_INACTIVE = 0;
        public const uint WA_ACTIVE = 1;
        public const uint WA_CLICKACTIVE = 2;

        public const uint WM_SETFOCUS = 0x0007;
        public const uint WM_KILLFOCUS = 0x0008;
        public const uint WM_ENABLE = 0x000A;
        public const uint WM_SETREDRAW = 0x000B;
        public const uint WM_SETTEXT = 0x000C;
        public const uint WM_GETTEXT = 0x000D;
        public const uint WM_GETTEXTLENGTH = 0x000E;
        public const uint WM_PAINT = 0x000F;
        public const uint WM_CLOSE = 0x0010;
        public const uint WM_QUERYENDSESSION = 0x0011;
        public const uint WM_QUERYOPEN = 0x0013;
        public const uint WM_ENDSESSION = 0x0016;
        public const uint WM_QUIT = 0x0012;
        public const uint WM_ERASEBKGND = 0x0014;
        public const uint WM_SYSCOLORCHANGE = 0x0015;
        public const uint WM_SHOWWINDOW = 0x0018;
        public const uint WM_WININICHANGE = 0x001A;
        public const uint WM_SETTINGCHANGE = WM_WININICHANGE;
            
        public const uint WM_DEVMODECHANGE = 0x001B;
        public const uint WM_ACTIVATEAPP = 0x001C;
        public const uint WM_FONTCHANGE = 0x001D;
        public const uint WM_TIMECHANGE = 0x001E;
        public const uint WM_CANCELMODE = 0x001F;
        public const uint WM_SETCURSOR = 0x0020;
        public const uint WM_MOUSEACTIVATE = 0x0021;
        public const uint WM_CHILDACTIVATE = 0x0022;
        public const uint WM_QUEUESYNC = 0x0023;

        public const uint WM_GETMINMAXINFO = 0x0024;

        public const uint WM_INITDIALOG = 0x0110;
        public const uint WM_COMMAND = 0x0111;
        public const uint WM_SYSCOMMAND = 0x0112;
        public const uint WM_TIMER = 0x0113;
        public const uint WM_HSCROLL = 0x0114;
        public const uint WM_VSCROLL = 0x0115;
        public const uint WM_INITMENU = 0x0116;
        public const uint WM_INITMENUPOPUP = 0x0117;

        public const int SC_SIZE = 0xF000;
        public const int SC_MOVE = 0xF010;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_NEXTWINDOW = 0xF040;
        public const int SC_PREVWINDOW = 0xF050;
        public const int SC_CLOSE = 0xF060;
        public const int SC_VSCROLL = 0xF070;
        public const int SC_HSCROLL = 0xF080;
        public const int SC_MOUSEMENU = 0xF090;
        public const int SC_KEYMENU = 0xF100;
        public const int SC_ARRANGE = 0xF110;
        public const int SC_RESTORE = 0xF120;
        public const int SC_TASKLIST = 0xF130;
        public const int SC_SCREENSAVE = 0xF140;
        public const int SC_HOTKEY = 0xF150;
        public const int SC_DEFAULT = 0xF160;
        public const int SC_MONITORPOWER = 0xF170;
        public const int SC_CONTEXTHELP = 0xF180;
        public const int SC_SEPARATOR = 0xF00F;

        public const uint PM_NOREMOVE = 0x0000;
        public const uint PM_REMOVE = 0x0001;
        public const uint PM_NOYIELD = 0x0002;

        public const uint WM_KEYFIRST = 0x0100;
        public const uint WM_KEYDOWN = 0x0100;
        public const uint WM_KEYUP = 0x0101;
        public const uint WM_CHAR = 0x0102;
        public const uint WM_DEADCHAR = 0x0103;
        public const uint WM_SYSKEYDOWN = 0x0104;
        public const uint WM_SYSKEYUP = 0x0105;
        public const uint WM_SYSCHAR = 0x0106;
        public const uint WM_SYSDEADCHAR = 0x0107;
        public const uint WM_UNICHAR = 0x0109;
        public const uint WM_KEYLAST = 0x0109;
        public const uint UNICODE_NOCHAR = 0xFFFF;

        public const uint WM_MOUSEFIRST = 0x0200;
        public const uint WM_MOUSEMOVE = 0x0200;
        public const uint WM_LBUTTONDOWN = 0x0201;
        public const uint WM_LBUTTONUP = 0x0202;
        public const uint WM_LBUTTONDBLCLK = 0x0203;
        public const uint WM_RBUTTONDOWN = 0x0204;
        public const uint WM_RBUTTONUP = 0x0205;
        public const uint WM_RBUTTONDBLCLK = 0x0206;
        public const uint WM_MBUTTONDOWN = 0x0207;
        public const uint WM_MBUTTONUP = 0x0208;
        public const uint WM_MBUTTONDBLCLK = 0x0209;
        public const uint WM_MOUSEWHEEL = 0x020A
            ;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr LoadCursorW(
            [In] IntPtr hInstance,
            [In] IntPtr lpCursorName
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr LoadIconW(
            [In] IntPtr hInstance,
            [In] IntPtr lpIconName
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern ushort RegisterClassW(
            [In] ref WNDCLASS lpWndClass
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool AdjustWindowRectEx(
            [In, Out] ref RECT lpRect,
            [In] uint dwStyle,
            [In] bool bMenu,
            [In] uint dwExStyle
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr CreateWindowExW(
            [In] uint dwExStyle,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpClassName,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpWindowName,
            [In] uint dwStyle,
            [In] int X,
            [In] int Y,
            [In] int nWidth,
            [In] int nHeight,
            [In] IntPtr hWndParent,
            [In] IntPtr hMenu,
            [In] IntPtr hInstance,
            [In] IntPtr lpParam
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetSystemMetrics(
            [In] int nIndex
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDC(
            [In] IntPtr hWnd
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ShowWindow(
            [In] IntPtr hWnd,
            [In] int nCmdShow
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetFocus(
            [In] IntPtr hWnd
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr DefWindowProcW(
            [In] IntPtr hWnd,
            [In] uint Msg,
            [In] IntPtr wParam,
            [In] IntPtr lParam
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void PostQuitMessage(
            [In] int nExitCode
            );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PeekMessageW(
            [In, Out] ref MSG lpMsg,
            [In] IntPtr hWnd,
            [In] uint wMsgFilterMin,
            [In] uint wMsgFilterMax,
            [In] uint wRemoveMsg
            );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool TranslateMessage(
            [In] ref MSG lpMsg
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr DispatchMessageW(
            [In] ref MSG lpMsg
            );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowTextW(
            [In] IntPtr lpMsg,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpString
            );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetCursorPos(
            [In, Out] ref POINT lpPoint
            );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ClientToScreen(
            [In] IntPtr hWnd,
            [In, Out] ref POINT lpPoint
            );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ScreenToClient(
            [In] IntPtr hWnd,
            [In, Out] ref POINT lpPoint
            );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetCursorPos(
            [In] int X,
            [In] int Y
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetCursor(
            [In] IntPtr hCursor
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int ShowCursor(
            [In] bool bShow
            );
        
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetCapture(
            [In] IntPtr hWnd
            );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ReleaseCapture();

    }

}
