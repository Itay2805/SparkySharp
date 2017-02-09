using SP.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.System
{
    public class FileSystem
    {

        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public static long GetFileSize(string path)
        {
            return new FileInfo(path).Length;
        }

        public static byte[] ReadFile(string path)
        {
            try
            {
                return File.ReadAllBytes(path);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return null;
            }
        }

        public static bool ReadFile(string path, out byte[] buffer)
        {
            try
            {
                buffer = File.ReadAllBytes(path);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                buffer = null;
                return false;
            }
            return true;
        }

        public static string ReadTextFile(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return null;
            }
        }

        public static bool ReadFile(string path, byte[] buffer)
        {
            try
            {
                File.WriteAllBytes(path, buffer);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return false;
            }
            return true;
        }

        public static bool WriteFile(string path, byte[] buffer)
        {
            try
            {
                File.WriteAllBytes(path, buffer);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return false;
            }
            return true;
        }

        public static bool WriteTextFile(string path, string text)
        {
            try
            {
                File.WriteAllText(path, text);
            }catch(Exception e)
            {
                Log.Error(e.Message);
                return false;
            }
            return true;
        }

    }
}
