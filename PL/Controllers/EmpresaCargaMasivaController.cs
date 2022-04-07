using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpresaCargaMasivaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
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
        public ActionResult CargaMasiva (ML.ErrorExcel errorItem)
        {
            //string direccionExcel = (string)Session["RutaExcel"];
            //string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["ConexionExcel"].ToString();
            //string connectionString = CadenaConexion + direccionExcel;

            //ML.Result resultDataTable = BL.Empresa.ConvertToDataTable(direccionExcel, connectionString);
            //if(resultDataTable.Correct)
            //{
            //string errorMessage = " ";
            //DataTable tableEmpresa = (DataTable)resultDataTable.Object;

            return View();
        }


    }
}
