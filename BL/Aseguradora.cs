using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
namespace BL
{
    public class Aseguradora
    {
        public static ML.Result Add(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    //var username = new SqlParameter();
                    //username.ParameterName = "@Username";
                    //username.SqlDbType = SqlDbType.VarChar;
                    //username.Value = aseguradora.Nombre;

                    //var email = new SqlParameter();
                    //email.ParameterName = "@Email";
                    //email.SqlDbType = SqlDbType.VarChar;
                    //email.Value = aseguradora.Nombre;
              
                    //var IdUsuario = new SqlParameter();
                    //IdUsuario.ParameterName = "@IdUsuario";
                    //IdUsuario.SqlDbType = SqlDbType.Int;
                    //IdUsuario.Direction = ParameterDirection.Output;

                    //var parameters = new[] {
                    //new SqlParameter("@Username", SqlDbType.VarChar) {Value=aseguradora.Nombre},
                    //new SqlParameter("@Email", SqlDbType.VarChar) {Value=aseguradora.Nombre},
                    //new SqlParameter("@IdUsuario", SqlDbType.Int) { Direction = ParameterDirection.InputOutput, Value = 0 }
                    //};

                    //var procedure =context.Database.ExecuteSqlInterpolatedAsync(
                    //    $"EXEC dbo.InsertTest @param1={"test"},@param2={"test2"}, @param3={output}");

                    //var procedure = context.Database.ExecuteSqlRaw($"EXEC UsuarioTest @Username={"username"},@Email={"email"}, @IdUsuario={IdUsuario}");

                    //var procedure = context.Database.ExecuteSqlRaw("UsuarioTest", username, Email, IdUsuario);
                    //+
                    //"@Username," +
                    //"@Email," +                        
                    //"@IdUsuario OUTPUT",);

                    //result.Object = Convert.ToInt32(context.Parameters["@IdUsuario"].Value);

                    var procedure = context.Database.ExecuteSqlRaw($"AseguradoraAdd '{aseguradora.Nombre}', '{aseguradora.Usuario.IdUsuario}', '{aseguradora.Imagen}'");

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
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result Update(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Database.ExecuteSqlRaw($"AseguradoraUpdate '{aseguradora.IdAseguradora}', '{aseguradora.Nombre}', '{aseguradora.Usuario.IdUsuario}', '{aseguradora.Imagen}'");

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
        public static ML.Result Delete(int IdAseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Database.ExecuteSqlRaw($"AseguradoraDelete {IdAseguradora}");

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
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Aseguradoras.FromSqlRaw("AseguradoraGetAll").ToList();
                    result.Objects = new List<object>();

                    if (procedure != null)
                    {
                        foreach (var obj in procedure)
                        {
                            ML.Aseguradora aseguradora = new ML.Aseguradora();
                            aseguradora.IdAseguradora = obj.IdAseguradora;
                            aseguradora.Nombre = obj.Nombre;
                            aseguradora.FechaCreacion = Convert.ToString(obj.FechaCreacion);
                            aseguradora.FechaModificacion = Convert.ToString(obj.FechaModificacion);
                            aseguradora.Usuario = new ML.Usuario();
                            aseguradora.Usuario.IdUsuario = obj.IdUsuario.Value;
                            aseguradora.Imagen = obj.Imagen;
                            aseguradora.Usuario.Nombre = obj.UsuarioNombre;
                            result.Objects.Add(aseguradora);
                        }
                    }
                    else
                    {
                        result.Correct = false;
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
        public static ML.Result GetById(int IdAseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Aseguradoras.FromSqlRaw($"AseguradoraGetId {IdAseguradora}").AsEnumerable().FirstOrDefault();
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
                        aseguradora.Usuario.Nombre = procedure.UsuarioNombre;
                        aseguradora.Imagen = procedure.Imagen;
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