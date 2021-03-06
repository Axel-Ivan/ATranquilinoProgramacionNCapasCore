using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Poliza
    {
        public static ML.Result Add(ML.Poliza poliza)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Database.ExecuteSqlRaw($"PolizaAdd '{poliza.Nombre}', '{poliza.SubPoliza.IdSubPoliza}', '{poliza.NumeroPoliza}', '{poliza.Usuario.IdUsuario}'");

                    if(procedure >= 1)
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
        public static ML.Result Update(ML.Poliza poliza)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Database.ExecuteSqlRaw($"PolizaUpdate '{poliza.IdPoliza}', '{poliza.Nombre}', '{poliza.SubPoliza.IdSubPoliza}', '{poliza.NumeroPoliza}', '{poliza.Usuario.IdUsuario}'");

                    if(procedure >= 1)
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
        public static ML.Result Delete(int IdPoliza)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Database.ExecuteSqlRaw($"PolizaDelete {IdPoliza}");

                    if(procedure >= 1)
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
                    var procedure = context.Polizas.FromSqlRaw("PolizaGetAll").ToList();

                    result.Objects = new List<object>();

                    if (procedure != null)
                    {
                        foreach (var obj in procedure)
                        {
                            ML.Poliza poliza = new ML.Poliza();
                            poliza.IdPoliza = obj.IdPoliza;
                            poliza.Nombre = obj.Nombre;
                            poliza.SubPoliza = new ML.SubPoliza();
                            poliza.SubPoliza.IdSubPoliza = obj.IdSubPoliza;
                            poliza.SubPoliza.Nombre = obj.SubPolizaNombre;
                            poliza.NumeroPoliza = obj.NumeroPoliza;
                            poliza.FechaCreacion = Convert.ToString(obj.FechaCreacion);
                            poliza.FechaCreacion = poliza.FechaCreacion.Remove(10, 15);
                            poliza.FechaModificacion = Convert.ToString(obj.FechaModificacion);
                            poliza.FechaModificacion = poliza.FechaModificacion.Remove(10, 15);
                            poliza.Usuario = new ML.Usuario();
                            poliza.Usuario.IdUsuario = obj.IdUsuario;
                            poliza.Usuario.Nombre = obj.UsuarioNombre;

                            result.Objects.Add(poliza);
                        }

                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros...";
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
        public static ML.Result GetById(int IdPoliza)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Polizas.FromSqlRaw($"PolizaGetById {IdPoliza}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if(procedure != null)
                    {
                        ML.Poliza poliza = new ML.Poliza();
                        poliza.IdPoliza = procedure.IdPoliza;
                        poliza.Nombre = procedure.Nombre;
                        poliza.SubPoliza = new ML.SubPoliza();
                        poliza.SubPoliza.IdSubPoliza = procedure.IdSubPoliza;
                        poliza.NumeroPoliza = procedure.NumeroPoliza;
                        poliza.FechaCreacion = Convert.ToString(procedure.FechaCreacion);
                        poliza.FechaCreacion = poliza.FechaCreacion.Remove(10, 15);
                        poliza.FechaModificacion = Convert.ToString(procedure.FechaModificacion);
                        poliza.FechaModificacion = poliza.FechaModificacion.Remove(10, 15);
                        poliza.Usuario = new ML.Usuario();
                        poliza.Usuario.IdUsuario = procedure.IdUsuario;
                        result.Object = poliza;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros...";
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
