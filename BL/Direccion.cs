using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Direccion
    {
        public static ML.Result Add(ML.Direccion direccion)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Database.ExecuteSqlRaw($"DireccionAdd '{direccion.Calle}', '{direccion.NumeroExterior}', '{direccion.NumeroInterior}', '{direccion.Colonia.IdColonia}', '{direccion.Usuario.IdUsuario}'");

                    if (procedure >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result Update(ML.Direccion direccion)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Database.ExecuteSqlRaw($"DireccionUpdate {direccion.Usuario.IdUsuario}, {direccion.Calle}, {direccion.NumeroInterior}, {direccion.NumeroExterior}, {direccion.Colonia.IdColonia}");
                    //var procedure = context.DireccionUpdate(direccion.Usuario.IdUsuario, direccion.Calle, direccion.NumeroInterior, direccion.NumeroExterior, direccion.Colonia.IdColonia);

                    if (procedure >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result Delete(int IdDireccion)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Database.ExecuteSqlRaw($"DireccionDelete {IdDireccion}");

                    if (procedure >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (procedure != null)
                    {
                        ML.Direccion direccion = new ML.Direccion();
                        direccion.Usuario = new ML.Usuario();
                        direccion.Usuario.IdUsuario = procedure.IdUsuario;
                        direccion.Usuario.UserName = procedure.UserName;
                        direccion.Usuario.Contrasenia = procedure.Contrasenia;
                        direccion.Usuario.Nombre = procedure.Nombre; //AS
                        direccion.Usuario.ApellidoPaterno = procedure.ApellidoPaterno;
                        direccion.Usuario.ApellidoMaterno = procedure.ApellidoMaterno;
                        direccion.Usuario.Email = procedure.Email;
                        direccion.Usuario.Sexo = procedure.Sexo;
                        direccion.Usuario.Telefono = procedure.Telefono;
                        direccion.Usuario.Celular = procedure.Celular;
                        direccion.Usuario.FechaNacimiento = Convert.ToString(procedure.FechaNacimiento);
                        direccion.Usuario.Estatus = Convert.ToByte(procedure.Estatus);
                        direccion.Usuario.CURP = procedure.Curp;
                        direccion.Usuario.Imagen = procedure.Imagen;
                        direccion.Usuario.Rol = new ML.Rol();
                        direccion.Usuario.Rol.IdRol = procedure.IdRol;
                        direccion.Usuario.Rol.Nombre = procedure.RolNombre; //AS
                        direccion.IdDireccion = Convert.ToInt32(procedure.IdDireccion);
                        direccion.Calle = procedure.Calle;
                        direccion.NumeroInterior = procedure.NumeroInterior;
                        direccion.NumeroExterior = procedure.NumeroExterior;
                        direccion.Colonia = new ML.Colonia();
                        direccion.Colonia.IdColonia = Convert.ToInt32(procedure.IdColonia);
                        direccion.Colonia.Nombre = procedure.ColoniaNombre; //AS
                        direccion.Colonia.CodigoPostal = procedure.CodigoPostal;
                        direccion.Colonia.Municipio = new ML.Municipio();
                        direccion.Colonia.Municipio.IdMunicipio = Convert.ToInt32(procedure.IdMunicipio);
                        direccion.Colonia.Municipio.Nombre = procedure.MunicipioNombre; //AS
                        direccion.Colonia.Municipio.Estado = new ML.Estado();
                        direccion.Colonia.Municipio.Estado.IdEstado = Convert.ToInt32(procedure.IdEstado);
                        direccion.Colonia.Municipio.Estado.Nombre = procedure.EstadoNombre; //AS
                        direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        direccion.Colonia.Municipio.Estado.Pais.IdPais = Convert.ToInt32(procedure.IdPais);
                        direccion.Colonia.Municipio.Estado.Pais.Nombre = procedure.PaisNombre; //AS
                        result.Object = direccion;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        } //Aqui
        public static ML.Result DireccionGetByIdColonia(int IdColonia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Direccions.FromSqlRaw($"DireccionGetByIdColonia {IdColonia}").ToList();
                    result.Objects = new List<object>();

                    if (procedure != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in procedure)
                        {
                            ML.Direccion direccion = new ML.Direccion();
                            direccion.IdDireccion = obj.IdDireccion;
                            direccion.Calle = obj.Calle;
                            direccion.NumeroInterior = obj.NumeroInterior;
                            direccion.NumeroExterior = obj.NumeroExterior;
                            direccion.Colonia = new ML.Colonia();
                            direccion.Colonia.IdColonia = obj.IdColonia;
                            direccion.Usuario = new ML.Usuario();
                            direccion.Usuario.IdUsuario = obj.IdUsuario;
                            result.Objects.Add(direccion);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existen registros en la tabla direccion";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
