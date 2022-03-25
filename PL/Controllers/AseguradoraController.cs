using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AseguradoraController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Aseguradora.GetAll();
            ML.Aseguradora aseguradora = new ML.Aseguradora();

            if (result.Correct)
            {
                aseguradora.Aseguradoras = new List<object>();
                aseguradora.Aseguradoras = result.Objects;
            }

            return View(aseguradora);
        }

        [HttpGet]
        public ActionResult Form(int? IdAseguradora)
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            aseguradora.Usuario = new ML.Usuario();
            ML.Usuario usuario = new ML.Usuario(); //Here
            ML.Result resultUsuario = BL.Usuario.GetAll(usuario); //Here
            aseguradora.Usuario.Usuarios = resultUsuario.Objects;

            if (IdAseguradora == null)
            {
                aseguradora.Usuario.Usuarios = resultUsuario.Objects;
                return View(aseguradora);
            }
            else
            {
                ML.Result result = new ML.Result();
                result = BL.Aseguradora.GetById(IdAseguradora.Value);

                if (result.Correct)
                {
                    aseguradora = ((ML.Aseguradora)result.Object);
                    aseguradora.Usuario.Usuarios = resultUsuario.Objects;
                    return View(aseguradora);
                }
            }

            return PartialView("ValidationModal");
        }

        [HttpPost]
        public ActionResult Form(ML.Aseguradora aseguradora)
        {
            //HttpPostedFileBase file = Request.Files["fuImagen"];
            ML.Result result = new ML.Result();
            //if (file.ContentLength > 0)
            //{
            //    aseguradora.Imagen = ConvertToBytes(file);
            //}

            if (aseguradora.IdAseguradora == 0)
            {
                result = BL.Aseguradora.Add(aseguradora);
                if (result.Correct)
                {
                    ViewBag.Message = "La aseguradora se ingresó correctamente!!!";
                }
                else
                {
                    ViewBag.Message = "Ocurrió un error al momento de ingresar la aseguradora: " + result.ErrorMessage;
                }
            }
            else
            {
                result = BL.Aseguradora.Update(aseguradora);
                if (result.Correct)
                {
                    ViewBag.Message = "La aseguradora se actualizó correctamente!!!";
                }
                else
                {
                    ViewBag.Message = "La aseguradora no se actualizó, ocurrió el siguiente error: " + result.ErrorMessage;
                }
            }

            return PartialView("ValidationModal");
        }

        [HttpGet]
        public ActionResult Delete(int IdAseguradora)
        {
            ML.Result result = BL.Aseguradora.Delete(IdAseguradora);

            if (result.Correct)
            {
                ViewBag.Message = "El registro de aseguradora se eliminó correctamente!!!";
            }
            else
            {
                ViewBag.Message = "El registro no se eliminó, ocurrió el error: " + result.ErrorMessage;
            }

            return PartialView("ValidationModal");
        }

        //Método para convertir imagen 

    }
}
