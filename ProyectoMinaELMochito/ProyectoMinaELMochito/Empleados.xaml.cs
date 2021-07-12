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
// Agregar los namespaces requeridos
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;


namespace ProyectoMinaELMochito
{
    /// <summary>
    /// Lógica de interacción para Empleados.xaml
    /// </summary>

    public partial class Empleados : Window
    {

        // Variables miembros
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        // conexiones con clases
        private Empleado empleado = new Empleado();
        private Procedimientos procedimientos = new Procedimientos();
        private Validaciones validaciones = new Validaciones();

        public Empleados()
        {
            InitializeComponent();
            MostrarEmpleado();
            MostrarCargos();
            MostrarGeneros();
            botonfecha.Content = string.Format("{0}", DateTime.Now.ToString());
            //cmbGenero.Items.Add("F");
            //cmbGenero.Items.Add("M");
            //cmbCargo.Items.Add("Gerente");
        }
        // -----------------------------------------    BOTONES    --------------------------------------------- //
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (procedimientos.VerificarCamposLlenos(txtIdentidad.Text, txtNombreCompleto.Text, txtEdad.Text, txtSalario.Text, txtDireccion.Text, cmbGenero.SelectedValue, cmbCargo.SelectedValue))
            {
                try
                {
                    //parametro 0 por que no es actualizacion
                    procedimientos.ExtraerInformacionFormulario(0, txtEmpleadoID.Text, txtIdentidad.Text, txtNombreCompleto.Text, txtEdad.Text, cmbGenero.SelectedIndex, cmbCargo.SelectedIndex, txtSalario.Text);
                    empleado.CrearEmpleado(empleado);

                    // Mensaje de inserción exitosa
                    MessageBox.Show("¡Empleado insertado correctamente!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    LimpiarCasillas();
                    MostrarEmpleado();
                }
            }
        }
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (procedimientos.VerificarCamposLlenos(txtIdentidad.Text, txtNombreCompleto.Text, txtEdad.Text, txtSalario.Text, txtDireccion.Text, cmbGenero.SelectedValue, cmbCargo.SelectedValue))
            {
                try
                {
                    //parametro 1 por que es actualizacion
                    edicionDeCasillas(false, 1);
                    //ocultar todos los otones inecesarios
                    btnModificar.Visibility = Visibility.Hidden;
                    btnAgregar.Visibility = Visibility.Hidden;
                    btnEliminar.Visibility = Visibility.Hidden;
                    btnAceptarModificacion.Visibility = Visibility.Visible;
                    btnCancelarModificacion.Visibility = Visibility.Visible;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }
        private void btnAceptarModificacion_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (procedimientos.VerificarCamposLlenos(txtIdentidad.Text, txtNombreCompleto.Text, txtEdad.Text, txtSalario.Text, txtDireccion.Text, cmbGenero.SelectedValue, cmbCargo.SelectedValue))
            {
                //ocultar todos los otones inecesarios
                btnModificar.Visibility = Visibility.Visible;
                btnAgregar.Visibility = Visibility.Visible;
                btnEliminar.Visibility = Visibility.Visible;
                btnAceptarModificacion.Visibility = Visibility.Hidden;
                btnCancelarModificacion.Visibility = Visibility.Hidden;
                try
                {
                    //parametro 1 por que es actualizacion
                    procedimientos.ExtraerInformacionFormulario(1, txtEmpleadoID.Text, txtIdentidad.Text, txtNombreCompleto.Text, txtEdad.Text, cmbGenero.SelectedIndex, cmbCargo.SelectedIndex, txtSalario.Text);
                    empleado.ActualizarEmpleado(empleado);


                    // Mensaje de inserción exitosa
                    MessageBox.Show("¡Empleado modificado correctamente!");


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    LimpiarCasillas();
                    MostrarEmpleado();
                }
            }
        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (procedimientos.VerificarCamposLlenos(txtIdentidad.Text, txtNombreCompleto.Text, txtEdad.Text, txtSalario.Text, txtDireccion.Text, cmbGenero.SelectedValue, cmbCargo.SelectedValue))
            {
                try
                {
                    //parametro 1 por que es actualizacion
                    edicionDeCasillas(true, 0);
                    //ocultar todos los otones inecesarios
                    btnModificar.Visibility = Visibility.Hidden;
                    btnAgregar.Visibility = Visibility.Hidden;
                    btnEliminar.Visibility = Visibility.Hidden;
                    btnAceptarEliminacion.Visibility = Visibility.Visible;
                    btnCancelarEliminacion.Visibility = Visibility.Visible;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }
        private void btnAceptarEliminacion_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (procedimientos.VerificarCamposLlenos(txtIdentidad.Text, txtNombreCompleto.Text, txtEdad.Text, txtSalario.Text, txtDireccion.Text, cmbGenero.SelectedValue, cmbCargo.SelectedValue))
            {
                //ocultar todos los otones inecesarios
                btnModificar.Visibility = Visibility.Visible;
                btnAgregar.Visibility = Visibility.Visible;
                btnEliminar.Visibility = Visibility.Visible;
                btnAceptarEliminacion.Visibility = Visibility.Hidden;
                btnCancelarEliminacion.Visibility = Visibility.Hidden;
                try
                {
                    //parametro 1 por que es actualizacion
                    procedimientos.ExtraerInformacionFormulario(1, txtEmpleadoID.Text, txtIdentidad.Text, txtNombreCompleto.Text, txtEdad.Text, cmbGenero.SelectedIndex, cmbCargo.SelectedIndex, txtSalario.Text);
                    empleado.EliminarEmpleado(empleado);

                    // Mensaje de inserción exitosa
                    MessageBox.Show("¡Empleado Eliminado correctamente!");


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    LimpiarCasillas();
                    MostrarEmpleado();
                }
            }
        }
        // -----------------------------------------    FUNCIONES DE TEXTBOXES    --------------------------------------------- //
        private void LimpiarCasillas()
        {
            txtEmpleadoID.Text = string.Empty;
            txtIdentidad.Text = string.Empty;
            txtNombreCompleto.Text = string.Empty;
            txtEdad.Text = string.Empty;
            txtSalario.Text = string.Empty;
            txtDireccion.Text = string.Empty;

            cmbGenero.SelectedValue = null;
            cmbCargo.SelectedValue = null;
            edicionDeCasillas(false, 0);
        }

        private void edicionDeCasillas(bool opcion, int operacion)
        {
            //operacion sirve para distingir entre actualizar y activar o inabilitar todas las casillas
            if (operacion == 0)
            {
                //txtIdentidad.IsReadOnly = opcion;
                txtNombreCompleto.IsReadOnly = opcion;
                txtEdad.IsReadOnly = opcion;
                cmbGenero.IsReadOnly = opcion;
                cmbCargo.IsReadOnly = opcion;
                txtSalario.IsReadOnly = opcion;
                txtDireccion.IsReadOnly = opcion;
            }
            else
            {
                txtNombreCompleto.IsReadOnly = opcion;
                txtEdad.IsReadOnly = opcion;
                cmbCargo.IsReadOnly = opcion;
                txtSalario.IsReadOnly = opcion;
                txtDireccion.IsReadOnly = opcion;
            }

        }
        // -----------------------------------------    POSIBLE CONEXION    --------------------------------------------- //
        // esto puede ir en conexiones
        private void MostrarEmpleado()
        {
            try
            {
                string query = @"exec mostrarEmpleado";

                sqlConnection.Open();


                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.ExecuteNonQuery();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable dataTable = new DataTable("Minas.Empleado");
                sqlDataAdapter.Fill(dataTable);
                DgvEmpleados.ItemsSource = dataTable.DefaultView;
                sqlDataAdapter.Update(dataTable);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        // -----------------------------------------    MOSTRAR INFORMACION    --------------------------------------------- //

        private void DgvEmpleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView filaSeleccionada = dg.SelectedItem as DataRowView;
            if (filaSeleccionada != null)
            {
                txtEmpleadoID.Text = filaSeleccionada["Empleado ID"].ToString();
                txtIdentidad.Text = filaSeleccionada["identidad"].ToString();
                txtNombreCompleto.Text = filaSeleccionada["Nombre Completo"].ToString();
                txtEdad.Text = filaSeleccionada["edad"].ToString();
                cmbGenero.Text = filaSeleccionada["Genero"].ToString();
                txtDireccion.Text = filaSeleccionada["direccion"].ToString();
                cmbCargo.Text = filaSeleccionada["cargo"].ToString();
                txtSalario.Text = filaSeleccionada["salario"].ToString();

                edicionDeCasillas(true, 0);
            }
        }
        private void MostrarCargos()
        {
            cmbCargo.ItemsSource = empleado.LlenarComboBoxCargo();
            cmbCargo.DisplayMemberPath = "NombreCargo";
            cmbCargo.SelectedValuePath = "Cargo";
        }
        private void MostrarBotonesPrincipales()
        {
            btnModificar.Visibility = Visibility.Visible;
            btnAgregar.Visibility = Visibility.Visible;
            btnEliminar.Visibility = Visibility.Visible;
            btnAceptarModificacion.Visibility = Visibility.Hidden;
            btnCancelarModificacion.Visibility = Visibility.Hidden;
            btnAceptarEliminacion.Visibility = Visibility.Hidden;
            btnCancelarEliminacion.Visibility = Visibility.Hidden;
            LimpiarCasillas();
            edicionDeCasillas(false, 0);
        }
        private void MostrarGeneros()
        {
            cmbGenero.ItemsSource = empleado.LlenarComboBoxGenero();
            cmbGenero.DisplayMemberPath = "NombreGenero";
            cmbGenero.SelectedValuePath = "Genero";

        }
        // -----------------------------------------    EVENTOS KEYDOWN    --------------------------------------------- //

        private void txtIdentidad_KeyDown(object sender, KeyEventArgs e)
        {
            validaciones.validarTxtSinLetras(e);
        }
        private void txtEdad_KeyDown(object sender, KeyEventArgs e)
        {
            validaciones.validarTxtSinLetras(e);
        }
        private void txtSalario_KeyDown(object sender, KeyEventArgs e)
        {
            validaciones.validarTxtSinLetrasConCommas(e);
        }
        private void txtNombreCompleto_KeyDown(object sender, KeyEventArgs e)
        {

            validaciones.validarTxtSinNumeros(e);
        }

        // -----------------------------------------    EVENTOS CLICK    --------------------------------------------- //

        private void btnCancelarModificacion_Click(object sender, RoutedEventArgs e)
        {
            MostrarBotonesPrincipales();
        }
        private void btnCancelarEliminacion_Click(object sender, RoutedEventArgs e)
        {
            MostrarBotonesPrincipales();
        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCasillas();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        // -----------------------------------------    EVENTOS MOUSEDOWN    --------------------------------------------- //
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        // -----------------------------------------    EVENTOS SELECTED (CAMBIOS DE FORMULARIOS)    --------------------------------------------- //

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
        private void ListViewItem_Selected_7(object sender, RoutedEventArgs e)
        {
            menuPrincipal sld = new menuPrincipal();
            sld.Show();
            this.Close();
        }
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
        // -----------------------------------------    EVENTOS TEXTCHANGED    --------------------------------------------- //
        private void txtNombreCompleto_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void txtSalario_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtSalario.MaxLength = 12;
            double Valor = 0;
            try
            {
                if (txtSalario.Text == string.Empty || (Convert.ToDouble(txtSalario.Text) == Valor))
                {
                    txtSalario.Text = "";
                }
                //else if (txtSalario.Text == string.Empty)
                //{
                //    MessageBoxResult result = MessageBox.Show("Debe seleccionar un mineral",
                //      "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    txtSalario.Text = "";
                //}
                else
                {
                    //double Total;
                    //double Cantidad, precio;
                    //Cantidad = Convert.ToDouble(txtSalario.Text);
                    //precio = Convert.ToDouble(txtSalario.Text);
                    //Total = Cantidad * precio;
                    //txtSalario.Text = Cantidad.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show("No puede ingresar dos puntos deguidos",
                      "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtSalario.Text = "";
            }
        }
        // -----------------------------------------    EVENTOS SELECTION CHANGED   --------------------------------------------- //
        private void cmbGenero_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}