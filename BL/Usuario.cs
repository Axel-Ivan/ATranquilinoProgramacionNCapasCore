using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {

                    ObjectParameter OutputParameter = new ObjectParameter("IdUsuario", typeof(int));
                    
                    var procedure = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.UserName}', '{usuario.Contrasenia}', '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}', '{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.FechaNacimiento}', '{usuario.Estatus}', '{usuario.CURP}', '{usuario.Imagen}', '{usuario.Rol.IdRol}', '{OutputParameter}'");

                    result.Object = Convert.ToInt32(OutputParameter.Value);

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
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {

                    var procedure = context.Database.ExecuteSqlRaw($"UsuarioUpdate '{usuario.IdUsuario}', '{usuario.UserName}', '{usuario.Contrasenia}', '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}', '{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.FechaNacimiento}', '{usuario.Estatus}', '{usuario.CURP}', '{usuario.Imagen}', '{usuario.Rol.IdRol}'");
                    //var procedure = context.UsuarioUpdate(usuario.IdUsuario, usuario.UserName, usuario.Contrasenia, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Sexo, usuario.Telefono, usuario.Celular, DateTime.Parse(usuario.FechaNacimiento), usuario.Estatus, usuario.CURP, usuario.Imagen, usuario.Rol.IdRol);

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
        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Database.ExecuteSqlRaw($"UsuarioDelete {IdUsuario}");

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
        public static ML.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}'").ToList();

                    result.Objects = new List<object>();

                    if (procedure != null)
                    {
                        foreach (var obj in procedure)
                        {
                            //ML.Usuario usuario = new ML.Usuario();

                            usuario = new ML.Usuario();
                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.UserName = obj.UserName;
                            usuario.Contrasenia = obj.Contrasenia;
                            usuario.Nombre = obj.Nombre; //AS
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Email = obj.Email;
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.FechaNacimiento = Convert.ToString(obj.FechaNacimiento);
                            usuario.Estatus = Convert.ToByte(obj.Estatus);
                            usuario.CURP = obj.Curp;
                            usuario.Imagen = obj.Imagen;
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = obj.IdRol;
                            usuario.Rol.Nombre = obj.RolNombre; //AS
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = Convert.ToInt32(obj.IdDireccion);
                            usuario.Direccion.Calle = obj.Calle;
                            usuario.Direccion.NumeroInterior = obj.NumeroInterior;
                            usuario.Direccion.NumeroExterior = obj.NumeroExterior;
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = Convert.ToInt32(obj.IdColonia);
                            usuario.Direccion.Colonia.Nombre = obj.ColoniaNombre; //AS
                            usuario.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = Convert.ToInt32(obj.IdMunicipio);
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.MunicipioNombre; //AS
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = Convert.ToInt32(obj.IdEstado);
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.EstadoNombre; //AS
                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = Convert.ToInt32(obj.IdPais);
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.PaisNombre; //AS

                            result.Objects.Add(usuario);
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        Console.WriteLine("No se encontraron registros...");
                    }
                    result.Correct = true;
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
                    var procedure = context.Usuarios.FromSqlRaw($"UsuarioGetId '{IdUsuario}'").AsEnumerable().FirstOrDefault();

                    result.Objects = new List<object>();

                    if (procedure != null)
                    {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = procedure.IdUsuario;
                            usuario.UserName = procedure.UserName;
                            usuario.Contrasenia = procedure.Contrasenia;
                            usuario.Nombre = procedure.Nombre; //AS
                            usuario.ApellidoPaterno = procedure.ApellidoPaterno;
                            usuario.ApellidoMaterno = procedure.ApellidoMaterno;
                            usuario.Email = procedure.Email;
                            usuario.Sexo = procedure.Sexo;
                            usuario.Telefono = procedure.Telefono;
                            usuario.Celular = procedure.Celular;
                            usuario.FechaNacimiento = Convert.ToString(procedure.FechaNacimiento);
                            usuario.Estatus = Convert.ToByte(procedure.Estatus);
                            usuario.CURP = procedure.Curp;
                            usuario.Imagen = procedure.Imagen;
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = procedure.IdRol;
                            usuario.Rol.Nombre = procedure.RolNombre; //AS
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = Convert.ToInt32(procedure.IdDireccion);
                            usuario.Direccion.Calle = procedure.Calle;
                            usuario.Direccion.NumeroInterior = procedure.NumeroInterior;
                            usuario.Direccion.NumeroExterior = procedure.NumeroExterior;
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = Convert.ToInt32(procedure.IdColonia);
                            usuario.Direccion.Colonia.Nombre = procedure.ColoniaNombre; //AS
                            usuario.Direccion.Colonia.CodigoPostal = procedure.CodigoPostal;
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = Convert.ToInt32(procedure.IdMunicipio);
                            usuario.Direccion.Colonia.Municipio.Nombre = procedure.MunicipioNombre; //AS
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = Convert.ToInt32(procedure.IdEstado);
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = procedure.EstadoNombre; //AS
                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = Convert.ToInt32(procedure.IdPais);
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = procedure.PaisNombre; //AS

                            result.Object = usuario;

                            result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        Console.WriteLine("No se encontraron registros...");
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
        public static ML.Result GetAllConDireccion(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}'").ToList();
                    result.Objects = new List<object>();

                    if (procedure != null)
                    {
                        foreach (var obj in procedure)
                        {
                            ML.Direccion direccion = new ML.Direccion();
                            direccion.Usuario = new ML.Usuario();
                            direccion.Usuario.IdUsuario = obj.IdUsuario;
                            direccion.Usuario.UserName = obj.UserName;
                            direccion.Usuario.Contrasenia = obj.Contrasenia;
                            direccion.Usuario.Nombre = obj.Nombre; //AS
                            direccion.Usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            direccion.Usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            direccion.Usuario.Email = obj.Email;
                            direccion.Usuario.Sexo = obj.Sexo;
                            direccion.Usuario.Telefono = obj.Telefono;
                            direccion.Usuario.Celular = obj.Celular;
                            direccion.Usuario.FechaNacimiento = Convert.ToString(obj.FechaNacimiento);
                            direccion.Usuario.Estatus = Convert.ToByte(obj.Estatus);
                            direccion.Usuario.CURP = obj.Curp;
                            direccion.Usuario.Imagen = obj.Imagen;
                            direccion.Usuario.Rol = new ML.Rol();
                            direccion.Usuario.Rol.IdRol = obj.IdRol;
                            direccion.Usuario.Rol.Nombre = obj.RolNombre; //AS
                            direccion.IdDireccion = Convert.ToInt32(obj.IdDireccion);
                            direccion.Calle = obj.Calle;
                            direccion.NumeroInterior = obj.NumeroInterior;
                            direccion.NumeroExterior = obj.NumeroExterior;
                            direccion.Colonia = new ML.Colonia();
                            direccion.Colonia.IdColonia = Convert.ToInt32(obj.IdColonia);
                            direccion.Colonia.Nombre = obj.ColoniaNombre; //AS
                            direccion.Colonia.CodigoPostal = obj.CodigoPostal;
                            direccion.Colonia.Municipio = new ML.Municipio();
                            direccion.Colonia.Municipio.IdMunicipio = Convert.ToInt32(obj.IdMunicipio);
                            direccion.Colonia.Municipio.Nombre = obj.MunicipioNombre; //AS
                            direccion.Colonia.Municipio.Estado = new ML.Estado();
                            direccion.Colonia.Municipio.Estado.IdEstado = Convert.ToInt32(obj.IdEstado);
                            direccion.Colonia.Municipio.Estado.Nombre = obj.EstadoNombre; //AS
                            direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            direccion.Colonia.Municipio.Estado.Pais.IdPais = Convert.ToInt32(obj.IdPais);
                            direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.PaisNombre; //AS

                            result.Objects.Add(direccion);
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        Console.WriteLine("No se encontraron registros...");
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result GetDireccionByIdUsuario(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Direccions.FromSqlRaw($"DireccionGetByIdUsuario {IdUsuario}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (procedure != null)
                    {
                        ML.Direccion direccion = new ML.Direccion();
                        direccion.IdDireccion = procedure.IdDireccion;
                        direccion.Calle = procedure.Calle;
                        direccion.NumeroInterior = procedure.NumeroInterior;
                        direccion.NumeroExterior = procedure.NumeroExterior;
                        direccion.Colonia = new ML.Colonia();
                        direccion.Colonia.IdColonia = procedure.IdColonia;
                        direccion.Usuario = new ML.Usuario();
                        direccion.Usuario.IdUsuario = procedure.IdUsuario;
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
        }
        public static ML.Result GetAseguradoraByIdUsuario(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Aseguradoras.FromSqlRaw($"AseguradoraGetByIdUsuario {IdUsuario}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (procedure != null)
                    {
                        ML.Aseguradora aseguradora = new ML.Aseguradora();
                        aseguradora.IdAseguradora = procedure.IdAseguradora;
                        aseguradora.Nombre = procedure.Nombre;
                        aseguradora.FechaCreacion = Convert.ToString(procedure.FechaCreacion);
                        aseguradora.FechaModificacion = Convert.ToString(procedure.FechaModificacion);
                        aseguradora.Usuario = new ML.Usuario();
                        aseguradora.Usuario.IdUsuario = procedure.IdUsuario.Value;
                        result.Object = aseguradora;

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
    }
}
