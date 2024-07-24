using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LiteServer.Http
{
    public class ActionContext
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
