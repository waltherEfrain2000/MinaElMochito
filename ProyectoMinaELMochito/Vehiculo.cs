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
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Propiedades
        public int VehiculoID { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public string Color { get; set; }
        public int Estado { get; set; }
        public string NombreEstado { get; set; }

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
        public void ActualizarVehiculo(Vehiculo vehiculo)
        {
            try
            {
                string query = @"UPDATE	Minas.Vehiculo SET color = @color,estado = @estado Where idVehiculo  = @idVehiculo";

                sqlConnection.Open();


                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@idVehiculo", vehiculo.VehiculoID);
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

        public void EliminarVehiculo(Vehiculo vehiculo)
        {
            try
            {
                string query = @"UPDATE	Minas.Vehiculo SET estado = @estado Where idVehiculo  = @idVehiculo";

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@idVehiculo", vehiculo.VehiculoID);
                sqlCommand.Parameters.AddWithValue("@estado", 3);

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

        public List<Vehiculo> LlenarComboBoxEstados()
        {
            try
            {
                //Realizar el query que cargará la información correspondiente
                String query = @"Select * From Minas.EstadoVehiculo";

                //Establecer la conexión
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<Vehiculo> estados = new List<Vehiculo>();

                while (reader.Read())
                {
                    estados.Add(new Vehiculo
                    {
                        NombreEstado = reader["descripcion"].ToString(),
                        Estado = Convert.ToInt32(reader["idEstado"].ToString())
                    });
                }

                return estados;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                sqlConnection.Close();
            }

        }


    }
}
