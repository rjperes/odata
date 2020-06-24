using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace odata.server.Configuration
{
    public class BlogPostConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
        {
            builder
                .Function("CountBlogPosts")
                .Returns<int>();
        }
    }
}
