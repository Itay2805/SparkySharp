using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Maths
{
    public struct Vector4
    {

        public float x, y, z, w;

        public Vector4(float scalar)
        {
            this.x = scalar;
            this.y = scalar;
            this.z = scalar;
            this.w = scalar;
        }

        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector4(Vector3 xyz, float w)
        {
            this.x = xyz.x;
            this.y = xyz.y;
            this.z = xyz.z;
            this.w = w;
        }

        public Vector4 Add(Vector4 other)
        {
            x += other.x;
            y += other.y;
            z += other.z;
            w += other.w;

            return this;
        }

        public Vector4 Subtract(Vector4 other)
        {
            x -= other.x;
            y -= other.y;
            z -= other.z;
            w -= other.w;

            return this;
        }

        public Vector4 Multiply(Vector4 other)
        {
            x *= other.x;
            y *= other.y;
            z *= other.z;
            w *= other.w;

            return this;
        }

        public Vector4 Divide(Vector4 other)
        {
            x /= other.x;
            y /= other.y;
            z /= other.z;
            w /= other.w;

            return this;
        }

        public Vector4 Multiply(Matrix4 transform)
        {
            return new Vector4(
            transform.rows[0].x * x + transform.rows[0].y * y + transform.rows[0].z * z + transform.rows[0].w * w,
            transform.rows[1].x * x + transform.rows[1].y * y + transform.rows[1].z * z + transform.rows[1].w * w,
            transform.rows[2].x * x + transform.rows[2].y * y + transform.rows[2].z * z + transform.rows[2].w * w,
            transform.rows[3].x * x + transform.rows[3].y * y + transform.rows[3].z * z + transform.rows[3].w * w
            );
        }

        public float Dot(Vector4 other)
        {
            return x * other.x + y * other.y + z * other.z + w * other.w;
        }

        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            return left.Add(right);
        }

        public static Vector4 operator -(Vector4 left, Vector4 right)
        {
            return left.Subtract(right);
        }

        public static Vector4 operator *(Vector4 left, Vector4 right)
        {
            return left.Multiply(right);
        }

        public static Vector4 operator /(Vector4 left, Vector4 right)
        {
            return left.Divide(right);
        }

        public static bool operator ==(Vector4 left, Vector4 right)
        {
            return left.x == right.x && left.y == right.y && left.z == right.z && left.w == right.w;
        }

        public static bool operator !=(Vector4 left, Vector4 right)
        {
            return !(left == right);
        }

        public static bool operator <(Vector4 left, Vector4 right)
        {
            return left.x < right.x && left.y < right.y && left.z < right.z && left.w < right.w;
        }

        public static bool operator <=(Vector4 left, Vector4 right)
        {
            return left.x <= right.x && left.y <= right.y && left.z <= right.z && left.w <= right.w;
        }

        public static bool operator >(Vector4 left, Vector4 right)
        {
            return left.x > right.x && left.y > right.y && left.z > right.z && left.w > right.w;
        }

        public static bool operator >=(Vector4 left, Vector4 right)
        {
            return left.x >= right.x && left.y >= right.y && left.z >= right.z && left.w >= right.w;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector4)
            {
                return this == (Vector4)obj;
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
                hash = hash * 23 + w.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return "vec4: (x: " + x + ", y: " + y + ", z: " + z + ", w: " + w + ")";
        }

    }
}
