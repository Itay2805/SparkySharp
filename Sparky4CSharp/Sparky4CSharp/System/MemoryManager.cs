using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WIN32;

namespace SP.System
{

    public struct SystemMemoryInfo
    {
        public ulong availablePhysicalMemory;
        public ulong totalPhysicalMemory;

        public ulong availableVirtualMemory;
        public ulong totalVirtualMemory;

        public void Log()
        {
            string apm, tpm, avm, tvm;

            apm = MemoryManager.BytesToString(availablePhysicalMemory);
            tpm = MemoryManager.BytesToString(totalPhysicalMemory);
            avm = MemoryManager.BytesToString(availableVirtualMemory);
            tvm = MemoryManager.BytesToString(totalVirtualMemory);

            Utils.Log.Info();
            Utils.Log.Info("Memory Info:");
            Utils.Log.Info("\tPhysical Memory (Avail/Total): ", apm, " / ", tpm);
            Utils.Log.Info("\tVirtual Memory  (Avail/Total): ", avm, " / ", tvm);
            Utils.Log.Info();
        }
    }

    public struct MemoryStats
    {
        long totalAllocated;
        long totalFreed;
        long currentUsed;
        long totalAllocations;
    }

    public class MemoryManager
    {
        private static MemoryManager instance;
        public static MemoryManager Get {
            get
            {
                if(instance == null)
                {
                    instance = new MemoryManager();
                }
                return instance;
            }
        }

        public MemoryStats MemoryStats { get; } = new MemoryStats();

        public SystemMemoryInfo SystemInfo {
            get
            {
                SystemMemoryInfo info = new SystemMemoryInfo();
                MEMORYSTATUSEX memStatus = new MEMORYSTATUSEX();
                memStatus.dwLength = (uint)Marshal.SizeOf(memStatus);
                if (SysInfoAPI.GlobalMemoryStatusEx(ref memStatus))
                {
                    info.availablePhysicalMemory = memStatus.ullAvailPhys;
                    info.availableVirtualMemory = memStatus.ullAvailVirtual;
                    info.totalPhysicalMemory = memStatus.ullTotalPhys;
                    info.totalVirtualMemory = memStatus.ullTotalVirtual;
                }
                return info;
            }
        }

        public MemoryManager()
        {

        }

        public static void Init()
        {

        }

        public static void Shutdown()
        {
            instance = null;
        }
        
        public static string BytesToString(ulong bytes)
        {
            const float gb = 1024 * 1024 * 1024;
            const float mb = 1024 * 1024;
            const float kb = 1024;

            string result;
            if(bytes > gb)
            {
                result = StringFormat.Float(bytes / gb) + " GB";
            }
            else if (bytes > mb)
            {
                result = StringFormat.Float(bytes / mb) + " MB";
            }
            else if (bytes > kb)
            {
                result = StringFormat.Float(bytes / kb) + " KB";
            }else
            {
                result = StringFormat.Float((float)bytes) + " bytes";
            }

            return result;
        }

    }
}
