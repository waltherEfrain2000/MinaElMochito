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
//Utilizar las extensiones necesarias
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace ProyectoMinaELMochito
{
    /// <summary>
    /// Interaction logic for Produccion.xaml
    /// </summary>
    public partial class Produccion : Window
    {
        //Realizar la conexión a la base de datos
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Vaiables miembro
        Producciion producciion = new Producciion();
        Validaciones Validacion = new Validaciones();
        Procedimientos Procedimiento = new Procedimientos();

        public int IdViaje { get; internal set; }

        public Produccion()
        {
            InitializeComponent();

            MostrarMinerales();

            MostrarDatosTabla();

            AsignarUltimoId();
            botonfecha.Content = string.Format("{0}", DateTime.Now.ToString());

        }

        /*-----------------------------------------------------------------------------------------------------------------------------------------
      -------------------------------------------------------LIMPIAR OBTENER---------------------------------------------------------
     ----------------------------------------------------------------------------------------------------------------------------------------- */


        /// <summary>
        /// Este método eliminará los datos que estén en las casillas en 
        /// ese momento y la dejará limpias para una nueva tarea
        /// </summary>
        private void LimpiarCasillasDeDatos()
        {
            //Cajas de texto
            txtPrecio.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            //ComboBox
            cmbMinerales.SelectedValue = null;
        }

        //Obtener los datos ingresados del formulario
        private void ObtenerDatos(int operacion)
        {
            if (operacion == 1)
            {
                producciion.IdProduccion = Convert.ToInt32(txtIdProduccion.Text);
            }
            producciion.IdViaje = Convert.ToInt32(txtNumeroViaje.Text);
            producciion.IdMineral = Convert.ToInt32(cmbMinerales.SelectedValue);
            producciion.Precio = Convert.ToDecimal(txtPrecio.Text);
            producciion.Peso = Convert.ToDecimal(txtCantidad.Text);
        }

        //Valores del formulario objeto
        private void ValoresFormularioObjeto()
        {
            txtNumeroViaje.Text = producciion.IdViaje.ToString();
            txtPrecio.Text = producciion.Precio.ToString();
            txtCantidad.Text = producciion.Peso.ToString();
            cmbMinerales.SelectedValue = producciion.IdMineral;
        }


        //Ocultar los botones
        private void OcultarBotonesOperaciones(Visibility ocultar)
        {
            btnInsertar.Visibility = ocultar;
            btnModificar.Visibility = ocultar;
            btnEliminar.Visibility = ocultar;
            btnLimpiar.Visibility = ocultar;
        }

        private void ObtienePropiedades(Producciion producciion)
        {
            this.txtCantidad.Text = Convert.ToString(producciion.Peso);
            this.txtNumeroViaje.Text = Convert.ToString(producciion.IdViaje);
            this.txtPrecio.Text = Convert.ToString(producciion.Precio);
            this.cmbMinerales.SelectedItem = Convert.ToString(producciion.NombreMineral);
            //this.txtfecha.Text = Convert.ToString (producciion.Fecha);
        }
        /// <summary>
        /// Este método tiene la funcionaliad de pasar los datos que se seleccionan
        /// en el datagridview a las acjas de texto
        /// </summary>
        private void DatosEnCasillas(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView filaSeleccionada = dg.SelectedItem as DataRowView;
            try
            {
                if (filaSeleccionada != null)
                {
                    txtNumeroViaje.Text = filaSeleccionada["N° Viaje"].ToString();
                    cmbMinerales.Text = filaSeleccionada["Mineral"].ToString();
                    txtCantidad.Text = filaSeleccionada["Peso(Kg)"].ToString();
                    txtPrecio.Text = filaSeleccionada["Precio"].ToString();
                    //txtTotal.Text = filaSeleccionada["Total"].ToString();
                    //txtfecha.Text = filaSeleccionada["Fecha del viaje"].ToString();
                    txtIdProduccion.Text = filaSeleccionada["Código"].ToString();


                }
            }catch(Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
            
        }

        private void Casillas(bool opcion, int operacion)
        {
            //Operacion se utiliza para distingir entre actualizar y activar o
            ////inabilitar todas las casillas
            if (operacion == 0)
            {
                txtCantidad.IsReadOnly = opcion;
                txtNumeroViaje.IsReadOnly = opcion;
                cmbMinerales.IsReadOnly = opcion;
            }
            else
            {
                txtCantidad.IsReadOnly = opcion;
                txtNumeroViaje.IsReadOnly = opcion;
                cmbMinerales.IsReadOnly = opcion;
            }

        }

        /*-----------------------------------------------------------------------------------------------------------------------------------------
      -------------------------------------------------------MOSTRAR---------------------------------------------------------
     ----------------------------------------------------------------------------------------------------------------------------------------- */

        private void MostarContenidoEnCasillas(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            DataRowView row_selected = dataGrid.SelectedItem as DataRowView;

            //Validar que realmente se esta seleccionando un elemento del datagrid
            if (row_selected != null)
            {
                txtNumeroViaje.Text = row_selected["Código"].ToString();
                txtCantidad.Text = row_selected["Peso"].ToString();
                txtPrecio.Text = row_selected["Precio"].ToString();
                cmbMinerales.SelectedItem = row_selected["Mineral"].ToString();
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila del datagrid", "¡¡Seleccionar!!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Al cargar el formulario esta función cargará la tabla de Producción
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        public void AsignarUltimoId()
        {

            Producciion idUltimo = producciion.UltimoId();

            txtNumeroViaje.Text = idUltimo.IdViaje.ToString();
        }


        /// <summary>
        /// Con este método se llenará el comboBox trayendo 
        /// sus datos desde la base de datos 
        /// </summary>
        private void MostrarMinerales()
        {
            cmbMinerales.ItemsSource = producciion.LlenarComboBox();
            cmbMinerales.DisplayMemberPath = "NombreMineral";
            cmbMinerales.SelectedValuePath = "IdMineral";

        }

        public void MostrarDatosTabla()
        {
            try
            {
                //Realizar el query que mostrara la información
                String queryProduccion = @"execute MostrarDatosTablaProduccion";

                //Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL tanto como para el query de emplados y de vehículos
                SqlCommand sqlCommand = new SqlCommand(queryProduccion, sqlConnection);

                //Se crea el sqlCommand para poder ejecuatar cada query
                sqlCommand.ExecuteNonQuery();

                //Crear el comando SQL
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                //Crear el dataTable que contendrá las tablas desde la base
                DataTable dataTable1 = new DataTable("Produccion.Produccion");

                //Llenar los datagrid con la información necesaria
                sqlDataAdapter.Fill(dataTable1);
                dgvProduccion.ItemsSource = dataTable1.DefaultView;
                sqlDataAdapter.Update(dataTable1);
            }
            catch (Exception)
            {

                MessageBox.Show("Al mostrar datos en la tabla",
                      "Confirmar", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void MostrarBotonesPrincipales()
        {
            btnModificar.Visibility = Visibility.Visible;
            btnInsertar.Visibility = Visibility.Visible;
            btnEliminar.Visibility = Visibility.Visible;
            btnLimpiar.Visibility = Visibility.Visible;
            LimpiarCasillasDeDatos();
            Casillas(false, 0);
        }


        /*-----------------------------------------------------------------------------------------------------------------------------------------
  -------------------------------------------------------BOTONES---------------------------------------------------------
 ----------------------------------------------------------------------------------------------------------------------------------------- */


        /// <summary>
        /// Método que realiza la operación de insertar datos
        /// a la tabla de Minas.Producción
        /// </summary>
        private void btnInsertar_Click(object sender, RoutedEventArgs e)
        {
            //Primero verificamos que las casillas no estén vacías
            if (Validacion.VerificacionDedatosRequeridos(txtCantidad.Text, txtPrecio.Text, cmbMinerales.Text))
            {
                try
                {
                    //Vamos a obtener los valores ingresados para la tabla
                    ObtenerDatos(0);

                    //Insertamos los valores en la tabla
                    producciion.AgregarProduccion(producciion);

                    //Si los datos se intertarón mostrar un mensje
                    MessageBox.Show("Los datos han sido ingresados correctamente!", "Datos Correctos", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar los datos","¡¡Error!!",MessageBoxButton.OK, MessageBoxImage.Error);
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    LimpiarCasillasDeDatos();

                    MostrarDatosTabla();
                }
            }
        }



        /// <summary>
        /// Precio
        /// </summary>

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (Validacion.VerificacionDedatosRequeridos(txtCantidad.Text, txtPrecio.Text, cmbMinerales.Text))
            {
                try
                {
                    ObtenerDatos(1);
                    producciion.ModificarProduccion(producciion);

                    MessageBox.Show("¡Registro Actualizado Correctamente!", "Datos Actualizados", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                catch (Exception )
                {
                    MessageBox.Show("Los datos no se modificaron correctamente","¡¡Error!!",MessageBoxButton.OK, MessageBoxImage.Error);

                }
                finally
                {
                    LimpiarCasillasDeDatos();
                    MostrarDatosTabla();
                }

            }
        }

        /*------------------------------------------------------------------------------------------------------------------------
       ---------------------------------------------- vALIDACION-----------------------------------------------------------
       -------------------------------------------------------------------------------------------------------------------------*/

        public void VerificarP()
        {
            double Valor = 0;
            try
            {
                if (txtCantidad.Text == string.Empty || (Convert.ToDouble(txtCantidad.Text) == Valor))
                {
                    txtCantidad.Text = string.Empty;
                }
                else if (txtPrecio.Text == String.Empty)
                {
                    MessageBoxResult result = MessageBox.Show("Debe seleccionar un mineral","Verificar", MessageBoxButton.OK, MessageBoxImage.Error);
                     // "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtCantidad.Text = string.Empty;
                }

            }
            catch (Exception )
            {
                MessageBoxResult result = MessageBox.Show("No puede ingresar dos puntos deguidos", "Verificar", MessageBoxButton.OK, MessageBoxImage.Hand);
                    //"Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtCantidad.Text = string.Empty;
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------
        //Checked
        private void ActualizarPrecio(object sender, RoutedEventArgs e)
        {
            txtPrecio.IsReadOnly = false;
        }

        //Uncheked
        private void ActualizarElPrecio(object sender, RoutedEventArgs e)
        {
            txtPrecio.IsReadOnly = true;
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCasillasDeDatos();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (Validacion.VerificacionDedatosRequeridos(txtCantidad.Text, txtPrecio.Text, cmbMinerales.Text))
            {
                try
                {
                    ObtenerDatos(1);
                    producciion.BorrarProduccion(producciion);

                    MessageBox.Show("´¡El registro se eliminó Correctamente!", "Datos Eliminados", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                catch (Exception )
                {
                    MessageBox.Show("Los datos no se eliminaron correctamente", "¡¡Error!!", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                finally
                {
                    LimpiarCasillasDeDatos();
                    MostrarDatosTabla();
                }

            }

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



        private void txtCantidad_KeyDown_1(object sender, KeyEventArgs e)
        {
            Validacion.ValidarLetras(e);

        }

        private void txtCantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            VerificarP();
        }



        private void ListViewItem_Selected_9(object sender, RoutedEventArgs e)
        {
            ViajesInternos sld = new ViajesInternos();
            sld.Show();
            this.Close();
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

        //Fin del programa
    }
}
