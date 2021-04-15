using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace ProyectoMinaELMochito
{
    class EntradaMineral
    {


        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        // Propiedades
        public int IdEntrada { get; set; }
        public int IdMineral { get; set; }

        public double Cantidad { get; set; }

        public Decimal Total { get; set; }

        public string DetalleEntrada { get; set; }
        public DateTime FechaDeEntrada { get; set; }




        // Constructores
        public EntradaMineral() { }

        public EntradaMineral(int idEntrada, int idMineral, double peso, decimal total, string detalleEntrada, DateTime fechaActualizacion)
        {
            IdEntrada = idEntrada;
            IdMineral = idMineral;
            Cantidad = peso;
            Total = total;
            DetalleEntrada = detalleEntrada;
            fechaActualizacion = fechaActualizacion;
        }

        // Métodos

        public EntradaMineral CargarOro()
        {
            // Crear el objeto que almacena la información de los resultados
            EntradaMineral mineral = new EntradaMineral();

            try
            {
                // Query de selección
                string query = @"select  [idProducto],sum([cantidad]) as 'Total K Ingresados', sum([Total]) as 'Total'  from [Minas].[Entrada] 
                                 where  [idProducto] =1 group by [idProducto] ";


                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("idProductol", 1);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Obtener los valores del mineral si la consulta retorna valores
                        mineral.IdMineral = Convert.ToInt32(rdr["idProducto"]);
                        mineral.Cantidad = Convert.ToDouble(rdr["Total K Ingresados"]);
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

        public EntradaMineral CargarZinc()
        {
            EntradaMineral mineral1 = new EntradaMineral();

            try
            {
                // Query de selección
                string query = @"select  [idProducto],sum([cantidad]) as 'Total K Ingresados', sum([Total]) as 'Total'  from [Minas].[Entrada] 
                                 where  [idProducto] =2 group by [idProducto] ";


                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("idProducto", 2);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Obtener los valores del mineral si la consulta retorna valores
                        mineral1.IdMineral = Convert.ToInt32(rdr["idProducto"]);
                        mineral1.Cantidad = Convert.ToDouble(rdr["Total K Ingresados"]);
                        mineral1.Total = Convert.ToDecimal(rdr["Total"]);
                    }
                }

                // Retornar el mineral con los valores
                return mineral1;
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

        public EntradaMineral CargarPlomo()
        {
            EntradaMineral mineral1 = new EntradaMineral();

            try
            {
                // Query de selección
                string query = @"select  [idProducto],sum([cantidad]) as 'Total K Ingresados', sum([Total]) as 'Total'  from [Minas].[Entrada] 
                                 where  [idProducto] =3 group by [idProducto] ";


                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("idProductol", 3);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Obtener los valores del mineral si la consulta retorna valores
                        mineral1.IdMineral = Convert.ToInt32(rdr["idProducto"]);
                        mineral1.Cantidad = Convert.ToDouble(rdr["Total K Ingresados"]);
                        mineral1.Total = Convert.ToDecimal(rdr["Total"]);
                    }
                }

                // Retornar el mineral con los valores
                return mineral1;
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
        public EntradaMineral CargarPlata()
        {
            EntradaMineral mineral1 = new EntradaMineral();

            try
            {
                // Query de selección
                string query = @"select  [idProducto],sum([cantidad]) as 'Total K Ingresados', sum([Total]) as 'Total'  from [Minas].[Entrada] 
                                 where  [idProducto] =4 group by [idProducto] ";


                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("idProducto", 4);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Obtener los valores del mineral si la consulta retorna valores
                        mineral1.IdMineral = Convert.ToInt32(rdr["idProducto"]);
                        mineral1.Cantidad = Convert.ToDouble(rdr["Total K Ingresados"]);
                        mineral1.Total = Convert.ToDecimal(rdr["Total"]);
                    }
                }

                // Retornar el mineral con los valores
                return mineral1;
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
        public EntradaMineral CargarCobre()
        {
            EntradaMineral mineral1 = new EntradaMineral();

            try
            {
                // Query de selección
                string query = @"select  [idProducto],sum([cantidad]) as 'Total K Ingresados', sum([Total]) as 'Total'  from [Minas].[Entrada] 
                                 where  [idProducto] =5 group by [idProducto] ";


                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("idProductol", 5);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Obtener los valores del mineral si la consulta retorna valores
                        mineral1.IdMineral = Convert.ToInt32(rdr["idProducto"]);
                        mineral1.Cantidad = Convert.ToDouble(rdr["Total K Ingresados"]);
                        mineral1.Total = Convert.ToDecimal(rdr["Total"]);
                    }
                }

                // Retornar el mineral con los valores
                return mineral1;
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
