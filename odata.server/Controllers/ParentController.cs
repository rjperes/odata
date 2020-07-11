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

        //GET https://localhost:5001/odata/Parents
        [ODataRoute]
        public IQueryable<Parent> Get()
        {
            return this._ctx.Parents.AsQueryable();
        }

        //GET https://localhost:5001/odata/Parents(1)
        [ODataRoute("{id}")]
        public Parent Get([FromODataUri] int id)
        {
            return this._ctx.Parents.Find(id);
        }
    }
}
