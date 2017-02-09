using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Utils
{
    public class Timestep
    {

        private float timestep;
        private float lastTime;

        public Timestep(float initialTime)
        {
            this.timestep = 0.0f;
            this.lastTime = initialTime;
        }

        public void Update(float currentTime)
        {
            timestep = currentTime - lastTime;
            lastTime = currentTime;
        }

        public float GetMillis()
        {
            return timestep;
        }

        public float GetSeconds()
        {
            return timestep * 0.001f;
        }

    }
}
