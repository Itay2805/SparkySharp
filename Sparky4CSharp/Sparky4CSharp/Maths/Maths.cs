using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Maths
{

    public class IVector2 : TVector2<int> { }
    public class UVector2 : TVector2<uint> { }

    public class Maths
    {
        
        public const float SP_PI = 3.14159265358f;

        public static float ToRadians(float degrees)
        {
            return (degrees * (SP_PI / 180.0f));
        }

        public static float ToDegrees(float radians)
        {
            return (radians / (180.0f / SP_PI));
        }

        public static int Sign(float value)
        {
            return (value > 0 ? 1 : 0) - (value < 0 ? 1 : 0);
        }

        public static float Sin(float value)
        {
            return (float)Math.Sin(value);
        }

        public static float Cos(float value)
        {
            return (float)Math.Cos(value);
        }

        public static float Tan(float value)
        {
            return (float)Math.Tan(value);
        }

        public static float Sqrt(float value)
        {
            return (float)Math.Sqrt(value);
        }

        public static float RSqrt(float value)
        {
            return (float)(1.0f / Math.Sqrt(value));
        }

        public static float ASin(float value)
        {
            return (float)Math.Asin(value);
        }

        public static float ACos(float value)
        {
            return (float) Math.Acos(value);
        }

        public static float ATan(float value)
        {
            return (float) Math.Atan(value);
        }

        public static float ATan2(float y, float x)
        {
            return (float) Math.Atan2(y, x);
        }

        public static float _min(float value, float minimum)
        {
            return (value < minimum) ? minimum : value;
        }

        public static float _max(float value, float maximum)
        {
            return (value > maximum) ? maximum : value;
        }

        public static float Clamp(float value, float minimum, float maximum)
        {
            return (value > minimum) ? (value < maximum) ? value : maximum : minimum;
        }

    }
}
