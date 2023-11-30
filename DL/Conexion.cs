using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Conexion
    {
        public static string Get()
        {
            try
            {
                // Obtén la cadena de conexión por nombre
                string conexion = ConfigurationManager.ConnectionStrings["FLunaBarreraEntities"].ConnectionString;

                // Si todo está bien, devuelve la cadena de conexión
                return conexion;
            }
            catch (Exception ex)
            {
                // Si hay algún problema, imprime la excepción y devuelve una cadena vacía o maneja el error según tus necesidades
                Console.WriteLine("Error al obtener la cadena de conexión: " + ex.Message);
                return string.Empty;
            }
        }
    }
}
