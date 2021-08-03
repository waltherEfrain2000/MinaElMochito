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
        conexion cn = new conexion();
        int idVehiculo;
        string placaSeleccionada;

        private Procedimientos vehiculo = new Procedimientos();
        private Vehiculo vehiculos = new Vehiculo();
        private Validaciones validacion = new Validaciones();
        public Vehiculos()
        {
            InitializeComponent();
            MostrarVehiculo();
            MostrarComboBoxes();
            botonfecha.Content = string.Format("{0}", DateTime.Now.ToString());
            txtPlaca.MaxLength = 15;
        }

        private void DgvVehiculos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cn.abrir();
            DataGrid dg = (DataGrid)sender;
            DataRowView filaSeleccionada = dg.SelectedItem as DataRowView;
            if (filaSeleccionada != null)
            {
                txtVehiculoID.Text = filaSeleccionada["Código Vehículo"].ToString();
                idVehiculo = Convert.ToInt32(filaSeleccionada["Código Vehículo"]);
                txtPlaca.Text = filaSeleccionada["Placa"].ToString();
                placaSeleccionada = filaSeleccionada["Placa"].ToString();
                txtColor.Text = filaSeleccionada["Color"].ToString();
                cmbEstado.Text = filaSeleccionada["Estado"].ToString();
                cmbMarca.Text = filaSeleccionada["Marca"].ToString();
                cmbModelo.Text = filaSeleccionada["Modelo"].ToString();

                cn.cerrar();
            }
        }
        private void LimpiarCasillas()
        {
            txtVehiculoID.Text = string.Empty;
            txtPlaca.Text = string.Empty;
            txtColor.Text = string.Empty;
            cmbEstado.SelectedValue = null;
            cmbModelo.SelectedValue = null;
            cmbMarca.SelectedValue = null;
            colorPicker.SelectedColor = null;
        }
        private bool VerificarCamposLlenos()
        {
            if (txtPlaca.Text == string.Empty || txtColor.Text == string.Empty)
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


                string query = @"EXEC AdministrarVehiculo @tipoConsulta, @idVehiculo, @idModelo, @idMarca, @placa, @color, @estado";

                cn.abrir();

                SqlCommand sqlCommand = new SqlCommand(query, cn.Conectarbd);
                sqlCommand.Parameters.AddWithValue("@tipoConsulta", 1);
                sqlCommand.Parameters.AddWithValue("@idVehiculo", 1);
                sqlCommand.Parameters.AddWithValue("@idModelo", 1);
                sqlCommand.Parameters.AddWithValue("@idMarca", 1);
                sqlCommand.Parameters.AddWithValue("@placa", 1);
                sqlCommand.Parameters.AddWithValue("@color", 1);
                sqlCommand.Parameters.AddWithValue("@estado", 1);

                sqlCommand.ExecuteNonQuery();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable dataTable = new DataTable("Vehiculos.Vehiculo");
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
                cn.cerrar();
            }
        }
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarCamposLlenos())
            {

                int verificarPlaca = validacion.validacionesVehiculos(2, txtPlaca.Text, 1);

                if (verificarPlaca == 0)
                {

                    try
                    {
                        //parametro 0 por que no es actualizacion
                        ExtraerInformacionFormulario(0);
                        vehiculo.CrearVehiculo(Convert.ToInt32(cmbModelo.SelectedValue), Convert.ToInt32(cmbMarca.SelectedValue), txtPlaca.Text, txtColor.Text, Convert.ToInt32(cmbEstado.SelectedValue));

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
                else { MessageBox.Show("No se puede ingresar una placa repetida."); }
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
            int verificarPlaca = validacion.validacionesVehiculos(2, txtPlaca.Text, 1);

            if (txtPlaca.Text != placaSeleccionada)
            {
                if (verificarPlaca == 0)
                {
                    ProcesoModificar();
                }
                else { MessageBox.Show("No se puede ingresar una placa repetida."); }
            }
            else { ProcesoModificar(); }            
        }
        

        private void ProcesoModificar()
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarCamposLlenos())
            {
                BotonesCancelar();

                try
                {
                    //parametro 1 por que es actualizacion
                    ExtraerInformacionFormulario(1);
                    vehiculo.ActualizarVehiculo(idVehiculo, Convert.ToInt32(cmbModelo.SelectedValue), Convert.ToInt32(cmbMarca.SelectedValue), txtPlaca.Text, txtColor.Text, Convert.ToInt32(cmbEstado.SelectedValue));


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
                    //ocultar todos los botones inecesarios
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
                    vehiculo.EliminarVehiculo(idVehiculo);

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
            //mostar todos los Botones necesarios
            btnModificar.Visibility = Visibility.Visible;
            btnAgregar.Visibility = Visibility.Visible;
            btnEliminar.Visibility = Visibility.Visible;
            btnAceptarModificacion.Visibility = Visibility.Hidden;
            btnCancelarModificacion.Visibility = Visibility.Hidden;
            btnAceptarEliminacion.Visibility = Visibility.Hidden;
            btnCancelarEliminacion.Visibility = Visibility.Hidden;
            //edicionDeCasillas(false, 0);
            DgvVehiculos.SelectedItem = null;
        }

        private void btnCancelarModificacion_Click(object sender, RoutedEventArgs e)
        {
            BotonesCancelar();
        }
        private void MostrarComboBoxes()
        {
            cmbEstado.ItemsSource = vehiculos.LlenarComboBoxEstados();
            cmbEstado.DisplayMemberPath = "NombreEstado";
            cmbEstado.SelectedValuePath = "Estado";
            /*-------------------------------------------------------*/
            cmbModelo.ItemsSource = vehiculos.LlenarComboBoxModelos(Convert.ToInt32(cmbMarca.SelectedValue));
            cmbModelo.DisplayMemberPath = "Modelo";
            cmbModelo.SelectedValuePath = "idModelo";
            /*-------------------------------------------------------*/
            Marcas marcas = new Marcas();
            cmbMarca.ItemsSource = marcas.LlenarComboBoxMarcas();
            cmbMarca.DisplayMemberPath = "NombreMarca";
            cmbMarca.SelectedValuePath = "CodigoMarca";
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

        private void btnModelosMarcas_Click(object sender, RoutedEventArgs e)
        {
            MarcaVehiculo sld = new MarcaVehiculo();
            sld.Show();
            this.Close();
        }

        private void cmbMarca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbModelo.ItemsSource = vehiculos.LlenarComboBoxModelos(Convert.ToInt32(cmbMarca.SelectedValue));
            cmbModelo.DisplayMemberPath = "Modelo";
            cmbModelo.SelectedValuePath = "idModelo";
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            try
            {
                txtColor.Text = colorPicker.SelectedColorText;
                squareText.Fill = new BrushConverter().ConvertFromString(txtColor.Text) as SolidColorBrush;
            }
            catch (Exception)
            {

            }
        }

        private void txtColor_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                colorPicker.SelectedColor = (Color)ColorConverter.ConvertFromString(txtColor.Text);
            }
            catch (Exception)
            {

            }
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

        private void ListViewItem_Selected_9(object sender, RoutedEventArgs e)
        {
            ViajesInternos sld = new ViajesInternos();
            sld.Show();
            this.Close();
        }
    }
}
