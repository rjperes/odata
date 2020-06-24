using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using odata.common;

namespace odata.server.Configuration
{
    public class PostConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
        {
            var products = builder.EntitySet<Post>("Posts").EntityType.HasKey(p => p.PostId);
        }
    }
}
