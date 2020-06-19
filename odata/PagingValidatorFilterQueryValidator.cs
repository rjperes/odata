using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Query.Validators;

namespace odata.server
{
    public sealed class PagingValidatorFilterQueryValidator : FilterQueryValidator
    {
        public PagingValidatorFilterQueryValidator(DefaultQuerySettings defaultQuerySettings) : base(defaultQuerySettings)
        {
        }
    }
}