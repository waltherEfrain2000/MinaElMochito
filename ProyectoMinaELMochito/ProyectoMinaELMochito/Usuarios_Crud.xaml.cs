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


namespace ProyectoMinaELMochito
{
    /// <summary>
    /// Lógica de interacción para Usuarios_Crud.xaml
    /// </summary>
    public partial class Usuarios_Crud : Window
    {

        private User user = new User();
        private List<User> users;
        public Usuarios_Crud()
        {
            InitializeComponent();
            ObtenerUsuarios();

        }

        private void ObtenerUsuarios()
        {
            users = user.MostrarUsuario();

            dtgriduser.DisplayMemberPath = "id";
            dtgriduser.DisplayMemberPath = "Nombre Completo";
            dtgriduser.DisplayMemberPath = "UserName";
            dtgriduser.DisplayMemberPath = "Password";
            dtgriduser.DisplayMemberPath = "Rol";
            dtgriduser.DisplayMemberPath = "Estado";
            dtgriduser.SelectedValuePath = "id";

            dtgriduser.ItemsSource = users;
         
        }

        private bool VerificarValores()
        {
            if (txtnombre.Text == string.Empty || txtusername.Text == string.Empty)
            {
                MessageBox.Show("Por favor ingresa todos los valores en las cajas de texto");
                return false;
            }
            else if (cmbEstado.SelectedValue == null)
            {
                MessageBox.Show("Por favor selecciona  el estado del Usuario");
                return false;
            }
            else if(cmbRol.SelectedValue==null)
            {

                MessageBox.Show("Por favor selecciona el Rol del Usuario");
                return false;

            }

            return true;
        }

        private void ObtenerValoresFormulario()
        {

           
            user.NombreCompleto = txtnombre.Text;
            user.Username = txtnombre.Text;
            user.Password = pssPassword.Password;
            user.Rol = cmbRol.SelectedItem.ToString();
            //if (cmbRol.SelectedItem.ToString() == "ADMINISTRADOR")
            //{
            //    user.Rol = "ADMINISTRADOR";
            //}
            //user.Rol = "EMPLEADODETURNO";

            if (cmbEstado.SelectedItem == "Inactivo")
            {
                user.Estado = false;
            }
            else
                user.Estado = true;
  
        }



        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValores())
            {
                try
                {
                    ObtenerValoresFormulario();

                    user.CrearUsuario(user);
                    MessageBox.Show("Nuevo usuario ingresados correctamente");

                }
                catch (Exception f)
                {

                    MessageBox.Show( $"{f}");
                }
            }
        }
    }
}
