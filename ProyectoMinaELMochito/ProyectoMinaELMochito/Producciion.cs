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

        public int IdProduccion { get; set; }

        public string Fecha { get; set; }

        //Constructores
        public Producciion() { }

        public Producciion(int idProduccion, int numeroViaje, int numeroMineral, string tipoMineral, decimal precioMineral, decimal pesoMineral)
        {
            IdProduccion = idProduccion;
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
            conexion cn = new conexion();
            Validaciones validaciones = new Validaciones();
            try
            {
                SqlCommand cmd = new SqlCommand("AgregarProduccion", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;


                cn.abrir();

                cmd.Parameters.AddWithValue("@idViaje", producciion.IdViaje);
                cmd.Parameters.AddWithValue("@idMineral", producciion.IdMineral);
                cmd.Parameters.AddWithValue("@precio", producciion.Precio);
                cmd.Parameters.AddWithValue("@peso", producciion.Peso);
                cmd.Parameters.AddWithValue("@Fecha", producciion.Fecha);

                cmd.ExecuteNonQuery();



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

        public Producciion UltimoId()
        {
            Producciion ultimoId = new Producciion();
            conexion cn = new conexion();
            try
            {
                SqlCommand cmd = new SqlCommand("UltimoId", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;


                cn.abrir();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        ultimoId.IdViaje = Convert.ToInt32(rdr["idViaje"]);
                    }
                }

                return ultimoId;

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
        public void ModificarProduccion(Producciion producciion)
        {
            conexion cn = new conexion();
            Validaciones validaciones = new Validaciones();
            try
            {
                SqlCommand cmd = new SqlCommand("ModificarProduccion", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;


                cn.abrir();

                cmd.Parameters.AddWithValue("@idProduccion", producciion.IdProduccion);
                cmd.Parameters.AddWithValue("@idMineral", producciion.IdMineral);
                cmd.Parameters.AddWithValue("@peso", producciion.Peso);
                cmd.Parameters.AddWithValue("@Fecha", producciion.Fecha);

                cn.abrir();

                cmd.ExecuteNonQuery();



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

        /// <summary>
        /// Este método se encargará de abstraer los datos de la base de datos de la tabla
        /// Minerales, este se encargará de llenar el comboBox con la abstracción del campo
        /// descripción de esta tabla de mineral, y poder así visualizar estos elementos en 
        /// el ComboBox
        /// </summary>
        /// <returns>Descripcion o tipo de Mineral</returns>
        public List<Producciion> LlenarComboBox()
        {
            conexion cn = new conexion();
            Validaciones validaciones = new Validaciones();
            try
            {
                SqlCommand cmd = new SqlCommand("LlenarCombobox", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;


                cn.abrir();

                SqlDataReader reader = cmd.ExecuteReader();

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
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cn.cerrar();
            }
        }

        public void BorrarProduccion(Producciion producciion)
        {
            conexion cn = new conexion();
            Validaciones validaciones = new Validaciones();
            try
            {
                SqlCommand cmd = new SqlCommand("BorrarProduccion", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;


                cn.abrir();

                cmd.Parameters.AddWithValue("@idProduccion", producciion.IdProduccion);

                cmd.ExecuteNonQuery();
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


    }
}
