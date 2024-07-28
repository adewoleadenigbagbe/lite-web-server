﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using LiteServer.Http;
using LiteServer.Http.Attributes;
using System.CodeDom;

namespace LiteServer
{
    public class Server
    {
        private const int _port = 3022;
        private const int _maxSimultaneousConnections = 200;
        private readonly HttpListener _listener = new HttpListener();
        private readonly Semaphore _semaphore = new Semaphore(_maxSimultaneousConnections, _maxSimultaneousConnections);
        private readonly RouterManager _routeManager = new RouterManager();

        public void Start()
        {
            //load up the route first



            _listener.Prefixes.Add("http://localhost" +":" +_port +"/");

            var hostIps = GetHostIps();
            foreach (var ip in hostIps)
            {
                _listener.Prefixes.Add("http://" + ip + ":" + _port + "/");
            }

            if (_listener.IsListening)
            {
                throw new InvalidOperationException("Server is currently running");
            }

            _listener.Start();
            Console.WriteLine("Server is currently listening on port:" + _port);

            while (true)
            {
                _semaphore.WaitOne();
                ProcessRequest();
            }


            //_listener.Abort();
        }

        private void Stop()
        {
            _listener.Stop();
        }

        private static IEnumerable<string> GetHostIps()
        {
            var hostName = Dns.GetHostName();
            var ipAddresses =  Dns.GetHostEntry(hostName).AddressList;

            foreach (var ipAddress in ipAddresses.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork))
            {
                yield return ipAddress.ToString();
            }
        }

        private async void ProcessRequest()
        {
           var _stopWatch = new Stopwatch();
           var requestContext = await _listener.GetContextAsync();

            _stopWatch.Start();
            var buffer = Encoding.ASCII.GetBytes("Hi there from the web server");
            requestContext.Response.StatusCode = (int)HttpStatusCode.OK;
            requestContext.Response.ContentType = "text/plain";
            requestContext.Response.OutputStream.Write(buffer, 0, buffer.Length);
            requestContext.Response.OutputStream.Close();

            _stopWatch.Stop();

            _semaphore.Release();

            Console.WriteLine("Request was executed in {0} Milliseconds",_stopWatch.ElapsedMilliseconds);
        }

        public void Load()
        {
            var controllerBaseType = typeof(ApiControllerBase);
            var controllerAssembly = Assembly.GetAssembly(controllerBaseType);

             var controlleractionlist = controllerAssembly.GetTypes()
            .Where(type => type.IsAssignableFrom(controllerBaseType) && type.GetCustomAttributes().Any(x => x is RouteBaseAttribute))
            .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
            .Where(methodInfo => methodInfo.GetCustomAttributes().Any(x => x is RouteAttribute && x is ActionMethodAttribute))
            //.Select(x => new { 
            //    Controller = x.DeclaringType.Name, 
            //    Action = x.Name,
            //    HttpMethod = x.GetCustomAttribute<ActionMethodAttribute>().Method,
            //    //ReturnType = x.ReturnType.Name, 
            //    //Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) 
            //})
            .Select(x => new {
                            Controller = x.DeclaringType.Name,
                            Action = x.Name,
                            HttpMethod = x.GetCustomAttribute<ActionMethodAttribute>().Method,
                            //ReturnType = x.ReturnType.Name, 
                            //Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) 
            })
            .OrderBy(x => x.Controller)
            .ThenBy(x => x.Action).ToList();
        }
    }
}
