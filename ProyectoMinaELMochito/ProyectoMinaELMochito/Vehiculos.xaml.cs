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

namespace ProyectoMinaELMochito
{
    /// <summary>
    /// Lógica de interacción para Vehiculos.xaml
    /// </summary>
    public partial class Vehiculos : Window
    {
        // Variables miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        private Procedimientos vehiculo = new Procedimientos();
        private Vehiculo vehiculos = new Vehiculo();
        public Vehiculos()
        {
            InitializeComponent();
            MostrarVehiculo();
            MostrarEstados();
            botonfecha.Content = string.Format("{0}", DateTime.Now.ToString());
        }

        private void DgvVehiculos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView filaSeleccionada = dg.SelectedItem as DataRowView;
            if (filaSeleccionada != null)
            {
                txtVehiculoID.Text = filaSeleccionada["VehiculoID"].ToString();
                txtMarca.Text = filaSeleccionada["Marca"].ToString();
                txtModelo.Text = filaSeleccionada["Modelo"].ToString();
                txtPlaca.Text = filaSeleccionada["Placa"].ToString();
                txtColor.Text = filaSeleccionada["color"].ToString();
                cmbEstado.Text = filaSeleccionada["estado"].ToString();


                edicionDeCasillas(true, 0);
            }
        }
        private void edicionDeCasillas(bool opcion, int operacion)
        {
            //operacion sirve para distingir entre actualizar y activar o inabilitar todas las casillas
            if (operacion == 0)
            {
                txtMarca.IsReadOnly = opcion;
                txtModelo.IsReadOnly = opcion;
                txtPlaca.IsReadOnly = opcion;
                txtColor.IsReadOnly = opcion;
                cmbEstado.IsReadOnly = opcion;

            }
            else
            {
                txtColor.IsReadOnly = opcion;
                cmbEstado.IsReadOnly = opcion;
            }
        }
        private void LimpiarCasillas()
        {
            txtVehiculoID.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtModelo.Text = string.Empty;
            txtPlaca.Text = string.Empty;
            txtColor.Text = string.Empty;
            cmbEstado.SelectedValue = null;
            edicionDeCasillas(false, 0);
        }
        private bool VerificarCamposLlenos()
        {
            if (txtMarca.Text == string.Empty || txtModelo.Text == string.Empty || txtPlaca.Text == string.Empty || txtColor.Text == string.Empty)
            {
                MessageBox.Show("Por favor ingresa todos los valores en las cajas de texto");
                return false;
            }
            else if (cmbEstado.SelectedValue == null)
            {
                MessageBox.Show("Por favor selecciona el Estado del Vehiculo");
                return false;
            }
            return true;
        }
        private void ExtraerInformacionFormulario(int operacion)
        {
            //entra si va extraer informacion para actualizar
            if (operacion == 1)
            {
                vehiculos.VehiculoID = Convert.ToInt32(txtVehiculoID.Text);

            }

            vehiculos.Marca = txtMarca.Text;
            vehiculos.Modelo = txtModelo.Text;
            vehiculos.Placa = txtPlaca.Text;
            vehiculos.Color = txtColor.Text;


            switch (cmbEstado.SelectedIndex)
            {
                case 0:
                    vehiculos.Estado = 1;
                    break;
                case 1:
                    vehiculos.Estado = 2;
                    break;
                case 2:
                    vehiculos.Estado = 3;
                    break;
                default:
                    break;
            }
        }



        private void MostrarVehiculo()
        {
            try
            {


                string query = @"EXEC MostrarVehiculo @tipoMostrar";

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@tipoMostrar", 2);

                sqlCommand.ExecuteNonQuery();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable dataTable = new DataTable("Minas.Vehiculo");
                sqlDataAdapter.Fill(dataTable);
                DgvVehiculos.ItemsSource = dataTable.DefaultView;
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
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarCamposLlenos())
            {
                try
                {
                    //parametro 0 por que no es actualizacion
                    ExtraerInformacionFormulario(0);
                    vehiculo.CrearVehiculo(vehiculo);

                    // Mensaje de inserción exitosa
                    MessageBox.Show("¡Vehiculo insertado correctamente!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    LimpiarCasillas();
                    MostrarVehiculo();
                }
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarCamposLlenos())
            {
                try
                {
                    //parametro 1 por que es actualizacion
                    edicionDeCasillas(false, 1);
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
            if (VerificarCamposLlenos())
            {
                BotonesCancelar();

                try
                {
                    //parametro 1 por que es actualizacion
                    ExtraerInformacionFormulario(1);
                    vehiculo.ActualizarVehiculo(vehiculo);


                    // Mensaje de inserción exitosa
                    MessageBox.Show("¡Vehiculo modificado correctamente!");


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    LimpiarCasillas();
                    MostrarVehiculo();
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarCamposLlenos())
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
            if (VerificarCamposLlenos())
            {
                BotonesCancelar();
                try
                {
                    //parametro 1 por que es actualizacion
                    ExtraerInformacionFormulario(1);
                    vehiculo.EliminarVehiculo(vehiculo);

                    // Mensaje de inserción exitosa
                    MessageBox.Show("¡Vehiculo Eliminado correctamente!");


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    LimpiarCasillas();
                    MostrarVehiculo();
                }
            }

        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCasillas();
        }

        private void btnCancelarEliminacion_Click(object sender, RoutedEventArgs e)
        {
            BotonesCancelar();
        }



        private void BotonesCancelar()
        {
            //mostar todos los otones necesarios
            btnModificar.Visibility = Visibility.Visible;
            btnAgregar.Visibility = Visibility.Visible;
            btnEliminar.Visibility = Visibility.Visible;
            btnAceptarModificacion.Visibility = Visibility.Hidden;
            btnCancelarModificacion.Visibility = Visibility.Hidden;
            btnAceptarEliminacion.Visibility = Visibility.Hidden;
            btnCancelarEliminacion.Visibility = Visibility.Hidden;
            edicionDeCasillas(false, 0);
            DgvVehiculos.SelectedItem = null;
        }

        private void btnCancelarModificacion_Click(object sender, RoutedEventArgs e)
        {
            BotonesCancelar();
        }
        private void MostrarEstados()
        {
            cmbEstado.ItemsSource = vehiculos.LlenarComboBoxEstados();
            cmbEstado.DisplayMemberPath = "NombreEstado";
            cmbEstado.SelectedValuePath = "Estado";

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

        private void ListViewItem_Selected_7(object sender, RoutedEventArgs e)
        {
            menuPrincipal sld = new menuPrincipal();
            sld.Show();
            this.Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
