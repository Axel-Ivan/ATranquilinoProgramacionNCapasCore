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

            return View();
        }

    }
}
