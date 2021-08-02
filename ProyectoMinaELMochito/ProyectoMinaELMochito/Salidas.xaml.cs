
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
        public int IdDetalle { get; internal set; }


        public Salidas()
        {
            //botonfecha.Content = string.Format("{0}", DateTime.Now.ToString());
            InitializeComponent();
            MostrarMinerales();
            //MostrarDetalleSalidas();
            MostrarInfoSalidas();
            AsignarUltimoId();
           
        }

        private void limpiarTexto()
        {
            txtIdSalida.Text = string.Empty;
            cmbIdMineral.SelectedValue = null;
            txtCantidad.Text = string.Empty;
            //FSalida.Text = string.Empty;
            //cmbSalidas.SelectedValue = null;
          

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
            salidas.IdDetalle = Convert.ToInt32(txtIdDetalleSalida.Text);
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
    

           // salidas.FechaSalida = FSalida.Text;
           // salidas.IdDetalle = Convert.ToInt32(cmbSalidas.SelectedIndex);
            decimal cantidad = Convert.ToDecimal(txtCantidad.Text);
            salidas.Cantidad = Convert.ToDecimal(txtCantidad.Text);

        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {

            if (validaciones.VerificarCamposLlenos(txtCantidad.Text, cmbIdMineral.Text)) ;
            {
                try
                {

                    infoFormulario(0);
                    //Manda a llamar lo que esta en la clase de salidas.c
                    salidas.ingresarSalidas(salidas);

                    MessageBox.Show("Salida Ingresada");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar ");
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
            if (validaciones.VerificarCamposLlenos(txtCantidad.Text,cmbIdMineral.Text)) ;
            
            {
                try
                {
                   infoFormulario(1);
                    salidas.ActualizarSalidas(salidas);
                  
                    MessageBox.Show("Salida Modificada");

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Ha ocurrido un error al momento de modificar  " );
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

                MessageBox.Show("Ha ocurrido un error al momento de eliminar  " );
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
                txtIdDetalleSalida.Text = filaSeleccionada["idDetalle"].ToString();
                txtCantidad.Text = filaSeleccionada["Cantidad (kg)"].ToString();
                // FSalida.Text = filaSeleccionada["Fecha de Salida"].ToString();
                // cmbSalidas.Text = filaSeleccionada["Detalle de venta"].ToString();
               
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

        //private void MostrarDetalleSalidas()
        //{
        //    cmbSalidas.ItemsSource = salidas.LlenarDetalleSalidas();
        //    cmbSalidas.DisplayMemberPath = "DetalleSalida";
        //    cmbSalidas.SelectedValuePath = "idDetalle";

        //}

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

        public void AsignarUltimoId()
        {
            Salida ultimoId = salidas.UltimoId();
            txtIdDetalleSalida.Text = ultimoId.IdDetalle.ToString();
        }

        private void btnAsignar_Click(object sender, RoutedEventArgs e)
        {
            DetalleSalida sld = new DetalleSalida();
            sld.Show();
            //txtIdDetalleSalida.Visibility = Visibility.Visible;
        }
        //public bool VerificarValor0()
        //{
        //    Double Valor = 0;

        //  if (Convert.ToDouble(txtCantidad.Text) == Valor)
        //    {
        //        MessageBoxResult result = MessageBox.Show("La cantidad no puede ser 0",
        //          "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return false;
        //    }

        //    return true;
        //}
      
        public void VerificarCantidad()
        {
            double Valor = 0;
            try
            {
                if (txtCantidad.Text == string.Empty || (Convert.ToDouble(txtCantidad.Text) == Valor))
                {
                    txtCantidad.Text = string.Empty;
                }
                else if (cmbIdMineral.SelectedItem == null)
                {
                    MessageBoxResult result = MessageBox.Show("Debe seleccionar un mineral",
                      "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtCantidad.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show("No puede ingresar dos puntos deguidos",
                "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCantidad.Text = string.Empty;
            }
        }
        private void txtCantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            VerificarCantidad();
        }
    }
}
