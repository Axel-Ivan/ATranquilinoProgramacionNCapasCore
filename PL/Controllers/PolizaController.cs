using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class PolizaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Poliza poliza = new ML.Poliza();
            poliza.SubPoliza = new ML.SubPoliza();
            poliza.Usuario = new ML.Usuario();
            ML.Result resultPolizas = new ML.Result();
            resultPolizas = BL.Poliza.GetAll();

            if(resultPolizas.Correct)
            {
                poliza.Polizas = new List<object>();
                poliza.Polizas = resultPolizas.Objects;
            }
            return View(poliza);
        }

        [HttpGet]
        public ActionResult Form(int? IdPoliza)
        {
            ML.Poliza poliza = new ML.Poliza();
            poliza.SubPoliza = new ML.SubPoliza();
            poliza.Usuario = new ML.Usuario();
            ML.Usuario usuario = new ML.Usuario();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";

            ML.Result resultPolizas = new ML.Result();
            ML.Result resultUsuarios = new ML.Result();

            resultPolizas = BL.SubPoliza.GetAll();
            resultUsuarios = BL.Usuario.GetAll(usuario);

            if(IdPoliza == 0 || IdPoliza == null)
            {
                poliza.SubPoliza.SubPolizas = resultPolizas.Objects;
                poliza.Usuario.Usuarios = resultUsuarios.Objects;
                return View(poliza);
            }
            else
            {
                ML.Result result = new ML.Result();
                result = BL.Poliza.GetById(IdPoliza.Value);

                if(result.Correct)
                {
                    poliza = ((ML.Poliza)result.Object);
                    poliza.SubPoliza.SubPolizas = resultPolizas.Objects;
                    poliza.Usuario.Usuarios = resultUsuarios.Objects;
                    return View(poliza);
                }
            }
            return PartialView("ValidationModal");
        }

        [HttpPost]
        public ActionResult Form(ML.Poliza poliza)
        {
            if(ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                ML.Result result = new ML.Result();

                if(poliza.IdPoliza == 0)
                {
                    result = BL.Poliza.Add(poliza);
                    if(result.Correct)
                    {
                        ViewBag.Message = "La poliza se agregó correctamente!!!";
                    }
                    else
                    {
                        ViewBag.Message = "La poliza no se agregó, ocurrió el siguiente error: " + result.ErrorMessage;
                    }
                }
                else
                {
                    result = BL.Poliza.Update(poliza);
                    if(result.Correct)
                    {
                        ViewBag.Message = "La poliza se actualizó correctamente!!!";
                    }
                    else
                    {
                        ViewBag.Message = "La poliza no se actualizó, ocurrió el siguiente error: " + result.ErrorMessage;
                    }
                }

                return PartialView("ValidationModal");
            }
            else
            {
                poliza.SubPoliza = new ML.SubPoliza();
                poliza.Usuario = new ML.Usuario();
                ML.Usuario usuario = new ML.Usuario();
                usuario.Nombre = "";
                usuario.ApellidoPaterno = "";
                usuario.ApellidoMaterno = "";
                ML.Result resultSubPolizas = BL.SubPoliza.GetAll();
                ML.Result resultUsuarios = BL.Usuario.GetAll(usuario);
                poliza.SubPoliza.SubPolizas = resultSubPolizas.Objects;
                poliza.Usuario.Usuarios = resultUsuarios.Objects;

                return View(poliza);
            }         
        }

        [HttpGet]
        public ActionResult Delete(int IdPoliza)
        {
            ML.Result result = BL.Poliza.Delete(IdPoliza);

            if(result.Correct)
            {
                ViewBag.Message = "La poliza se eliminó correctamente!!!";
            }
            else
            {
                ViewBag.Message = "La poliza no se eliminó, ocurrió el error: " + result.ErrorMessage;
            }

            return PartialView("ValidationModal");
        }

    }
}
