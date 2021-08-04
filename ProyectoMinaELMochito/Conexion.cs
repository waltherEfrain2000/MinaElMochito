using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace ProyectoMinaELMochito
{
    class conexion
    {
        // ojo, arreglar esta conexión si no sirve en su pc
        // Data Source=192.168.0.7, 1433 ;Initial Catalog=MinaElMochitoVersion2;user id = usuarioMina; password = 123456
        String Cadena = " Data Source=(local)\\SQLEXPRESS;Initial Catalog=MinaElMochitoVersion2;Integrated Security=True ";
        public SqlConnection Conectarbd = new SqlConnection();

        public conexion()
        {
            Conectarbd.ConnectionString = Cadena;
        }

        public void abrir()
        {
            try
            {
                Conectarbd.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error al abrir BD " + ex.Message);
            }
        }

        //Metodo para cerrar la conexion
        public void cerrar()
        {
            Conectarbd.Close();
        }

    }
}