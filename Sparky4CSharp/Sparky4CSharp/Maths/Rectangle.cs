using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SP.Maths
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Rectangle
    {

        [FieldOffset(0)]
        public Vector2 position;

        [FieldOffset(0)]
        public float x;

        [FieldOffset(4)]
        public float y;

        [FieldOffset(8)]
        public Vector2 size;

        [FieldOffset(8)]
        public float width;

        [FieldOffset(12)]
        public float height;

        public Rectangle(AABB aabb)
        {
            this.x = 0;
            this.y = 0;
            this.width = 0;
            this.height = 0;


            this.position = new Vector2(aabb.min);
            this.size = new Vector2(aabb.max);
        }

        public Rectangle(Vector2 position, Vector2 size)
        {
            this.x = 0;
            this.y = 0;
            this.width = 0;
            this.height = 0;

            this.position = position;
            this.size = size;
        }

        public Rectangle(float x, float y, float width, float height)
        {
            this.position = new Vector2();
            this.size = new Vector2();

            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public bool Intersects(Rectangle other)
        {
            return (size > other.position && position < other.size) || (position > other.size && size < other.position);
        }

        public bool Contains(Vector2 point)
        {
            return point > GetMinimumBound() && point < GetMaximumBound();
        }

        public bool Contains(Vector3 point)
        {
            return Contains(new Vector2(point));
        }

        public Vector2 GetMinimumBound()
        {
            return position - size;
        }

        public Vector2 GetMaximumBound()
        {
            return position + size;
        }

        public override bool Equals(object obj)
        {
            if (obj is Rectangle)
            {
                return this == (Rectangle)obj;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + position.GetHashCode();
                hash = hash * 23 + size.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static bool operator ==(Rectangle left, Rectangle right)
        {
            return left.position == right.position && left.size == right.size;
        }

        public static bool operator !=(Rectangle left, Rectangle right)
        {
            return !(left == right);
        }

        public static bool operator <(Rectangle left, Rectangle right)
        {
            return left.size < right.size;
        }

        public static bool operator >(Rectangle left, Rectangle right)
        {
            return left.size > right.size;
        }

    }
}
