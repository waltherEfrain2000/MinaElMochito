using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar los namespaces requeridos
using System.Data.SqlClient;
using System.Configuration;

namespace ProyectoMinaELMochito
{
    class Vehiculo
    {
        // Variables miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.VehiculoConnection"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Propiedades
        public int VehiculoID { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public string Color { get; set; }
        public int Estado { get; set; }

        // Constructores
        public Vehiculo() { }


        public Vehiculo(int vehiculoID ,string marca, string modelo, string placa, string color, int estado)
        {
            VehiculoID = vehiculoID;
            Marca = marca;
            Modelo = modelo;
            Placa = placa;
            Color = color;
            Estado = estado;
        }

        public void CrearVehiculo(Vehiculo vehiculo)
        {
            try
            {
                string query = @"INSERT	INTO Minas.Vehiculo values (@marca,@modelo,@placa,@color,@estado)";

                sqlConnection.Open();


                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@marca", vehiculo.Marca);
                sqlCommand.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
                sqlCommand.Parameters.AddWithValue("@placa", vehiculo.Placa);
                sqlCommand.Parameters.AddWithValue("@color", vehiculo.Color);
                sqlCommand.Parameters.AddWithValue("@estado", vehiculo.Estado);

                sqlCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                sqlConnection.Close();

            }
        }


    }
}
