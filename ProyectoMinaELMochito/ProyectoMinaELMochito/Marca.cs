using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar los namespaces requeridos
using System.Data.SqlClient;
using System.Configuration;

namespace ProyectoMinaELMochito
{
    class Marcas
    {
        // Variables miembro
        Conexion cn = new Conexion();

        //Propiedades
        public int CodigoMarca { get; set; }
        public string NombreMarca { get; set; }

        // Constructores
        public Marcas() { }


        public Marcas(string marca, int codigoMarca)
        {
            NombreMarca = marca;
            CodigoMarca = codigoMarca;
        }


        public List<Marcas> LlenarComboBoxMarcas()
        {
            try
            {
                //Realizar el query que cargará la información correspondiente
                String query = @"EXEC MostrarMarcas";

                //Establecer la conexión
                cn.abrir();

                SqlCommand sqlCommand = new SqlCommand(query, cn.Conectarbd);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<Marcas> estados = new List<Marcas>();

                while (reader.Read())
                {
                    estados.Add(new Marcas
                    {
                        NombreMarca = reader["nombreMarca"].ToString(),
                        CodigoMarca = Convert.ToInt32(reader["idMarca"].ToString())
                    });
                }

                return estados;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cn.cerrar();
            }

        }


    }
}
