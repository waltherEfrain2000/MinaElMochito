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
        String Cadena = " Data Source=DESKTOP-4AAROTR;Initial Catalog=MinaElMochito;Integrated Security=True ";
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

