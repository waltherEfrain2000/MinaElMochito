using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoMinaELMochito
{
    /// <summary>
    /// Lógica de interacción para recuperarContraseña.xaml
    /// </summary>
    public partial class recuperarContraseña : Window
    {
        public recuperarContraseña()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void txtCorreo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtCorreo.Clear();
        }

        private void txtUsuario_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtUsuario.Clear();
        }

        conexion cn = new conexion();
        User usuario = new User();
        private void btnRecuperar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string prueba = txtUsuario.Text;

                cn.abrir();
                SqlCommand contraseña = new SqlCommand("recuperarContrasenia", cn.Conectarbd);
                contraseña.CommandType = CommandType.StoredProcedure;
                contraseña.Parameters.AddWithValue("@usuario", prueba);

                User elUsuario = usuario.BuscarUsuario(txtUsuario.Text);
                string contraseñaRecuperada = Convert.ToString(contraseña.ExecuteScalar());

                // Verificar si el usuario existe y si el rol es diferente al rol empleado de turno de esta manera se identifica quye es un usuario administrador
                if (elUsuario.Username == null)
                {
                    MessageBox.Show("El usuario no es correcto. Favor verificar.", "Usuario o contraseña incorrecta", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient Smtp = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress("minamochito7@gmail.com");
                    mail.To.Add(txtCorreo.Text);
                    mail.Subject = "Recuperación de contraseña";
                    mail.Body = "Estimado usuario " + prueba + ", por este medio le hacemos envío de su contraseña. \n" +
                                contraseñaRecuperada +
                                "\nTenga un muy buen día.";
                    Smtp.Port = 587;
                    Smtp.Credentials = new NetworkCredential("minamochito7@gmail.com", "MinaMochito2021");
                    Smtp.EnableSsl = true;
                    Smtp.Send(mail);
                    MessageBox.Show("operación realizada", "Contraseña enviada", MessageBoxButton.OK, MessageBoxImage.Information);
                }            
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                cn.cerrar();
            }
        }
    }
}
