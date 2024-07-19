using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LiteServer.Http
{
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
