using SP.Events;
using SP.Utils;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP.Graphics.API;
using WIN32;
using SP.Sound;
using SP.Graphics;

namespace SP.App
{

    public delegate void WindowEventCallback(Event e);

    public struct WindowProperties
    {
        public uint width, height;
        public bool fullscreen;
        public bool vsync;
    }

    public class Window
    {

        private static Dictionary<object, Window> handles = new Dictionary<object, Window>();

        private string title;
        private WindowProperties properties;
        private bool closed;
        private object handle;

        private bool vsync;
        private WindowEventCallback eventCallback;
        private InputManager inputManager;

        private IntPtr hInstance;
        private IntPtr hDc;
        private IntPtr hWnd;
        private WNDPROC proc;

        public Window(string title, WindowProperties properties)
        {
            this.title = title;
            this.properties = properties;
            this.handle = null;
            this.closed = false;
            this.eventCallback = null;

            if(!Init())
            {
                Log.Error("Failed base Window Initialization!");
                return;
            }

            // Font Manager

            SoundManager.Init();
            inputManager = new InputManager();
        }

        public void Clear()
        {
            Renderer.Clear((uint)RendererBufferType.COLOR | (uint)RendererBufferType.DEPTH);
        }

        public void Update()
        {
            PlatformUpdate();
            SoundManager.Update();
        }

        public bool Closed()
        {
            return closed;
        }

        public void SetTitle(string title)
        {
            this.title = title + "  |  " + Application.GetApplication().GetBuildConfiguration() + " " + Application.GetApplication().GetPlatform() + "  |  Renderer: " + Renderer.GetTitle();
            WinUser.SetWindowTextW(hWnd, this.title);
        }

        public uint GetWidth()
        {
            return properties.width;
        }

        public uint GetHeight()
        {
            return properties.height;
        }

        public void SetVsync(bool enabled)
        {
            vsync = enabled;
        }

        public bool IsVsync()
        {
            return vsync;
        }

        public InputManager GetInputManager()
        {
            return inputManager;
        }

        public void SetEventCallback(WindowEventCallback callback)
        {
            this.eventCallback = callback;
            inputManager.SetEventCallback(eventCallback);
        }

        private bool Init()
        {
            if(!PlatformInit())
            {
                Log.Fatal("Failed to initialize platform!");
                return false;
            }

            Renderer.Init();

            SetTitle(title);
            return true;
        }

        private static PIXELFORMATDESCRIPTOR GetPixelFormat()
        {
            PIXELFORMATDESCRIPTOR result = new PIXELFORMATDESCRIPTOR();
            result.nSize = (ushort) Marshal.SizeOf(result);
            result.nVersion = 1;
            result.dwFlags = WinGDI.PFD_DRAW_TO_WINDOW | WinGDI.PFD_SUPPORT_OPENGL | WinGDI.PFD_DOUBLEBUFFER;
            result.iPixelType = WinGDI.PFD_TYPE_RGBA;
            result.cColorBits = 32;
            result.cDepthBits = 24;
            result.cStencilBits = 8;
            result.cAuxBuffers = 0;
            result.iLayerType = WinGDI.PFD_MAIN_PLANE;
            return result;
        }

        private bool PlatformInit()
        {
            WNDCLASS winClass = new WNDCLASS();
            winClass.style = WinUser.CS_HREDRAW | WinUser.CS_VREDRAW | WinUser.CS_OWNDC;
            proc = WndProc;
            winClass.lpfnWndProc = Marshal.GetFunctionPointerForDelegate(proc);
            winClass.lpszClassName = "Sparky Win32 Window";
            winClass.hCursor = WinUser.LoadCursorW(IntPtr.Zero, WinUser.IDC_ARROW);
            winClass.hIcon = WinUser.LoadIconW(IntPtr.Zero, WinUser.IDI_WINLOGO);

            if (WinUser.RegisterClassW(ref winClass) == 0)
            {
                Log.Error("Could not register Win32 class!");
                return false;
            }

            RECT size = new RECT(0, 0, (int)properties.width, (int)properties.height);
            WinUser.AdjustWindowRectEx(ref size, WinUser.WS_OVERLAPPEDWINDOW | WinUser.WS_CLIPSIBLINGS | WinUser.WS_CLIPCHILDREN, false, WinUser.WS_EX_APPWINDOW | WinUser.WS_EX_WINDOWEDGE);

            hWnd = WinUser.CreateWindowExW(WinUser.WS_EX_APPWINDOW | WinUser.WS_EX_WINDOWEDGE,
                                                         winClass.lpszClassName, title,
                                                            WinUser.WS_OVERLAPPEDWINDOW | WinUser.WS_CLIPSIBLINGS | WinUser.WS_CLIPCHILDREN,
                                                            (int)(WinUser.GetSystemMetrics(WinUser.SM_CXSCREEN) / 2 - properties.width / 2),
                                                            (int)(WinUser.GetSystemMetrics(WinUser.SM_CYSCREEN) / 2 - properties.height / 2),
                                                            (int) (size.right + (-size.left)), (int) (size.bottom + (-size.top)), IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero
                                                            );
            if(hWnd.ToInt32() == 0)
            {
                Log.Error("Could not create window!");
                return false;
            }

            RegisterWindowClass(hWnd, this);

            hDc = WinUser.GetDC(hWnd);
            PIXELFORMATDESCRIPTOR pfd = GetPixelFormat();
            int pixelFormat = WinGDI.ChoosePixelFormat(hDc, ref pfd);
            if(pixelFormat != 0)
            {
                if(!WinGDI.SetPixelFormat(hDc, pixelFormat, ref pfd))
                {
                    Log.Error("Failed setting pixel format!");
                    return false;
                }
            }
            else
            {
                Log.Error("Failed choosing pixel format!");
                return false;
            }

            Context.Create(properties, hWnd);

            WinUser.ShowWindow(hWnd, WinUser.SW_SHOW);
            WinUser.SetFocus(hWnd);
            
            return true;
        }

