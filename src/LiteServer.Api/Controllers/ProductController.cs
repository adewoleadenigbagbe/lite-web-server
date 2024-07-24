using LiteServer.Api.Models;
using LiteServer.Http;
using LiteServer.Http.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteServer.Api.Controllers
{
    [RouteBase("api/v1/products")]
    public class ProductController : ApiControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public Product GetProductById(int id)
        {
            return null;
        }

        [HttpPost]
        [Route("")]
        public int CreateProduct(Product product)
        {
            return 0;
        }

        [HttpGet]
        [Route("")]
        public int GetProducts()
        {
            return 0;
        }

        [HttpDelete]
        [Route("{id}")]
        public int DeleteProduct(int id)
        {
            return 0;
        }

        [HttpPut]
        [Route("")]
        public int UpdateProduct(Product product)
        {
            return 0;
        }

        [HttpGet]
        [Route("search")]
        public int GetProductByName(string name)
        {
            return 0;
        }
    }
}
