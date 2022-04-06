using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_C
{
    public class Empleado
    {
        public static ML.Result CargaMasiva()
        {
            ML.Result result = new ML.Result();

            StreamReader archivo = new StreamReader(@"C:\Users\digis\Documents\Axel Iván Tranquilino Beltran\LayoutEmpleadoCopia.txt");
            string line;
            bool isFirstLine = true;
            ML.Result resultErrores = new ML.Result();
            resultErrores.Objects = new List<object>();

            while ((line = archivo.ReadLine()) != null)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    line = archivo.ReadLine();
                }

                Console.WriteLine(line);
                string[] datos = line.Split('|');

                ML.Empleado empleado = new ML.Empleado();
                empleado.RFC = datos[0];
                empleado.Nombre = datos[1];
                empleado.ApellidoPaterno = datos[2];
                empleado.ApellidoMaterno = datos[3];
                empleado.Email = datos[4];
                empleado.Telefono = datos[5];
                empleado.FechaNacimiento = datos[6];
                empleado.NSS = datos[7];
                empleado.FechaIngreso = datos[8];
                empleado.Empresa = new ML.Empresa();
                empleado.Empresa.IdEmpresa = Convert.ToInt32(datos[9]);
                empleado.Poliza = new ML.Poliza();
                empleado.Poliza.IdPoliza = Convert.ToInt32(datos[10]);

                result = BL.Empleado.Add(empleado);

                if (result.Correct == false)
                {
                    resultErrores.Objects.Add("Puede haber un error en el RFC: " + empleado.RFC + ", "
                    + "Puede haber un error en el Nombre: " + empleado.Nombre + ", "
                    + "Puede haber un error en el Apellido Paterno: " + empleado.ApellidoPaterno + ", "
                    + "Puede haber un error en el Apellido Materno: " + empleado.ApellidoMaterno + ", "
                    + "Puede haber un error en el Email: " + empleado.Email + ", "
                    + "Puede haber un error en el Telefono: " + empleado.Telefono + ", "
                    + "Puede haber un error en la Fecha de Nacimiento: " + empleado.FechaNacimiento + ", "
                    + "Puede haber un error en el NSS: " + empleado.NSS + ", " 
                    + "Puede haber un error en la Fecha de Ingreso: " + empleado.FechaIngreso + ", " 
                    + "Puede haber un error en el Id de la Empresa" + empleado.Empresa.IdEmpresa + ", "
                    + "Puede haber un error en el Id de la Poliza: " + empleado.Poliza.IdPoliza + ", " 
                    + "El error general fue:" + result.ErrorMessage);
                }
                
            }
                
            if(resultErrores != null)
            {
                TextWriter textError = new StreamWriter(@"C:\Users\digis\Documents\Axel Iván Tranquilino Beltran\ErroresCargaMasiva.txt");
                foreach (string error in resultErrores.Objects)
                {
                    textError.WriteLine(error);
                }
                textError.Close();
            }
            
            return result;
        }
    }
}
