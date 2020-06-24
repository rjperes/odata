using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using odata.common;
using System.Linq;

namespace odata.server.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("odata/Blogs/v{version:apiversion}")]
    public class BlogController : ODataController
    {
        private readonly BlogContext _ctx;

        public BlogController(BlogContext ctx)
        {
            this._ctx = ctx;
        }

        //GET https://localhost:5001/odata/blogs
        [EnableQuery]
        [ApiVersion("2.0")]
        public IQueryable<Blog> Get()
        {
            return _ctx.Blogs.AsQueryable();
        }
    }
}
