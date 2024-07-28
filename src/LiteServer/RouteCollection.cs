using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteServer
{
    internal class RouteCollection : IReadOnlyCollection<Route>, IEnumerable<Route>, IEnumerable
    {
        private readonly List<Route> _routes = new List<Route>();

        public int Count => _routes.Count;

        public IReadOnlyCollection<Route> Routes => _routes;

        public IEnumerator<Route> GetEnumerator()
        {
            return _routes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_routes).GetEnumerator();
        }

        public void Add(Route route)
        {
            if (route == null)
            {
                throw new ArgumentNullException(nameof(route));
            }

            _routes.Add(route);
        }

        public void AddRange(IEnumerable<Route> routes)
        {
            _routes.AddRange(routes);
        }
    }
}
