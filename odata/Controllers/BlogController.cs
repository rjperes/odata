using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using odata.common;
using System;
using System.Linq;

namespace odata.server.Controllers
{
    [ODataRoutePrefix("Blogs")]
    public class BlogController : ODataController
    {
        private readonly BlogContext _ctx;

        public BlogController(BlogContext ctx)
        {
            this._ctx = ctx;
        }

        //GET https://localhost:5001/odata/blogs
        [ODataRoute]
        //[EnableQuery]
        [PagingValidatorQuery]
        public IQueryable<Blog> Get(/*ODataQueryOptions<Blog> options*/)
        {
            //Expression<Func<Blog, bool>> func = (blog) => blog.Name.Contains("Ricardo");
            //return _ctx.Blogs.Where(func).AsQueryable();
            return _ctx.Blogs.AsQueryable();
        }

        //GET https://localhost:5001/odata/blogs(1)
        [ODataRoute("{id}")]
        public Blog Get([FromODataUri] int id)
        {
            return _ctx.Blogs.Find(id);
        }

        //GET https://localhost:5001/odata/blogs/FindByCreation(date=2009-08-01)
        [EnableQuery]
        [ODataRoute("FindByCreation(date={date})")]
        [HttpGet]
        public IQueryable<Blog> FindByCreation(DateTime date)
        {
            return _ctx.Blogs.Where(x => x.Creation.Date == date);
        }

        //GET https://localhost:5001/odata/blogs/CountPosts(id=1)
        [ODataRoute("CountPosts(id={id})")]
        [HttpGet]
        public int CountPosts(int id)
        {
            return _ctx.Blogs.Where(x => x.BlogId == id).Select(x => x.Posts).Count();
        }

        //POST https://localhost:5001/odata/blogs/Blog(1)/Count
        [ODataRoute("({id})/Count")]
        [HttpPost]
        public int Count([FromODataUri] int id)
        {
            return _ctx.Blogs.Where(x => x.BlogId == id).Select(x => x.Posts).Count();
        }

        //POST https://localhost:5001/odata/blogs(1)/Update
        [HttpPost]
        [ODataRoute("({id})/Update")]
        public Blog Update([FromODataUri] int id, ODataActionParameters parameters)
        {
            var blog = parameters["blog"] as Blog;
            _ctx.Entry(blog).State = EntityState.Modified;
            _ctx.SaveChanges();
            return blog;
        }
    }
}
