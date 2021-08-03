
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

        private conexion cn = new conexion();
        public Usuarios_Crud()
        {

            InitializeComponent();
            CargarDatos();
            ObtenerRol();
            botonfecha.Content = string.Format("{0}", DateTime.Now.ToString());

        }

        private void ObtenerRol()
        {

            cmbRol.ItemsSource = user.LlenarComboBoxEstados();
            cmbRol.DisplayMemberPath = "NombreRol";
            cmbRol.SelectedValuePath = "Rol";
        }



        private bool VerificarValores()
        {
            User Usuario = user.BuscarUsuario(txtusername.Text);

            if (Usuario.Username == null)
            {
              
                return true;
            }
            else
            {
                MessageBox.Show("El usuario ya existe, porfavor ingrese otro nombre de usuario");
                return false;
            }

            if (txtnombre.Text == string.Empty || txtusername.Text == string.Empty || txtApellido.Text == string.Empty || pssPassword.Password == string.Empty)
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

        private bool VerificarValoresModificar()
        {

            User Usuario = user.BuscarUsuario(txtusername.Text);


            if(Usuario.Password != pssPassword.Password)
            {
                MessageBox.Show("Contraseña Incorrecta, no puede modificar los datos");
                return false;
                
            }
            if (txtnombre.Text == string.Empty || txtusername.Text == string.Empty || txtApellido.Text == string.Empty)
            {
                MessageBox.Show("Por favor ingresa todos los valores en las cajas de texto");
                return false;
            }
            else if(pssPassword.Password == string.Empty){
                MessageBox.Show("Si desea modificar sus datos confirme su contraseña");

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


            user.PrimerNombre = txtnombre.Text;
            user.SegundoNombre = txtsegNombre.Text;
            user.PrimerApellido = txtApellido.Text;
            user.SegundoApellido = txtSegApellido.Text;
            user.Username = txtusername.Text;
            user.Password = pssPassword.Password;

            if (cmbRol.SelectedIndex == 0)
            {
                
                user.Rol = 1;
            }
            else
            {
                
                user.Rol = 2;
            }
            if (cmbEstado.SelectedIndex == 0)
            {
                
                user.Estado = false;
            }
            else
                user.Estado = true;

        }
        private void ObtenerValoresFormularioModify()
        {

            user.Id = Convert.ToInt32(txtid.Text);
            user.PrimerNombre = txtnombre.Text;
            user.SegundoNombre = txtsegNombre.Text;
            user.PrimerApellido = txtApellido.Text;
            user.SegundoApellido = txtSegApellido.Text;
            user.Username = txtusername.Text;
            user.Password = pssPassword.Password;

            if (cmbRol.SelectedIndex == 0)
            {
               
                user.Rol = 1;
            }
            else
            {
                
                user.Rol = 2;
            }

            if (cmbEstado.SelectedIndex == 0)
            {
              
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
            txtSegApellido.Text = "";
            txtApellido.Text = "";
            txtsegNombre.Text = "";
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
                    

                }
                catch (Exception ex)
                {

                    MessageBox.Show($"Error al momento de ingresar el Usuario...{ex}");

                }
                finally
                {
                    MessageBox.Show("Nuevo usuario ingresados correctamente");
                    limpiar();
                    CargarDatos();

                }
            }


        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValoresModificar())
            {
                try
                {
                    ObtenerValoresFormularioModify();

                    btnModificar.Visibility = Visibility.Hidden;
                    btnIngresar.Visibility = Visibility.Hidden;
                    btnAceptarEliminacion.Visibility = Visibility.Visible;
                    btnCancelarEliminacion.Visibility = Visibility.Visible;


                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show($"Error al momento de actualizar el Usuario...{ex}");
                }
                finally
                {
                    MessageBox.Show("Modifique los valores que desee");
                    CargarDatos();
                    
                }
            }
        }


        private void dtgriduser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            cn.abrir();

            DataGrid dg = (DataGrid)sender;
            DataRowView filaSeleccionada = dg.SelectedItem as DataRowView;




            if (filaSeleccionada != null)
            {
                string estado;

                txtid.Text = filaSeleccionada["ID"].ToString();
                txtnombre.Text = filaSeleccionada["Primer Nombre"].ToString();
                txtsegNombre.Text = filaSeleccionada["Segundo Nombre"].ToString();
                txtApellido.Text = filaSeleccionada["Primer Apellido"].ToString();
                txtSegApellido.Text = filaSeleccionada["Segundo Apellido"].ToString();
                txtusername.Text = filaSeleccionada["Usuario"].ToString();
                cmbRol.Text = filaSeleccionada["Rol"].ToString();
                estado = filaSeleccionada["Estado"].ToString();

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
            limpiar();
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



        private void txtApellido_KeyDown(object sender, KeyEventArgs e)
        {
            validaciones.validarTxtSinNumeros(e);
        }

        private void txtSegApellido_KeyDown(object sender, KeyEventArgs e)
        {
            validaciones.validarTxtSinNumeros(e);
        }

        private void txtnombre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnAceptarEliminacion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ObtenerValoresFormularioModify();

                User Usuario = user.BuscarUsuario(txtusername.Text);

                if (Usuario.Username == null)
                {

                    user.ModificarUsuario(user);
                    MessageBox.Show("Los datos han sido modificados exitosamente");
                }
                else
                {
                    MessageBox.Show("El usuario ya existe, porfavor ingrese otro nombre de usuario");
                    
                }
                

            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error al momento de actualizar el Usuario...{ex}");
            }
            finally
            {
                
                CargarDatos();
                limpiar();
                btnModificar.Visibility = Visibility.   Visible;
                btnIngresar.Visibility = Visibility.Visible;
                btnAceptarEliminacion.Visibility = Visibility.Hidden;
                btnCancelarEliminacion.Visibility = Visibility.Hidden;
            }
        }

        private void btnCancelarEliminacion_Click(object sender, RoutedEventArgs e)
        {
            btnModificar.Visibility = Visibility.Visible;
            btnIngresar.Visibility = Visibility.Visible;
            btnAceptarEliminacion.Visibility = Visibility.Hidden;
            btnCancelarEliminacion.Visibility = Visibility.Hidden;
        }

        private void ListViewItem_Selected_10(object sender, RoutedEventArgs e)
        {
            Cargos cargos = new Cargos();
            cargos.Show();
            this.Close();
        }

        private void ListViewItem_Selected_11(object sender, RoutedEventArgs e)
        {
            Herramientas herramientas = new Herramientas();
            herramientas.Show();
            this.Close();
        }
    }
}
