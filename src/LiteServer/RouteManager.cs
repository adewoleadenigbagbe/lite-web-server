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
using System.Collections;

namespace LiteServer
{
    internal class RouterManager
    {
        private readonly RouteCollection _routes;

        public RouterManager() : this(new RouteCollection())
        {
        }

        public RouterManager(RouteCollection routes)
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

                if(route.HttpMethod == httpMethod.Method)
                {
                    return route;
                }
            }

            return null;
        }
    }
}
