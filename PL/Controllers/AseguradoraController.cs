using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class AseguradoraController : Controller
    {
        private IHostingEnvironment _hostingEnvironment; //Dependencies Injection
        private IConfiguration _configuration;
        public AseguradoraController(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            //ML.Result result = BL.Aseguradora.GetAll();
            //ML.Aseguradora aseguradora = new ML.Aseguradora();

            //if (result.Correct)
            //{
            //    aseguradora.Aseguradoras = new List<object>();
            //    aseguradora.Aseguradoras = result.Objects;
            //}

            //return View(aseguradora);

            ML.Result resultAseguradoras = new ML.Result();
            resultAseguradoras.Objects = new List<Object>();
            ML.Aseguradora aseguradora = new ML.Aseguradora();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]); //AppSettings Json

                //var responseTask = client.GetAsync("Aseguradora");
                var responseTask = client.GetAsync("Aseguradora/GetAll");

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Aseguradora resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Aseguradora>(resultItem.ToString());
                        resultAseguradoras.Objects.Add(resultItemList);
                    }
                }
                aseguradora.Aseguradoras = resultAseguradoras.Objects;
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
            else //GetById - Update
            {
                //ML.Result result = new ML.Result();
                //result = BL.Aseguradora.GetById(IdAseguradora.Value);
                ML.Aseguradora aseguradoraItem = new ML.Aseguradora();
                ML.Result result = new ML.Result();
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebAPI"]);
                        var responseTask = client.GetAsync("Aseguradora/GetById/" + IdAseguradora);
                        responseTask.Wait();
                        var resultAPI = responseTask.Result;

                        if (resultAPI.IsSuccessStatusCode)
                        {
                            var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            aseguradoraItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Aseguradora>(readTask.Result.Object.ToString());
                            result.Object = aseguradoraItem;

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en la tabla Departamento";
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
                    //aseguradora = ((ML.Aseguradora)result.Object);
                    //aseguradora.Usuario.Usuarios = resultUsuario.Objects;
                    aseguradoraItem.Usuario.Usuarios = resultUsuario.Objects;
                    return View(aseguradoraItem);
                }
            }

            return PartialView("ValidationModal");
        }

        [HttpPost]
        public ActionResult Form(ML.Aseguradora aseguradora)
        {
            if(ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                ML.Result result = new ML.Result();
                IFormFile Imagen = Request.Form.Files["fuImagen"];

                if (Imagen != null)
                {
                    byte[] imageArray = ConvertToBytes(Imagen);
                    aseguradora.Imagen = Convert.ToBase64String(imageArray);
                }

                if (aseguradora.IdAseguradora == null) //Inicia Add 
                {
                    //result = BL.Aseguradora.Add(aseguradora);

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebAPI"]);

                        var postTask = client.PostAsJsonAsync<ML.Aseguradora>("Aseguradora/Add", aseguradora);
                        postTask.Wait();

                        var resultAdd = postTask.Result;
                        if (resultAdd.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "La aseguradora se ingresó correctamente!!!";
                        }
                        else
                        {
                            ViewBag.Message = "Ocurrió un error al momento de ingresar la aseguradora: " + result.ErrorMessage;
                        }
                    
                    }
                    //if (result.Correct)
                    //{
                    //    ViewBag.Message = "La aseguradora se ingresó correctamente!!!";
                    //}
                    //else
                    //{
                    //    ViewBag.Message = "Ocurrió un error al momento de ingresar la aseguradora: " + result.ErrorMessage;
                    //}
                }
                else //Comienza Update
                {
                    //result = BL.Aseguradora.Update(aseguradora);

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebAPI"]);

                        //HTTP POST
                        var postTask = client.PutAsJsonAsync<ML.Aseguradora>("Aseguradora/Update", aseguradora);
                        postTask.Wait();

                        var resultUpdate = postTask.Result;
                        if (resultUpdate.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "La aseguradora se actualizó correctamente!!!";
                        }
                        else
                        {
                            ViewBag.Message = "La aseguradora no se actualizó, ocurrió el siguiente error: " + result.ErrorMessage;                       
                        }
                    }

                    //if (result.Correct)
                    //{
                    //    ViewBag.Message = "La aseguradora se actualizó correctamente!!!";
                    //}
                    //else
                    //{
                    //    ViewBag.Message = "La aseguradora no se actualizó, ocurrió el siguiente error: " + result.ErrorMessage;
                    //}
                }

                return PartialView("ValidationModal");
            }

            else
            {
                aseguradora.Usuario = new ML.Usuario();
                ML.Usuario usuario = new ML.Usuario(); 
                ML.Result resultUsuario = BL.Usuario.GetAll(usuario); 
                aseguradora.Usuario.Usuarios = resultUsuario.Objects;

                return View(aseguradora);
            }
        }

        [HttpGet]
        public ActionResult Delete(int IdAseguradora)
        {
            //ML.Result result = BL.Aseguradora.Delete(IdAseguradora);

            ML.Result result = new ML.Result();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                var postTask = client.DeleteAsync("Aseguradora/Delete/" + IdAseguradora);
                postTask.Wait();

                var resultDelete = postTask.Result;
                if (resultDelete.IsSuccessStatusCode)
                {
                    ViewBag.Message = "El registro de aseguradora se eliminó correctamente!!!";
                }
                else
                {
                    ViewBag.Message = "El registro no se eliminó, ocurrió el error: " + result.ErrorMessage;
                }
            }

            //if (result.Correct)
            //{
            //    ViewBag.Message = "El registro de aseguradora se eliminó correctamente!!!";
            //}
            //else
            //{
            //    ViewBag.Message = "El registro no se eliminó, ocurrió el error: " + result.ErrorMessage;
            //}

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