using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LiteServer
{
    internal class Route
    {
        public string ActionName { get; }

        public string ControllerName { get; }

        public string HttpMethod { get; }

        public string Pattern { get; }

        public Route(string actionName, string controllerName, string pattern, string httpMethod)
        {
            ActionName = actionName;
            ControllerName = controllerName;
            Pattern = pattern;
            HttpMethod = httpMethod;
        }
    }
}
