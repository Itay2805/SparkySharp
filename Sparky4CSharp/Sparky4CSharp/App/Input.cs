using SP.Events;
using SP.Maths;
using SP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIN32;

namespace SP.App
{

    public class InputManager
    {

        public const int MAX_KEYS = 1024;
        public const int MAX_BUTTONS = 32;

        private bool[] keyState = new bool[MAX_KEYS];
        private bool[] lastKeyState = new bool[MAX_KEYS];

        private bool[] mouseButtons = new bool[MAX_BUTTONS];
        private bool[] mouseState = new bool[MAX_BUTTONS];
        private bool[] mouseClicked = new bool[MAX_BUTTONS];
        private bool mouseGrabbed;
        private uint keyModifiers;

        private Vector2 mousePosition;
        private WindowEventCallback eventCallback;

        public InputManager()
        {
            ClearKeys();
            ClearMouseButtons();

            mouseGrabbed = true;


        }

        public void SetEventCallback(WindowEventCallback eventCallback)
        {
            this.eventCallback = eventCallback;
        }

        public void Update()
        {
            for(int i = 0; i < MAX_BUTTONS; i++)
            {
                mouseClicked[i] = mouseButtons[i] && !mouseState[i];
            }

            Array.Copy(keyState, lastKeyState, MAX_KEYS);
            Array.Copy(mouseButtons, mouseState, MAX_BUTTONS);

        }

        public void PlatformUpdate()
        {
            POINT mouse = new POINT();
            WinUser.GetCursorPos(ref mouse);
            WinUser.ScreenToClient(Window.GetWindowClass().GetHWND(), ref mouse);

            Vector2 mousePos = new Vector2(mouse.x, mouse.y);
            if(mousePos != mousePosition)
            {
                // TPDP Put left 
                eventCallback(new MouseMovedEvent(mousePos.x, mousePos.y, mouseButtons[0]));
                mousePosition = mousePos;
            }
        }

        public bool IsKeyPressed(uint keycode)
        {
            if (keycode >= MAX_KEYS)
                return false;

            return keyState[keycode];
        }

        public bool IsMouseButtonPressed(uint button)
        {
            if (button >= MAX_BUTTONS)
                return false;

            return mouseButtons[button];
        }

        public bool IsMouseButtonClicked(uint button)
        {
            if (button >= MAX_BUTTONS)
                return false;

            return mouseClicked[button];
        }

        public Vector2 GetMousePosition()
        {
            return mousePosition;
        }

        public void SetMousePosition(Vector2 position)
        {
            POINT pt = new POINT { x = (int) position.x, y = (int) position.y };
            WinUser.ClientToScreen(Window.GetWindowClass().GetHWND(), ref pt);
            WinUser.SetCursorPos(pt.x, pt.y);
        }

        public bool IsMouseGrabbed()
        {
            return mouseGrabbed;
        }

        public void SetMouseGrabbed(bool grabbed)
        {
            mouseGrabbed = grabbed;
        }

        public void SetMouseCursor(uint cursor)
        {
            if(cursor == Input.SP_NO_CURSOR)
            {
                WinUser.SetCursor(new IntPtr(Input.SP_NO_CURSOR));
                while (WinUser.ShowCursor(false) >= 0) ;
            }else
            {
                WinUser.SetCursor(WinUser.LoadCursorW(IntPtr.Zero, WinUser.IDC_ARROW));
                WinUser.ShowCursor(true);
            }
        }

        public void ClearKeys()
        {
            for (int i = 0; i < MAX_KEYS; i++)
            {
                keyState[i] = false;
                lastKeyState[i] = false;
            }
            keyModifiers = 0;
        }

        public void ClearMouseButtons()
        {
            for (int i = 0; i < MAX_BUTTONS; i++)
            {
                mouseButtons[i] = false;
                mouseState[i] = false;
                mouseClicked[i] = false;
            }
        }

        public static void KeyCallback(InputManager inputManager, uint flags, uint key, uint message)
        {
            bool pressed = message == WinUser.WM_KEYDOWN || message == WinUser.WM_SYSKEYDOWN;
            inputManager.keyState[key] = pressed;

            uint repeat = ((flags >> 30) & 1);

            uint modifier = 0;
            switch(key)
            {
                case Input.SP_KEY_CONTROL:
                    modifier = Input.SP_MODIFIER_LEFT_CONTROL;
                    break;
                case Input.SP_KEY_ALT:
                    modifier = Input.SP_MODIFIER_LEFT_ALT;
                    break;
                case Input.SP_KEY_SHIFT:
                    modifier = Input.SP_MODIFIER_LEFT_SHIFT;
                    break;
            }
            if (pressed)
                inputManager.keyModifiers |= modifier;
            else
                inputManager.keyModifiers &= ~(modifier);

            if (pressed)
                inputManager.eventCallback(new KeyPressedEvent(key, repeat, inputManager.keyModifiers));
            else
                inputManager.eventCallback(new KeyReleasedEvent(key));
        }

