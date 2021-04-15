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
        public int IdViaje { get; set; }

        public int IdMineral { get; set; }

        public decimal Peso { get; set; }

        public String NombreMineral { get; set; }

        public decimal Precio { get; set; }

        //Constructores
        public Producciion() { }

        public Producciion(int numeroViaje, int numeroMineral, string tipoMineral, decimal precioMineral, decimal pesoMineral)
        {
            IdViaje = numeroViaje;
            IdMineral = numeroMineral;
            Peso = pesoMineral;
            Precio = precioMineral;
        }

        //Métodos
        /// <summary>
        /// Crea una una producción según el viaje
        /// </summary>
        public void AgregarProduccion(Producciion producciion)
        {
            try
            {
                //Este query permitirá insertar una nueva producción
                string queryProduccion = @"Insert Into Minas.Produccion(idViaje, idMineral, precio, peso)
                                        Values(@idViaje, @idMineral, @precio, @peso)";

                //Establecer la conexión con la base de datos
                sqlConnection.Open();

                //Crear el sqlCommand necesario
                SqlCommand sqlCommand = new SqlCommand(queryProduccion, sqlConnection);

                //Establecer los prámetros de las variables
                sqlCommand.Parameters.AddWithValue("@idViaje", producciion.IdViaje);
                sqlCommand.Parameters.AddWithValue("@idMineral", producciion.IdMineral);
                sqlCommand.Parameters.AddWithValue("@precio", producciion.Precio);
                sqlCommand.Parameters.AddWithValue("@peso", producciion.Peso);

                //Ejecutar la insersición
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //Cerrar la conexión
                sqlConnection.Close();
            }
        }

        public Producciion UltimoId() 
        {
            Producciion ultimoId = new Producciion();

            try
            {
                String query = @"Select top 1 [idViaje] from [Minas].[viajeInterno] 
                                order by [idViaje] desc";

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        ultimoId.IdViaje = Convert.ToInt32(rdr["idViaje"]);
                    }
                }

                return ultimoId;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Modifica solo los datos permitidos en la producción
        /// </summary>
        public void ModificarProduccion(Producciion producciion)
        {
            try
            {
                //Query que permitirá la actualización de datos en la tabla
                string queryModificacion = @"Update Minas.Produccion 
                                         Set idMineral = @idMineral, peso = @peso
                                         Where idProduccion = @idProduccion";

                //Establecer la conexión
                sqlConnection.Open();

                //Crear el sqlCommant
                SqlCommand sqlCommand = new SqlCommand(queryModificacion, sqlConnection);

                //Crear los parámetros que serán actualizados en la tabla
                sqlCommand.Parameters.AddWithValue("@idMineral", producciion.IdMineral);
                sqlCommand.Parameters.AddWithValue("@peso", producciion.Peso);

                //Ejecutar el comando para la actualización de datos
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //Cerrar la conexión
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Este método se encargará de abstraer los datos de la base de datos de la tabla
        /// Minerales, este se encargará de llenar el comboBox con la abstracción del campo
        /// descripción de esta tabla de mineral, y poder así visualizar estos elementos en 
        /// el ComboBox
        /// </summary>
        /// <returns>Descripcion o tipo de Mineral</returns>
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
                    minerales.Add(new Producciion
                    {
                        NombreMineral = reader["descripcion"].ToString(),
                        Precio = Convert.ToDecimal(reader["precio"].ToString()),
                        IdMineral = Convert.ToInt32(reader["idMineral"].ToString())
                    });
                }

                return minerales;
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

        public void BorrarProduccion(Producciion producciion)
        {
            try
            {
                //Query que permitirá la opción de eliminar una producción
                string queryEliminacion = @"Delete From Minas.Produccion 
                                      Where idProduccion = @idProduccion";

                // Establecer la conexión
                sqlConnection.Open();

                //Crear el sqlCommant
                SqlCommand sqlCommand = new SqlCommand(queryEliminacion, sqlConnection);

                //Crear los parámetros que serán actualizados en la tabla
                sqlCommand.Parameters.AddWithValue("@idViaje", producciion.IdViaje);


                //Ejecutar el comando para la actualización de datos
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //Cerrar la conexión
                sqlConnection.Close();
            }
        }


    }
}
