using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace PL.Controllers
{
    public class EmpresaController : Controller
    {
        private IHostingEnvironment _hostingEnvironment; //Dependencies Injection
        private IConfiguration _configuration;
        public EmpresaController(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            //ML.Result result = BL.Empresa.GetAll();
            //ML.Empresa empresa = new ML.Empresa();

            //if (result.Correct)
            //{
            //    empresa.Empresas = new List<object>();
            //    empresa.Empresas = result.Objects;
            //}

            ML.Result resultEmpresas = new ML.Result();
            resultEmpresas.Objects = new List<Object>();
            ML.Empresa empresa = new ML.Empresa();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]); //AppSettings Json
                var responseTask = client.GetAsync("Empresa/GetAll");

                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Empresa resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empresa>(resultItem.ToString());
                        resultEmpresas.Objects.Add(resultItemList);
                    }
                }
                empresa.Empresas = resultEmpresas.Objects;
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
            else //GetById - Update
            {
                //ML.Result result = new ML.Result();
                //result = BL.Empresa.GetById(IdEmpresa.Value);
                ML.Empresa empresaItem = new ML.Empresa();
                ML.Result result = new ML.Result();
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebAPI"]);
                        var responseTask = client.GetAsync("Empresa/GetById/" + IdEmpresa);
                        responseTask.Wait();
                        var resultAPI = responseTask.Result;

                        if (resultAPI.IsSuccessStatusCode)
                        {
                            var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            empresaItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empresa>(readTask.Result.Object.ToString());
                            result.Object = empresaItem;

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en la tabla Empresa";
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
                    //empresa = ((ML.Empresa)result.Object);
                    return View(empresaItem);
                }
            }
            return PartialView("ValidationModal");
        }

        [HttpPost]
        public ActionResult Form(ML.Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                ML.Result result = new ML.Result();
                IFormFile Imagen = Request.Form.Files["fuImagen"];

                if (Imagen != null)
                {
                    byte[] imageArray = ConvertToBytes(Imagen);
                    empresa.Logo = Convert.ToBase64String(imageArray);
                }

                if (empresa.IdEmpresa == null) //Inicia Add
                {
                    //result = BL.Empresa.Add(empresa);

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebAPI"]);

                        var postTask = client.PostAsJsonAsync<ML.Empresa>("Empresa/Add", empresa);
                        postTask.Wait();

                        var resultAdd = postTask.Result;
                        if (resultAdd.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "La empresa se ingresó correctamente!!!";
                        }
                        else
                        {
                            ViewBag.Message = "Ocurrió un error al momento de ingresar la empresa: " + result.ErrorMessage;
                        }

                    }

                    //if (result.Correct)
                    //{
                    //    ViewBag.Message = "La empresa se ingresó correctamente!!!";
                    //}
                    //else
                    //{
                    //    ViewBag.Message = "Ocurrió un error al momento de ingresar la empresa: " + result.ErrorMessage;
                    //}
                }
                else //Comienza Update
                {
                    //result = BL.Empresa.Update(empresa);

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebAPI"]);

                        //HTTP POST
                        var postTask = client.PutAsJsonAsync<ML.Empresa>("Empresa/Update", empresa);
                        postTask.Wait();

                        var resultUpdate = postTask.Result;
                        if (resultUpdate.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "La empresa se actualizó correctamente!!!";
                        }
                        else
                        {
                            ViewBag.Message = "La empresa no se actualizó, ocurrió el siguiente error: " + result.ErrorMessage;
                        }
                    }

                    //if (result.Correct)
                    //{
                    //    ViewBag.Message = "La empresa se actualizó correctamente!!!";
                    //}
                    //else
                    //{
                    //    ViewBag.Message = "La empresa no se actualizó, ocurrió el siguiente error: " + result.ErrorMessage;
                    //}
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
            //ML.Result result = BL.Empresa.Delete(IdEmpresa);

            ML.Result result = new ML.Result();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                var postTask = client.DeleteAsync("Empresa/Delete/" + IdEmpresa);
                postTask.Wait();

                var resultDelete = postTask.Result;
                if (resultDelete.IsSuccessStatusCode)
                {
                    ViewBag.Message = "El registro de la empresa se eliminó correctamente!!!";
                }
                else
                {
                    ViewBag.Message = "El registro no se eliminó, ocurrió el error: " + result.ErrorMessage;
                }
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
