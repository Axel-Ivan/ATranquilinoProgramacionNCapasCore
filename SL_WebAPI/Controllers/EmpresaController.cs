using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        // GET: api/<EmpresaController>
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Empresa.GetAll();

            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<EmpresaController>/5
        [HttpGet("{IdEmpresa}")]
        public IActionResult GetById(int IdEmpresa)
        {
            ML.Result result = BL.Empresa.GetById(IdEmpresa);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<EmpresaController>
        [HttpPost]
        public IActionResult Add([FromBody] ML.Empresa empresa)
        {
            ML.Result result = BL.Empresa.Add(empresa);

            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // PUT api/<EmpresaController>/5
        //[HttpPut("{IdEmpresa}")]
        [HttpPut]
        public IActionResult Update([FromBody] ML.Empresa empresa)
        {
            ML.Result result = BL.Empresa.Update(empresa);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE api/<EmpresaController>/5
        [HttpDelete("{IdEmpresa}")]
        public IActionResult Delete(int IdEmpresa)
        {
            ML.Result result = BL.Empresa.Delete(IdEmpresa);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
