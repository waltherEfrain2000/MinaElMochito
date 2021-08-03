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
        public string Estado { get; set; }

        public Cargocrud(int ID, string nombrecargo, string estado)
        {
            CargoID = ID;
            NombreCargo = nombrecargo;
            Estado = estado;


        }

        // Constructores
        public Cargocrud() { }

        public int Cargorepetido(string laValidacion)
        {
            conexion cn = new conexion();

            try
            {
                cn.abrir();
                SqlCommand CrearCargor = new SqlCommand("Cargorepetido", cn.Conectarbd);
                CrearCargor.CommandType = CommandType.StoredProcedure;


                CrearCargor.Parameters.AddWithValue("@validacion", laValidacion);


                int cantidadItemSolicitado = Convert.ToInt32(CrearCargor.ExecuteScalar());

                return cantidadItemSolicitado;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 5;
            }
            finally { cn.cerrar(); }


        }




        public void CrearCargo(Cargocrud Cargo)
        {
            conexion cn = new conexion();
            try
            {
                SqlCommand cmd = new SqlCommand("InsertarCargos", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@descripcion", Cargo.NombreCargo);

                cn.abrir();
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                MessageBox.Show("Por favor ingresa los datos requeridos");

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
                cmd.Parameters.AddWithValue("@estado", Cargo.Estado);

                cn.abrir();

                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                DataTable tabla = new DataTable();
                adp.Fill(tabla);

                cn.abrir();
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

            }
            finally
            {
                //sqlConnection.Close();
                cn.cerrar();
            }
        }

        public void ActualizarCargoEstado(Cargocrud Cargo)
        {
            conexion cn = new conexion();
            try
            {
                SqlCommand cmd = new SqlCommand("Actualizarestadocargo", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idCargo", Cargo.CargoID);
                cmd.Parameters.AddWithValue("@estado", Cargo.Estado);

                cn.abrir();

                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                DataTable tabla = new DataTable();
                adp.Fill(tabla);

                cn.abrir();
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

            }
            finally
            {
                //sqlConnection.Close();
                cn.cerrar();
            }
        }







        //public void Actualizarestado(Cargocrud Cargo)
        //{
        //    Conexion cn = new Conexion();
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("Actualizarestado", cn.Conectarbd);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@idCargo", Cargo.CargoID);
        //        cmd.Parameters.AddWithValue("@estado", Cargo.Estado);

        //        cn.abrir();

        //        SqlDataAdapter adp = new SqlDataAdapter();
        //        adp.SelectCommand = cmd;
        //        DataTable tabla = new DataTable();
        //        adp.Fill(tabla);

        //        cn.abrir();
        //        cmd.ExecuteNonQuery();

        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }
        //    finally
        //    {
        //        //sqlConnection.Close();
        //        cn.cerrar();
        //    }
        //}

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
