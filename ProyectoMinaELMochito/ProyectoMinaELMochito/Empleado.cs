using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar los namespaces requeridos
using System.Data.SqlClient;
using System.Configuration;

//holaa

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
        public string NombreCompleto { get; set; }
        public int Edad { get; set; }
        public int Genero { get; set; }
        public string NombreGenero { get; set; }
        public int Cargo { get; set; }

        public string NombreCargo { get; set; }
        public string Estado { get; set; }
        public decimal Salario { get; set; }
        public string Direccion { get; set; }

        // Constructores
        public Empleado() { }


        public Empleado(string identidad, string nombreCompleto, int edad, int genero, int cargo, string estado, decimal salario, string direccion)
        {

            Identidad = identidad;
            NombreCompleto = nombreCompleto;
            Edad = edad;
            Genero = genero;
            Cargo = cargo;
            Estado = estado;
            Salario = salario;
            Direccion = direccion;
        }
        public Empleado(int ID, string identidad, string nombreCompleto, int edad, int genero, int cargo, string estado, decimal salario, string direccion)
        {
            EmpledoID = ID;
            Identidad = identidad;
            NombreCompleto = nombreCompleto;
            Edad = edad;
            Genero = genero;
            Cargo = cargo;
            Estado = estado;
            Salario = salario;
            Direccion = direccion;
        }

        public void CrearEmpleado(Empleado empleado)
        {
            try
            {
                string query = @"INSERT	INTO Minas.Empleado values (@identidad,@primerNombre,@edad,@idGenero,@direccion,@idCargo,@salario,@estado)";

                sqlConnection.Open();


                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@identidad", empleado.Identidad);
                sqlCommand.Parameters.AddWithValue("@primerNombre", empleado.NombreCompleto);
                sqlCommand.Parameters.AddWithValue("@edad", empleado.Edad);
                sqlCommand.Parameters.AddWithValue("@idGenero", empleado.Genero);
                sqlCommand.Parameters.AddWithValue("@direccion", empleado.Direccion);
                sqlCommand.Parameters.AddWithValue("@idCargo", empleado.Cargo);
                sqlCommand.Parameters.AddWithValue("@salario", empleado.Salario);
                sqlCommand.Parameters.AddWithValue("@estado", empleado.Estado);

                sqlCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                sqlConnection.Close();

            }
        }

        public void ActualizarEmpleado(Empleado empleado)
        {
            try
            {
                string query = @"UPDATE	Minas.Empleado SET primerNombre = @primerNombre, 
                                                           edad = @edad,
                                                           direccion = @direccion,
                                                           idCargo = @idCargo,
                                                           salario = @salario
                                                            Where idEmpleado = @idEmpleado";

                sqlConnection.Open();


                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@idEmpleado", empleado.EmpledoID);
                sqlCommand.Parameters.AddWithValue("@primerNombre", empleado.NombreCompleto);
                sqlCommand.Parameters.AddWithValue("@edad", empleado.Edad);
                sqlCommand.Parameters.AddWithValue("@direccion", empleado.Direccion);
                sqlCommand.Parameters.AddWithValue("@idCargo", empleado.Cargo);
                sqlCommand.Parameters.AddWithValue("@salario", empleado.Salario);


                sqlCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                sqlConnection.Close();

            }
        }

        public void EliminarEmpleado(Empleado empleado)
        {
            try
            {
                string query = @"UPDATE	Minas.Empleado SET estado = @estado Where idEmpleado = @idEmpleado";

                sqlConnection.Open();


                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@idEmpleado", empleado.EmpledoID);
                sqlCommand.Parameters.AddWithValue("@estado", "inactivo");

                sqlCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                sqlConnection.Close();

            }
        }

        public List<Empleado> LlenarComboBoxCargo()
        {

            try
            {
                //Realizar el query que cargará la información correspondiente
                String query = @"Select * From Minas.cargo";

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
                String query = @"Select * From Minas.Genero";

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






    }
}