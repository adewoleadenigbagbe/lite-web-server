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
        private string _actionName;

        private string _controllerName;

        public HttpMethod HttpMethod { get; }

        public string Pattern { get; }

        public Route(string actionName, string controllerName, string pattern, HttpMethod httpMethod)
        {
            _actionName = actionName;
            _controllerName = controllerName;
            Pattern = pattern;
            HttpMethod = httpMethod;
        }
    }
}
