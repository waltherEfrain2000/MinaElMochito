
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

        private Salida salidas = new Salida();
        private Validaciones validaciones = new Validaciones();
        private Procedimientos procedimientos = new Procedimientos();


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

        public Salidas()
        {
            InitializeComponent();
            MostrarMinerales();
            botonfecha.Content = string.Format("{0}", DateTime.Now.ToString());
            MostrarInfoSalidas();

        }

        private void limpiarTexto()
        {
            txtIdSalida.Text = string.Empty;
            cmbIdMineral.SelectedValue = null;
            txtCantidad.Text = string.Empty;
            txtTotal.Text = string.Empty;
            FSalida.Text = string.Empty;
            txtDetalle.Text = string.Empty;

            //edicionDeCasillas(false, 0);
        }


        private void MostrarInfoSalidas()
        {
            try
            {
                string query = @"Select S.[idSalida] as 'ID de Salida', M.[descripcion] as 'Mineral', S.[cantidad] AS 'Cantidad (Kg)', S.[Total] as 'Total',
								S.[fechaSalida] as 'Fecha de Salida', S.[detalleVenta] as 'Detalle de venta'
								from Minas.Salida S Inner Join Minas.Mineral M on S.idmineral = M.idMineral";

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.ExecuteNonQuery();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable dataTable = new DataTable("Minas.Salida");
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

        private void dgvSalida_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView filaSeleccionada = dg.SelectedItem as DataRowView;
            if (filaSeleccionada != null)
            {
                txtIdSalida.Text = filaSeleccionada["ID de Salida"].ToString();
                txtCantidad.Text = filaSeleccionada["Cantidad (Kg)"].ToString();
                txtTotal.Text = filaSeleccionada["Total"].ToString();
                FSalida.Text = filaSeleccionada["Fecha de Salida"].ToString();
                txtDetalle.Text = filaSeleccionada["Detalle de venta"].ToString();
                txtIdSalida.IsReadOnly = false;
            }
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {

            if (validaciones.VerificarCamposLlenos(txtCantidad.Text, txtDetalle.Text, cmbIdMineral.Text, txtTotal.Text, FSalida.Text)) ;
            {
                try
                {
                    //Para definir el ancho de caracteres que debe aceptar el textbox
                    txtDetalle.MaxLength = 50;


                    procedimientos.infoFormulario(0, txtIdSalida.Text, txtCantidad.Text, txtTotal.Text, FSalida.Text, txtDetalle.Text, cmbIdMineral.SelectedIndex);
                    salidas.ingresarSalidas(salidas);

                    MessageBox.Show("Salida Ingresada");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar ...");
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
            if (validaciones.VerificarCamposLlenos(txtCantidad.Text, txtDetalle.Text, cmbIdMineral.Text, txtTotal.Text, FSalida.Text)) ;
            {
                try
                {

                    salidas.ActualizarSalidas(salidas);

                    MessageBox.Show("Salida Modificada");


                }
                catch (Exception ex)
                {
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

                salidas.EliminarSalidas(salidas);

                MessageBox.Show("Salida Eliminada");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                limpiarTexto();
                MostrarInfoSalidas();
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



        /* private void txtTotal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
         {
             try
             {
                 if (txtCantidad.Text == string.Empty)
                 {
                     MessageBox.Show("Favor, ingresar una cantidad a calcular!");
                 }
                 else
                 {

                     decimal precio, cantidad, total;

                     precio = Convert.ToDecimal(txtPrecio.Text, CultureInfo.InvariantCulture);
                     cantidad = Convert.ToDecimal(txtCantidad.Text, CultureInfo.InvariantCulture);

                     total = precio * cantidad;
                     txtTotal.Text = total.ToString("0000.00", CultureInfo.InvariantCulture);

                 }
             }
             catch (Exception ex)
             {
                 MessageBoxResult result = MessageBox.Show("Error no ingresar mas de 1 punto",
                       "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                 txtCantidad.Text = "";
             }



         }
        */
        private void txtTotal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            procedimientos.txtTotal(txtCantidad.Text, txtPrecio.Text, txtTotal.Text);
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