        public static void MouseButtonCallback(IntPtr hWnd, InputManager inputManager, uint button, uint x, uint y)
        {
            bool down = false;

            switch(button)
            {
                case WinUser.WM_LBUTTONDOWN:
                    WinUser.SetCapture(hWnd);
                    button = Input.SP_MOUSE_LEFT;
                    down = true;
                    break;
                case WinUser.WM_LBUTTONUP:
                    WinUser.ReleaseCapture();
                    button = Input.SP_MOUSE_LEFT;
                    down = false;
                    break;
                case WinUser.WM_RBUTTONDOWN:
                    WinUser.SetCapture(hWnd);
                    button = Input.SP_MOUSE_RIGHT;
                    down = true;
                    break;
                case WinUser.WM_RBUTTONUP:
                    WinUser.ReleaseCapture();
                    button = Input.SP_MOUSE_RIGHT;
                    down = false;
                    break;
                case WinUser.WM_MBUTTONDOWN:
                    WinUser.SetCapture(hWnd);
                    button = Input.SP_MOUSE_MIDDLE;
                    down = true;
                    break;
                case WinUser.WM_MBUTTONUP:
                    WinUser.ReleaseCapture();
                    button = Input.SP_MOUSE_MIDDLE;
                    down = false;
                    break;
            }

            inputManager.mouseButtons[button] = down;
            inputManager.mousePosition.x = x;
            inputManager.mousePosition.y = y;

            Log.Assert(() => inputManager.eventCallback != null);

            if (down)
                inputManager.eventCallback(new MousePressedEvent(button, x, y));
            else
                inputManager.eventCallback(new MouseReleasedEvent(button, x, y));
        }

    }

    class Input
    {

        public const uint SP_MOUSE_LEFT = 0x00;
        public const uint SP_MOUSE_MIDDLE = 0x01;
        public const uint SP_MOUSE_RIGHT = 0x02;

        public const uint SP_NO_CURSOR = 0;

        public const uint SP_MODIFIER_LEFT_CONTROL = 1 + 0;
        public const uint SP_MODIFIER_LEFT_ALT = 1 + 1;
        public const uint SP_MODIFIER_LEFT_SHIFT = 1 + 2;
        public const uint SP_MODIFIER_RIGHT_CONTROL = 1 + 3;
        public const uint SP_MODIFIER_RIGHT_ALT = 1 + 4;
        public const uint SP_MODIFIER_RIGHT_SHIFT = 1 + 5;

        public const uint SP_KEY_TAB = 0x09;

        public const uint SP_KEY_0 = 0x30;
        public const uint SP_KEY_1 = 0x31;
        public const uint SP_KEY_2 = 0x32;
        public const uint SP_KEY_3 = 0x33;
        public const uint SP_KEY_4 = 0x34;
        public const uint SP_KEY_5 = 0x35;
        public const uint SP_KEY_6 = 0x36;
        public const uint SP_KEY_7 = 0x37;
        public const uint SP_KEY_8 = 0x38;
        public const uint SP_KEY_9 = 0x39;

        public const uint SP_KEY_A = 0x41;
        public const uint SP_KEY_B = 0x42;
        public const uint SP_KEY_C = 0x43;
        public const uint SP_KEY_D = 0x44;
        public const uint SP_KEY_E = 0x45;
        public const uint SP_KEY_F = 0x46;
        public const uint SP_KEY_G = 0x47;
        public const uint SP_KEY_H = 0x48;
        public const uint SP_KEY_I = 0x49;
        public const uint SP_KEY_J = 0x4A;
        public const uint SP_KEY_K = 0x4B;
        public const uint SP_KEY_L = 0x4C;
        public const uint SP_KEY_M = 0x4D;
        public const uint SP_KEY_N = 0x4E;
        public const uint SP_KEY_O = 0x4F;
        public const uint SP_KEY_P = 0x50;
        public const uint SP_KEY_Q = 0x51;
        public const uint SP_KEY_R = 0x52;
        public const uint SP_KEY_S = 0x53;
        public const uint SP_KEY_T = 0x54;
        public const uint SP_KEY_U = 0x55;
        public const uint SP_KEY_V = 0x56;
        public const uint SP_KEY_W = 0x57;
        public const uint SP_KEY_X = 0x58;
        public const uint SP_KEY_Y = 0x59;
        public const uint SP_KEY_Z = 0x5A;

