using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Empleado
    {
        public static ML.Result GetAll(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Empleados.FromSqlRaw($"EmpleadoGetAll '{empleado.Nombre}', '{empleado.ApellidoPaterno}', '{empleado.ApellidoMaterno}'").ToList();

                    result.Objects = new List<object>();

                    if (procedure != null)
                    {
                        foreach (var obj in procedure)
                        {
                            empleado = new ML.Empleado();
                            empleado.IdEmpleado = obj.IdEmpleado;
                            empleado.RFC = obj.Rfc;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.Email = obj.Email;
                            empleado.Telefono = obj.Telefono;
                            empleado.FechaNacimiento = Convert.ToString(obj.FechaNacimiento);
                            empleado.FechaNacimiento = empleado.FechaNacimiento.Remove(10, 15);
                            empleado.NSS = obj.Nss;
                            empleado.FechaIngreso = Convert.ToString(obj.FechaIngreso);
                            empleado.FechaIngreso = empleado.FechaIngreso.Remove(10, 15);
                            empleado.Foto = obj.Foto;
                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = obj.IdEmpresa;
                            empleado.Empresa.Nombre = obj.EmpresaNombre;
                            empleado.Poliza = new ML.Poliza();
                            empleado.Poliza.IdPoliza = obj.IdPoliza;
                            empleado.Poliza.Nombre = obj.PolizaNombre;

                            result.Objects.Add(empleado);
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
        public static ML.Result GetById(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Empleados.FromSqlRaw($"EmpleadoGetById {IdEmpleado}").AsEnumerable().FirstOrDefault();

                    result.Objects = new List<object>();

                    if (procedure != null)
                    {
                        ML.Empleado empleado = new ML.Empleado();
                        empleado.IdEmpleado = procedure.IdEmpleado;
                        empleado.RFC = procedure.Rfc;
                        empleado.Nombre = procedure.Nombre;
                        empleado.ApellidoPaterno = procedure.ApellidoPaterno;
                        empleado.ApellidoMaterno = procedure.ApellidoMaterno;
                        empleado.Email = procedure.Email;
                        empleado.Telefono = procedure.Telefono;
                        empleado.FechaNacimiento = Convert.ToString(procedure.FechaNacimiento);
                        empleado.FechaNacimiento = empleado.FechaNacimiento.Remove(10, 15);
                        empleado.NSS = procedure.Nss;
                        empleado.FechaIngreso = Convert.ToString(procedure.FechaIngreso);
                        empleado.FechaIngreso = empleado.FechaIngreso.Remove(10, 15);
                        empleado.Foto = procedure.Foto;
                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = procedure.IdEmpresa;
                        empleado.Empresa.Nombre = procedure.EmpresaNombre;
                        empleado.Poliza = new ML.Poliza();
                        empleado.Poliza.IdPoliza = procedure.IdPoliza;
                        empleado.Poliza.Nombre = procedure.PolizaNombre;

                        result.Object = empleado;                    
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
