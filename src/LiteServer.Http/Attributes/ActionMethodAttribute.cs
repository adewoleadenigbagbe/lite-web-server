using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteServer.Http.Attributes
{
    public abstract class ActionMethodAttribute : Attribute
    {
        public abstract string Method { get; }
    }
}
