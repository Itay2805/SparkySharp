using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Sound
{
    public class SoundManager
    {

        private static List<Sound> sounds = new List<Sound>();

        public static void Init()
        {

        }

        public static Sound Add(Sound sound)
        {
            sounds.Add(sound);
            return sound;
        }

        public static Sound Get(string name)
        {
            foreach(Sound sound in sounds)
            {
                if (sound.GetName() == name)
                    return sound;
            }
            return null;
        }

        public static void Update()
        {

        }

        public static void Clean()
        {
            for (int i = 0; i < sounds.Count; i++)
                sounds[i].Dispose();

            sounds.Clear();
        }

        private SoundManager()
        {

        }

    }
}
