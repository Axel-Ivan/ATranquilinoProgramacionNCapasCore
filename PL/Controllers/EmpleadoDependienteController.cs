using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoDependienteController : Controller
    {
        [HttpGet]
        public ActionResult GetAll(int IdEmpleado)
        {
            ML.Dependiente dependiente = new ML.Dependiente();

            ML.Result resultDependientes = BL.Dependiente.GetAllByIdEmpleado(IdEmpleado);


            if(resultDependientes.Correct)
            {
                dependiente.Dependientes = new List<object>();
                dependiente.Dependientes = resultDependientes.Objects;
            }

            return View(dependiente);
        }
        [HttpPost]
        public ActionResult Form(ML.Dependiente dependiente)
        {
            return View();
        }
    }
}
