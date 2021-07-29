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
   
    class DetalleSalidaSalidas
    {

        //Agregar las variables miembro que contienen la conexión con la base de datos
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        
        public int idDetalle { get; set; }
        public String DetalleSalida { get; set; }

        //Constructores
        public DetalleSalidaSalidas() { }

        public DetalleSalidaSalidas(int iddetallesalida, string detalleSalida)
        {
            idDetalle = iddetallesalida;
            DetalleSalida = detalleSalida;
        }

     
        public void AgregarDetalleSalida(DetalleSalidaSalidas detalleSalida)
        {
            Conexion cn = new Conexion();
           
            try
            {

                SqlCommand cmd = new SqlCommand("ingresarDetalleSalida", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.abrir();
                cmd.Parameters.AddWithValue("@DetalleSalida", detalleSalida.DetalleSalida);
                

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
       

    }
}
