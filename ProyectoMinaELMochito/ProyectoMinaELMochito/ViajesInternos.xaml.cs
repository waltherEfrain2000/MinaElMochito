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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ProyectoMinaELMochito
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class ViajesInternos : Window
    {
        //Variables Miembro
        private ViajeInterno viajeinterno = new ViajeInterno();

        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        /// <summary>
        /// Muestra la tabla en donde se encuentran los empleados
        /// de la mina, de aquí el usuario seleccionará uno para el viaje
        /// </summary>
        private void CargarDatos(object sender, RoutedEventArgs e)
        {
            try
            {
                //Query para seleccionar los datos de la tabla
                String queryEmpleado = @"Select IdEmpleado as 'Id Empleado', identidad as 'Identidad', 
                               primerNombre as 'Nombre Empleado'
                               from Minas.Empleado";

                String queryVehiculo = @"Select idVehiculo as 'Id Vehiculo', marca as 'Marca', 
                                        modelo as 'Modelo', placa as 'Placa', color as 'Color'
                                         from Minas.Vehiculo";

                //Establecer la conexion
                sqlConnection.Open();

                // Crear el comando SQL tanto como para el query de emplados y de vehículos
                SqlCommand sqlCommand1 = new SqlCommand(queryEmpleado, sqlConnection);
                SqlCommand sqlCommand2 = new SqlCommand(queryVehiculo, sqlConnection);

                //Se crea el sqlCommand para poder ejecuatar cada quuery
                sqlCommand1.ExecuteNonQuery();
                sqlCommand2.ExecuteNonQuery();

                //Crear el comando SQL
                SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(sqlCommand1);
                SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(sqlCommand2);

                //Crear el dataTable que contendrá las tablas desde la base
                DataTable dataTable1 = new DataTable("Minas.Empleado");
                DataTable dataTable2 = new DataTable("Minas.Vehiculo");

                //Llenar los datagrid con la información necesaria
                sqlDataAdapter1.Fill(dataTable1);
                dgvEmpleados.ItemsSource = dataTable1.DefaultView;
                sqlDataAdapter1.Update(dataTable1);

                sqlDataAdapter2.Fill(dataTable2);
                dgvVehiculos.ItemsSource = dataTable2.DefaultView;
                sqlDataAdapter2.Update(dataTable2);
              
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

        //Limpiar las cajas de texto
        private void LimpiarCasillas()
        {
            txtIdEmpleado.Text = string.Empty;
            txtIdVehiculo.Text = string.Empty;
            txtnombreEmpleado.Text = string.Empty;
            txtVehiculo.Text = string.Empty;
        }

        //Obtener los datos de las cajas de texto
        private void ObtenerDatos()
        {
            viajeinterno.IdEmpleado = Convert.ToInt32(txtIdEmpleado.Text);
            viajeinterno.IdVehiculo = Convert.ToInt32(txtIdVehiculo.Text);
        }

        //Valores formulario desde el objeto
        private void ValoresFormularioObjeto()
        {
            txtIdEmpleado.Text = viajeinterno.IdEmpleado.ToString();
            txtIdVehiculo.Text = viajeinterno.IdVehiculo.ToString();
            txtnombreEmpleado.Text = viajeinterno.NombreEmpleado;
            txtVehiculo.Text = viajeinterno.Vehiculo;
        }

        //Vamos a verificar que todas las casillas estén llenas
        private bool VerificacionCasillas()
        {
            if (txtIdEmpleado.Text == string.Empty || txtIdVehiculo.Text == string.Empty)
            {
                MessageBoxResult result = MessageBox.Show("Por favor!, Verifique que las casillas contengan la infromación requerida!",
                    "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //Si hay valores en las casillas entonces se retornará verdadero
            return true;
        }

        /// <summary>
        /// En el momento en que el usuario decida presionar el botón siguiente 
        /// automáticamente se agregarán los datos en la tabla de viajeInterno para
        /// así guardar el registro de quién y en qué vehículo se exportará el material
        /// </summary>
        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            //Primero verificamos que las casillas no estén vacías
            if (VerificacionCasillas())
            {
                try
                {
                    //Vamos a obtener los valores ingresados para la tabla
                    ObtenerDatos();

                    //Insertamos los valores en la tabla
                    viajeinterno.AgregarDatosAViajeInterno(viajeinterno);

                    //Si los datos se intertarón mostrar un mensje
                    MessageBox.Show("Los datos han sido ingresados correctamente!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar los datos...");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    LimpiarCasillas();
                }
            }
        }

        /// <summary>
        /// Este método va a obtener las propiedades del datagrid
        /// y las va a pasar a los textBox
        /// </summary>
        private void ObtienePropiedades(ViajeInterno viajeInterno)
        {
            this.txtIdEmpleado.Text = Convert.ToString(viajeinterno.IdEmpleado);
            this.txtnombreEmpleado.Text = Convert.ToString(viajeinterno.NombreEmpleado);
            this.txtIdVehiculo.Text = Convert.ToString(viajeInterno.IdVehiculo);
            this.txtVehiculo.Text = Convert.ToString(viajeInterno.Vehiculo);
        }

        private void MostarEnTexBoxContenido(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            DataRowView row_selected = dataGrid.SelectedItem as DataRowView;

            //Validar que realmente se esta seleccionando un elemento del datagrid
            if (row_selected != null)
            {
                txtIdEmpleado.Text = row_selected["Id Empleado"].ToString();
                txtnombreEmpleado.Text = row_selected["Nombre Empleado"].ToString();
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila del datagrid");
            }
        }

        private void MostrarVehiculosTextBox(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            DataRowView row_selected = dataGrid.SelectedItem as DataRowView;

            //Validar que realmente se esta seleccionando un elemento del datagrid
            if (row_selected != null)
            {
                txtIdVehiculo.Text = row_selected["Id Vehiculo"].ToString();
                txtVehiculo.Text = row_selected["Marca"].ToString();
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila del datagrid");
            }
        }

        /// <summary>
        /// Este evento permitirá que solo se pueda insertar datos de tipo 
        /// númerico en el el textBox de Empleados
        /// </summary>
        private void SoloNumeros(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        /// <summary>
        /// Este evento permitirá que solo se pueda insertar datos de tipo 
        /// númerico en el el textBox de Vehiculos
        /// </summary>
        private void PermiteSoloNumeros(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
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

    }


}
