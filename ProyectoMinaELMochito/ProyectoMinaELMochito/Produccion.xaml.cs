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

        public Produccion()
        {
            InitializeComponent();

            MostrarMinerales();
        }

        /// <summary>
        /// Este método eliminará los datos que estén en las casillas en 
        /// ese momento y la dejará limpias para una nueva tarea
        /// </summary>
        private void LimpiarCasillasDeDatos()
        {
            //Cajas de texto
            txtNumeroViaje.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtCantidad.Text = string.Empty;

            //ComboBox
            cmbMinerales.SelectedValue = null;
        }

        //Obtener los datos ingresados del formulario
        private void ObtenerDatos()
        {
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

        //Se verificará que todos los campos estén llenoss antes de realizar cualquier acción
        private bool VerificacionDedatosRequeridos()
        {
            if (txtCantidad.Text == string.Empty || txtPrecio.Text == string.Empty || cmbMinerales.SelectedValue == null)
            {
                MessageBoxResult result = MessageBox.Show("Por favor!, Verifique que las casillas" +
                    " contengan la infromación requerida!",
                   "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        //Ocultar los botones
        private void OcultarBotonesOperaciones(Visibility ocultar)
        {
            btnInsertar.Visibility = ocultar;
            btnModificar.Visibility = ocultar;
            btnEliminar.Visibility = ocultar;
            btnSalir.Visibility = ocultar;
        }

        /// <summary>
        /// Al cargar el formulario esta función cargará la tabla de Producción
        /// </summary>
        private void MuestraDatos(object sender, RoutedEventArgs e)
        {
            try
            {
                //Realizar el query que mostrara la información
                String queryProduccion = @"Select P.idProduccion as 'id Producción', P.idViaje as 'N° Viaje', 
                                    M.descripcion as 'Mineral',P.precio as 'Precio', P.peso as 'Peso(T)'
                                    From Minas.Produccion as P
                                    Inner Join Minas.Mineral as M on P.idMineral = M.idMineral";

                //Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL tanto como para el query de emplados y de vehículos
                SqlCommand sqlCommand = new SqlCommand(queryProduccion, sqlConnection);

                //Se crea el sqlCommand para poder ejecuatar cada query
                sqlCommand.ExecuteNonQuery();

                //Crear el comando SQL
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                //Crear el dataTable que contendrá las tablas desde la base
                DataTable dataTable1 = new DataTable("Minas.Produccion");

                //Llenar los datagrid con la información necesaria
                sqlDataAdapter.Fill(dataTable1);
                dgvProduccion.ItemsSource = dataTable1.DefaultView;
                sqlDataAdapter.Update(dataTable1);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
 
       

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

        /// <summary>
        /// Método que realiza la operación de insertar datos
        /// a la tabla de Minas.Producción
        /// </summary>
        private void btnInsertar_Click(object sender, RoutedEventArgs e)
        {
            //Primero verificamos que las casillas no estén vacías
            if (VerificacionDedatosRequeridos())
            {
                try
                {
                    //Vamos a obtener los valores ingresados para la tabla
                    ObtenerDatos();

                    //Insertamos los valores en la tabla
                    producciion.AgregarProduccion(producciion);

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
                    LimpiarCasillasDeDatos();
                }
            }
        }


    }
}
