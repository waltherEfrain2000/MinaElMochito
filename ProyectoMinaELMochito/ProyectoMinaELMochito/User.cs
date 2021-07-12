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
            conexion cn = new conexion();
            Validaciones val = new Validaciones();
            try
            {
                // Query de selección


                SqlCommand cmd = new SqlCommand("buscarUsusario", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);


                cn.abrir();

                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                DataTable tabla = new DataTable();
                adp.Fill(tabla);

                cn.abrir();

                cmd.ExecuteNonQuery();



                using (SqlDataReader rdr = cmd.ExecuteReader())
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
                cn.cerrar();
            }
        }

        /// <summary>
        /// aqui creamos una lista con los datos de los usuarios existentes
        /// </summary>
        /// <returns>los datos de los usuarios</returns>
        public List<User> MostrarUsuario()
        {
            List<User> users = new List<User>();

            conexion cn = new conexion();
            Validaciones val = new Validaciones();
            try
            {
                // Query de selección


                SqlCommand cmd = new SqlCommand("MostrarUsuarios", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.abrir();

                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                DataTable tabla = new DataTable();
                adp.Fill(tabla);

                cn.abrir();

                cmd.ExecuteNonQuery();




                // Obtener los datos de los usuarios
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                        users.Add(new User { Id = Convert.ToInt32(rdr["id"]), NombreCompleto = rdr["nombreCompleto"].ToString(), Username = rdr["username"].ToString(), Password = rdr["password"].ToString(), Rol = rdr["rol"].ToString(), Estado = Convert.ToBoolean(rdr["estado"]) });
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
                cn.cerrar();
            }

        }


        public void CrearUsuario(User user)
        {
            conexion cn = new conexion();
            Validaciones val = new Validaciones();
            try
            {


                SqlCommand cmd = new SqlCommand("InsertarUsuario", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombreCompleto", user.NombreCompleto);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@rol", user.Rol);
                cmd.Parameters.AddWithValue("@estado", user.Estado);

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
        /// <summary>
        /// aqui modificamos el usuario
        /// </summary>
        /// <param name="user"></param>
        public void ModificarUsuario(User user)
        {
            conexion cn = new conexion();
            Validaciones val = new Validaciones();
            try
            {



                SqlCommand cmd = new SqlCommand("ModificarUsuario", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombreCompleto", user.NombreCompleto);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@rol", user.Rol);
                cmd.Parameters.AddWithValue("@estado", user.Estado);

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



        public void InvalidarUsuario(User user)
        {
            conexion cn = new conexion();
            Validaciones val = new Validaciones();
            try
            {


                SqlCommand cmd = new SqlCommand("EliminarUsuario", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", user.Id);
                cmd.Parameters.AddWithValue("@estado", user.Estado);

                cn.abrir();

                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                DataTable tabla = new DataTable();
                adp.Fill(tabla);

                cn.abrir();

                cmd.ExecuteNonQuery();

                cn.cerrar();
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