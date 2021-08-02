using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoMinaELMochito
{

    class Conexion
    {
        // ojo, arreglar esta conexión si no sirve en su pc
        String Cadena = " Data Source=DESKTOP-JRBU49C;Initial Catalog=MinaElMochitoVersion2;Integrated Security=True ";
        public SqlConnection Conectarbd = new SqlConnection();
        
        //constructor
        //public Conexionion()
        //{
        //    Conectarbd.ConnectionString = Cadena;
        //}
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
