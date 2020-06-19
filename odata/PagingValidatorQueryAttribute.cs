using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.OData;

namespace odata.server
{
    public sealed class PagingValidatorQueryAttribute : EnableQueryAttribute
    {
        public override void ValidateQuery(HttpRequest request, ODataQueryOptions queryOptions)
        {
            queryOptions.Filter.Validator = new PagingValidatorFilterQueryValidator(new DefaultQuerySettings());

            if ((queryOptions.Skip != null) && (queryOptions.OrderBy == null))
            {
                throw new ODataException("Cannot use $skip without $orderby.");
            }

            base.ValidateQuery(request, queryOptions);
        }
    }
}
