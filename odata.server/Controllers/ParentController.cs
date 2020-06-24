using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using odata.common;
using System.Linq;

namespace odata.server.Controllers
{
    [ODataRoutePrefix("Parents")]
    public class ParentController : ODataController
    {
        private ParentChildContext _ctx;

        public ParentController(ParentChildContext ctx)
        {
            this._ctx = ctx;
        }

        [ODataRoute]
        public IQueryable<Parent> Get()
        {
            return this._ctx.Parents.AsQueryable();
        }

        [ODataRoute("{id}")]
        public Parent Get([FromODataUri] int id)
        {
            return this._ctx.Parents.Find(id);
        }
    }
}
