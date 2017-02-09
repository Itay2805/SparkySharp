using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP.Utils;

namespace SP.System
{

    public struct SystemInfo
    {
        public SystemMemoryInfo memoryInfo;
    }

    public class System
    {

        private static SystemInfo info = new SystemInfo();
        public SystemInfo SystemInfo { get { return info; } }

        public static void Init()
        {
            Log.Info("Initializing Sparky System...");
            MemoryManager.Init();
            VFS.Init();

            info.memoryInfo = MemoryManager.Get.SystemInfo;
            LogSystemInfo();
        }

        public static void Shutdown()
        {
            Log.Info("Shutting down Sparky System...");
            VFS.Shutdown();
            MemoryManager.Shutdown();
        }

        private static void LogSystemInfo()
        {
            Log.Info("--------------------");
            Log.Info(" System Information ");
            Log.Info("--------------------");
            info.memoryInfo.Log();
        }

    }
}
