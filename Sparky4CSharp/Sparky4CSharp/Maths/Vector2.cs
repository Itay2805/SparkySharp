using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Maths
{
    public struct Vector2
    {

        public float x, y;

        public Vector2(float scalar)
        {
            this.x = scalar;
            this.y = scalar;
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(Vector3 vector)
        {
            this.x = vector.x;
            this.y = vector.y;
        }

        public Vector2 Add(Vector2 other)
        {
            x += other.x;
            y += other.y;

            return this;
        }

        public Vector2 Subtract(Vector2 other)
        {
            x -= other.x;
            y -= other.y;

            return this;
        }

        public Vector2 Multiply(Vector2 other)
        {
            x *= other.x;
            y *= other.y;

            return this;
        }

        public Vector2 Divide(Vector2 other)
        {
            x /= other.x;
            y /= other.y;

            return this;
        }

        public Vector2 Add(float value)
        {
            x += value;
            y += value;

            return this;
        }

        public Vector2 Subtract(float value)
        {
            x -= value;
            y -= value;

            return this;
        }

        public Vector2 Multiply(float value)
        {
            x *= value;
            y *= value;

            return this;
        }

        public Vector2 Divide(float value)
        {
            x /= value;
            y /= value;

            return this;
        }

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return left.Add(right);
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return left.Subtract(right);
        }

        public static Vector2 operator *(Vector2 left, Vector2 right)
        {
            return left.Multiply(right);
        }

        public static Vector2 operator /(Vector2 left, Vector2 right)
        {
            return left.Divide(right);
        }

        public static Vector2 operator +(Vector2 left, float value)
        {
            return left.Add(value);
        }

        public static Vector2 operator -(Vector2 left, float value)
        {
            return left.Subtract(value);
        }

        public static Vector2 operator *(Vector2 left, float value)
        {
            return left.Multiply(value);
        }

        public static Vector2 operator /(Vector2 left, float value)
        {
            return left.Divide(value);
        }

        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.x == right.x && left.y == right.y;
        }

        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return !(left == right);
        }

        public static bool operator <(Vector2 left, Vector2 right)
        {
            return left.x < right.x && left.y < right.y;
        }

        public static bool operator <=(Vector2 left, Vector2 right)
        {
            return left.x <= right.x && left.y <= right.y;
        }

        public static bool operator >(Vector2 left, Vector2 right)
        {
            return left.x > right.x && left.y > right.y;
        }

        public static bool operator >=(Vector2 left, Vector2 right)
        {
            return left.x >= right.x && left.y >= right.y;
        }

        public float Magnitude()
        {
            return (float) Math.Sqrt(x * x + y * y);
        }

        public Vector2 Normalise()
        {
            float length = Magnitude();
            return new Vector2(x / length, y / length);
        }

        public float Distance(Vector2 other)
        {
            float a = x - other.x;
            float b = y - other.y;
            return (float) Math.Sqrt(a * a + b * b);
        }

        public float Dot(Vector2 other)
        {
            return x * other.x + y * other.y;
        }

        public override string ToString()
        {
            return "vec2: (" + x + ", " + y + ")";
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector2)
            {
                return this == (Vector2)obj;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                return hash;
            }
        }

    }
}
