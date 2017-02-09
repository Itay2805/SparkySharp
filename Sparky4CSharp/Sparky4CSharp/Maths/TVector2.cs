using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Maths
{
    public class TVector2<T>
    {

        public T x, y;

        public TVector2()
        {
            this.x = default(T);
            this.y = default(T);
        }

        public TVector2(T x, T y)
        {
            this.x = x;
            this.y = y;
        }

        public TVector2<T> Add(TVector2<T> other)
        {
            x = Add(x, other.x);
            y = Add(y, other.y);

            return this;
        }

        public TVector2<T> Subtract(TVector2<T> other)
        {
            x = Subtract(x, other.x);
            y = Subtract(y, other.y);

            return this;
        }

        public TVector2<T> Multiply(TVector2<T> other)
        {
            x = Multiply(x, other.x);
            y = Multiply(y, other.y);

            return this;
        }

        public TVector2<T> Divide(TVector2<T> other)
        {
            x = Divide(x, other.x);
            y = Divide(y, other.y);

            return this;
        }

        public static TVector2<T> operator +(TVector2<T> left, TVector2<T> right)
        {
            return left.Add(right);
        }

        public static TVector2<T> operator -(TVector2<T> left, TVector2<T> right)
        {
            return left.Subtract(right);
        }

        public static TVector2<T> operator *(TVector2<T> left, TVector2<T> right)
        {
            return left.Multiply(right);
        }

        public static TVector2<T> operator /(TVector2<T> left, TVector2<T> right)
        {
            return left.Divide(right);
        }

        public static bool operator ==(TVector2<T> left, TVector2<T> right)
        {
            dynamic lx = left.x;
            dynamic ly = left.y;

            dynamic rx = right.x;
            dynamic ry= right.y;

            return lx == rx && ly == ry; 
        }

        public static bool operator !=(TVector2<T> left, TVector2<T> right)
        {
            return !(left == right);
        }

        private T Add(T val1, T val2)
        {
            dynamic a = val1;
            dynamic b = val2;
            return a + b;
        }

        private T Subtract(T val1, T val2)
        {
            dynamic a = val1;
            dynamic b = val2;
            return a + b;
        }

        private T Multiply(T val1, T val2)
        {
            dynamic a = val1;
            dynamic b = val2;
            return a + b;
        }

        private T Divide(T val1, T val2)
        {
            dynamic a = val1;
            dynamic b = val2;
            return a + b;
        }

        public override string ToString()
        {
            return "tvec2: (" + x + ", " + y + ")";
        }

        public override bool Equals(object obj)
        {
            if(obj is TVector2<T>)
            {
                return this == (TVector2<T>)obj;
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
