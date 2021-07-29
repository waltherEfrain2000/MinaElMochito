using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Data;

namespace ProyectoMinaELMochito
{
    class Cargocrud
    {
        // Variables miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);
        

        //Propiedades
        public int CargoID { get; set; }
        public string NombreCargo { get; set; }
       
        public Cargocrud(int ID, string nombrecargo)
        {
            CargoID = ID;
            NombreCargo = nombrecargo;
           
        }

        // Constructores
        public Cargocrud() { }

        public void CrearCargo(Cargocrud Cargo)
        {
            conexion cn = new conexion();
            try
            {  
                SqlCommand cmd = new SqlCommand("insertarCargos", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@descripcion", Cargo.NombreCargo);
               

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
        public void ActualizarCargo(Cargocrud Cargo)
        {
            conexion cn = new conexion();
            try
            {
                SqlCommand cmd = new SqlCommand("ActualizarCargos", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idCargo", Cargo.CargoID);
                cmd.Parameters.AddWithValue("@descripcion", Cargo.NombreCargo);

                cn.abrir();

                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                DataTable tabla = new DataTable();
                adp.Fill(tabla);

                cn.abrir();
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                //sqlConnection.Close();
                cn.cerrar();
            }
        }

        public void EliminarCargo(Cargocrud Cargo)
        {
            conexion cn = new conexion();
            try
            {
                SqlCommand cmd = new SqlCommand("EliminarCargo", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idCargo", Cargo.CargoID);

                cn.abrir();

                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                DataTable tabla = new DataTable();
                adp.Fill(tabla);

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

       
    }
}
