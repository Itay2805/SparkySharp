using SP.Events;
using SP.Graphics.API;
using SP.Graphics.Layers;
using SP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.App
{
    public class Application
    {

        private static Application instance;

        private Window window;
        // Debug Layer

        private bool running, suspended;
        private Timer timer;
        private uint updatesPerSecond, framesPerSecond;
        private float frameTime;

        private string name;
        private WindowProperties properties;

        private List<Layer> layerStack = new List<Layer>();
        private List<Layer> overlayStack = new List<Layer>();

        public Application(string name, WindowProperties properties, RenderAPI api = RenderAPI.OPENGL)
        {
            this.name = name;
            this.properties = properties;
            this.frameTime = 0.0f;

            instance = this;
            Context.SetRenderAPI(api);
        }

        public virtual void Init()
        {
            System.System.Init();
            PlatformInit();

            // debug menu
        }

        public void PushLayer(Layer layer)
        {
            layerStack.Add(layer);
        }

        public Layer PopLayer()
        {
            Layer layer = layerStack.First();
            layerStack.Remove(layer);
            return layer;
        }

        public Layer PopLayer(Layer layer)
        {
            for(int i = 0; i < layerStack.Count; i++)
            {
                if(layerStack[i] == layer)
                {
                    layerStack.RemoveAt(i);
                    break;
                }
            }
            return layer;
        }

        public void PushOverlay(Layer layer)
        {
            overlayStack.Add(layer);
        }

        public Layer PopOverlay()
        {
            Layer layer = layerStack.First();
            overlayStack.Remove(layer);
            return layer;
        }

        public Layer PopOverlay(Layer layer)
        {
            for (int i = 0; i < overlayStack.Count; i++)
            {
                if (overlayStack[i] == layer)
                {
                    overlayStack.RemoveAt(i);
                    break;
                }
            }
            return layer;
        }

        public void Start()
        {
            Init();
            running = true;
            suspended = false;
            Run();
        }

        public void Suspend()
        {
            suspended = true;
        }

        public void Resume()
        {
            suspended = false;
        }

        public void Stop()
        {
            running = false;
        }

        public uint GetFPS()
        {
            return framesPerSecond;
        }

        public uint GetUPS()
        {
            return updatesPerSecond;
        }

        public float GetFrametime()
        {
            return frameTime;
        }

        public uint GetWindowWidth()
        {
            return window.GetWidth();
        }

        public uint GetWindowHeight()
        {
            return window.GetHeight();
        }

        public string GetBuildConfiguration()
        {
#if (DEBUG)
            return "Debug";
#else
            return "Release";
#endif
        }

        public string GetPlatform()
        {
            if(Environment.Is64BitOperatingSystem)
            {
                return "Win64";
            }else
            {
                return "Win32";
            }
        }

        private void PlatformInit()
        {
            window = new Window(name, properties);
            window.SetEventCallback(OnEvent);
        }

        private void Run()
        {
            this.timer = new Timer();
            float timer = 0.0f;
            float updateTimer = this.timer.ElapsedMillis();
            float updateTick = 1000.0f / 60.0f;
            uint frames = 0;
            uint updates = 0;
            Timestep timestep = new Timestep(this.timer.ElapsedMillis());
            while(running)
            {
                window.Clear();
                float now = this.timer.ElapsedMillis();
                if(now - updateTimer > updateTick)
                {
                    timestep.Update(now);
                    OnUpdate(timestep);
                    updates++;
                    updateTimer += updateTick;
                }

                {
                    Timer frametime = new Timer();
                    OnRender();
                    frames++;
                    frameTime = frametime.ElapsedMillis();
                }

                window.Update();

                if(this.timer.Elapsed() - timer > 1.0f)
                {
                    timer += 1.0f;
                    framesPerSecond = frames;
                    updatesPerSecond = updates;
                    frames = 0;
                    updates = 0;
                    OnTick();
                }

                if (window.Closed())
                    running = false;
            }
        }

        private void OnTick()
        {
            // Debug layer

            for (int i = overlayStack.Count - 1; i >= 0; i--)
            {
                overlayStack[i].OnTick();
            }

            for (int i = layerStack.Count - 1; i >= 0; i--)
            {
                layerStack[i].OnTick();
            }
        }

        private void OnUpdate(Timestep ts)
        {
            // Debug layer

            for (int i = overlayStack.Count - 1; i >= 0; i--)
            {
                overlayStack[i].OnUpdateInternal(ts);
            }

            for (int i = layerStack.Count - 1; i >= 0; i--)
            {
                layerStack[i].OnUpdateInternal(ts);
            }
        }

        private void OnRender()
        {
            // Debug layer

            for (int i = overlayStack.Count - 1; i >= 0; i--)
            {
                overlayStack[i].OnRender();
            }

            for (int i = layerStack.Count - 1; i >= 0; i--)
            {
                layerStack[i].OnRender();
            }
        }

        private void OnEvent(Event e)
        {
            // Debug layer

            for(int i = overlayStack.Count - 1; i >= 0; i--)
            {
                overlayStack[i].OnEvent(e);
                if (e.Handled)
                    return;
            }

            for(int i = layerStack.Count - 1; i >= 0; i--)
            {
                layerStack[i].OnEvent(e);
                if (e.Handled)
                    return;
            }
        }

        public static Application GetApplication()
        {
            return instance;
        }

    }
}
