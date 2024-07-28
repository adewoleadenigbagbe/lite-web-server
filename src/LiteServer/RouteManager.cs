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
using LiteServer.Http.Attributes;
using System.Reflection;
using System.CodeDom;

namespace LiteServer
{
    internal enum ParamDataType
    {
        Int,
        String,
        Bool,
        Float
    }

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

        public void LoadRoute()
        {
            var controllerBaseType = typeof(ApiControllerBase);
            var controllerAssembly = Assembly.GetAssembly(controllerBaseType);

            var controlleractionlist = controllerAssembly.GetTypes()
           .Where(type => type.IsAssignableFrom(controllerBaseType) && type.GetCustomAttributes().Any(x => x is RouteBaseAttribute))
           .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
           .Where(methodInfo => methodInfo.GetCustomAttributes().Any(x => x is RouteAttribute && x is ActionMethodAttribute))
           .Select(x =>
           {
               var pattern = BuildRegex(x.DeclaringType.GetCustomAttribute<RouteBaseAttribute>().Name + '/' + x.DeclaringType.GetCustomAttribute<RouteAttribute>().Name);
               var method = x.GetCustomAttribute<ActionMethodAttribute>().Method;
               var route = new Route(x.Name, x.DeclaringType.Name, pattern, method);
               return route;
           })
           .OrderBy(x => x.ControllerName)
           .ThenBy(x => x.ActionName).ToList();

            _routes.AddRange(controlleractionlist);
        }


        public Route PickRoute(string url, string httpMethod)
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

        //{id:int}, {name:string}, {price:float}, {exist:bool}
        //api/v1/product/123/search/Dave
        //api/v1/product/{id}/search/{name}
        //api/v1/product/{id:int}/search/{name}
        public string BuildRegex(string input)
        {
            if (input == null || input == string.Empty)
            {
                throw new ArgumentNullException("input cannot be null or Empty");
            }

            input = input.Trim();
            int startIndex = 0;

            var strBuilder = new StringBuilder();
            strBuilder.Append('^');

            for (var i = 0; i < input.Length; i++)
            {
                if (input[i] == '{')
                {
                    startIndex = i;
                }

                else if(input[i] == '}')
                {
                    var substr = input.Substring(startIndex, i - startIndex);
                    // in case we have {id:int} , {name:string} we extract the datatype
                    if (substr.Contains(":"))
                    {
                        var extract = substr.Split(':');
                        var type = extract[1];

                        switch (type)
                        {
                            case "int":
                                strBuilder.Append(@"\d*");
                                break;
                            case "float":
                                strBuilder.Append(@"[0-9]*(?:\.[0-9]*)?");
                                break;
                            case "string":
                                strBuilder.Append(@"\w*");
                                break;
                            case "bool":
                                strBuilder.Append("true|false");
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        strBuilder.Append(@"\w+");
                    }
                }

                else
                {
                    strBuilder.Append(input[i]);
                }
            }
            strBuilder.Append('$');
            return strBuilder.ToString();
        }
    }
}
