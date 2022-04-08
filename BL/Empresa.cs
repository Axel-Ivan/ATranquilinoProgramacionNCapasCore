using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Empresa
    {
        public static ML.Result Add(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Database.ExecuteSqlRaw($"EmpresaAdd '{empresa.Nombre}', '{empresa.Telefono}', '{empresa.Email}', '{empresa.DireccionWeb}', '{empresa.Logo}'");

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
        public static ML.Result Update(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Database.ExecuteSqlRaw($"EmpresaUpdate '{empresa.IdEmpresa}', '{empresa.Nombre}', '{empresa.Telefono}', '{empresa.Email}', '{empresa.DireccionWeb}', '{empresa.Logo}'");

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
        public static ML.Result Delete(int IdEmpresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Database.ExecuteSqlRaw($"EmpresaDelete {IdEmpresa}");
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
                    var procedure = context.Empresas.FromSqlRaw("EmpresaGetAll").ToList();

                    result.Objects = new List<object>();

                    if (procedure != null)
                    {
                        foreach (var obj in procedure)
                        {
                            ML.Empresa empresa = new ML.Empresa();
                            empresa.IdEmpresa = obj.IdEmpresa;
                            empresa.Nombre = obj.Nombre;                           
                            empresa.Telefono = obj.Telefono;
                            empresa.Email = obj.Email;
                            empresa.Logo = obj.Logo;
                            empresa.DireccionWeb = obj.DireccionWeb;

                            result.Objects.Add(empresa);
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
        public static ML.Result GetById(int IdEmpresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Empresas.FromSqlRaw($"EmpresaGetById {IdEmpresa}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (procedure != null)
                    {
                        ML.Empresa empresa = new ML.Empresa();
                        empresa.IdEmpresa = procedure.IdEmpresa;
                        empresa.Nombre = procedure.Nombre;
                        empresa.Telefono = procedure.Telefono;
                        empresa.Email = procedure.Email;
                        empresa.DireccionWeb = procedure.DireccionWeb;
                        empresa.Logo = procedure.Logo;
                        result.Object = empresa;

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

        public static ML.Result GetAllByExcel(string connectionString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.Connection.Open();

                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        DataTable tableEmpresa = new DataTable();
                        da.SelectCommand = cmd;
                        
                        da.Fill(tableEmpresa);

                        if (tableEmpresa.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach(DataRow row in tableEmpresa.Rows)
                            {
                                ML.Empresa empresa = new ML.Empresa();
                                empresa.Nombre = row[0].ToString();
                                empresa.Telefono = row[1].ToString();
                                empresa.Email = row[2].ToString();
                                empresa.DireccionWeb = row[3].ToString();
                                result.Objects.Add(empresa);
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el documento";
                        }
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
        public static ML.Result Validacion(List<object> Object)
        {
            ML.Result result = new ML.Result();

            result.Objects = new List<object>();
            string mensajeError;
            int contador = 2;
            foreach(ML.Empresa empresa in Object)
            {
                mensajeError = "";
                mensajeError += (empresa.Nombre == "") ? "Falta el nombre" : "";
                mensajeError += (empresa.Telefono == "") ? "Falta el telefono" : "";
                mensajeError += (empresa.Email == "") ? "Falta el email" : "";
                mensajeError += (empresa.DireccionWeb == "") ? "Falta la direccion web" : "";

                if(mensajeError != "")
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdErrorExcel = contador;
                    error.Message = mensajeError;
                    result.Objects.Add(error);
                }
            }

            return result;
        }



    }
}
