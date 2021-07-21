
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
    class ViajeInterno
    {
        //Agregar las variables miembro que contienen la conexión con la base de datos
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Propiedades 
        /// <summary>
        /// Esta variable es la que va a capturar el id del Empleado que realizará el viaje
        /// </summary>
        public int IdEmpleado { get; set; }

        /// <summary>
        /// Esta variable contiende el id de el vehículo en el que se moverá el material 
        /// </summary>
        public int IdVehiculo { get; set; }

        public string NombreEmpleado { get; set; }
        public string Vehiculo { get; set; }

        //Constructores
        public ViajeInterno() { }

        public ViajeInterno(int idVehiculoDelViaje, int idEmpleadoDelViaje)
        {
            IdVehiculo = idVehiculoDelViaje;
            IdEmpleado = idEmpleadoDelViaje;
        }

        //Métodos
        /// <summary>
        /// Se insertará y guardará el id del empleado que realizará el viaje
        /// para transportar el material extraido de la mina
        /// </summary>
        /// <param name="empleado">El id del empleado</param>
        public void AgregarDatosAViajeInterno(ViajeInterno viajeinterno)
        {
            conexion cn = new conexion();
            Validaciones validaciones = new Validaciones();
            try
            {


                SqlCommand cmd = new SqlCommand("AgregarDatosAViajeInterno", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;


                cn.abrir();
                cmd.Parameters.AddWithValue("@idVehiculo", viajeinterno.IdVehiculo);
                cmd.Parameters.AddWithValue("@idEmpleado", viajeinterno.IdEmpleado);

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