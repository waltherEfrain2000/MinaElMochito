using System;
using System.Collections.Generic;
using System.Data;
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
using System.Data.SqlClient;
using System.Configuration;

namespace ProyectoMinaELMochito
{


    public partial class Usuarios_Crud : Window
    {

        private Validaciones validaciones = new Validaciones();

        private User user = new User();
        private List<User> users;
        public Usuarios_Crud()
        {
            InitializeComponent();
            CargarDatos();
            botonfecha.Content = string.Format("{0}", DateTime.Now.ToString());

        }

        private void ObtenerUsuarios()
        {
            users = user.MostrarUsuario();

            dtgriduser.DisplayMemberPath = "id";
            dtgriduser.DisplayMemberPath = "nombreCompleto";
            dtgriduser.DisplayMemberPath = "UserName";
            dtgriduser.DisplayMemberPath = "Password";
            dtgriduser.DisplayMemberPath = "Rol";
            dtgriduser.DisplayMemberPath = "Estado";
            dtgriduser.SelectedValuePath = "id";

            dtgriduser.ItemsSource = users;

        }

        private bool VerificarValores()
        {

            if (txtnombre.Text == "" || txtusername.Text == "")
            {
                MessageBox.Show("Por favor ingresa todos los valores en las cajas de texto");
                return false;
            }
            else if (cmbEstado.SelectedValue == null)
            {
                MessageBox.Show("Por favor selecciona  el estado del Usuario");
                return false;
            }
            else if (cmbRol.SelectedValue == null)
            {

                MessageBox.Show("Por favor selecciona el Rol del Usuario");
                return false;

            }


            return true;
        }

        private void ObtenerValoresFormulario()
        {


            user.NombreCompleto = txtnombre.Text;
            user.Username = txtusername.Text;
            user.Password = pssPassword.Password;

            if (cmbRol.SelectedIndex == 0)
            {
                MessageBox.Show("sera un admin");
                user.Rol = "ADMINISTRADOR";
            }
            else
            {
                MessageBox.Show("sera un empleado normal");
                user.Rol = "EMPLEADODETURNO";
            }
            if (cmbEstado.SelectedIndex == 0)
            {
                MessageBox.Show("sera un empleado inactivo");
                user.Estado = false;
            }
            else
                user.Estado = true;

        }
        private void ObtenerValoresFormularioModify()
        {

            user.Id = Convert.ToInt32(txtid.Text);
            user.NombreCompleto = txtnombre.Text;
            user.Username = txtusername.Text;
            user.Password = pssPassword.Password;

            if (cmbRol.SelectedIndex == 0)
            {
                MessageBox.Show("sera un admin");
                user.Rol = "ADMINISTRADOR";
            }
            else
            {
                MessageBox.Show("sera un empleado normal");
                user.Rol = "EMPLEADODETURNO";
            }
            if (cmbEstado.SelectedIndex == 0)
            {
                MessageBox.Show("sera un empleado inactivo");
                user.Estado = false;
            }
            else
                user.Estado = true;

        }

