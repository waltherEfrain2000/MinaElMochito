using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


//hola

namespace ProyectoMinaELMochito
{
    class Empleado
    {

        // Variables miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Propiedades
        public int EmpledoID { get; set; }
        public string Identidad { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string FechaNacimiento { get; set; }
        public int Genero { get; set; }
        public string NombreGenero { get; set; }
        public int Cargo { get; set; }
        public string NombreCargo { get; set; }
        public string Estado { get; set; }
        public decimal Salario { get; set; }
        public string Direccion { get; set; }

        // Constructores
        public Empleado() { }


        public Empleado(string identidad, string primerNombre, string segundoNombre, string primerApellido, string segundoApellido, string fechaNacimiento, int genero, int cargo, string estado, decimal salario, string direccion)
        {

            Identidad = identidad;
            PrimerNombre = primerNombre;
            SegundoNombre = segundoNombre;
            PrimerApellido = primerApellido;
            SegundoApellido = segundoApellido;
            FechaNacimiento = fechaNacimiento;
            Genero = genero;
            Cargo = cargo;
            Estado = estado;
            Salario = salario;
            Direccion = direccion;
        }

        public void CrearEmpleado(Empleado empleado)
        {
            conexion cn = new conexion();
            try
            {
                SqlCommand cmd = new SqlCommand("ingresarEmpleado", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@identidad", empleado.Identidad);
                cmd.Parameters.AddWithValue("@primerNombre", empleado.PrimerNombre);
                cmd.Parameters.AddWithValue("@segundoNombre", empleado.SegundoNombre);
                cmd.Parameters.AddWithValue("@primerApellido", empleado.PrimerApellido);
                cmd.Parameters.AddWithValue("@segundoApellido", empleado.SegundoApellido);
                cmd.Parameters.AddWithValue("@fechaNacimiento", empleado.FechaNacimiento);
                cmd.Parameters.AddWithValue("@idGenero", empleado.Genero);
                cmd.Parameters.AddWithValue("@direccion", empleado.Direccion);
                cmd.Parameters.AddWithValue("@idCargo", empleado.Cargo);
                cmd.Parameters.AddWithValue("@salario", empleado.Salario);

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

        public void ActualizarEmpleado(Empleado empleado)
        {
            conexion cn = new conexion();
            try
            {
                SqlCommand cmd = new SqlCommand("actualizarEmpleado", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", empleado.EmpledoID);
                cmd.Parameters.AddWithValue("@identidad", empleado.Identidad);
                cmd.Parameters.AddWithValue("@primerNombre", empleado.PrimerNombre);
                cmd.Parameters.AddWithValue("@segundoNombre", empleado.SegundoNombre);
                cmd.Parameters.AddWithValue("@primerApellido", empleado.PrimerApellido);
                cmd.Parameters.AddWithValue("@segundoApellido", empleado.SegundoApellido);
                cmd.Parameters.AddWithValue("@fechaNacimiento", empleado.FechaNacimiento);
                cmd.Parameters.AddWithValue("@idGenero", empleado.Genero);
                cmd.Parameters.AddWithValue("@direccion", empleado.Direccion);
                cmd.Parameters.AddWithValue("@idCargo", empleado.Cargo);
                cmd.Parameters.AddWithValue("@salario", empleado.Salario);

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

        public void EliminarEmpleado(Empleado empleado)
        {
            conexion cn = new conexion();
            try
            {
                SqlCommand cmd = new SqlCommand("elimiarEmpleado", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", empleado.EmpledoID);

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

        public List<Empleado> LlenarComboBoxCargo()
        {

            try
            {
                //Realizar el query que cargará la información correspondiente
                String query = @"Select * From Empleados.cargo where estado = 'Activo'";

                //Establecer la conexión
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<Empleado> cargos = new List<Empleado>();

                while (reader.Read())
                {
                    cargos.Add(new Empleado
                    {
                        NombreCargo = reader["descripcion"].ToString(),
                        Cargo = Convert.ToInt32(reader["idCargo"].ToString())
                    });
                }

                return cargos;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                sqlConnection.Close();
            }

        }
        public List<Empleado> LlenarComboBoxGenero()
        {
            try
            {
                //Realizar el query que cargará la información correspondiente
                String query = @"Select * From Empleados.Genero";

                //Establecer la conexión
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<Empleado> generos = new List<Empleado>();

                while (reader.Read())
                {
                    generos.Add(new Empleado
                    {
                        NombreGenero = reader["descripcion"].ToString(),
                        Genero = Convert.ToInt32(reader["idGenero"].ToString())
                    });
                }

                return generos;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public int validarIdentidad(string identidad)
        {
            conexion cn = new conexion();
            try
            {
                cn.abrir();
                SqlCommand sqlCommand = new SqlCommand("validarIdentidades", cn.Conectarbd);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@identidad", identidad);

                int valor = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return valor;
            }
            catch (Exception)
            {

                return 7;
            }
            finally
            {
                cn.cerrar();
            }
        }

    }
}