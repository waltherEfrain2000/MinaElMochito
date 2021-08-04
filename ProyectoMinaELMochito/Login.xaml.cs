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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoMinaELMochito
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {

        private User usuario = new User();


        public Login()
        {
            InitializeComponent();
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
                //System.Media.SystemSounds.Beep.Play();
                //System.Media.SystemSounds.Hand.Play();
                System.Media.SystemSounds.Asterisk.Play();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            System.Media.SystemSounds.Question.Play();
            try
            {
                // Implementar la búsqueda del usuario desde la clase Usuario
                User elUsuario = usuario.BuscarUsuario(txtUsername.Text);

                // Verificar si el usuario existe y si el rol es diferente al rol empleado de turno de esta manera se identifica quye es un usuario administrador
                if (elUsuario.Username == null)
                    MessageBox.Show("El usuario o la contraseña no es correcta. Favor verificar.", "Usuario o contraseña incorrecta", MessageBoxButton.OK, MessageBoxImage.Error);

                else
                {
                    // Verificar que la contraseña ingresada es igual a la contraseña
                    // almacenada en la base de datos
                    if (elUsuario.Password == pwbPassword.Password && elUsuario.Estado && elUsuario.Rol == 1)
                    {
                        // Mostrar el formulario de menú principal
                        //MenuPrincipal menu = new MenuPrincipal(elUsuario.NombreCompleto);
                        //menu.Show();
                        MessageBox.Show($"Bienvenido {elUsuario.Username} eres un Usuario Administrador", "Usuario ADMINISTRADOR", MessageBoxButton.OK, MessageBoxImage.Information);

                        menuPrincipal sld = new menuPrincipal();
                        sld.Show();
                        this.Close();


                        Close();
                    }
                    else if (elUsuario.Password == pwbPassword.Password && elUsuario.Estado)
                    {
                        MessageBox.Show($"Bienvenido {elUsuario.Username} eres un Empleado de turno", "Usuario Emplado de Turno", MessageBoxButton.OK, MessageBoxImage.Information);
                        menuEmpleado sld = new menuEmpleado();
                        sld.Show();
                        this.Close();
                        Close();
                    }


                    else if (!elUsuario.Estado)
                        MessageBox.Show("Tu usuario se encuentra inactivo. Favor comunicarte con el personal de IT");
                    else
                        MessageBox.Show("El usuario o la contraseña no es correcta. Favor verificar.", "Usuario o contraseña incorrecta", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ha ocurrido un error al momento de realizar la consulta...");
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

        private void btnLogin_MouseEnter(object sender, MouseEventArgs e)
        {


            //System.Media.SystemSounds.Hand.Play();


        }


        private void txtUsername_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtUsername.Text = String.Empty;
        }

        private void pwbPassword_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            pwbPassword.Password = String.Empty;
        }

        private void PackIconMaterial_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            pwbPassword.Visibility = Visibility.Hidden;
            icon.Visibility = Visibility.Visible;
            txtpassword.Visibility = Visibility.Visible;
            txtpassword.Text = pwbPassword.Password;
            iconeye.Visibility = Visibility.Hidden;

        }

        private void PackIconMaterial_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            pwbPassword.Password = txtpassword.Text;
            pwbPassword.Visibility = Visibility.Visible;
            icon.Visibility = Visibility.Hidden;
            txtpassword.Visibility = Visibility.Hidden;

            iconeye.Visibility = Visibility.Visible;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            recuperarContraseña recuperarContraseña = new recuperarContraseña();
            recuperarContraseña.Show();

        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
