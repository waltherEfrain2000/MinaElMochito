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

        conexion cn = new conexion();
        Validaciones val = new Validaciones();
        public int Id { get; set; }

        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string NombreRol { get; set; }
        public int Rol { get; set; }
        public bool Estado { get; set; }

        public User() { }

        public User(string primerNombre, string segundoNombre, string primerApellido, string segundoApellido, string username, string password, int rol, bool estado)
        {
            PrimerNombre = primerNombre;
            SegundoNombre = segundoNombre;
            PrimerApellido = primerApellido;
            SegundoApellido = segundoApellido;
            Username = username;
            Password = password;
            Rol = rol;
            Estado = estado;
        }



        /// <param name="username">El nombre del usuario</param>

        public User BuscarUsuario(string username)
        {
            // Crear el objeto que almacena la información de los resultados
            User usuario = new User();
            conexion cn = new conexion();
            Validaciones val = new Validaciones();
            try
            {
                // Query de selección


                SqlCommand cmd = new SqlCommand("BuscarUsuario", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@usuario", username);


                cn.abrir();

                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                DataTable tabla = new DataTable();
                adp.Fill(tabla);



                cmd.ExecuteNonQuery();



                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Obtener los valores del usuario si la consulta retorna valores
                        usuario.Username = rdr["nombreUsuario"].ToString();
                        usuario.Password = rdr["contraseña"].ToString();
                        usuario.Rol = Convert.ToInt32(rdr["idRol"].ToString());
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


        //public List<User> MostrarUsuario()
        //{
        //    List<User> users = new List<User>();

        //    conexion cn = new conexion();
        //    Validaciones val = new Validaciones();
        //    try
        //    {
        //        // Query de selección


        //        SqlCommand cmd = new SqlCommand("MostrarUsuarios", cn.Conectarbd);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cn.abrir();

        //        SqlDataAdapter adp = new SqlDataAdapter();
        //        adp.SelectCommand = cmd;
        //        DataTable tabla = new DataTable();
        //        adp.Fill(tabla);

        //        cn.abrir();

        //        cmd.ExecuteNonQuery();




        //        // Obtener los datos de los usuarios
        //        using (SqlDataReader rdr = cmd.ExecuteReader())
        //        {
        //            while (rdr.Read())
        //                users.Add(new User { Id = Convert.ToInt32(rdr["id"]), PrimerNombre = rdr["PrimerNombre"].ToString(), SegundoNombre = rdr["segundoNombre"].ToString(), PrimerApellido = rdr["PrmerApellido"].ToString(), SegundoApellido = rdr["SegundoApellido"].ToString(), Username = rdr["username"].ToString(), Password = rdr["password"].ToString(), Rol = Convert.ToInt32(rdr["rol"]), Estado = Convert.ToBoolean(rdr["estado"]) });
        //        }

        //        return users;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    finally
        //    {
        //        // Cerrar la conexión
        //        cn.cerrar();
        //    }

        //}


        public void CrearUsuario(User user)
        {
            conexion cn = new conexion();
            Validaciones val = new Validaciones();
            try
            {


                SqlCommand cmd = new SqlCommand("RegistrarUsuario", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@primerNombre", user.PrimerNombre);
                cmd.Parameters.AddWithValue("@segundoNombre", user.SegundoNombre);
                cmd.Parameters.AddWithValue("@primerApellido", user.PrimerApellido);
                cmd.Parameters.AddWithValue("@segundoApellido", user.SegundoApellido);
                cmd.Parameters.AddWithValue("@nombreUsuario", user.Username);
                cmd.Parameters.AddWithValue("@contraseña", user.Password);
                cmd.Parameters.AddWithValue("@idRol", user.Rol);
                cmd.Parameters.AddWithValue("@estado", user.Estado);

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

        /// <param name="user"></param>
        public void ModificarUsuario(User user)
        {
            conexion cn = new conexion();
            Validaciones val = new Validaciones();
            try
            {



                SqlCommand cmd = new SqlCommand("ModificarUsuario", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@primerNombre", user.PrimerNombre);
                cmd.Parameters.AddWithValue("@segundoNombre", user.SegundoNombre);
                cmd.Parameters.AddWithValue("@primerApellido", user.PrimerApellido);
                cmd.Parameters.AddWithValue("@segundoApellido", user.SegundoApellido);
                cmd.Parameters.AddWithValue("@nombreUsuario", user.Username);
                cmd.Parameters.AddWithValue("@contraseña", user.Password);
                cmd.Parameters.AddWithValue("@idRol", user.Rol);
                cmd.Parameters.AddWithValue("@estado", user.Estado);
                cmd.Parameters.AddWithValue("@id", user.Id);

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

            try
            {


                SqlCommand cmd = new SqlCommand("EliminarUsuario", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", user.Id);
                cmd.Parameters.AddWithValue("@estado", user.Estado);

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



        public List<User> LlenarComboBoxEstados()
        {
            try
            {
                //Realizar el query que cargará la información correspondiente
                String query = @"EXEC MostrarRol";

                //Establecer la conexión
                cn.abrir();

                SqlCommand sqlCommand = new SqlCommand(query, cn.Conectarbd);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<User> rol = new List<User>();

                while (reader.Read())
                {
                    rol.Add(new User
                    {
                        NombreRol = reader["Rol"].ToString(),
                        Rol = Convert.ToInt32(reader["idRol"].ToString())
                    });
                }

                return rol;
            }
            catch (Exception)
            {

                throw;

            }
        }

    }
}