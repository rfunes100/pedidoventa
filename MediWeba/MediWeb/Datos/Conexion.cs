
using System.Data.SqlClient; 

namespace MediWeb.Datos
{
    public class Conexion
    {
        private string cadenasql = string.Empty;

        public Conexion() { 
          var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            cadenasql = builder.GetSection("connectionStrings:prometeoConnection").Value;

        }


        public string  GetCadenaSQL()
        {
            return cadenasql;
        }

    }


}
