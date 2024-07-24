using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteServer.Http.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class RouteBaseAttribute : Attribute
    {
        private string _name;
        public RouteBaseAttribute(string name)
        { 
            _name = name;
        }
    }
}
