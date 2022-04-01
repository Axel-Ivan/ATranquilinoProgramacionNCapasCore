using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Dependiente
    {
        public static ML.Result GetAllByIdEmpleado(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoProgramacionNCapasContext context = new DL.ATranquilinoProgramacionNCapasContext())
                {
                    var procedure = context.Dependientes.FromSqlRaw($"DependienteGetAllByIdEmpleado {IdEmpleado}").ToList();
                    result.Objects = new List<object>();

                    if(procedure != null)
                    {
                        foreach(var obj in procedure)
                        {
                            ML.Dependiente dependiente = new ML.Dependiente();
                            dependiente.IdDependiente = obj.IdDependiente;
                            dependiente.Empleado = new ML.Empleado();
                            dependiente.Empleado.IdEmpleado = obj.IdEmpleado;
                            dependiente.Nombre = obj.Nombre;
                            dependiente.ApellidoPaterno = obj.ApellidoPaterno;
                            dependiente.ApellidoMaterno = obj.ApellidoMaterno;
                            dependiente.FechaNacimiento = Convert.ToString(obj.FechaNacimiento);
                            dependiente.FechaNacimiento = dependiente.FechaNacimiento.Remove(10, 15);
                            dependiente.EstadoCivil = obj.EstadoCivil;
                            dependiente.Genero = obj.Genero;
                            dependiente.Telefono = obj.Telefono;
                            dependiente.RFC = obj.Rfc;
                            dependiente.DependienteTipo = new ML.DependienteTipo();
                            dependiente.DependienteTipo.IdDependienteTipo = obj.IdDependienteTipo;
                            dependiente.DependienteTipo.Nombre = obj.DependienteTipoNombre;
                            result.Objects.Add(dependiente);
                        }
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
