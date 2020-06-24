using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using odata.common;
using System;
using System.Linq;

namespace odata.server.Controllers
{
    [ApiVersion("1.0")]
    [Route("odata/Blogs/v{version:apiversion}")]
    public class BlogController : ODataController
    {
        protected readonly BlogContext _ctx;

        public BlogController(BlogContext ctx)
        {
            this._ctx = ctx;
        }

        //GET https://localhost:5001/odata/blogs
        [HttpGet("")]
        [EnableQuery]
        [ApiVersion("1.0")]
        public IQueryable<Blog> Get(/*ODataQueryOptions<Blog> options*/)
        {
            //Expression<Func<Blog, bool>> func = (blog) => blog.Name.Contains("Ricardo");
            //return _ctx.Blogs.Where(func).AsQueryable();
            return _ctx.Blogs.AsQueryable();
        }

        //GET https://localhost:5001/odata/blogs(1)
        [HttpGet("({id})")]
        [ApiVersion("1.0")]
        public Blog Get([FromODataUri] int id)
        {
            return _ctx.Blogs.Find(id);
        }

        //GET https://localhost:5001/odata/blogs/FindByCreation(date=2009-08-01)
        [HttpGet("FindByCreation(date={date})")]
        [EnableQuery]
        [ApiVersion("1.0")]
        public IQueryable<Blog> FindByCreation(DateTime date)
        {
            return _ctx.Blogs.Where(x => x.Creation.Date == date);
        }

        //GET https://localhost:5001/odata/blogs/CountPosts(id=1)
        [HttpGet("CountPosts(id={id})")]
        [ApiVersion("1.0")]
        //[ODataRoute("CountPosts(id={id})")]
        public int CountPosts(int id)
        {
            return _ctx.Blogs.Where(x => x.BlogId == id).Select(x => x.Posts).Count();
        }

        //POST https://localhost:5001/odata/blogs/Blog(1)/Count
        [HttpPost("({id})/Count")]
        [ApiVersion("1.0")]
        //[ODataRoute("({id})/Count")]
        public int Count([FromODataUri] int id)
        {
            return _ctx.Blogs.Where(x => x.BlogId == id).Select(x => x.Posts).Count();
        }

        //POST https://localhost:5001/odata/blogs(1)/Update
        [HttpPost("({id})/Update")]
        [ApiVersion("1.0")]
        //[ODataRoute("({id})/Update")]
        public Blog Update([FromODataUri] int id, ODataActionParameters parameters)
        {
            var blog = parameters["blog"] as Blog;
            _ctx.Entry(blog).State = EntityState.Modified;
            _ctx.SaveChanges();
            return blog;
        }
    }
}