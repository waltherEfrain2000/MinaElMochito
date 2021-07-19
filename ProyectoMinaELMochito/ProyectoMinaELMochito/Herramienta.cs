using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ProyectoMinaELMochito
{
    class Herramienta
    {
        public int ID { get; set; }
        public string NombreHerramienta { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int HerramientasEnUso { get; set; }

        public Herramienta() { }

        public Herramienta(int id, string nombreHerramieta, int cantidad, decimal precioUnitario, int herramientasEnUso)
        {
            ID = id;
            NombreHerramienta = nombreHerramieta;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            HerramientasEnUso = herramientasEnUso;
        }

        public void CrearHerramienta (Herramienta herramienta)
        {
            conexion cn = new conexion();
            try
            {
                cn.abrir();

                SqlCommand cmd = new SqlCommand("ingresarHerramienta", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombreHerramienta", herramienta.NombreHerramienta);
                cmd.Parameters.AddWithValue("@cantidad", herramienta.Cantidad);
                cmd.Parameters.AddWithValue("@precioUnitario", herramienta.PrecioUnitario);         


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

        public void ActualizarHerramienta(Herramienta herramienta)
        {
            conexion cn = new conexion();
            try
            {
                SqlCommand cmd = new SqlCommand("actualizarHerramienta", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", herramienta.ID);
                cmd.Parameters.AddWithValue("@nombreHerramienta", herramienta.NombreHerramienta);
                cmd.Parameters.AddWithValue("@cantidad", herramienta.Cantidad);
                cmd.Parameters.AddWithValue("@precioUnitario", herramienta.PrecioUnitario);

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

        public void EliminarHerramienta(Herramienta herramienta)
        {
            conexion cn = new conexion();
            try
            {
                cn.abrir();
                SqlCommand cmd = new SqlCommand("eliminarHerramienta ", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", herramienta.ID);

                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                DataTable tabla = new DataTable();
                adp.Fill(tabla);

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

        public void AsignarHerramienta(Herramienta herramienta)
        {
            conexion cn = new conexion();
            try
            {
                cn.abrir();

                SqlCommand cmd = new SqlCommand("calculoHerramientas", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", herramienta.ID);
                cmd.Parameters.AddWithValue("@herramienta", herramienta.HerramientasEnUso);

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