        private void ObtenerValoresFormularioInabilitar()
        {

            user.Id = Convert.ToInt32(txtid.Text);

            if (cmbEstado.SelectedIndex == 0)
            {
                MessageBox.Show("El usuario ya esta invalidado, no se puede hacer nada");

            }
            else

                user.Estado = false;

        }
        private void limpiar()
        {
            txtnombre.Text = "";
            txtusername.Text = "";
            pssPassword.Password = "";
            cmbEstado.Text = "";
            cmbRol.Text = "";
            txtid.Text = "";

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
                catch (Exception ex)
                {

                    MessageBox.Show($"Error al momento de ingresar el Usuario...{ex}");

                }
                finally
                {
                    limpiar();
                    ObtenerUsuarios();

                }
            }


        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValores())
            {
                try
                {
                    ObtenerValoresFormularioModify();

                    user.ModificarUsuario(user);

                    MessageBox.Show("el Usuario se modifico correctamente!");

                    limpiar();

                }
                catch (Exception ex)
                {

                    MessageBox.Show($"Error al momento de actualizar el Usuario...{ex}");
                }
                finally
                {
                    CargarDatos();
                    limpiar();
                }
            }
        }


        private void SetUsuario(User user)
        {
            try
            {
                this.txtnombre.Text = Convert.ToString(user.NombreCompleto);
                this.txtusername.Text = Convert.ToString(user.Username);
                this.pssPassword.Password = Convert.ToString(user.Password);
                this.cmbRol.SelectedItem = Convert.ToString(user.Rol);
                this.cmbEstado.SelectedValue = Convert.ToBoolean(user.Estado);
            }
            catch (Exception)
            {

                throw;
            }


        }
        private void dtgriduser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataGrid dataGrid = (DataGrid)sender;
            DataRowView dataRowView = dataGrid.SelectedItem as DataRowView;



            if (dataRowView != null)
            {
                string estado;

                txtid.Text = dataRowView["ID"].ToString();
                txtnombre.Text = dataRowView["Nombre Completo"].ToString();
                txtusername.Text = dataRowView["Nombre de Usuario"].ToString();
                pssPassword.Password = dataRowView["Contraseña"].ToString();
                cmbRol.Text = dataRowView["Rol"].ToString();
                estado = dataRowView["Estado"].ToString();

                if (estado == "True")
                {
                    cmbEstado.Text = "Activo";
                }
                else
                {
                    cmbEstado.Text = "Inactivo";
                }


            }

        }

        private void CargarDatos()
        {
            conexion cn = new conexion();
            Validaciones val = new Validaciones();
            try
            {
                //Query para seleccionar los datos de la tabla


                SqlCommand cmd = new SqlCommand("MostrarUsuarios", cn.Conectarbd);
                cmd.CommandType = CommandType.StoredProcedure;

                // Crear el comando SQL

                cn.abrir();
                cmd.ExecuteNonQuery();


                SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(cmd);

                DataTable dataTable1 = new DataTable("Usuarios.Usuario");


                sqlDataAdapter1.Fill(dataTable1);
                dtgriduser.ItemsSource = dataTable1.DefaultView;
                sqlDataAdapter1.Update(dataTable1);



            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cn.cerrar();
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValores())
            {
                try
                {
                    ObtenerValoresFormularioInabilitar();

                    user.InvalidarUsuario(user);

                    MessageBox.Show("el Usuario se deshabilito correctamente!");

                    limpiar();

                }
                catch (Exception ex)
                {

                    MessageBox.Show($"Error al momento de deshabilitar el Usuario...{ex}");
                }
                finally
                {
                    CargarDatos();
                    limpiar();
                }
            }
        }

        private void btnsalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            Empleados sld = new Empleados();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            Vehiculos sld = new Vehiculos();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_2(object sender, RoutedEventArgs e)
        {
            Inventario_Mineral sld = new Inventario_Mineral();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_3(object sender, RoutedEventArgs e)
        {
            EntradasHistoricas sld = new EntradasHistoricas();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_4(object sender, RoutedEventArgs e)
        {
            Salidas sld = new Salidas();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_5(object sender, RoutedEventArgs e)
        {
            ViajesInternos sld = new ViajesInternos();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_6(object sender, RoutedEventArgs e)
        {
            Usuarios_Crud sld = new Usuarios_Crud();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            menuPrincipal sld = new menuPrincipal();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_7(object sender, RoutedEventArgs e)
        {
            menuPrincipal sld = new menuPrincipal();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_8(object sender, RoutedEventArgs e)
        {
            Login sld = new Login();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_9(object sender, RoutedEventArgs e)
        {
            ViajesInternos sld = new ViajesInternos();
            sld.Show();
            this.Close();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtnombre_KeyDown(object sender, KeyEventArgs e)
        {
            validaciones.validarTxtSinNumeros(e);
        }

        private void dtgriduser_CurrentCellChanged(object sender, EventArgs e)
        {
            dtgriduser.IsReadOnly = true;
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }


    }
}