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
        public MarcaVehiculo()
        {
            InitializeComponent();
            MostrarVehiculo();
            botonfecha.Content = string.Format("{0}", DateTime.Now.ToString());
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
                cn.cerrar();
            }
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
                cn.cerrar();
                MostrarModelos(Convert.ToInt32(filaSeleccionada["Código Marca"]));
            }
        }

        private void LimpiarCasillas()
        {
            txtCodigoMarca.Text = string.Empty;
            txtMarca.Text = string.Empty;
        }

        private bool VerificarCamposLlenos()
        {
            if (txtMarca.Text == string.Empty)
            {
                MessageBox.Show("Por favor ingresa todos los valores en las cajas de texto");
                return false;
            }
            return true;
        }


        private Vehiculo elVehiculo = new Vehiculo();


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

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarCamposLlenos())
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
                    btnModificar.Visibility = Visibility.Hidden;
                    btnAgregar.Visibility = Visibility.Hidden;
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
                    vehiculo.ActualizarMarca(txtMarca.Text, Convert.ToInt32(txtCodigoMarca.Text));

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

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCasillas();
        }

        private void BotonesCancelar()
        {
            //mostar todos los botones necesarios
            btnModificar.Visibility = Visibility.Visible;
            btnAgregar.Visibility = Visibility.Visible;
            btnAceptarModificacion.Visibility = Visibility.Hidden;
            btnCancelarModificacion.Visibility = Visibility.Hidden;

            DgvVehiculos.SelectedItem = null;
        }

        private void btnCancelarModificacion_Click(object sender, RoutedEventArgs e)
        {
            BotonesCancelar();
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
