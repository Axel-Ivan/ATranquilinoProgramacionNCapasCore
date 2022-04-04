using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class SubPolizaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.SubPoliza subpoliza = new ML.SubPoliza();
            ML.Result result = BL.SubPoliza.GetAll();

            if (result.Correct)
            {
                subpoliza.SubPolizas = new List<object>();
                subpoliza.SubPolizas = result.Objects;
            }

            return View(subpoliza);
        }

        [HttpGet]
        public ActionResult Form(byte? IdSubPoliza)
        {
            ML.SubPoliza subpoliza = new ML.SubPoliza();

            if (IdSubPoliza == null)
            {
                return View(subpoliza);
            }
            else
            {
                ML.Result result = new ML.Result();
                result = BL.SubPoliza.GetById(IdSubPoliza.Value);

                if (result.Correct)
                {
                    subpoliza = ((ML.SubPoliza)result.Object);
                    return View(subpoliza);
                }
            }

            return PartialView("ValidationModal");
        }

        [HttpPost]
        public ActionResult Form(ML.SubPoliza subpoliza)
        {
            if (ModelState.IsValid == false)
            {
                ML.Result result = new ML.Result();

                if (subpoliza.IdSubPoliza == 0)
                {
                    result = BL.SubPoliza.Add(subpoliza);
                    if (result.Correct)
                    {
                        ViewBag.Message = "La subpoliza se agregó correctamente!!!";
                    }
                    else
                    {
                        ViewBag.Message = "La subpoliza no se agregó, ocurrió el siguiente error: " + result.ErrorMessage;
                    }
                }
                else
                {
                    result = BL.SubPoliza.Update(subpoliza);
                    if (result.Correct)
                    {
                        ViewBag.Message = "La subpoliza se actualizó correctamente!!!";
                    }
                    else
                    {
                        ViewBag.Message = "La subpoliza no se actualizó, ocurrió el siguiente error: " + result.ErrorMessage;
                    }
                }

                return PartialView("ValidationModal");
            }
            else
            {
                return View(subpoliza);
            }
        }

        [HttpGet]
        public ActionResult Delete(byte IdSubPoliza)
        {
            ML.Result result = BL.SubPoliza.Delete(IdSubPoliza);

            if(result.Correct)
            {
                ViewBag.Message = "La subpoliza se eliminó correctamente!!!";
            }
            else
            {
                ViewBag.Message = "La subpoliza no se eliminó, ocurrió el siguiente error: " + result.ErrorMessage;
            }

            return PartialView("ValidationModal");
        }
    

    }
}
