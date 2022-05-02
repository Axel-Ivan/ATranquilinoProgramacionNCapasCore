using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // GET: api/<LoginController>
        
        [HttpGet("{username}")]
        public IActionResult GetByUsername(string username)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.UserName = username;
            ML.Result result = BL.Usuario.GetByUsername(usuario);

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
