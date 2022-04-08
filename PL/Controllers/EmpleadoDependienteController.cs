using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using PL.Models;


namespace PL.Controllers
{
    public class EmpleadoDependienteController : Controller
    {
        [HttpGet]
        public ActionResult EmpleadoGetAll()
        {
            ML.Empleado empleado = new ML.Empleado();

            ML.Result resultEmpleados = BL.Empleado.GetAll(empleado);

            if (resultEmpleados.Correct)
            {
                empleado.Empleados = new List<object>();
                empleado.Empleados = resultEmpleados.Objects;
            }

            return View(empleado);
        }

        [HttpGet]
        public ActionResult DependientesAsignados(int IdEmpleado)
        {
            ML.EmpleadoDependiente empleadoDependiente = new ML.EmpleadoDependiente();
            empleadoDependiente.Empleado = new ML.Empleado();
            empleadoDependiente.Dependiente = new ML.Dependiente();
            empleadoDependiente.Dependiente.Empleado = new ML.Empleado();
            empleadoDependiente.Dependiente.DependienteTipo = new ML.DependienteTipo();
            ML.Result resultDependientes = BL.Dependiente.GetAllByIdEmpleado(IdEmpleado);

            ML.Result resultEmpleado = BL.Empleado.GetById(IdEmpleado); //Datos del empleado seleccionado (Boxing)

            ML.Empleado empleado = (ML.Empleado)resultEmpleado.Object; //Unboxing

            HttpContext.Session.SetInt32("SessionId", empleado.IdEmpleado); //Asignamos valores a la sesion
            HttpContext.Session.SetString("SessionName", empleado.Nombre);
            HttpContext.Session.SetString("SessionApellidoPaterno", empleado.ApellidoPaterno);
            HttpContext.Session.SetString("SessionApellidoMaterno", empleado.ApellidoMaterno);

            if (resultDependientes.Correct)
            {
                empleadoDependiente.Dependiente.Dependientes = new List<object>();
                empleadoDependiente.Dependiente.Dependientes = resultDependientes.Objects;

                empleadoDependiente.Empleado.IdEmpleado = empleado.IdEmpleado;
                empleadoDependiente.Empleado.Nombre = empleado.Nombre;
                empleadoDependiente.Empleado.ApellidoPaterno = empleado.ApellidoPaterno;
                empleadoDependiente.Empleado.ApellidoMaterno = empleado.ApellidoMaterno;
            }

            return View(empleadoDependiente);
        }

        [HttpGet]
        public ActionResult Form()
        {
            ML.EmpleadoDependiente empleadoDependiente = new ML.EmpleadoDependiente();
            empleadoDependiente.Dependiente = new ML.Dependiente();
            empleadoDependiente.Dependiente.DependienteTipo = new ML.DependienteTipo();

            ML.Result result = BL.DependienteTipo.GetAll();
            empleadoDependiente.Dependiente.DependienteTipo.DependientesTipo = result.Objects;
            empleadoDependiente.Empleado = new ML.Empleado();

            empleadoDependiente.Empleado.IdEmpleado = Convert.ToInt32(HttpContext.Session.GetInt32("SessionId")); //Obtenemos los valores
            empleadoDependiente.Empleado.Nombre = HttpContext.Session.GetString("SessionName");
            empleadoDependiente.Empleado.ApellidoPaterno = HttpContext.Session.GetString("SessionApellidoPaterno");
            empleadoDependiente.Empleado.ApellidoMaterno = HttpContext.Session.GetString("SessionApellidoMaterno");


            return View(empleadoDependiente);
        }

        [HttpPost]
        public ActionResult Form(ML.EmpleadoDependiente empleadoDependiente)
        {
            if(ModelState.IsValid)
            {
                ML.Result result = new ML.Result();

                result = BL.Dependiente.Add(empleadoDependiente);

                if(result.Correct)
                {
                    ViewBag.Message = "El dependiente ha sido agregado correctamente!!!";
                }
                else
                {
                    ViewBag.Message = "El dependiente no se agregó, ocurrió el siguiente error: " + result.ErrorMessage;
                }
            }
            return PartialView("ValidationModal");
        }

        [HttpGet]
        public ActionResult Delete(int IdDependiente)
        {
            ML.Result result = BL.Dependiente.Delete(IdDependiente);

            if(result.Correct)
            {
                ViewBag.Message = "El dependiente se eliminó el correctamente!!!";
            }
            else
            {
                ViewBag.Message = "No se eliminó al dependiente, ocurrió el siguiente problema: " + result.ErrorMessage;
            }

            return PartialView("ValidationModal");
        }

    }
}
