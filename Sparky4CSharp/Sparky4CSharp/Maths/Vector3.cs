using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Maths
{
    public struct Vector3
    {
        public float x, y, z;

        public Vector3(float scalar)
        {
            this.x = scalar;
            this.y = scalar;
            this.z = scalar;
        }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(Vector2 other)
        {
            this.x = other.x;
            this.y = other.y;
            this.z = 0;
        }

        public Vector3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0;
        }

        public Vector3(Vector4 other)
        {
            this.x = other.x;
            this.y = other.y;
            this.z = other.z;
        }

        public static Vector3 Up()
        {
            return new Vector3(0.0f, 1.0f, 0.0f);
        }

        public static Vector3 Down()
        {
            return new Vector3(0.0f, -1.0f, 0.0f);
        }

        public static Vector3 Left()
        {
            return new Vector3(-1.0f, 0.0f, 0.0f);
        }

        public static Vector3 Right()
        {
            return new Vector3(1.0f, 1.0f, 0.0f);
        }

        public static Vector3 Zero()
        {
            return new Vector3(0.0f, 0.0f, 0.0f);
        }

        public static Vector3 XAxis()
        {
            return new Vector3(1.0f, 0.0f, 0.0f);
        }

        public static Vector3 YAxis()
        {
            return new Vector3(0.0f, 1.0f, 0.0f);
        }

        public static Vector3 ZAxis()
        {
            return new Vector3(0.0f, 0.0f, 1.0f);
        }

        public Vector3 Add(Vector3 other)
        {
            x += other.x;
            y += other.y;
            z += other.z;

            return this;
        }

        public Vector3 Subtract(Vector3 other)
        {
            x -= other.x;
            y -= other.y;
            z -= other.z;

            return this;
        }

        public Vector3 Multiply(Vector3 other)
        {
            x *= other.x;
            y *= other.y;
            z *= other.z;

            return this;
        }

        public Vector3 Divide(Vector3 other)
        {
            x /= other.x;
            y /= other.y;
            z /= other.z;

            return this;
        }

        public Vector3 Add(float other)
        {
            x += other;
            y += other;
            z += other;

            return this;
        }

        public Vector3 Subtract(float other)
        {
            x -= other;
            y -= other;
            z -= other;

            return this;
        }

        public Vector3 Multiply(float other)
        {
            x *= other;
            y *= other;
            z *= other;

            return this;
        }

        public Vector3 Divide(float other)
        {
            x /= other;
            y /= other;
            z /= other;

            return this;
        }

        public Vector3 Multiply(Matrix4 transform)
        {
            return new Vector3(
            transform.rows[0].x * x + transform.rows[0].y * y + transform.rows[0].z * z + transform.rows[0].w,
            transform.rows[1].x * x + transform.rows[1].y * y + transform.rows[1].z * z + transform.rows[1].w,
            transform.rows[2].x * x + transform.rows[2].y * y + transform.rows[2].z * z + transform.rows[2].w
            );
        }

        public Vector3 Cross(Vector3 other)
        {
            return new Vector3(y * other.z - z * other.y, z * other.x - x * other.z, x * other.y - y * other.x);
        }

        public float Dot(Vector3 other)
        {
            return x * other.x + y * other.y + z * other.z;
        }

        public float Magnitude()
        {
            return Maths.Sqrt(x * x + y * y + z * z);
        }

        public Vector3 Normalize()
        {
            float length = Magnitude();
            return new Vector3(x / length, y / length, z / length);
        }

        public float Distance(Vector3 other)
        {
            float a = x - other.x;
            float b = y - other.y;
            float c = z - other.z;
            return Maths.Sqrt(a * a + b * b + c * c);
        }

        public override string ToString()
        {
            return "vec3: (x: " + x + ", y: " + y + ", z: " + z + ")";
        }

        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return left.Add(right);
        }

        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return left.Subtract(right);
        }

        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            return left.Multiply(right);
        }

        public static Vector3 operator /(Vector3 left, Vector3 right)
        {
            return left.Divide(right);
        }

        public static Vector3 operator +(Vector3 left, float value)
        {
            return left.Add(value);
        }

        public static Vector3 operator -(Vector3 left, float value)
        {
            return left.Subtract(value);
        }

        public static Vector3 operator *(Vector3 left, float value)
        {
            return left.Multiply(value);
        }

        public static Vector3 operator /(Vector3 left, float value)
        {
            return left.Divide(value);
        }

        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return left.x == right.x && left.y == right.y && left.z == right.z;
        }

        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return !(left == right);
        }

        public static bool operator <(Vector3 left, Vector3 right)
        {
            return left.x < right.x && left.y < right.y && left.z < right.z;
        }

        public static bool operator <=(Vector3 left, Vector3 right)
        {
            return left.x <= right.x && left.y <= right.y && left.z <= right.z;
        }

        public static bool operator >(Vector3 left, Vector3 right)
        {
            return left.x > right.x && left.y > right.y && left.z > right.z;
        }

        public static bool operator >=(Vector3 left, Vector3 right)
        {
            return left.x >= right.x && left.y >= right.y && left.z >= right.z;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector3)
            {
                return this == (Vector3)obj;
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
                hash = hash * 23 + z.GetHashCode();
                return hash;
            }
        }

    }
}
