using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteServer.Http.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class ApiVersionAttribute : Attribute
    {
        public string Version { get; }

        public ApiVersionAttribute(string version) 
        { 
            Version = version;
        }
    }
}
