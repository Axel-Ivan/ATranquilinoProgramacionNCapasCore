using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        private IHostingEnvironment _hostingEnvironment; //Dependencies Injection
        private IConfiguration _configuration;
        public LoginController(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.statusUsername = true;
            usuario.statusContrasenia = true;
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Login(ML.Usuario usuario)
        {
            //ML.Result result = BL.Usuario.GetByUsername(usuario);
            ML.Result result = new ML.Result();
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);
                    var responseTask = client.GetAsync("Login/GetByUsername/" + usuario.UserName);
                    responseTask.Wait();
                    var resultAPI = responseTask.Result;

                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        ML.Usuario usuarioItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                        result.Object = usuarioItem;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existen el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            if (result.Correct)
            {
                ML.Usuario usuarioItem = (ML.Usuario)result.Object;
                if(usuarioItem.Contrasenia == usuario.Contrasenia)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    usuario.statusUsername = true;
                    usuario.statusContrasenia = false;
                    return View(usuario);
                }
            }
            else
            {
                usuario.statusUsername = false;
                usuario.statusContrasenia = false;
                return View(usuario);
            }
        }
    }
}
