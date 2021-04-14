using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar los namespaces requeridos
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ProyectoMinaELMochito
{
    class Producciion
    {
        //Agregar las variables miembro que contienen la conexión con la base de datos
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Propiedades
        public int IdMineral { get; set; }

        public String NombreMineral { get; set; }

        public decimal Precio { get; set; }

        public List<Producciion> LlenarComboBox()
        {

            try
            {
                //Realizar el query que cargará la información correspondiente
                String queryMinerales = @"Select * From Minas.Mineral";

                //Establecer la conexión
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(queryMinerales, sqlConnection);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                List<Producciion> minerales = new List<Producciion>();

                while (reader.Read())
                {
                    minerales.Add(new Producciion { NombreMineral = reader["descripcion"].ToString(),
                        Precio = Convert.ToDecimal(reader["precio"].ToString()), 
                        IdMineral = Convert.ToInt32(reader["idMineral"].ToString())});   
                }

                return minerales;
            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}
