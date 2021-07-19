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
    class Marcas
    {
        // Variables miembro
        Conexion cn = new Conexion();

        //Propiedades
        public int VehiculoID { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public string Color { get; set; }
        public int elCodigoMarca { get; set; }
        public string NombreMarca { get; set; }

        // Constructores
        public Marcas() { }


        public Marcas(int vehiculoID, int codigoMarca, string modelo, string placa, string color, int estado)
        {
            VehiculoID = vehiculoID;
            codigoMarca = elCodigoMarca;
            Modelo = modelo;
            Placa = placa;
            Color = color;
        }


        public List<Vehiculo> LlenarComboBoxModelos()
        {
            try
            {
                //Realizar el query que cargará la información correspondiente
                String query = @"EXEC MostrarMarcas";

                //Establecer la conexión
                cn.abrir();

                SqlCommand sqlCommand = new SqlCommand(query, cn.Conectarbd);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<Vehiculo> estados = new List<Vehiculo>();

                while (reader.Read())
                {
                    estados.Add(new Vehiculo
                    {
                        NombreMarca = reader["nombreMarca"].ToString(),
                        codigoMarca = Convert.ToInt32(reader["idMarca"].ToString())
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
