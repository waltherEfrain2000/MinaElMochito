using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    /// Lógica de interacción para Cargos.xaml
    /// </summary>
    public partial class Cargos : Window
    {
        // Variables miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);


        private Cargocrud cargo = new Cargocrud();
        private int opcion;

        public Cargos()
        {
            InitializeComponent();
            MostrarCargo();
        }

        private void txtnombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtnombre.MaxLength = 30;
        }



        private void LimpiarCasillas()
        {
            txtcodigo.Text = string.Empty;
            txtnombre.Text = string.Empty;
            cmbestado.Text = null;
            btnAceptareliminar.Visibility = Visibility.Hidden;
            btnAceptarM.Visibility = Visibility.Hidden;
            btnCancelar.Visibility = Visibility.Hidden;
            btnIngresar.Visibility = Visibility.Visible;
            btnModificar.Visibility = Visibility.Visible;
            btnEliminar.Visibility = Visibility.Visible;
        }

        string verificacioncargo;
        private void dtgridcargo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dg = (DataGrid)sender;
                DataRowView filaSeleccionada = dg.SelectedItem as DataRowView;
                if (filaSeleccionada != null)
                {
                    txtcodigo.Text = filaSeleccionada["Código"].ToString();
                    txtnombre.Text = filaSeleccionada["Cargo"].ToString();
                    verificacioncargo = filaSeleccionada["Cargo"].ToString();
                    cmbestado.Text = filaSeleccionada["Estado"].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------Validar------------------------------------------------------------------------------------
        private bool VerificarCamposLlenos()
        {
            //Validamos que el campo nombre del cargo no se encuentre vacio

            if (txtnombre.Text == string.Empty || cmbestado.SelectedItem == null)
            {
                MessageBox.Show("Por favor ingresar los datos requeridos\n", "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;

        }


        private void ExtraerInformacionFormularioestado(int operacion)
        {
            //entra si va extraer informacion para actualizar
            if (operacion == 1)
            {
                cargo.CargoID = Convert.ToInt32(txtcodigo.Text);
            }

            cargo.Estado = cmbestado.Text;
        }




        private void ExtraerInformacionFormulario(int operacion)
        {
            //entra si va extraer informacion para actualizar
            if (operacion == 1)
            {
                cargo.CargoID = Convert.ToInt32(txtcodigo.Text);
            }

            cargo.NombreCargo = txtnombre.Text;
            cargo.Estado = cmbestado.Text;

        }

        private void MostrarCargo()
        {
            try
            {

                string query = @"exec MostrarCargos";

                sqlConnection.Open();


                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.ExecuteNonQuery();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable dataTable = new DataTable("Empleados.cargo");
                sqlDataAdapter.Fill(dataTable);
                dtgridcargo.ItemsSource = dataTable.DefaultView;
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

        //----------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------BOTONES------------------------------------------------------------------------------------

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarCamposLlenos())
            {
                int verificarcargo = cargo.Cargorepetido(txtnombre.Text);

                if (verificarcargo == 0)
                {
                    try
                    {
                        //parametro 0 por que no es actualizacion
                        ExtraerInformacionFormulario(0);
                        cargo.CrearCargo(cargo);

                        // Mensaje de inserción exitosa
                        MessageBox.Show("¡El cargo ha sido resgistrado exitosamente!");

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("El cargo no se puede ingresar", "¡Error!", MessageBoxButton.OK);

                    }
                    finally
                    {
                        LimpiarCasillas();
                        MostrarCargo();
                    }
                }
                else
                {
                    MessageBox.Show("El cargo ya existe, por favor ingrese otro", "¡Error!", MessageBoxButton.OK);
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
                    btnIngresar.Visibility = Visibility.Hidden;
                    btnAceptarM.Visibility = Visibility.Visible;
                    btnCancelar.Visibility = Visibility.Visible;

                    opcion = 1;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }


            }
        }

        private bool validacionbteliminar()
        {
            if (txtnombre.Text == string.Empty)
            {
                MessageBox.Show("Por favor ingresar los datos requeridos\n", "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;

        }


        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            // Verificar que se ingresaron los valores requeridos
            if (validacionbteliminar())
            {
                try
                {
                    btnModificar.Visibility = Visibility.Hidden;
                    btnIngresar.Visibility = Visibility.Hidden;
                    btnAceptareliminar.Visibility = Visibility.Visible;
                    btnCancelar.Visibility = Visibility.Visible;

                    opcion = 2;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }

        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            int verificarcargo = cargo.Cargorepetido(txtnombre.Text);
            //txtnombre.Text = verificarcargo
            try
            {
                if (txtnombre.Text == verificacioncargo)
                {
                    if (VerificarCamposLlenos())
                    {
                        ExtraerInformacionFormularioestado(1);
                        cargo.ActualizarCargo(cargo);
                        MessageBox.Show("¡El cargo ha sido modificado exitosamente!", "Cargo Modificado", MessageBoxButton.OK, MessageBoxImage.Information);
                        LimpiarCasillas();
                    }

                }
                else
                {
                    if (verificarcargo == 0 && VerificarCamposLlenos())
                    {
                        //parametro 1 por que es actualizacion
                        ExtraerInformacionFormulario(1);
                        cargo.ActualizarCargo(cargo);

                        // Mensaje de inserción exitosa
                        MessageBox.Show("¡El cargo ha sido modificado exitosamente!", "Cargo Modificado", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Este cargo ya existe ", "¡Error!", MessageBoxButton.OK);
                    }
                }



            }
            catch (Exception)
            {
                MessageBox.Show("Al modificar el cargo", "¡Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LimpiarCasillas();
                MostrarCargo();
            }



        }



        private void btnAceptareliminar_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (opcion == 2 && validacionbteliminar())
                {
                    //parametro 1 por que es actualizacion
                    ExtraerInformacionFormulario(1);
                    cargo.EliminarCargo(cargo);

                    // Mensaje de inserción exitosa
                    MessageBox.Show("¡El cargo ha sido eliminado exitosamente!");
                }
                MostrarCargo();
                LimpiarCasillas();
            }
            catch (Exception)
            {
                MessageBox.Show("Al eliminar la acción", "¡Error!", MessageBoxButton.OK);
            }
            finally
            {
                btnModificar.Visibility = Visibility.Visible;
                btnIngresar.Visibility = Visibility.Visible;
                btnAceptareliminar.Visibility = Visibility.Hidden;
                btnCancelar.Visibility = Visibility.Hidden;
                LimpiarCasillas();
                MostrarCargo();
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

            btnModificar.Visibility = Visibility.Visible;
            btnIngresar.Visibility = Visibility.Visible;
            btnAceptarM.Visibility = Visibility.Hidden;
            btnCancelar.Visibility = Visibility.Hidden;
            dtgridcargo.SelectedItem = null;
            LimpiarCasillas();
        }


        private void btnlimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCasillas();
        }




        //--------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            menuPrincipal sld = new menuPrincipal();
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

        private void ListViewItem_Selected_8(object sender, RoutedEventArgs e)
        {
            Login sld = new Login();
            sld.Show();
            this.Close();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void txtnombre_KeyDown(object sender, KeyEventArgs e)
        {
            //Validacion.validarTxtSinNumeros(e);
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.DbeAlphanumeric || e.Key == Key.Decimal || e.Key == Key.Divide ||
                e.Key >= Key.OemComma || e.Key == Key.OemPeriod)
                e.Handled = true;
            else
                e.Handled = false;


        }
    }
}