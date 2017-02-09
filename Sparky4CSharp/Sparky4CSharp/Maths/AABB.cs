using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Maths
{
    public struct AABB
    {

        public Vector3 min;
        public Vector3 max;

        public AABB(Rectangle rect)
        {
            this.min = new Vector3(rect.position);
            this.max = new Vector3(rect.size);
        }

        public AABB(Vector2 min, Vector2 max)
        {
            this.min = new Vector3(min);
            this.max = new Vector3(max);
        }

        public AABB(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }

        public AABB(float x, float y, float width, float height)
        {
            this.min = new Vector3(x, y);
            this.max = new Vector3(width, height);
        }

        public AABB(float x, float y, float z, float width, float height, float depth)
        {
            this.min = new Vector3(x, y, z);
            this.max = new Vector3(width, height, depth);
        }

        public bool Intersects(AABB other)
        {
            return (max > other.min && min < other.max) || (min > other.max && max < other.min);
        }

        public bool Contains(Vector2 point)
        {
            return Contains(new Vector3(point));
        }

        public bool Contains(Vector3 point)
        {
            return point > min && point < max;
        }

        public Vector3 Center()
        {
            return (max + min) * 0.5f;
        }

        public Vector3 GetSize()
        {
            return new Vector3(Math.Abs(max.x - min.x), Math.Abs(max.y - min.y), Math.Abs(max.z - min.z));
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is AABB)
            {
                return this == (AABB)obj;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + min.GetHashCode();
                hash = hash * 23 + max.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(AABB left, AABB right)
        {
            return left.min == right.min && left.max == right.max;
        }

        public static bool operator !=(AABB left, AABB right)
        {
            return !(left == right);
        }

        public static bool operator <(AABB left, AABB right)
        {
            return left.max < right.min;
        }

        public static bool operator >(AABB left, AABB right)
        {
            return left.min > right.max;
        }

    }
}
