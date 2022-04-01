using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();

            empleado.Nombre = (empleado.Nombre == null) ? "" : empleado.Nombre; //Operador ternario
            empleado.ApellidoPaterno = (empleado.ApellidoPaterno == null) ? "" : empleado.ApellidoPaterno;
            empleado.ApellidoMaterno = (empleado.ApellidoMaterno == null) ? "" : empleado.ApellidoMaterno;

            ML.Result result = BL.Empleado.GetAll(empleado);
            empleado = new ML.Empleado();

            if (result.Correct)
            {
                empleado.Empleados = new List<object>();
                empleado.Empleados = result.Objects;
            }
            return View(empleado);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Empleado empleado)
        {
            empleado.Nombre = (empleado.Nombre == null) ? "" : empleado.Nombre; //Operador ternario
            empleado.ApellidoPaterno = (empleado.ApellidoPaterno == null) ? "" : empleado.ApellidoPaterno;
            empleado.ApellidoMaterno = (empleado.ApellidoMaterno == null) ? "" : empleado.ApellidoMaterno;

            ML.Result result = BL.Empleado.GetAll(empleado);
            empleado = new ML.Empleado();

            if (result.Correct)
            {
                empleado.Empleados = new List<object>();
                empleado.Empleados = result.Objects;
            }
            return View(empleado);
        }

        [HttpGet]
        public ActionResult Form(int? IdEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            empleado.Poliza = new ML.Poliza();
            ML.Result resultEmpresas = BL.Empresa.GetAll(); //All GetAll´s
            ML.Result resultPolizas = BL.Poliza.GetAll(); //Checar el INNER JOIN

            empleado.Empresa.Empresas = resultEmpresas.Objects;
            empleado.Poliza.Polizas = resultPolizas.Objects;

            if (IdEmpleado == null)
            {
                empleado.Empresa.Empresas = resultEmpresas.Objects;
                empleado.Poliza.Polizas = resultPolizas.Objects;
                return View(empleado);
            }
            else
            {
                ML.Result result = new ML.Result();
                result = BL.Empleado.GetById(IdEmpleado.Value);

                if (result.Correct)
                {
                    empleado = ((ML.Empleado)result.Object);
                    empleado.Empresa.Empresas = resultEmpresas.Objects;
                    empleado.Poliza.Polizas = resultPolizas.Objects;
                    return View(empleado);
                }
            }

            return PartialView("ValidationModal");
        }

        [HttpPost]
        public ActionResult Form(ML.Empleado empleado)
        {
            return View();
        }
    }
}
