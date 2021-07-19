using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace ProyectoMinaELMochito
{
    class Conexion : Window
    {
        // ojo, arreglar esta conexión si no sirve en su pc
        String Cadena = " Data Source=DESKTOP-PEIA00M\\SQLEXPRESS;Initial Catalog=MinaElMochitoVersion2;Integrated Security=True ";
        public SqlConnection Conectarbd = new SqlConnection();

        public Conexion()
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
