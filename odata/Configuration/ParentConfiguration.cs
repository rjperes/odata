using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using odata.common;

namespace odata.server.Configuration
{
    public class ParentConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
        {
            builder
                .EntitySet<Parent>("Parents")
                .EntityType
                .Select()
                .Expand()
                .OrderBy()
                .Filter()
                .Count();

            builder
                .EntitySet<Child>("Children")
                .EntityType
                .Select()
                .Expand()
                .OrderBy()
                .Filter()
                .Count();
        }
    }
}
