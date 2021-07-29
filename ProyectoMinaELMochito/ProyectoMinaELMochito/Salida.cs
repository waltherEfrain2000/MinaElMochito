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
    class Salida 
{
        // Variable miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        // Propiedades
        public int IdDetalle { get; set; }
        public int IDsalida { get; set; }
        public int IDMineral { get; set; }
        
        public String NombreMineral { get; set; }
        public decimal Cantidad { get; set; }
       //// public string FechaSalida { get; set; }
       // public string DetalleSalida { get; set; }

        // Constructores
        public Salida() {}

        public Salida(int idSalida,int idMineral, int idDetalle, decimal cantidad)
        {
            IDsalida = idSalida;
            IDMineral = idMineral;
            IdDetalle = idDetalle;
            Cantidad = cantidad;
            //FechaSalida = fecha;

        }

        public void ingresarSalidas(Salida salidas)
        {
            conexion cn = new conexion();
            try
            {
                
                SqlCommand sqlCommand = new SqlCommand("insertarSalidas", cn.Conectarbd);
                sqlCommand.CommandType = CommandType.StoredProcedure;
   
                sqlCommand.Parameters.AddWithValue("@idmineral", salidas.IDMineral);
                sqlCommand.Parameters.AddWithValue("@idDetalle", salidas.IdDetalle);
                sqlCommand.Parameters.AddWithValue("@cantidad", salidas.Cantidad);
               // sqlCommand.Parameters.AddWithValue("@fechaSalida", salidas.FechaSalida);

                cn.abrir();
               
                sqlCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                cn.cerrar();

            }

        }

        public Salida UltimoId()
        {
            Salida ultimoId = new Salida();
            Conexion cn = new Conexion();
            try
            {
                SqlCommand cmd = new SqlCommand("UltimoIdSalida", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;


                cn.abrir();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        ultimoId.IdDetalle = Convert.ToInt32(rdr["idDetalle"]);
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
                cn.cerrar();
            }
        }


        public void ActualizarSalidas(Salida salidas)
        {
            conexion cn = new conexion();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("ActualizarSalida",cn.Conectarbd);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@idSalida", salidas.IDsalida);
                sqlCommand.Parameters.AddWithValue("@idmineral", salidas.IDMineral);
                sqlCommand.Parameters.AddWithValue("@idDetalle", salidas.IdDetalle);
                sqlCommand.Parameters.AddWithValue("@cantidad", salidas.Cantidad);
                //sqlCommand.Parameters.AddWithValue("@fechaSalida", salidas.FechaSalida);

                cn.abrir();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = sqlCommand;
                DataTable tabla = new DataTable();
                adp.Fill(tabla);
                cn.abrir();
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                cn.cerrar();

            }
        }

        public void EliminarSalidas(Salida salidas)
        {
            conexion cn = new conexion();
            try
            {
                sqlConnection.Open();


                SqlCommand sqlCommand = new SqlCommand("EliminarSalida", cn.Conectarbd);
                sqlCommand.CommandType = CommandType.StoredProcedure;
 
                sqlCommand.Parameters.AddWithValue("@idSalida",salidas.IDsalida);

                cn.abrir();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = sqlCommand;
                DataTable tabla = new DataTable();
                adp.Fill(tabla);
                cn.abrir();
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

                //Establecer la conexión
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("LlenarCombobox",sqlConnection);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                List<Salida> salidas = new List<Salida>();

                while (reader.Read())
                {
                    salidas.Add(new Salida
                    {
                        NombreMineral = reader["descripcion"].ToString(),

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
      
        /* public List<Salida> LlenarDetalleSalidas()
         {

             try
             {

                 //Establecer la conexión
                 sqlConnection.Open();

                 SqlCommand sqlCommand = new SqlCommand("LlenarComboboxDetalle", sqlConnection);

                 SqlDataReader reader = sqlCommand.ExecuteReader();

                 List<Salida> salidas = new List<Salida>();

                 while (reader.Read())
                 {
                     salidas.Add(new Salida
                     {
                         DetalleSalida = reader["DetalleSalida"].ToString(),
                         IdDetalle = Convert.ToInt32(reader["idDetalle"].ToString())
                     }); ;
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
        */
        //public Salida BuscarSalida(int idvuelo)
        //{
        //    Salida Salida = new Salida();


        //    try
        //    {

        //        // Establecer la conexión
        //        sqlConnection.Open();

        //        // Crear el comando SQL
        //        SqlCommand sqlCommand = new SqlCommand("BuscarSalidas", sqlConnection);

        //        // Establecer el valor del parámetro
        //        sqlCommand.Parameters.AddWithValue("@idSalida", IDsalida);

        //        using (SqlDataReader rdr = sqlCommand.ExecuteReader())
        //        {
        //            while (rdr.Read())
        //            {
        //               Salida.IDsalida = Convert.ToInt32(rdr["idSalida"]);
        //                Salida.IDMineral = Convert.ToInt32(rdr["idMineral"]);
        //                Salida.IdDetalle = Convert.ToInt32(rdr["idDetalle"]);
        //                Salida.FechaSalida = Convert.ToDateTime(rdr["fechaSalida"]);
        //                Salida.Cantidad = Convert.ToDecimal(rdr["Cantidad"]);



        //            }
        //        }

        //        return Salida;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    finally
        //    {
        //        // Cerrar la conexión
        //        sqlConnection.Close();
        //    }
        //}
    }
}

