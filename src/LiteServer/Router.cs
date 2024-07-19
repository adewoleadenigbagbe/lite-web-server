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

namespace LiteServer
{
    public class RouterManager
    {
        private readonly RouteTable routeTable;

        public RouterManager()
        {
        }
    }

    public class RouteTable
    {
        private readonly ConcurrentDictionary<(string, string), ActionMethod> _table = new ConcurrentDictionary<(string, string), ActionMethod>();

        public RouteTable()
        {
        }

        public ActionMethod GetActionMethod((string,string) key)
        {
            if (!_table.TryGetValue(key, out var actionMethod)){
                throw new KeyNotFoundException();
            }
            return actionMethod;
        }
    }
}
