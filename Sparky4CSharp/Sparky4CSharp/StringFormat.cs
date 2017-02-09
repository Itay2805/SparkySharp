using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP
{
    public class StringFormat
    {

        public static string Hex(decimal input)
        {
            return string.Format("%02x", input);
        }

        public static string Float(float input, uint places = 2)
        {
            return input.ToString().Substring(0, (int)(input.ToString().IndexOf(".") + places + 1));
        }

    }
}
