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

using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Diagnostics;

namespace ProyectoMinaELMochito
{
    /// <summary>
    /// Lógica de interacción para ViajeInternoEmp.xaml
    /// </summary>
    public partial class ViajeInternoEmp : Window
    {
        //Variables Miembro
        private ViajeInterno viajeinterno = new ViajeInterno();

        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        public ViajeInternoEmp()
        {
            InitializeComponent();
            CargarDatos();
            botonfecha.Content = string.Format("{0}", DateTime.Now.ToString());
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CargarDatos()
        {
            try
            {
                //Query para seleccionar los datos de la tabla
                String queryEmpleado = @"execute CargarDatosEmpleado";

                String queryVehiculo = @"execute CargarDatosVehiculo";

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
                DataTable dataTable1 = new DataTable("Empleado.Empleado");
                DataTable dataTable2 = new DataTable("Vehiculos.Vehiculo");

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

                    ProduccionEmp sld = new ProduccionEmp();
                    sld.Show();
                    this.Close();
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
                txtIdEmpleado.Text = row_selected["Código"].ToString();
                txtnombreEmpleado.Text = row_selected["Nombre"].ToString();
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
                txtIdVehiculo.Text = row_selected["Código"].ToString();
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
            ViajeInternoEmp sld = new ViajeInternoEmp();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_2(object sender, RoutedEventArgs e)
        {
            Login sld = new Login();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_3(object sender, RoutedEventArgs e)
        {
            EntradasHistoricas sld = new EntradasHistoricas();
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
            menuEmpleado sld = new menuEmpleado();
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

        private void botonfecha_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ListViewItem_Selected_4(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("https://waltherefrain2000.github.io/WebMinaMochito/AyudaProduccionMain/VProduccion.html");

            }
            catch (Exception)
            {

                MessageBox.Show("Error, no se ha encontrado la página...", "Error", MessageBoxButton.OK);
            }
        }
    }
}
