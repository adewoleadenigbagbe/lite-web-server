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

    public class ActionMethod
    {
        private Uri _uri;
        private HttpMethod httpMethod;
        private string _actionName;
        private string _controllerName;
        private object responseValue;
        private Type responseType;
        private Func<object[], object> actionFunc;
        private IList<MethodParameter> parameterCollection;
        private IList<Attribute> actionAttributes;
    }

    public class MethodParameter
    {
        private object value { get; set; }

        private Type type { get; set; }

        private int order { get; set; }
    }

    public class ControllerContext
    {
        private HttpListenerRequest _request;
        private HttpListenerResponse _response;
        private Uri _uri;
        private ModelState _modelState;
    }

    public class ModelState
    {
    } 

}
