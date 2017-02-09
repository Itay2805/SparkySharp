using SP.App;
using SP.Events;
using SP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Graphics.Layers
{
    public class Layer : IEventListener
    {

        protected Window window;
        protected bool visible;

        public Layer()
        {
            this.window = Window.GetWindowClass();
            this.visible = true;
        }

        public bool IsVisible()
        {
            return visible;
        }

        public void SetVisible(bool visible)
        {
            this.visible = visible;
        }

        public virtual void Init() { }

        public virtual void OnEvent(Event e)
        {

            Log.Info(e.EventType);

            EventDispatcher dispatcher = new EventDispatcher(e);
            dispatcher.Dispatch<ResizeWindowEvent>((ev) =>
            {
                return OnResize(ev.GetWidth(), ev.GetHeight());
            });
        }

        public virtual void OnTick() { }

        public virtual void OnUpdate(Timestep ts) { }

        public virtual void OnUpdateInternal(Timestep ts) { }

        public virtual void OnRender() { }

        protected virtual bool OnResize(uint width, uint height)
        {
            return false;        
        }

    }
}
