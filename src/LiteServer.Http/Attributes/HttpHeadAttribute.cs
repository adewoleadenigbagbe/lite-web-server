using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteServer.Http.Attributes
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple = false, Inherited =false)]
    public sealed class HttpGetAttribute : ActionMethodAttribute
    {
        public override string Method { get; }  = "GET";
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class HttpPostAttribute : ActionMethodAttribute
    {
        public override string Method { get; } = "POST";

    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class HttpPutAttribute : ActionMethodAttribute
    {
        public override string Method { get; } = "PUT";

    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class HttpDeleteAttribute : ActionMethodAttribute
    {
        public override string Method { get; } = "DELETE";
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class HttpHeadAttribute : ActionMethodAttribute
    {
        public override string Method { get; } = "HEAD";
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class HttpOptionsAttribute : ActionMethodAttribute
    {
        public override string Method { get; } = "OPTIONS";
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class HttpPatchAttribute : ActionMethodAttribute
    {
        public override string Method { get; } = "PATCH";
    }
}
