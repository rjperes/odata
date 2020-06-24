using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.OData;
using System.Linq;

namespace odata.server
{
    public sealed class PagingValidatorQueryAttribute : EnableQueryAttribute
    {
        public override object ApplyQuery(object entity, ODataQueryOptions queryOptions)
        {
            return base.ApplyQuery(entity, queryOptions);
        }

        public override IQueryable ApplyQuery(IQueryable queryable, ODataQueryOptions queryOptions)
        {
            return base.ApplyQuery(queryable, queryOptions);
        }

        public override void ValidateQuery(HttpRequest request, ODataQueryOptions queryOptions)
        {
            if ((queryOptions.Skip != null) && (queryOptions.Skip.Value > 0) && (queryOptions.OrderBy == null))
            {
                throw new ODataException("Cannot use $skip without $orderby.");
            }

            base.ValidateQuery(request, queryOptions);
        }
    }
}
