using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();

            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre; //Operador ternario
            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;

            ML.Result result = BL.Usuario.GetAll(usuario); 
            usuario = new ML.Usuario();

            if (result.Correct)
            {
                usuario.Usuarios = new List<object>();
                usuario.Usuarios = result.Objects;
            }
            return View(usuario);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre; //Operador ternario
            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;

            ML.Result result = BL.Usuario.GetAll(usuario);
            usuario = new ML.Usuario();

            if (result.Correct)
            {
                usuario.Usuarios = new List<object>();
                usuario.Usuarios = result.Objects;
            }
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Direccion direccion = new ML.Direccion();
            direccion.Usuario = new ML.Usuario();
            direccion.Usuario.Rol = new ML.Rol();
            direccion.Colonia = new ML.Colonia();
            direccion.Colonia.Municipio = new ML.Municipio();
            direccion.Colonia.Municipio.Estado = new ML.Estado();
            direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
            ML.Result resultPais = BL.Pais.GetAll();
            ML.Result resultEstados = BL.Estado.GetAll();
            ML.Result resultMunicipios = BL.Municipio.GetAll();
            ML.Result resultColonias = BL.Colonia.GetAll();

            direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
            direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;
            direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;
            direccion.Colonia.Colonias = resultColonias.Objects;

            ML.Result resultRol = BL.Rol.GetAll();

            if (IdUsuario == null)
            {
                direccion.Usuario.Rol.Roles = resultRol.Objects;
                return View(direccion);
            }
            else
            {
                ML.Result result = new ML.Result();
                result = BL.Direccion.GetById(IdUsuario.Value);

                if (result.Correct)
                {
                    direccion.Colonia = new ML.Colonia();
                    direccion.Colonia.Municipio = new ML.Municipio();
                    direccion.Colonia.Municipio.Estado = new ML.Estado();
                    direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                    direccion = ((ML.Direccion)result.Object);

                    ML.Result resultDireccion = BL.Direccion.DireccionGetByIdColonia(direccion.Colonia.IdColonia.Value);
                    ML.Result resultColonia = BL.Colonia.ColoniaGetByIdMunicipio(direccion.Colonia.Municipio.IdMunicipio.Value);
                    ML.Result resultMunicipio = BL.Municipio.MunicipioGetByIdEstado(direccion.Colonia.Municipio.Estado.IdEstado.Value);
                    ML.Result resultEstado = BL.Estado.EstadoGetByIdPais(direccion.Colonia.Municipio.Estado.Pais.IdPais.Value);

                    direccion.Usuario.Rol.Roles = resultRol.Objects;
                    direccion.Colonia.Colonias = resultColonia.Objects;
                    direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                    direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;

                    return View(direccion);
                }
            }
            return PartialView("ValidationModal");
        }

        [HttpPost]
        public ActionResult Form(ML.Direccion direccion)
        {
            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                IFormFile Imagen = Request.Form.Files["fuImagen"];

                if (Imagen != null)
                {                   
                    byte[] imageArray = ConvertToBytes(Imagen);
                    direccion.Usuario.Imagen = Convert.ToBase64String(imageArray);
                }

                if (direccion.Usuario.IdUsuario == 0)
                {
                    ML.Result result = BL.Usuario.Add(direccion.Usuario); //Cambio

                    if (result.Correct)
                    {
                        //ML.Result LastIdUsuario = BL.Usuario.GetLastId();
                        //ML.Direccion direccion2 = ((ML.Direccion)LastIdUsuario.Object);
                        //int IdUsuarioItem = direccion2.Usuario.IdUsuario;
                        //direccion.Usuario.IdUsuario = IdUsuarioItem;

                        direccion.Usuario.IdUsuario = ((int)result.Object);
                        ML.Result resultDireccion = BL.Direccion.Add(direccion);

                        if (resultDireccion.Correct)
                        {
                            ViewBag.Message = "Usuario ingresado correctamente!!!";
                        }
                        else
                        {
                            ViewBag.Message = "Ocurrió un error al insertar el usuario: " + resultDireccion.ErrorMessage;
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrió un error al insertar el usuario: " + result.ErrorMessage;
                    }
                }

            else
            {
                ML.Result result = BL.Usuario.Update(direccion.Usuario);
                    
                if (result.Correct)
                {
                    ML.Result resultDireccion = BL.Direccion.Update(direccion);
                    if (resultDireccion.Correct)
                    {
                        ViewBag.Message = "Usuario actualizado correctamente!!!";
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrió un error al actualizar el usuario: " + resultDireccion.ErrorMessage;
                    }
                }
                else
                {
                    ViewBag.Message = "Ocurrió un error al actualizar el usuario: " + result.ErrorMessage;
                }
            }

            return PartialView("ValidationModal");

            }
            else
            {
                direccion.Usuario = new ML.Usuario();
                direccion.Usuario.Rol = new ML.Rol();
                direccion.Colonia = new ML.Colonia();
                direccion.Colonia.Municipio = new ML.Municipio();
                direccion.Colonia.Municipio.Estado = new ML.Estado();
                direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                ML.Result resultPais = BL.Pais.GetAll();
                ML.Result resultRol = BL.Rol.GetAll();
                ML.Result resultColonia = BL.Colonia.ColoniaGetByIdMunicipio(direccion.Colonia.Municipio.IdMunicipio.Value);
                ML.Result resultMunicipio = BL.Municipio.MunicipioGetByIdEstado(direccion.Colonia.Municipio.Estado.IdEstado.Value);
                ML.Result resultEstado = BL.Estado.EstadoGetByIdPais(direccion.Colonia.Municipio.Estado.Pais.IdPais.Value);

                direccion.Usuario.Rol.Roles = resultRol.Objects;
                direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                direccion.Colonia.Colonias = resultColonia.Objects;

                return View(direccion);
            }
        }

        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result resultDireccion = BL.Usuario.GetDireccionByIdUsuario(IdUsuario);
            ML.Result resultAseguradora = BL.Usuario.GetAseguradoraByIdUsuario(IdUsuario);

            ML.Direccion direccionItem = (ML.Direccion)resultDireccion.Object;
            ML.Result resultDeleteDireccion = BL.Direccion.Delete(direccionItem.IdDireccion.Value);
            ML.Aseguradora aseguradoraItem = (ML.Aseguradora)resultAseguradora.Object;
            ML.Result resultDeleteAseguradora = BL.Aseguradora.Delete(aseguradoraItem.IdAseguradora.Value);

            if (resultDeleteDireccion.Correct)
            {
                if (resultDeleteAseguradora.Correct)
                {
                    ML.Result resultDeleteUsuario = BL.Usuario.Delete(IdUsuario);

                    if (resultDeleteUsuario.Correct)
                    {
                        ViewBag.Message = "EL usuario ha sido eliminado correctamente!!!";
                    }
                    else
                    {
                        ViewBag.Message = "El usuario no se eiminó correctamente. Ocurrió el error: " + resultDeleteUsuario.ErrorMessage;
                    }
                }
            }
            return PartialView("ValidationModal");
        }

        public ActionResult UpdateStatus (int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetById(IdUsuario);
            if(result.Correct)
            {
                usuario = ((ML.Usuario)result.Object);

                //usuario.Estatus = (Convert.ToByte(usuario.Estatus == 1)) ? 0 : 1;

                if (usuario.Estatus == 0)
                {
                    usuario.Estatus = 1;
                }
                else
                {
                    usuario.Estatus = 0;
                }

                ML.Result resultUpdate = BL.Usuario.Update(usuario);
                ViewBag.Message = "Se actualizó el estatus del usuario";
            }
            else
            {
                ViewBag.Message = "No se actualizó el estatus del usuario" + result.ErrorMessage;
            }

            return PartialView("ValidationModal");
        }

        public JsonResult GetEstado(int IdPais)
        {
            var result = BL.Estado.EstadoGetByIdPais(IdPais);
            return Json(result.Objects);
        }
        public JsonResult GetMunicipio(int IdEstado)
        {
            var result = BL.Municipio.MunicipioGetByIdEstado(IdEstado);
            return Json(result.Objects);
        }
        public JsonResult GetColonia(int IdMunicipio)
        {
            var result = BL.Colonia.ColoniaGetByIdMunicipio(IdMunicipio);
            return Json(result.Objects);
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
