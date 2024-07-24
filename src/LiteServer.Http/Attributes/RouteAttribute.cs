using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteServer.Http.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class RouteAttribute : Attribute
    {
        private string _name;
        public RouteAttribute(string name)
        {
            _name = name;
        }
    }
}
