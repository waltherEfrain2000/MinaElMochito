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
        public Cargos()
        {
            InitializeComponent();
            MostrarCargo();
        }

        private void txtnombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtnombre.MaxLength = 30;
        }

        private void dtgriduser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {  
                DataGrid dg = (DataGrid)sender;
                DataRowView filaSeleccionada = dg.SelectedItem as DataRowView;
                if (filaSeleccionada != null)
                {
                    txtcodigo.Text = filaSeleccionada["Código"].ToString();
                    txtnombre.Text = filaSeleccionada["Cargo"].ToString();
 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LimpiarCasillas()
        {
            txtcodigo.Text = string.Empty;
            txtnombre.Text = string.Empty;
        }

        private void ExtraerInformacionFormulario(int operacion)
        {
            //entra si va extraer informacion para actualizar
            if (operacion == 1)
            {
                cargo.CargoID = Convert.ToInt32(txtcodigo.Text);
            }

            cargo.NombreCargo = txtnombre.Text;

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
                try
                {
                    //parametro 0 por que no es actualizacion
                    ExtraerInformacionFormulario(0);
                    cargo.CrearCargo(cargo);

                    // Mensaje de inserción exitosa
                    MessageBox.Show("¡Cargo insertado exitosamente!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    LimpiarCasillas();
                    MostrarCargo();
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
                    ExtraerInformacionFormulario(1);
                    cargo.ActualizarCargo(cargo);


                    // Mensaje de inserción exitosa
                    MessageBox.Show("¡Cargo modificado exitosamente!");


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    LimpiarCasillas();
                    MostrarCargo();
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarCamposLlenos())
            {
                // Verificar que se ingresaron los valores requeridos
                if (VerificarCamposLlenos())
                {
                    try
                    {
                        //parametro 1 por que es actualizacion
                        ExtraerInformacionFormulario(1);
                        cargo.EliminarCargo(cargo);

                        // Mensaje de procedimiento exitosa
                        MessageBox.Show("¡Cargo Eliminado exitosamente!");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        LimpiarCasillas();
                        MostrarCargo();
                    }
                }
            }
        }


        //----------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------Validar------------------------------------------------------------------------------------
        private bool VerificarCamposLlenos()
        {
            //Validamos que el campo nombre del cargo no se encuentre vacio
            if (txtnombre.Text == string.Empty )
            {
                MessageBox.Show("Por favor ingresa los datos requeridos");
                return false;
            }
             

            return true;

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