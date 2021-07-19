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
        Conexion cn = new Conexion();
        //private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        //private SqlConnection sqlConnection = new SqlConnection(connectionString);

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


        public Vehiculo(int vehiculoID, string marca, string modelo, string placa, string color, int estado)
        {
            VehiculoID = vehiculoID;
            Marca = marca;
            Modelo = modelo;
            Placa = placa;
            Color = color;
            Estado = estado;
        }


        public List<Vehiculo> LlenarComboBoxEstados()
        {
            try
            {
                //Realizar el query que cargará la información correspondiente
                String query = @"EXEC MostrarVehiculo @tipoMostrar";

                //Establecer la conexión
                cn.abrir();

                SqlCommand sqlCommand = new SqlCommand(query, cn.Conectarbd);

                sqlCommand.Parameters.AddWithValue("@tipoMostrar", 1);

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
                cn.cerrar();
            }

        }


    }
}
