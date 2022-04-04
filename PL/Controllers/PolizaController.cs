using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class PolizaController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Poliza poliza = new ML.Poliza();

            return View();
        }
    }
}
