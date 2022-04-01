using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class DependienteController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {

            return View();
        }
    }
}
