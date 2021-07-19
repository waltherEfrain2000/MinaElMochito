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

        //Propiedades
        public int VehiculoID { get; set; }
        public string Marca { get; set; }
        public int idMarca { get; set; }
        public string NombreMarca { get; set; }
        public string Modelo { get; set; }
        public int idModelo { get; set; }
        public string Placa { get; set; }
        public string Color { get; set; }
        public int Estado { get; set; }
        public string NombreEstado { get; set; }

        // Constructores
        public Vehiculo() { }


        public Vehiculo(int idmarca, int vehiculoID, string marca, string modelo, string placa, string color, int estado, int idmodelo)
        {
            VehiculoID = vehiculoID;
            Marca = marca;
            idMarca = idmarca;
            Modelo = modelo;
            idModelo = idmodelo;
            Placa = placa;
            Color = color;
            Estado = estado;
        }


        public List<Vehiculo> LlenarComboBoxEstados()
        {
            try
            {
                //Realizar el query que cargará la información correspondiente
                String query = @"EXEC MostrarEstado";

                //Establecer la conexión
                cn.abrir();

                SqlCommand sqlCommand = new SqlCommand(query, cn.Conectarbd);

                //sqlCommand.Parameters.AddWithValue("@tipoMostrar", 1);

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


        public List<Marcas> LlenarComboBoxMarcas()
        {
            try
            {
                //Realizar el query que cargará la información correspondiente
                String query = @"EXEC MostrarMarcas";

                //Establecer la conexión
                cn.abrir();

                SqlCommand sqlCommand = new SqlCommand(query, cn.Conectarbd);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<Marcas> estados = new List<Marcas>();

                while (reader.Read())
                {
                    estados.Add(new Marcas
                    {
                        NombreMarca = reader["nombreMarca"].ToString(),
                        CodigoMarca = Convert.ToInt32(reader["idMarca"].ToString())
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



        public List<Vehiculo> LlenarComboBoxModelos(int idMarca)
        {
            try
            {
                //Realizar el query que cargará la información correspondiente
                String query = @"EXEC MostrarModelos @idMarca";

                //Establecer la conexión
                cn.abrir();

                SqlCommand sqlCommand = new SqlCommand(query, cn.Conectarbd);

                sqlCommand.Parameters.AddWithValue("@idMarca", idMarca);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<Vehiculo> estados = new List<Vehiculo>();

                while (reader.Read())
                {
                    estados.Add(new Vehiculo
                    {
                        Modelo = reader["nombreModelo"].ToString(),
                        idModelo = Convert.ToInt32(reader["idModelo"].ToString()),
                        idMarca = Convert.ToInt32(reader["idMarca"].ToString())
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
