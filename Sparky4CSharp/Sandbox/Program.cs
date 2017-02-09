using SP.App;
using SP.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    class Program : Application
    {
        public Program() : base("Sandbox", new WindowProperties { width = 1280, height = 720, fullscreen = false, vsync = false })
        {
        }

        public override void Init()
        {
            base.Init();
            VFS.Get.Mount("models", "res/models");
            VFS.Get.Mount("pbr", "res/pbr");
            VFS.Get.Mount("shaders", "shaders");
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
        }
    }
}
