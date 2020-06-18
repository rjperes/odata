using Microsoft.AspNetCore.Mvc;

namespace odata.server.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
