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
    public partial class MarcaVehiculo : Window
    {
        // Variables miembro
        Conexion cn = new Conexion();

        private Procedimientos vehiculo = new Procedimientos();
        private Validaciones validacion = new Validaciones();
        string nombreMarca, nombreModelo;

        public MarcaVehiculo()
        {
            InitializeComponent();
            MostrarVehiculo();
            botonfecha.Content = string.Format("{0}", DateTime.Now.ToString());
            LlenarModelos();
        }

        private void DgvModelos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cn.abrir();
            DataGrid dg = (DataGrid)sender;
            DataRowView filaSeleccionada = dg.SelectedItem as DataRowView;
            if (filaSeleccionada != null)
            {
                txtCodigoModelo.Text = filaSeleccionada["Código Modelo"].ToString();
                txtModelo.Text = filaSeleccionada["Modelo"].ToString();
                cmbMarca.Text = (filaSeleccionada["Marca"].ToString());
            }
            cn.cerrar();
        }

        private void DgvVehiculos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cn.abrir();
            DataGrid dg = (DataGrid)sender;
            DataRowView filaSeleccionada = dg.SelectedItem as DataRowView;
            if (filaSeleccionada != null)
            {
                txtCodigoMarca.Text = filaSeleccionada["Código Marca"].ToString();
                txtMarca.Text = filaSeleccionada["Marca"].ToString();
                nombreMarca = filaSeleccionada["Marca"].ToString();
                MostrarModelos(Convert.ToInt32(filaSeleccionada["Código Marca"]));
                cmbMarca.SelectedValue = Convert.ToInt32(filaSeleccionada["Código Marca"]);
            }
            cn.cerrar();
        }

        private void LimpiarCasillas(int tipoLimpiar)
        {
            if (tipoLimpiar == 1)
            {
                txtCodigoMarca.Text = string.Empty;
                txtMarca.Text = string.Empty;
            }

            else
            {
                txtModelo.Text = string.Empty;
                txtCodigoModelo.Text = string.Empty;
                cmbMarca.SelectedItem = null;
            }
        }

        private bool VerificarCamposLlenos(int tipoVerificado)
        {

            if (tipoVerificado == 1)
            {
                if (txtMarca.Text == string.Empty)
                {
                    MessageBox.Show("Por favor ingresa todos los valores en las cajas de texto");
                    return false;
                }
                return true;
            }
            else {
                if (txtModelo.Text == string.Empty || cmbMarca.SelectedItem == null)
                {
                    MessageBox.Show("Por favor ingresa todos los valores en las cajas de texto");
                    return false;
                }
                return true;
            }
        }

        private void LlenarModelos()
        {
            Marcas marcas = new Marcas();
            cmbMarca.ItemsSource = marcas.LlenarComboBoxMarcas();
            cmbMarca.DisplayMemberPath = "NombreMarca";
            cmbMarca.SelectedValuePath = "CodigoMarca";
        }


        private void MostrarModelos(int codigoMarca)
        {
            try
            {
                string query = @"EXEC AdministrarModelo @TipoConsulta, @idMarca, @idModelo, @nombreModelo";

                cn.abrir();
                SqlCommand sqlCommand = new SqlCommand(query, cn.Conectarbd);
                sqlCommand.Parameters.AddWithValue("@TipoConsulta", 2);
                sqlCommand.Parameters.AddWithValue("@idMarca", codigoMarca);
                sqlCommand.Parameters.AddWithValue("@idModelo", 1);
                sqlCommand.Parameters.AddWithValue("@nombreModelo", 1);

                sqlCommand.ExecuteNonQuery();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable dataTable = new DataTable("Vehiculos.ModeloVehiculo");
                sqlDataAdapter.Fill(dataTable);
                DgvModelos.ItemsSource = dataTable.DefaultView;
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


        private void MostrarVehiculo()
        {
            try
            {
                string query = @"EXEC AdministrarMarca @tipoConsulta, @idMarca, @nombreMarca";

                cn.abrir();
                SqlCommand sqlCommand = new SqlCommand(query, cn.Conectarbd);
                sqlCommand.Parameters.AddWithValue("@TipoConsulta", 2);
                sqlCommand.Parameters.AddWithValue("@idMarca", 1);
                sqlCommand.Parameters.AddWithValue("@nombreMarca", 1);

                sqlCommand.ExecuteNonQuery();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable dataTable = new DataTable("Vehiculos.MarcaVehiculo");
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

        private void btnAgregarModelo_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarCamposLlenos(2))
            {
                int verificarModelo = validacion.validacionesVehiculos(3, txtModelo.Text, Convert.ToInt32(cmbMarca.SelectedValue));
                if (verificarModelo == 0)
                {
                    try
                    {
                        vehiculo.CrearModelo(txtModelo.Text, Convert.ToInt32(cmbMarca.SelectedValue));
                        // Mensaje de inserción exitosa
                        MessageBox.Show("¡Modelo registrado correctamente!");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        MostrarModelos(Convert.ToInt32(cmbMarca.SelectedValue));
                        LlenarModelos();
                        LimpiarCasillas(2);
                    }
                }
                else { MessageBox.Show("Ya existe un modelo con el mismo nombre en esta marca."); }
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarCamposLlenos(1))
            {
                int verificarMarca = validacion.validacionesVehiculos(1, txtMarca.Text, 1);
                if (verificarMarca == 0)
                {
                    try
                    {
                        vehiculo.CrearMarca(txtMarca.Text);

                        // Mensaje de inserción exitosa
                        MessageBox.Show("¡Marca registrada correctamente!");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        LimpiarCasillas(1);
                        MostrarVehiculo();
                        LlenarModelos();
                    }
                }
                else { MessageBox.Show("Ya existe una marca con ese nombre."); }
            }
        }

        private void btnModificarModelo_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarCamposLlenos(2))
            {
                try
                {
                    btnAgregarModelo.Visibility = Visibility.Hidden;
                    btnModificarModelo.Visibility = Visibility.Hidden;
                    btnAceptarModelo.Visibility = Visibility.Visible;
                    btnCancelarModelo.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarCamposLlenos(1))
            {
                try
                {
                    btnModificar.Visibility = Visibility.Hidden;
                    btnAgregar.Visibility = Visibility.Hidden;
                    btnAceptarModificacion.Visibility = Visibility.Visible;
                    btnCancelarModificacion.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    LlenarModelos();
                }
            }
        }

        private void btnAceptarModelo_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarCamposLlenos(2))
            {
                int verificarModelo = validacion.validacionesVehiculos(3, txtModelo.Text, Convert.ToInt32(cmbMarca.SelectedValue));
                BotonesCancelar(2);

                if (txtModelo.Text != nombreModelo)
                {
                    if (verificarModelo == 0)
                    {
                        ProcesoModificarModelo();
                    }
                    else { MessageBox.Show("Ya existe un modelo con el mismo nombre en esta marca."); }
                }
                else { ProcesoModificarModelo(); }
            }
        }

        private void ProcesoModificarModelo()
        {
            try
            {
                vehiculo.ActualizarModelo(Convert.ToInt32(txtCodigoModelo.Text), Convert.ToInt32(cmbMarca.SelectedValue), txtModelo.Text);

                // Mensaje de inserción exitosa
                MessageBox.Show("¡Modelo modificado correctamente!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                MostrarVehiculo();
                MostrarModelos(Convert.ToInt32(cmbMarca.SelectedValue));
                LimpiarCasillas(2);
            }
        }


            private void btnAceptarModificacion_Click(object sender, RoutedEventArgs e)
            {
                // Verificar que se ingresaron los valores requeridos
                if (VerificarCamposLlenos(1))
                {
                int verificarMarca = validacion.validacionesVehiculos(1, txtMarca.Text, 1);
                BotonesCancelar(1);

                if (txtMarca.Text != nombreMarca)
                {
                    if (verificarMarca == 0)
                    {
                        ProcesoModificarMarca();
                    }
                    else { MessageBox.Show("Ya existe una marca con ese nombre."); }
                }
                else { ProcesoModificarMarca(); }
            }
            }

        private void ProcesoModificarMarca()
        {
            try
            {
                vehiculo.ActualizarMarca(txtMarca.Text, Convert.ToInt32(txtCodigoMarca.Text));

                // Mensaje de inserción exitosa
                MessageBox.Show("¡Marca modificada correctamente!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MostrarVehiculo();
                LlenarModelos();
                MostrarModelos(Convert.ToInt32(txtCodigoMarca.Text));
                LimpiarCasillas(1);
            }
        }

            private void btnLimpiar_Click(object sender, RoutedEventArgs e)
            {
                LimpiarCasillas(1);
            }

            private void BotonesCancelar(int tipoCancelar)
            {
                if (tipoCancelar == 1)
                {
                    btnModificar.Visibility = Visibility.Visible;
                    btnAgregar.Visibility = Visibility.Visible;
                    btnAceptarModificacion.Visibility = Visibility.Hidden;
                    btnCancelarModificacion.Visibility = Visibility.Hidden;
                    DgvVehiculos.SelectedItem = null;
                }
                else {
                    btnModificarModelo.Visibility = Visibility.Visible;
                    btnAgregarModelo.Visibility = Visibility.Visible;
                    btnAceptarModelo.Visibility = Visibility.Hidden;
                    btnCancelarModelo.Visibility = Visibility.Hidden;
                    DgvModelos.SelectedItem = null;
                }
            }


            private void btnCancelarModelo_Click(object sender, RoutedEventArgs e)
            {
                BotonesCancelar(2);
            }

            private void btnCancelarModificacion_Click(object sender, RoutedEventArgs e)
            {
                BotonesCancelar(1);
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

            private void btnLimpiarModelo_Click(object sender, RoutedEventArgs e)
            {
                LimpiarCasillas(2);
            }

        }
    }
