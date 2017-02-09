using SP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.System
{
    public class VFS
    {

        private static VFS instance;
        public static VFS Get { get
            {
                return instance;
            }
        }

        private Dictionary<string, List<string>> mountPoints = new Dictionary<string, List<string>>();

        public static void Init()
        {
            instance = new VFS();
        }

        public static void Shutdown()
        {
            instance = null;
        }

        public void Mount(string virtualPath, string physicalPath)
        {
            Log.Assert(() => instance != null);
            if(!mountPoints.ContainsKey(virtualPath))
            {
                mountPoints[virtualPath] = new List<string>();
            }

            mountPoints[virtualPath].Add(physicalPath);
        }

        public void Unmount(string path)
        {
            Log.Assert(() => instance != null);
            mountPoints[path].Clear();
        }

        public bool ResolvePhysicalPath(string path, out string outPhysicalPath)
        {
            if(path[0] != '/')
            {
                outPhysicalPath = path;
                return FileSystem.FileExists(path);
            }

            List<string> dirs = new List<string>(path.Split('/'));
            string virtualDir = dirs.First();

            if (!mountPoints.ContainsKey(virtualDir) || mountPoints[virtualDir].Count == 0)
            {
                outPhysicalPath = null;
                return false;
            }

            string remainder = path.Substring(virtualDir.Length + 1, path.Count() - virtualDir.Count());
            foreach(string physicalPath in mountPoints[virtualDir])
            {
                path = physicalPath + remainder;
                if(FileSystem.FileExists(path))
                {
                    outPhysicalPath = path;
                    return true;
                }
            }

            outPhysicalPath = null;
            return false;
        }

        public byte[] ReadFile(string path)
        {
            Log.Assert(() => instance != null);
            string physicalPath;
            return ResolvePhysicalPath(path, out physicalPath) ? FileSystem.ReadFile(physicalPath) : null;
        }

        public string ReadTextFile(string path)
        {
            Log.Assert(() => instance != null);
            string physicalPath;
            return ResolvePhysicalPath(path, out physicalPath) ? FileSystem.ReadTextFile(physicalPath) : null;
        }

        public bool WriteFile(string path, byte[] buffer)
        {
            Log.Assert(() => instance != null);
            string physicalPath;
            return ResolvePhysicalPath(path, out physicalPath) ? FileSystem.WriteFile(physicalPath, buffer) : false;
        }

        public bool WriteTextFile(string path, string text)
        {
            Log.Assert(() => instance != null);
            string physicalPath;
            return ResolvePhysicalPath(path, out physicalPath) ? FileSystem.WriteTextFile(physicalPath, text) : false;
        }

    }
}