        private void PlatformUpdate()
        {
            MSG message = new MSG();
            while(WinUser.PeekMessageW(ref message, IntPtr.Zero, 0, 0, WinUser.PM_REMOVE))
            {
                if(message.message == WinUser.WM_QUIT)
                {
                    closed = true;
                    return;
                }

                WinUser.TranslateMessage(ref message);
                WinUser.DispatchMessageW(ref message);
            }

            inputManager.PlatformUpdate();
            Renderer.Present();
        }

        public static void RegisterWindowClass(object handle, Window window)
        {
            handles[handle] = window;
        }

        public static Window GetWindowClass(object handle = null)
        {
            if (handle == null)
            {
                if(handles.Count == 0)
                {
                    return null;
                }
                return handles.First().Value;
            }
            return handles[handle];
        }
        
        public static void ResizeCallback(Window window, int width, int height)
        {
            window.properties.width = (uint)width;
            window.properties.height = (uint)height;
            // SetScale FontManager

            window.eventCallback?.Invoke(new ResizeWindowEvent((uint)width, (uint)height));
        }

        public static void FocusCallback(Window window, bool focused)
        {
            if(!focused)
            {
                window.GetInputManager().ClearKeys();
                window.GetInputManager().ClearMouseButtons();
            }
        }

        public static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {


            IntPtr result = new IntPtr();
            Window window = Window.GetWindowClass();
            if (window == null)
                return WinUser.DefWindowProcW(hWnd, msg, wParam, lParam);

            InputManager inputManager = window.GetInputManager();
            switch(msg)
            {
                case WinUser.WM_ACTIVATE:
                    {
                        if(wParam.ToInt32().HighWord() != 0)
                        {
                            // active
                        }else {
                            // inactive
                        }
                        return new IntPtr(0);
                    }
                case WinUser.WM_SYSCOMMAND:
                    {
                        switch(wParam.ToInt32())
                        {
                            case WinUser.SC_SCREENSAVE:
                            case WinUser.SC_MONITORPOWER:
                                return new IntPtr(0);
                        }
                        result = WinUser.DefWindowProcW(hWnd, msg, wParam, lParam);
                    }
                    break;
                case WinUser.WM_SETFOCUS:
                    FocusCallback(window, true);
                    break;
                case WinUser.WM_KILLFOCUS:
                    FocusCallback(window, false);
                    break;
                case WinUser.WM_CLOSE:
                case WinUser.WM_DESTROY:
                    WinUser.PostQuitMessage(0);
                    break;
                case WinUser.WM_KEYDOWN:
                case WinUser.WM_KEYUP:
                case WinUser.WM_SYSKEYDOWN:
                case WinUser.WM_SYSKEYUP:
                    InputManager.KeyCallback(inputManager, (uint)lParam.ToInt32(), (uint)wParam.ToInt32(), msg);
                    break;
                case WinUser.WM_LBUTTONDOWN:
                case WinUser.WM_LBUTTONUP:
                case WinUser.WM_RBUTTONDOWN:
                case WinUser.WM_RBUTTONUP:
                case WinUser.WM_MBUTTONDOWN:
                case WinUser.WM_MBUTTONUP:
                    InputManager.MouseButtonCallback(hWnd, inputManager, msg, (uint)lParam.ToInt32().LowWord(), (uint)lParam.ToInt32().LowWord());
                    break;
                case WinUser.WM_SIZE:
                    ResizeCallback(window, lParam.ToInt32().LowWord(), lParam.ToInt32().HighWord());
                    break;
                default:
                    result = WinUser.DefWindowProcW(hWnd, msg, wParam, lParam);
                    break;
            }
            return result;
        }

        public IntPtr GetHWND()
        {
            return hWnd;
        }

    }

}