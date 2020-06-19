using Microsoft.OData.Client;
using odata.common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace odata.client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var ctx = new DataServiceContext(new Uri("http://localhost:5000/odata"));

            Console.WriteLine("Click when ready to go...");
            Console.ReadLine();

            var blogs = await ctx
                .CreateQuery<Blog>("Blogs")
                .IncludeCount()
                .ExecuteAsync();

            var blogQuery = ctx
                .CreateSingletonQuery<Blog>("Blogs")
                .AddQueryOption("id", 1)
                //.Where(x => x.BlogId == 1)
                as DataServiceQuery<Blog>;

            var blog = (await blogQuery.ExecuteAsync()).SingleOrDefault();
            


        }
    }
}
