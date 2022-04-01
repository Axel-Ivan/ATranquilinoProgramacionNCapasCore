using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class PolizaController : Controller
    {
        public ActionResult GetAll()
        {
            return View();
        }
    }
}
