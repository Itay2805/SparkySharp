using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Graphics.Shaders
{
    public abstract class ShaderResourceDeclaration
    {

        public abstract string GetName();
        public abstract uint GetRegister();
        public abstract uint GetCount();

    }
}
