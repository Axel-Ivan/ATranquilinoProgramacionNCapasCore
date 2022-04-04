using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpresaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Empresa.GetAll();
            ML.Empresa empresa = new ML.Empresa();

            if (result.Correct)
            {
                empresa.Empresas = new List<object>();
                empresa.Empresas = result.Objects;
            }

            return View(empresa);
        }
        [HttpGet]
        public ActionResult Form(int? IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();

            if (IdEmpresa == null)
            {
                return View(empresa);
            }
            else
            {
                ML.Result result = new ML.Result();
                result = BL.Empresa.GetById(IdEmpresa.Value);

                if (result.Correct)
                {
                    empresa = ((ML.Empresa)result.Object);
                    return View(empresa);
                }
            }

            return PartialView("ValidationModal");
        }

        [HttpPost]
        public ActionResult Form(ML.Empresa empresa)
        {
            if (ModelState.IsValid == false)
            {
                ML.Result result = new ML.Result();
                IFormFile Imagen = Request.Form.Files["fuImagen"];

                if (Imagen != null)
                {
                    byte[] imageArray = ConvertToBytes(Imagen);
                    empresa.Logo = Convert.ToBase64String(imageArray);
                }

                if (empresa.IdEmpresa == 0)
                {
                    result = BL.Empresa.Add(empresa);
                    if (result.Correct)
                    {
                        ViewBag.Message = "La empresa se ingresó correctamente!!!";
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrió un error al momento de ingresar la empresa: " + result.ErrorMessage;
                    }
                }
                else
                {
                    result = BL.Empresa.Update(empresa);
                    if (result.Correct)
                    {
                        ViewBag.Message = "La empresa se actualizó correctamente!!!";
                    }
                    else
                    {
                        ViewBag.Message = "La empresa no se actualizó, ocurrió el siguiente error: " + result.ErrorMessage;
                    }
                }

                return PartialView("ValidationModal");
            }

            else
            {
                return View(empresa);
            }
        }

        [HttpGet]
        public ActionResult Delete(int IdEmpresa)
        {
            ML.Result result = BL.Empresa.Delete(IdEmpresa);

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
