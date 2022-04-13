using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AseguradoraController : ControllerBase
    {
        // GET: api/<AseguradoraController>
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Aseguradora.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<AseguradoraController>/5       
        [HttpGet("{IdAseguradora}")]
        public IActionResult GetById(int IdAseguradora)
        {
            ML.Result result = BL.Aseguradora.GetById(IdAseguradora);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<AseguradoraController>
        [HttpPost]
        public IActionResult Add([FromBody] ML.Aseguradora aseguradora)
        {
            ML.Result result = BL.Aseguradora.Add(aseguradora);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // PUT api/<AseguradoraController>/5
        //[HttpPut("{IdAseguradora}")]
        [HttpPut]
        public IActionResult Update([FromBody] ML.Aseguradora aseguradora)
        {
            ML.Result result = BL.Aseguradora.Update(aseguradora);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE api/<AseguradoraController>/5
        [HttpDelete("{IdAseguradora}")]
        public IActionResult Delete(int IdAseguradora)
        {
            ML.Result result = BL.Aseguradora.Delete(IdAseguradora);

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
