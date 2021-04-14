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
        public Produccion()
        {
            InitializeComponent();

            MostrarMinerales();
        }

        //Vaiables miembro
        Producciion producciion = new Producciion();

        //Realizar la conexión a la base de datos
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

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
        /// Con este método se llenará el comboBox trayendo sus datos desde
        /// la base de datos 
        /// </summary>
        private void MostrarMinerales()
        {
            cmbMinerales.ItemsSource = producciion.LlenarComboBox();
            cmbMinerales.DisplayMemberPath = "NombreMineral";
            cmbMinerales.SelectedValuePath = "IdMineral";
            
        }

    }
}
