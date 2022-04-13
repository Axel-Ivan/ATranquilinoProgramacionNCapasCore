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
            ML.Result resultEmpleados = BL.Empleado.GetByIdEmpresa(empleado.Empresa.IdEmpresa.Value);
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

        [HttpPost]
        public ActionResult CargaMasiva()
        {
            IFormFile file = Request.Form.Files["Archivo"];

            using (StreamReader archivo = new StreamReader(file.OpenReadStream()))
            {
                ML.Result result = new ML.Result();
                string line;
                bool isFirstLine = true;
                ML.Result resultErrores = new ML.Result();
                resultErrores.Objects = new List<object>();

                while ((line = archivo.ReadLine()) != null)
                {
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        line = archivo.ReadLine();
                    }

                    Console.WriteLine(line);
                    string[] datos = line.Split('|');

                    ML.Empleado empleado = new ML.Empleado();
                    empleado.RFC = datos[0];
                    empleado.Nombre = datos[1];
                    empleado.ApellidoPaterno = datos[2];
                    empleado.ApellidoMaterno = datos[3];
                    empleado.Email = datos[4];
                    empleado.Telefono = datos[5];
                    empleado.FechaNacimiento = datos[6];
                    empleado.NSS = datos[7];
                    empleado.FechaIngreso = datos[8];
                    empleado.Empresa = new ML.Empresa();
                    empleado.Empresa.IdEmpresa = Convert.ToInt32(datos[9]);
                    empleado.Poliza = new ML.Poliza();
                    empleado.Poliza.IdPoliza = Convert.ToInt32(datos[10]);

                    result = BL.Empleado.Add(empleado);

                    if (result.Correct == false)
                    {
                        resultErrores.Objects.Add("Puede haber un error en el RFC: " + empleado.RFC + ", "
                        + "Puede haber un error en el Nombre: " + empleado.Nombre + ", "
                        + "Puede haber un error en el Apellido Paterno: " + empleado.ApellidoPaterno + ", "
                        + "Puede haber un error en el Apellido Materno: " + empleado.ApellidoMaterno + ", "
                        + "Puede haber un error en el Email: " + empleado.Email + ", "
                        + "Puede haber un error en el Telefono: " + empleado.Telefono + ", "
                        + "Puede haber un error en la Fecha de Nacimiento: " + empleado.FechaNacimiento + ", "
                        + "Puede haber un error en el NSS: " + empleado.NSS + ", "
                        + "Puede haber un error en la Fecha de Ingreso: " + empleado.FechaIngreso + ", "
                        + "Puede haber un error en el Id de la Empresa" + empleado.Empresa.IdEmpresa + ", "
                        + "Puede haber un error en el Id de la Poliza: " + empleado.Poliza.IdPoliza + ", "
                        + "El error general fue:" + result.ErrorMessage);
                    }
                    HttpContext.Session.SetInt32("EstadoError", 0);
                }

                if (resultErrores != null)
                {
                    TextWriter textError = new StreamWriter(@"C:\Users\digis\Documents\Axel Iván Tranquilino Beltran\ErroresCargaMasiva.txt");
                    foreach (string error in resultErrores.Objects)
                    {
                        textError.WriteLine(error);
                    }
                    textError.Close();

                    HttpContext.Session.SetInt32("EstadoError", 1);
                    //empleadoDependiente.Empleado.IdEmpleado = Convert.ToInt32(HttpContext.Session.GetInt32(SessionId));
                }
            }

            ViewBag.Message = "El archivo se subió y ha sido leído, si es necesario, revise si existe algún error revisando el archivo ErroresCargaMasiva que podrá descargar";

            return PartialView("ValidationModal");
        }


        [HttpPost]
        public ActionResult Download()
        {
            string filePath = @"C:\Users\digis\Documents\Axel Iván Tranquilino Beltran\ErroresCargaMasiva.txt";
            string fileName = "ErroresCargaMasiva.txt";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", fileName);

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
