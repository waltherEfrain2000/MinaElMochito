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

namespace ProyectoMinaELMochito
{
    /// <summary>
    /// Lógica de interacción para AsignarHerramientas.xaml
    /// </summary>
    public partial class AsignarHerramientas : Window
    {
        Herramienta herramienta = new Herramienta();
        public AsignarHerramientas()
        {
            InitializeComponent();
            MostrarHerramienta();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void DgvHerramientas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView filaSeleccionada = dg.SelectedItem as DataRowView;
            if (filaSeleccionada != null)
            {
                txtId.Text = filaSeleccionada["Código"].ToString();
            }
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            Herramientas herramientas = new Herramientas();
            herramientas.Show();
            this.Close();
        }

        private bool VerificarCamposLlenos()
        {
            if (txtId.Text == string.Empty || txtCantidad.Text == string.Empty)
            {
                MessageBox.Show("Por favor ingresa todos los valores (incluyendo la lista)");
                return false;
            }
            return true;
        }

        private void ExtraerInformacionFormulario(int operacion)
        {
            //entra si va extraer informacion para actualizar
            if (operacion == 1)
            {
                herramienta.ID = Convert.ToInt32(txtId.Text);
            }
            herramienta.ID = Convert.ToInt32(txtId.Text);
            herramienta.HerramientasEnUso = Convert.ToInt32(txtCantidad.Text);
        }

        private void LimpiarCasillas()
        {
            txtId.Text = string.Empty;
            txtCantidad.Text = string.Empty;

        }

        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        private void MostrarHerramienta()
        {
            try
            {

                string query = @"exec mostrarHerramientaEnUso";

                sqlConnection.Open();


                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.ExecuteNonQuery();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable dataTable = new DataTable("Minas.HeramientasyEquipos");
                sqlDataAdapter.Fill(dataTable);
                DgEquipamiento.ItemsSource = dataTable.DefaultView;
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
            if (VerificarCamposLlenos())
            {
                try
                {
                    //parametro 0 por que no es actualizacion
                    ExtraerInformacionFormulario(0);
                    herramienta.AsignarHerramienta(herramienta);

                    // Mensaje de inserción exitosa
                    MessageBox.Show("¡Herramienta insertada correctamente!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    LimpiarCasillas();
                    MostrarHerramienta();
                }
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
