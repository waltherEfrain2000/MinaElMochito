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
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.VehiculoConnection"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        private Vehiculo vehiculo = new Vehiculo();
        public Vehiculos()
        {
            InitializeComponent();
            MostrarVehiculo();
            cmbEstado.Items.Add("activo");
            cmbEstado.Items.Add("reparacion");
        }

        private void DgvVehiculos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView filaSeleccionada = dg.SelectedItem as DataRowView;
            if (filaSeleccionada != null)
            {
                txtVehiculoID.Text = filaSeleccionada["Vehiculo ID"].ToString();
                txtMarca.Text = filaSeleccionada["Marca"].ToString();
                txtModelo.Text = filaSeleccionada["Modelo"].ToString();
                txtPlaca.Text = filaSeleccionada["Placa"].ToString();
             
                txtColor.Text = filaSeleccionada["color"].ToString();
              

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
                vehiculo.VehiculoID = Convert.ToInt32(txtVehiculoID.Text);
            }

            vehiculo.Marca = txtMarca.Text;
            vehiculo.Modelo = txtModelo.Text;
            vehiculo.Placa = txtPlaca.Text;
            vehiculo.Color = txtColor.Text;


            switch (cmbEstado.SelectedIndex)
            {
                case 0:
                    vehiculo.Estado = 1;
                    break;
                case 1:
                    vehiculo.Estado = 2;
                    break;
                case 2:
                    vehiculo.Estado = 3;
                    break;
                default:
                    break;
            }
        }

        private void MostrarVehiculo()
        {
            try
            {
                string query = @"SELECT V.idVehiculo  AS 'Vehiculo ID',V.marca AS 'Marca', V.modelo AS 'Modelo',
                                V.placa AS 'Placa',V.color FROM  Minas.Vehiculo V
								where V.estado = 1";

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

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
                    MessageBox.Show("¡Empleado insertado correctamente!");

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
    }
}
