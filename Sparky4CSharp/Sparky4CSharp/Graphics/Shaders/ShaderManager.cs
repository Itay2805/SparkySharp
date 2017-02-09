using SP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Graphics.Shaders
{
    public class ShaderManager
    {

        private static List<Shader> shaders = new List<Shader>();

        public static void Add(Shader shader)
        {
            shaders.Add(shader);
        }

        public static Shader Get(string name)
        {
            foreach(Shader shader in shaders)
            {
                if (shader.GetName() == name)
                    return shader;
            }
            return null;
        }

        public static void Reload(string name)
        {
            for(int i = 0; i < shaders.Count; i++)
            {
                if(shaders[i].GetName() == name)
                {
                    string path = shaders[i].GetFilePath();
                    string error;
                    if(!Shader.TryCompileFromFile(path, out error))
                    {
                        Log.Error(error);
                    }else
                    {
                        shaders[i] = Shader.CreateFromFile(name, path);
                        Log.Info("Reloaded shader: " + name);
                    }
                    return;
                }
            }
            Log.Warn("Could not find ", name, " shader to reload.");
        }

        public static void Reload(Shader shader)
        {
            for (int i = 0; i < shaders.Count; i++)
            {
                if (shaders[i] == shader)
                {
                    string name = shader.GetName();
                    string path = shader.GetFilePath();
                    shaders[i] = Shader.CreateFromFile(name, path);
                    return;
                }
            }
            Log.Warn("Could not find specified shader to reload.");
        }

        public static void Clean()
        {
            shaders.Clear();
        }

        private ShaderManager()
        {

        }

    }
}
