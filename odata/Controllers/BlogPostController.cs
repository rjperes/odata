using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace odata.server.Controllers
{
    public class BlogPostController : ODataController
    {
        private readonly BlogContext _ctx;

        public BlogPostController(BlogContext ctx)
        {
            this._ctx = ctx;
        }

        //GET https://localhost:5001/odata/CountBlogPosts()
        [HttpGet]
        [ODataRoute("CountBlogPosts()")]
        public int CountBlogPosts()
        {
            return _ctx.Posts.Count();
        }
    }
}
