using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Form(int? IdEmpleado)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Form(ML.Empleado empleado)
        {
            return View();
        }
    }
}
