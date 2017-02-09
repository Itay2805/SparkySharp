using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SP.Maths
{

    [StructLayout(LayoutKind.Explicit)]
    public class Matrix4
    {

        [FieldOffset(0)]
        public float[] elements;
        [FieldOffset(0)]
        public Vector4[] rows;

        public float this[int index]
        {
            get
            {
                return elements[index];
            }
            set
            {
                elements[index] = value;
            }
        }

        public Matrix4()
        {
            this.rows = new Vector4[4];
            this.elements = new float[4 * 4];
        }

        public Matrix4(float diagonal)
        {
            this.rows = new Vector4[4];
            this.elements = new float[4 * 4];

            elements[0 + 0 * 4] = diagonal;
            elements[1 + 1 * 4] = diagonal;
            elements[2 + 2 * 4] = diagonal;
            elements[3 + 3 * 4] = diagonal;
        }

        public Matrix4(float[] elements)
        {
            this.rows = new Vector4[4];
            this.elements = new float[4 * 4];

            Array.Copy(elements, this.elements, 4 * 4);
        }

        public Matrix4(Vector4 row0, Vector4 row1, Vector4 row2, Vector4 row3)
        {
            rows = new Vector4[4];
            elements = new float[4 * 4];

            rows[0] = row0;
            rows[1] = row1;
            rows[2] = row2;
            rows[3] = row3;
        }

        public static Matrix4 Identity()
        {
            return new Matrix4(1.0f);
        }

        public Matrix4 Multiply(Matrix4 other)
        {
            float[] data = new float[16];
            for(int row = 0; row < 4; row++)
            {
                for(int col = 0; col < 4; col++)
                {
                    float sum = 0.0f;
                    for(int e = 0; e < 4; e++)
                    {
                        sum += elements[e + row * 4] * other.elements[col + e * 4];
                    }
                    data[col + row * 4] = sum;
                }
            }
            Array.Copy(data, this.elements, 4 * 4);
            return this;
        }

        public Vector3 Multiply(Vector3 other)
        {
            return other.Multiply(this);
        }

        public Vector4 Multiply(Vector4 other)
        {
            return other.Multiply(this);
        }

        public Matrix4 Invert()
        {
            float[] temp = new float[16];

            temp[0] = elements[5] * elements[10] * elements[15] -
                elements[5] * elements[11] * elements[14] -
                elements[9] * elements[6] * elements[15] +
                elements[9] * elements[7] * elements[14] +
                elements[13] * elements[6] * elements[11] -
                elements[13] * elements[7] * elements[10];

            temp[4] = -elements[4] * elements[10] * elements[15] +
                elements[4] * elements[11] * elements[14] +
                elements[8] * elements[6] * elements[15] -
                elements[8] * elements[7] * elements[14] -
                elements[12] * elements[6] * elements[11] +
                elements[12] * elements[7] * elements[10];

            temp[8] = elements[4] * elements[9] * elements[15] -
                elements[4] * elements[11] * elements[13] -
                elements[8] * elements[5] * elements[15] +
                elements[8] * elements[7] * elements[13] +
                elements[12] * elements[5] * elements[11] -
                elements[12] * elements[7] * elements[9];

            temp[12] = -elements[4] * elements[9] * elements[14] +
                elements[4] * elements[10] * elements[13] +
                elements[8] * elements[5] * elements[14] -
                elements[8] * elements[6] * elements[13] -
                elements[12] * elements[5] * elements[10] +
                elements[12] * elements[6] * elements[9];

            temp[1] = -elements[1] * elements[10] * elements[15] +
                elements[1] * elements[11] * elements[14] +
                elements[9] * elements[2] * elements[15] -
                elements[9] * elements[3] * elements[14] -
                elements[13] * elements[2] * elements[11] +
                elements[13] * elements[3] * elements[10];

            temp[5] = elements[0] * elements[10] * elements[15] -
                elements[0] * elements[11] * elements[14] -
                elements[8] * elements[2] * elements[15] +
                elements[8] * elements[3] * elements[14] +
                elements[12] * elements[2] * elements[11] -
                elements[12] * elements[3] * elements[10];

            temp[9] = -elements[0] * elements[9] * elements[15] +
                elements[0] * elements[11] * elements[13] +
                elements[8] * elements[1] * elements[15] -
                elements[8] * elements[3] * elements[13] -
                elements[12] * elements[1] * elements[11] +
                elements[12] * elements[3] * elements[9];

            temp[13] = elements[0] * elements[9] * elements[14] -
                elements[0] * elements[10] * elements[13] -
                elements[8] * elements[1] * elements[14] +
                elements[8] * elements[2] * elements[13] +
                elements[12] * elements[1] * elements[10] -
                elements[12] * elements[2] * elements[9];

            temp[2] = elements[1] * elements[6] * elements[15] -
                elements[1] * elements[7] * elements[14] -
                elements[5] * elements[2] * elements[15] +
                elements[5] * elements[3] * elements[14] +
                elements[13] * elements[2] * elements[7] -
                elements[13] * elements[3] * elements[6];

            temp[6] = -elements[0] * elements[6] * elements[15] +
                elements[0] * elements[7] * elements[14] +
                elements[4] * elements[2] * elements[15] -
                elements[4] * elements[3] * elements[14] -
                elements[12] * elements[2] * elements[7] +
                elements[12] * elements[3] * elements[6];

            temp[10] = elements[0] * elements[5] * elements[15] -
                elements[0] * elements[7] * elements[13] -
                elements[4] * elements[1] * elements[15] +
                elements[4] * elements[3] * elements[13] +
                elements[12] * elements[1] * elements[7] -
                elements[12] * elements[3] * elements[5];

            temp[14] = -elements[0] * elements[5] * elements[14] +
                elements[0] * elements[6] * elements[13] +
                elements[4] * elements[1] * elements[14] -
                elements[4] * elements[2] * elements[13] -
                elements[12] * elements[1] * elements[6] +
                elements[12] * elements[2] * elements[5];

            temp[3] = -elements[1] * elements[6] * elements[11] +
                elements[1] * elements[7] * elements[10] +
                elements[5] * elements[2] * elements[11] -
                elements[5] * elements[3] * elements[10] -
                elements[9] * elements[2] * elements[7] +
                elements[9] * elements[3] * elements[6];

            temp[7] = elements[0] * elements[6] * elements[11] -
                elements[0] * elements[7] * elements[10] -
                elements[4] * elements[2] * elements[11] +
                elements[4] * elements[3] * elements[10] +
                elements[8] * elements[2] * elements[7] -
                elements[8] * elements[3] * elements[6];

            temp[11] = -elements[0] * elements[5] * elements[11] +
                elements[0] * elements[7] * elements[9] +
                elements[4] * elements[1] * elements[11] -
                elements[4] * elements[3] * elements[9] -
                elements[8] * elements[1] * elements[7] +
                elements[8] * elements[3] * elements[5];

            temp[15] = elements[0] * elements[5] * elements[10] -
                elements[0] * elements[6] * elements[9] -
                elements[4] * elements[1] * elements[10] +
                elements[4] * elements[2] * elements[9] +
                elements[8] * elements[1] * elements[6] -
                elements[8] * elements[2] * elements[5];

            float determinant = elements[0] * temp[0] + elements[1] * temp[4] + elements[2] * temp[8] + elements[3] * temp[12];
            determinant = 1.0f / determinant;

            for (int i = 0; i < 4 * 4; i++)
                elements[i] = temp[i] * determinant;

            return this;
        }

        public Vector4 GetColumn(int index)
        {
            return new Vector4(elements[index + 0 * 4], elements[index + 1 * 4], elements[index + 2 * 4], elements[index + 3 * 4]);
        }

        public void SetColumn(uint index, Vector4 column)
        {
            elements[index + 0 * 4] = column.x;
            elements[index + 1 * 4] = column.y;
            elements[index + 2 * 4] = column.z;
            elements[index + 3 * 4] = column.w;
        }

        public Vector3 GetPosition()
        {
            return new Vector3(GetColumn(3));
        }

        public void SetPosition(Vector3 position)
        {
            SetColumn(3, new Vector4(position, 1.0f));
        }

        public static Matrix4 operator *(Matrix4 left, Matrix4 right)
        {
            return left.Multiply(right);
        }

        public static Vector3 operator *(Matrix4 left, Vector3 right)
        {
            return left.Multiply(right);
        }

        public static Vector4 operator *(Matrix4 left, Vector4 right)
        {
            return left.Multiply(right);
        }

        public static Matrix4 Orthographic(float left, float right, float bottom, float top, float near, float far)
        {
            Matrix4 result = new Matrix4(1.0f);

            result.elements[0 + 0 * 4] = 2.0f / (right - left);

            result.elements[1 + 1 * 4] = 2.0f / (top - bottom);

            result.elements[2 + 2 * 4] = 2.0f / (near - far);

            result.elements[3 + 0 * 4] = (left + right) / (left - right);
            result.elements[3 + 1 * 4] = (bottom + top) / (bottom - top);
            result.elements[3 + 2 * 4] = (far + near) / (far - near);

            return result;
        }

        public static Matrix4 Perspective(float fov, float aspectRatio, float near, float far)
        {
            Matrix4 result = new Matrix4(1.0f);

            float q = 1.0f / Maths.Tan(Maths.ToRadians(0.5f * fov));
            float a = q / aspectRatio;

            float b = (near + far) / (near - far);
            float c = (2.0f * near * far) / (near - far);

            result.elements[0 + 0 * 4] = a;
            result.elements[1 + 1 * 4] = q;
            result.elements[2 + 2 * 4] = b;
            result.elements[2 + 3 * 4] = -1.0f;
            result.elements[3 + 2 * 4] = c;

            return result;
        }

        public static Matrix4 LookAt(Vector3 camera, Vector3 obj, Vector3 up)
        {
            Matrix4 result = Identity();
            Vector3 f = (obj - camera).Normalize();
            Vector3 s = f.Cross(up.Normalize());
            Vector3 u = s.Cross(f);

            result.elements[0 + 0 * 4] = s.x;
            result.elements[0 + 1 * 4] = s.y;
            result.elements[0 + 2 * 4] = s.z;

            result.elements[1 + 0 * 4] = u.x;
            result.elements[1 + 1 * 4] = u.y;
            result.elements[1 + 2 * 4] = u.z;

            result.elements[2 + 0 * 4] = -f.x;
            result.elements[2 + 1 * 4] = -f.y;
            result.elements[2 + 2 * 4] = -f.z;

            return result * Translate(new Vector3(-camera.x, -camera.y, -camera.z));
        }

        public static Matrix4 Translate(Vector3 translation)
        {
            Matrix4 result = new Matrix4(1.0f);

            result.elements[3 + 0 * 4] = translation.x;
            result.elements[3 + 1 * 4] = translation.y;
            result.elements[3 + 2 * 4] = translation.z;

            return result;
        }

        public static Matrix4 Rotate(float angle, Vector3 axis)
        {
            Matrix4 result = new Matrix4(1.0f);

            float r = Maths.ToRadians(angle);
            float c = Maths.Cos(r);
            float s = Maths.Sin(r);
            float omc = 1.0f - c;

            float x = axis.x;
            float y = axis.y;
            float z = axis.z;

            result.elements[0 + 0 * 4] = x * x * omc + c;
            result.elements[0 + 1 * 4] = y * x * omc + z * s;
            result.elements[0 + 2 * 4] = x * z * omc - y * s;

            result.elements[1 + 0 * 4] = x * y * omc - z * s;
            result.elements[1 + 1 * 4] = y * y * omc + c;
            result.elements[1 + 2 * 4] = y * z * omc + x * s;

            result.elements[2 + 0 * 4] = x * z * omc + y * s;
            result.elements[2 + 1 * 4] = y * z * omc - x * s;
            result.elements[2 + 2 * 4] = z * z * omc + c;

            return result;
        }

        public static Matrix4 Scale(Vector3 scale)
        {
            Matrix4 result = new Matrix4(1.0f);

            result[0 + 0 * 4] = scale.x;
            result[1 + 1 * 4] = scale.y;
            result[2 + 2 * 4] = scale.z;

            return result;
        }

        public static Matrix4 Invert([In] Matrix4 matrix)
        {
            return matrix.Invert();
        }

        public override string ToString()
        {
            string result = "";
            result += "mat4: (" + rows[0].x + ", " + rows[1].x + ", " + rows[2].x + ", " + rows[3].x + "), ";
            result += "(" + rows[0].y + ", " + rows[1].y + ", " + rows[2].y + ", " + rows[3].y + "), ";
            result += "(" + rows[0].z + ", " + rows[1].z + ", " + rows[2].z + ", " + rows[3].z + "), ";
            result += "(" + rows[0].w + ", " + rows[1].w + ", " + rows[2].w + ", " + rows[3].w + ")";
            return result;
        }

    }
}
