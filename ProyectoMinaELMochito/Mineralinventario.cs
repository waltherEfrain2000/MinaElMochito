using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace ProyectoMinaELMochito
{
    class Mineralinventario
    {

        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;

        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        // Propiedades
        public int IdMineral { get; set; }

        public double Peso { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public Decimal Total { get; set; }



        // Constructores
        public Mineralinventario() { }

        public Mineralinventario(int idMineral, double peso, DateTime fechaActualizacion, decimal total)
        {
            IdMineral = idMineral;
            Peso = peso;
            FechaActualizacion = fechaActualizacion;
            Total = total;
        }

        // Métodos

        public Mineralinventario CargarOro()
        {
            // Crear el objeto que almacena la información de los resultados
            Mineralinventario mineral = new Mineralinventario();

            try
            {
                // Query de selección
                string query = @"SELECT * FROM Minas.InventarioMineral
                                 WHERE idMineral = 1";


                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("idMineral", 1);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Obtener los valores del mineral si la consulta retorna valores
                        mineral.IdMineral = Convert.ToInt32(rdr["idMineral"]);
                        mineral.Peso = Convert.ToDouble(rdr["peso"]);
                        mineral.FechaActualizacion = Convert.ToDateTime(rdr["fechaActualizacion"]);
                        mineral.Total = Convert.ToDecimal(rdr["Total"]);
                    }
                }

                // Retornar el mineral con los valores
                return mineral;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// extraemos la informacion del mineral Zinz
        /// </summary>
        /// <returns> la informacion del inventario del zinc </returns>

        public Mineralinventario CargarZinc()
        {
            // Crear el objeto que almacena la información de los resultados
            Mineralinventario mineral = new Mineralinventario();

            try
            {
                // Query de selección
                string query = @"SELECT * FROM Minas.InventarioMineral
                                 WHERE idMineral = 2";


                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("idMineral", 2);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Obtener los valores del mineral si la consulta retorna valores
                        mineral.IdMineral = Convert.ToInt32(rdr["idMineral"]);
                        mineral.Peso = Convert.ToDouble(rdr["peso"]);
                        mineral.FechaActualizacion = Convert.ToDateTime(rdr["fechaActualizacion"]);
                        mineral.Total = Convert.ToDecimal(rdr["Total"]);
                    }
                }

                // Retornar el mineral con los valores
                return mineral;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// se cargan los valores del plomo
        /// </summary>
        /// <returns>la informacion del mineral de plomo </returns>

        public Mineralinventario CargarPlomo()
        {
            // Crear el objeto que almacena la información de los resultados
            Mineralinventario mineral = new Mineralinventario();

            try
            {
                // Query de selección
                string query = @"SELECT * FROM Minas.InventarioMineral
                                 WHERE idMineral = 3";


                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("idMineral", 3);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Obtener los valores del mineral si la consulta retorna valores
                        mineral.IdMineral = Convert.ToInt32(rdr["idMineral"]);
                        mineral.Peso = Convert.ToDouble(rdr["peso"]);
                        mineral.FechaActualizacion = Convert.ToDateTime(rdr["fechaActualizacion"]);
                        mineral.Total = Convert.ToDecimal(rdr["Total"]);
                    }
                }

                // Retornar el mineral con los valores
                return mineral;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// se cargan los datos del mineral de plata 
        /// </summary>
        /// <returns></returns>
        public Mineralinventario CargarPlata()
        {
            // Crear el objeto que almacena la información de los resultados
            Mineralinventario mineral = new Mineralinventario();

            try
            {
                // Query de selección
                string query = @"SELECT * FROM Minas.InventarioMineral
                                 WHERE idMineral = 4";


                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("idMineral", 4);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Obtener los valores del mineral si la consulta retorna valores
                        mineral.IdMineral = Convert.ToInt32(rdr["idMineral"]);
                        mineral.Peso = Convert.ToDouble(rdr["peso"]);
                        mineral.FechaActualizacion = Convert.ToDateTime(rdr["fechaActualizacion"]);
                        mineral.Total = Convert.ToDecimal(rdr["Total"]);
                    }
                }

                // Retornar el mineral con los valores
                return mineral;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
                sqlConnection.Close();
            }
        }
        /// <summary>
        /// se cargan los datos del mineral de cobre
        /// </summary>
        /// <returns></returns>
        public Mineralinventario CargarCobre()
        {
            // Crear el objeto que almacena la información de los resultados
            Mineralinventario mineral = new Mineralinventario();

            try
            {
                // Query de selección
                string query = @"SELECT * FROM Minas.InventarioMineral
                                 WHERE idMineral = 5";


                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("idMineral", 5);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Obtener los valores del mineral si la consulta retorna valores
                        mineral.IdMineral = Convert.ToInt32(rdr["idMineral"]);
                        mineral.Peso = Convert.ToDouble(rdr["peso"]);
                        mineral.FechaActualizacion = Convert.ToDateTime(rdr["fechaActualizacion"]);
                        mineral.Total = Convert.ToDecimal(rdr["Total"]);
                    }
                }

                // Retornar el mineral con los valores
                return mineral;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
                sqlConnection.Close();
            }
        }
    }
}
