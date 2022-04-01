using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();

            ML.Result resultEmpresas = BL.Empresa.GetAll();
            ML.Result resultEmpleados = BL.Empleado.GetAll(empleado);
            empleado.Empresa = new ML.Empresa();
            //empleado = new ML.Empleado();

            if (resultEmpleados.Correct)
            {
                empleado.Empleados = new List<object>();
                empleado.Empresa.Empresas = resultEmpresas.Objects;
                empleado.Empleados = resultEmpleados.Objects;               
            }
            return View(empleado);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Empleado empleado)
        {
            //ML.Empleado empleado = new ML.Empleado();
            ML.Result resultEmpresas = BL.Empresa.GetAll();
            ML.Result resultEmpleados = BL.Empleado.GetByIdEmpresa(empleado.Empresa.IdEmpresa);
            empleado.Empresa = new ML.Empresa();

            if (resultEmpleados.Correct)
            {
                empleado.Empleados = new List<object>();
                empleado.Empresa.Empresas = resultEmpresas.Objects;
                empleado.Empleados = resultEmpleados.Objects;
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
            if(ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                ML.Result result = new ML.Result();
                IFormFile Imagen = Request.Form.Files["fuImagen"];

                if(Imagen != null)
                {
                    byte[] imageArray = ConvertToBytes(Imagen);
                    empleado.Foto = Convert.ToBase64String(imageArray);
                }

                if(empleado.IdEmpleado == 0)
                {
                    result = BL.Empleado.Add(empleado);
                    if(result.Correct)
                    {
                        ViewBag.Message = "El empleado se ha ingresado correctamente!";
                    }
                    else
                    {
                        ViewBag.Message = "El empleado no se ingresó, ocurrió el error: " + result.ErrorMessage;
                    }
                }
                else
                {
                    result = BL.Empleado.Update(empleado);
                    if(result.Correct)
                    {
                        ViewBag.Message = "El empleado se actualizó correctamente!";
                    }
                    else
                    {
                        ViewBag.Message = "El empleado no se actualizó, ocurrió el error: " + result.ErrorMessage;
                    }
                }

                return PartialView("ValidationModal");

            }
            else
            {
                empleado.Poliza = new ML.Poliza();
                empleado.Empresa = new ML.Empresa();
                ML.Result resultPoliza = BL.Poliza.GetAll();
                ML.Result resultEmpresa = BL.Empresa.GetAll();
                empleado.Poliza.Polizas = resultPoliza.Objects;
                empleado.Empresa.Empresas = resultEmpresa.Objects;

                return View(empleado);
            }
        }

        [HttpGet]
        public ActionResult Delete(int IdEmpleado)
        {
            ML.Result result = BL.Empleado.Delete(IdEmpleado);

            if(result.Correct)
            {
                ViewBag.Message = "El empleado se eliminó correctamente!";
            }
            else
            {
                ViewBag.Message = "El empleado no se eliminó, ocurrió el error: " + result.ErrorMessage;
            }

            return PartialView("ValidationModal");
        }

        public static byte[] ConvertToBytes(IFormFile Imagen)
        {
            using var fileStream = Imagen.OpenReadStream();
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);
            return bytes;
        }

    }
}
