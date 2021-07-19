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
    class User
    {
        // Variables miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        // Propiedades
        public int Id { get; set; }

        public string NombreCompleto { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Rol { get; set; }
        public bool Estado { get; set; }

        // Constructores
        public User() { }

        public User(string nombreCompleto, string username, string password, string rol, bool estado)
        {
            NombreCompleto = nombreCompleto;
            Username = username;
            Password = password;
            Rol = rol;
            Estado = estado;
        }

        // Métodos

        /// <summary>
        /// Verifica si las credenciales de inicio de sesión son correctas.
        /// </summary>
        /// <param name="username">El nombre del usuario</param>
        /// <returns>Los datos del usuario</returns>
        public User BuscarUsuario(string username)
        {
            // Crear el objeto que almacena la información de los resultados
            User usuario = new User();

            try
            {
                // Query de selección
                string query = @"SELECT * FROM Usuarios.Usuario
                                 WHERE username = @username";


                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@username", username);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Obtener los valores del usuario si la consulta retorna valores
                        usuario.Id = Convert.ToInt32(rdr["id"]);
                        usuario.NombreCompleto = rdr["nombreCompleto"].ToString();
                        usuario.Username = rdr["username"].ToString();
                        usuario.Password = rdr["password"].ToString();
                        usuario.Rol = rdr["rol"].ToString();
                        usuario.Estado = Convert.ToBoolean(rdr["estado"]);
                    }
                }

                // Retornar el usuario con los valores
                return usuario;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// aqui creamos una lista con los datos de los usuarios existentes
        /// </summary>
        /// <returns>los datos de los usuarios</returns>
       public   List<User> MostrarUsuario()
        {
            List<User> users = new List<User>();
            try
            {
                // Query de selección
                string query = @"select	id,nombreCompleto,username, password,rol,estado from Usuarios.Usuario";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Obtener los datos de los usuarios
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                        users.Add(new User { Id = Convert.ToInt32(rdr["id"]), NombreCompleto = rdr["nombreCompleto"].ToString(), Username = rdr["username"].ToString(), Password=rdr["password"].ToString(), Rol = rdr["rol"].ToString(), Estado =Convert.ToBoolean( rdr["estado"] )});
                }

                return users;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
                sqlConnection.Close();
            }

        }


        public void CrearUsuario(User user)
        {
            try
            {
                string query = @"INSERT INTO Usuarios.Usuario (nombreCompleto, username, password, rol,estado)
                                 VALUES (@nombreCompleto, @username, @password,@rol,@estado)";

                sqlConnection.Open();

                SqlCommand sqlCommand= new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@nombreCompleto", user.NombreCompleto);
                sqlCommand.Parameters.AddWithValue("@username", user.Username);
                sqlCommand.Parameters.AddWithValue("@password", user.Password);
                sqlCommand.Parameters.AddWithValue("@rol", user.Rol);
                sqlCommand.Parameters.AddWithValue("@estado", user.Estado);

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
        /// <summary>
        /// aqui modificamos el usuario
        /// </summary>
        /// <param name="user"></param>
        public void ModificarUsuario(User user)
        {
            try
            {

                string query = @"UPDATE Usuarios.Usuario
                                 SET nombreCompleto = @nombreCompleto, username = @username,  password = @password, rol=@rol,estado=@estado
                                 WHERE id = @id";

                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@id", user.Id);
                sqlCommand.Parameters.AddWithValue("@nombreCompleto", user.NombreCompleto);
                sqlCommand.Parameters.AddWithValue("@username", user.Username);
                sqlCommand.Parameters.AddWithValue("@password", user.Password);
                sqlCommand.Parameters.AddWithValue("@rol", user.Rol);
                sqlCommand.Parameters.AddWithValue("@estado", user.Estado);

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



        public void InvalidarUsuario(User user)
        {
            try
            {

                string query = @"UPDATE Usuarios.Usuario
                                 SET estado=@estado
                                 WHERE id = @id";

                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@id", user.Id);
  
                sqlCommand.Parameters.AddWithValue("@estado", user.Estado);

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

    }
}
