using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using System.Windows;
using System.Data.SqlClient;
using System.Configuration;

namespace ProyectoMinaELMochito
{
    class Conexion : Window
    {
        String Cadena = " Data Source=(local)\\SQLEXPRESS;Initial Catalog=MinaElMochito;Integrated Security=True ";

        public static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
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

