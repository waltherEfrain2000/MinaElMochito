
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
// librerias sql
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
//libreria para error de decimales
using System.Globalization;
//libreria para validar caracteres especiales
using System.Text.RegularExpressions;

namespace ProyectoMinaELMochito
{
    /// <summary>
    /// Lógica de interacción para Salidas.xaml
    /// </summary>
    public partial class Salidas : Window
    {
        // Variable miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        private Validaciones validaciones = new Validaciones();
        private Procedimientos procedimientos = new Procedimientos();
        private Salida salidas = new Salida();


        public Salidas()
        {
            //botonfecha.Content = string.Format("{0}", DateTime.Now.ToString());
            InitializeComponent();
            MostrarMinerales();
            MostrarDetalleSalidas();
             MostrarInfoSalidas();
           
        }

        private void limpiarTexto()
        {
            txtIdSalida.Text = string.Empty;
            cmbIdMineral.SelectedValue = null;
            txtCantidad.Text = string.Empty;
            FSalida.Text = string.Empty;
            cmbSalidas.SelectedValue = null;
          

            //edicionDeCasillas(false, 0);
        }


        private void MostrarInfoSalidas()
        {
            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("MostrarInfoSalidas", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.ExecuteNonQuery();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable dataTable = new DataTable("Salidas.Salida");
                sqlDataAdapter.Fill(dataTable);
                dgvSalida.ItemsSource = dataTable.DefaultView;
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


        private void infoFormulario(int operacion)
        {


            if (operacion == 1)
            {
                //salidas.IDsalida = Convert.ToInt32(txtIdSalida.Text);
                salidas.IDsalida = Convert.ToInt32(txtIdSalida.Text);
            }

            switch (cmbIdMineral.SelectedIndex)
            {
                case 0:
                    salidas.IDMineral = 1;
                    break;
                case 1:
                    salidas.IDMineral = 2;
                    break;
                case 2:
                    salidas.IDMineral = 3;
                    break;
                case 3:
                    salidas.IDMineral = 4;
                    break;
                case 4:
                    salidas.IDMineral = 5;
                    break;
                default:
                    break;
            }
            switch (cmbSalidas.SelectedIndex)
            {
                case 0:
                    salidas.IdDetalle = 1;
                    break;
                case 1:
                    salidas.IdDetalle = 2;
                    break;
                case 2:
                    salidas.IdDetalle = 3;
                    break;
                case 3:
                    salidas.IdDetalle = 4;
                    break;
                case 4:
                    salidas.IdDetalle = 5;
                    break;
                case 5:
                    salidas.IdDetalle = 6;
                    break;
                case 6:
                    salidas.IdDetalle = 7;
                    break;
                case 7:
                    salidas.IdDetalle = 8;
                    break;
                default:
                    break;
            }


            salidas.FechaSalida = FSalida.Text;
           // salidas.IdDetalle = Convert.ToInt32(cmbSalidas.SelectedIndex);
            decimal cantidad = Convert.ToDecimal(txtCantidad.Text);
            salidas.Cantidad = Convert.ToDecimal(txtCantidad.Text);

        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {

            if (validaciones.VerificarCamposLlenos(txtCantidad.Text,  cmbIdMineral.Text, FSalida.Text)) ;
            {
                try
                {

                    infoFormulario(0);
                    salidas.ingresarSalidas(salidas);

                    MessageBox.Show("Salida Ingresada");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar  por " + ex);
                    Console.WriteLine(ex.Message);

                }
                finally
                {
                    limpiarTexto();
                    MostrarInfoSalidas();

                }
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (validaciones.VerificarCamposLlenos(txtCantidad.Text, cmbIdMineral.Text, FSalida.Text)) ;
            
            {
                try
                {
                   infoFormulario(1);
                    salidas.ActualizarSalidas(salidas);
                    MessageBox.Show("Salida Modificada");


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Ha ocurrido un error al momento de modificar por " + ex);
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    limpiarTexto();
                    MostrarInfoSalidas();
               
                }
            }
        }


        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                infoFormulario(1);
                salidas.EliminarSalidas(salidas);
                MessageBox.Show("Salida Eliminada");


            }
            catch (Exception ex)
            {

                MessageBox.Show("Ha ocurrido un error al momento de eliminar por " + ex);
                MessageBox.Show(ex.Message);

            }
            finally
            {
                limpiarTexto();
                MostrarInfoSalidas();
            }
        }
        private void dgvSalida_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView filaSeleccionada = dg.SelectedItem as DataRowView;
            if (filaSeleccionada != null)
            {
                txtIdSalida.Text = filaSeleccionada["Código"].ToString();
                cmbIdMineral.Text = filaSeleccionada["Mineral"].ToString();
                txtCantidad.Text = filaSeleccionada["Cantidad (kg)"].ToString();
                FSalida.Text = filaSeleccionada["Fecha de Salida"].ToString();
                //cmbSalidas.Text = filaSeleccionada["Código detalle"].ToString();

            }
        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiarTexto();
            txtIdSalida.IsReadOnly = true;
        }



        private void MostrarMinerales()
        {
            cmbIdMineral.ItemsSource = salidas.LlenarComboBox();
            cmbIdMineral.DisplayMemberPath = "NombreMineral";
            cmbIdMineral.SelectedValuePath = "IDMineral";

        }

        private void MostrarDetalleSalidas()
        {
            cmbSalidas.ItemsSource = salidas.LlenarDetalleSalidas();
            cmbSalidas.DisplayMemberPath = "DetalleSalida";
            cmbSalidas.SelectedValuePath = "idDetalle";

        }



        /*private void txtTotal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            procedimientos.txtTotal(txtCantidad.Text, txtPrecio.Text, txtTotal.Text);
        }*/

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

        private void txtDetalle_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            validaciones.ValidarLetras(e);

        }

       
    }
}
