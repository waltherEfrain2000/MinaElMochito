using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;


namespace ProyectoMinaELMochito 
{
    class Salida 
{
        // Variable miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        // Propiedades
        public int IDsalida { get; set; }
        public int IDMineral { get; set; }
        public String NombreMineral { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaSalida { get; set; }
        public string DetalleSalida { get; set; }

      



        // Constructores
        public Salida()

        {

          

        }

        public Salida(int idSalida, int idMineral, decimal cantidad, decimal total, DateTime fecha, string detalle)
        {
            IDsalida = idSalida;
            IDMineral = idMineral;
            Cantidad = cantidad;
            Total = total;
            FechaSalida = fecha;
            DetalleSalida = detalle;
        }

        public void ingresarSalidas(Salida salidas)
        {
            try
            {
                string query = @"Insert into Minas.Salida values(@idmineral, @cantidad, @Total, @detalleVenta, @fechaSalida)";

                sqlConnection.Open();


                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@idmineral", salidas.IDMineral);
                sqlCommand.Parameters.AddWithValue("@cantidad", salidas.Cantidad);
                sqlCommand.Parameters.AddWithValue("@Total", salidas.Total);
                sqlCommand.Parameters.AddWithValue("@detalleVenta", salidas.DetalleSalida);
                sqlCommand.Parameters.AddWithValue("@fechaSalida", salidas.FechaSalida.ToString("yyyy-MM-dd "));

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

        public void ActualizarSalidas(Salida salidas)
        {
            try
            {
                string query = @"UPDATE	Minas.Salida SET idmineral = @idmineral, 
                                                         cantidad = @cantidad,
                                                         Total = @Total,
                                                         detalleVenta = @detalleVenta,
                                                         fechaSalida = @fechaSalida
                                                         Where idSalida = @idSalida";

                sqlConnection.Open();


                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@idmineral", salidas.IDMineral);
                sqlCommand.Parameters.AddWithValue("@cantidad", salidas.Cantidad);
                sqlCommand.Parameters.AddWithValue("@Total", salidas.Total);
                sqlCommand.Parameters.AddWithValue("@detalleVenta", salidas.DetalleSalida);
                sqlCommand.Parameters.AddWithValue("@fechaSalida", salidas.FechaSalida.ToString("yyyy-MM-dd "));
                sqlCommand.Parameters.AddWithValue("@idSalida", salidas.IDsalida);



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

        public void EliminarSalidas(Salida salidas)
        {
            try
            {


                string query = @"DELETE FROM Minas.Salida Where idSalida = @idSalida";

                sqlConnection.Open();


                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //sqlCommand.Parameters.AddWithValue("@idmineral", salidas.IDMineral);
                //sqlCommand.Parameters.AddWithValue("@cantidad", salidas.Cantidad);
                //sqlCommand.Parameters.AddWithValue("@Total", salidas.Total);
                //sqlCommand.Parameters.AddWithValue("@detalleVenta", salidas.DetalleSalida);
                //sqlCommand.Parameters.AddWithValue("@fechaSalida", salidas.FechaSalida);
                sqlCommand.Parameters.AddWithValue("@idSalida", salidas.IDsalida);
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

        public List<Salida> LlenarComboBox()
        {

            try
            {
                //Realizar el query que cargará la información correspondiente
                String queryMinerales = @"Select * From Minas.Mineral";

                //Establecer la conexión
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(queryMinerales, sqlConnection);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                List<Salida> salidas = new List<Salida>();

                while (reader.Read())
                {
                    salidas.Add(new Salida
                    {
                        NombreMineral = reader["descripcion"].ToString(),

                        Precio = Convert.ToDecimal(reader["Precio"].ToString()),

                        IDMineral = Convert.ToInt32(reader["idMineral"].ToString())
                    });
                }

                return salidas;
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

