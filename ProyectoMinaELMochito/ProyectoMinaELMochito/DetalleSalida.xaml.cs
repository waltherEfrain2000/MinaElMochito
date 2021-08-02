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
    /// Interaction logic for DetalleSalida.xaml
    /// </summary>
    public partial class DetalleSalida : Window
    {
        // Variable miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        Conexion conexion = new Conexion();
        DetalleSalidaSalidas detallesalidas = new DetalleSalidaSalidas();
        public DetalleSalida()
        {
            InitializeComponent();
            MostrarDetalleSalidas();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void DgDetalleSalida_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView filaSeleccionada = dg.SelectedItem as DataRowView;
            if (filaSeleccionada != null)
            {
                txtIdDetalle.Text = filaSeleccionada["código"].ToString();
               // txtDetalle.Text = filaSeleccionada["Detalle de salida"].ToString();

            }
        }
        private void infoFormulario(int operacion)
        {


            if (operacion == 1)
            {
                //salidas.IDsalida = Convert.ToInt32(txtIdSalida.Text);
                detallesalidas.idDetalle = Convert.ToInt32(txtIdDetalle);

            }
            //detallesalidas.idDetalle = Convert.ToInt32(txtIdDetalle);
            detallesalidas.DetalleSalida = txtDetalle.Text;

            //herramienta.HerramientasEnUso = Convert.ToInt32(txtCantidad.Text);

        }
    
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarCamposLlenos()) 
            {
                try
                {

                    infoFormulario(0);
                    detallesalidas.AgregarDetalleSalida(detallesalidas);

                    MessageBox.Show("Detalle de salida Ingresada","Datos Correctos");
                    MostrarDetalleSalidas();
                    Salidas sa = new Salidas();
                    sa.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar un detalle de salida","Datos Incorrectos",MessageBoxButton.OK,MessageBoxImage.Error );
                    Console.WriteLine(ex.Message);

                }
                finally
                {
                    
                    
                }
            }
           
        }
        private void MostrarDetalleSalidas()
        {
            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("LlenarDetalle", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.ExecuteNonQuery();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable dataTable = new DataTable("Salidas.DetalleSalida");
                sqlDataAdapter.Fill(dataTable);
                DgDetalleSalida.ItemsSource = dataTable.DefaultView;
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

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {

            Salidas de = new Salidas();
            de.Show();
            this.Close();

        }
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private bool VerificarCamposLlenos()
        {
            if ( txtDetalle.Text == string.Empty)
            {
                MessageBox.Show("Por favor ingresa todos los datos solicitados","Error",MessageBoxButton.OK,MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            Salidas de = new Salidas();
            de.Show();
            this.Close();
        }
    }
}
