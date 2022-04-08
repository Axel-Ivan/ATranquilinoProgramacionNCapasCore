using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using System.Configuration;
using Microsoft.AspNetCore.Http;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class EmpresaCargaMasivaController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private IConfiguration _configuration;
        public EmpresaCargaMasivaController(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }


        [HttpGet]
        public ActionResult CargaMasiva()
        {
            ML.Result result = BL.Empresa.GetAll();
            ML.Empresa empresa = new ML.Empresa();

            if(result.Correct)
            {
                empresa.Empresas = new List<object>();
                empresa.Empresas = result.Objects;
            }

            return View(empresa);
        }

        [HttpPost]
        public ActionResult CargaMasiva(ML.Empresa empresa)
        {
            IFormFile file = Request.Form.Files["ArchivoExcel"];
            string fileName = Path.GetFileName(file.FileName);
            //string pathFolder = null;

            if(HttpContext.Session.GetString("PathArchivo") == null)
            {
                if (file.Length > 0)
                {
                    var folderPath = _configuration["PathFolder:value"];
                    var extensionFile = Path.GetExtension(file.FileName).ToLower();
                    var extensionModulo = _configuration["ExcelTipo:value"];

                    if (extensionFile == extensionModulo)
                    {
                        string path = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                        if (!System.IO.File.Exists(path))
                        {
                            using (FileStream stream = new FileStream(path, FileMode.CreateNew))
                            {
                                file.CopyTo(stream); //Revisar
                            }

                            string connectionString = _configuration["ConnectionStringExcel:value"] + path;
                            ML.Result resultEmpresas = BL.Empresa.GetAllByExcel(connectionString);

                            if(resultEmpresas.Correct)
                            {
                                ML.Result resultValidacion = BL.Empresa.Validacion(resultEmpresas.Objects);
                                if(resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                }
                                //else
                                //{
                                //    resultValidacion.Correct = false;
                                //    ViewBag.Message = "";
                                //}

                                ML.Result result = BL.Empresa.GetAll();
                                empresa.Empresas = new List<object>();
                                empresa.Empresas = result.Objects;

                                return View(empresa);
                            }
                            else
                            {
                                ViewBag.Message = "El excel no contiene registros";
                            }
                        }

                    }
                    else
                    {
                        ViewBag.Message = "El tipo de archivo es incorrecto, por favor verifique la extensión del archivo e intentelo de nuevo";
                    }
                }
            }

            return PartialView("ValidationModal");
        }

        //public ActionResult CargaMasiva (ML.ErrorExcel errorItem)
        //{
        //    //string direccionExcel = (string)Session["RutaExcel"];
        //    //string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["ConexionExcel"].ToString();
        //    //string connectionString = CadenaConexion + direccionExcel;

        //    //ML.Result resultDataTable = BL.Empresa.ConvertToDataTable(direccionExcel, connectionString);
        //    //if(resultDataTable.Correct)
        //    //{
        //    //string errorMessage = " ";
        //    //DataTable tableEmpresa = (DataTable)resultDataTable.Object;



        //    return View();
        //}


    }
}