        public const uint SP_KEY_LBUTTON = 0x01;
        public const uint SP_KEY_RBUTTON = 0x02;
        public const uint SP_KEY_CANCEL = 0x03;
        public const uint SP_KEY_MBUTTON = 0x04;

        public const uint SP_KEY_ESCAPE = 0x1B;
        public const uint SP_KEY_SHIFT = 0x10;
        public const uint SP_KEY_CONTROL = 0x11;
        public const uint SP_KEY_MENU = 0x12;
        public const uint SP_KEY_ALT = SP_KEY_MENU;
        public const uint SP_KEY_PAUSE = 0x13;
        public const uint SP_KEY_CAPITAL = 0x14;

        public const uint SP_KEY_SPACE = 0x20;
        public const uint SP_KEY_PRIOR = 0x21;
        public const uint SP_KEY_NEXT = 0x22;
        public const uint SP_KEY_END = 0x23;
        public const uint SP_KEY_HOME = 0x24;
        public const uint SP_KEY_LEFT = 0x25;
        public const uint SP_KEY_UP = 0x26;
        public const uint SP_KEY_RIGHT = 0x27;
        public const uint SP_KEY_DOWN = 0x28;
        public const uint SP_KEY_SELECT = 0x29;
        public const uint SP_KEY_PRINT = 0x2A;
        public const uint SP_KEY_EXECUTE = 0x2B;
        public const uint SP_KEY_SNAPSHOT = 0x2C;
        public const uint SP_KEY_INSERT = 0x2D;
        public const uint SP_KEY_DELETE = 0x2E;
        public const uint SP_KEY_HELP = 0x2F;

        public const uint SP_KEY_NUMPAD0 = 0x60;
        public const uint SP_KEY_NUMPAD1 = 0x61;
        public const uint SP_KEY_NUMPAD2 = 0x62;
        public const uint SP_KEY_NUMPAD3 = 0x63;
        public const uint SP_KEY_NUMPAD4 = 0x64;
        public const uint SP_KEY_NUMPAD5 = 0x65;
        public const uint SP_KEY_NUMPAD6 = 0x66;
        public const uint SP_KEY_NUMPAD7 = 0x67;
        public const uint SP_KEY_NUMPAD8 = 0x68;
        public const uint SP_KEY_NUMPAD9 = 0x69;
        public const uint SP_KEY_MULTIPLY = 0x6A;
        public const uint SP_KEY_ADD = 0x6B;
        public const uint SP_KEY_SEPARATOR = 0x6C;
        public const uint SP_KEY_SUBTRACT = 0x6D;
        public const uint SP_KEY_DECIMAL = 0x6E;
        public const uint SP_KEY_DIVIDE = 0x6F;
        public const uint SP_KEY_F1 = 0x70;
        public const uint SP_KEY_F2 = 0x71;
        public const uint SP_KEY_F3 = 0x72;
        public const uint SP_KEY_F4 = 0x73;
        public const uint SP_KEY_F5 = 0x74;
        public const uint SP_KEY_F6 = 0x75;
        public const uint SP_KEY_F7 = 0x76;
        public const uint SP_KEY_F8 = 0x77;
        public const uint SP_KEY_F9 = 0x78;
        public const uint SP_KEY_F10 = 0x79;
        public const uint SP_KEY_F11 = 0x7A;
        public const uint SP_KEY_F12 = 0x7B;
        public const uint SP_KEY_F13 = 0x7C;
        public const uint SP_KEY_F14 = 0x7D;
        public const uint SP_KEY_F15 = 0x7E;
        public const uint SP_KEY_F16 = 0x7F;
        public const uint SP_KEY_F17 = 0x80;
        public const uint SP_KEY_F18 = 0x81;
        public const uint SP_KEY_F19 = 0x82;
        public const uint SP_KEY_F20 = 0x83;
        public const uint SP_KEY_F21 = 0x84;
        public const uint SP_KEY_F22 = 0x85;
        public const uint SP_KEY_F23 = 0x86;
        public const uint SP_KEY_F24 = 0x87;

        public const uint SP_KEY_NUMLOCK = 0x90;
        public const uint SP_KEY_SCROLL = 0x91;

        public const uint SP_KEY_LSHIFT = 0xA0;
        public const uint SP_KEY_RSHIFT = 0xA1;
        public const uint SP_KEY_LCONTROL = 0xA2;
        public const uint SP_KEY_RCONTROL = 0xA3;
        public const uint SP_KEY_LMENU = 0xA4;
        public const uint SP_KEY_RMENU = 0xA5;

    }
}
