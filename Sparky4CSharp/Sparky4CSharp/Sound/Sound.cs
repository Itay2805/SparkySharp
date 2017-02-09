using SP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SP.Sound
{
    public class Sound : IDisposable
    {

        private string name;
        private string filename;
        private uint count;

        private SoundPlayer player;

        private bool playing;
        private float gain;

        public Sound(string name, string filename)
        {
            this.name = name;
            this.filename = filename;
            this.playing = false;
            this.count = 0;

            if(filename.Split('.').Length < 2)
            {
                Log.Error("[Sound] Invalid file name '" + filename + "'!");
                return;
            }

            try
            {
                player = new SoundPlayer();
                player.SoundLocation = filename;
                player.Load();
            }catch(Exception e)
            {
                Log.Error("[Sound]  Could not load file '" + filename + "'! (", e.Message, ")");
            }
        }

        public void Play()
        {
            player.Play();
        }

        public void Loop()
        {
            player.PlayLooping();
        }

        public void Pause()
        {
            throw new NotImplementedException("This was not implemented yet");
        }

        public void Resume()
        {
            throw new NotImplementedException("This was not implemented yet");
        }

        public void Stop()
        {
            player.Stop();
        }

        public void SetGain(float gain)
        {
            throw new NotImplementedException("This was not implemented yet");
        }

        public bool IsPlaying()
        {
            return playing;
        }

        public float GetGain()
        {
            return gain;
        }

        public string GetName()
        {
            return name;
        }

        public string GetFileName()
        {
            return filename;
        }

        public void Dispose()
        {
            player.Dispose();
        }
    }
}
