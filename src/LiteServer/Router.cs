using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Net;

using LiteServer.Http;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace LiteServer
{
    public class RouterManager
    {
        private readonly IList<Route> _routes;

        public RouterManager(IList<Route> routes)
        {
           _routes = routes;
        }


        public Route PickRoute(string url, HttpMethod httpMethod)
        {
            foreach (var route in _routes)
            {
                if(!Regex.IsMatch(url, route.Pattern))
                {
                    continue;
                }

                if(route.HttpMethod == httpMethod)
                {
                    return route;
                }
            }

            return null;
        }
    }

    public class Route
    {
        private string _actionName;
        private string _controllerName;

        public HttpMethod HttpMethod { get; }

        public string Pattern { get;}

        public Route(string actionName, string controllerName, string pattern, HttpMethod httpMethod)
        {
           _actionName = actionName;
           _controllerName = controllerName;
           Pattern = pattern;
           HttpMethod = httpMethod;
        }
    }

}
