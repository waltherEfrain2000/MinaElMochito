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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ProyectoMinaELMochito
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Variables Miembro
        private ViajeInterno viajeinterno = new ViajeInterno();

        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        /// <summary>
        /// Muestra la tabla en donde se encuentran los empleados
        /// de la mina, de aquí el usuario seleccionará uno para el viaje
        /// </summary>
        /// <param name="empleado">El id del empleado</param>
        private void CargarDatos(object sender, RoutedEventArgs e)
        {
            try
            {
                //Query para seleccionar los datos de la tabla
                String queryEmpleado = @"Select IdEmpleado as 'Id Empleado', identidad as 'Identidad', 
                               primerNombre as 'Nombre', primerApellido as 'Apellido'
                               from Minas.Empleado";

                String queryVehiculo = @"Select idVehiculo as 'Id Vehiculo', marca as 'Marca', 
                                        modelo as 'Modelo', placa as 'Placa', color as 'Color'
                                         from Minas.Vehiculo";

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
                DataTable dataTable1 = new DataTable("Minas.Empleado");
                DataTable dataTable2 = new DataTable("Minas.Vehiculo");

                //Llenar los datagrid con la información necesaria
                sqlDataAdapter1.Fill(dataTable1);
                dgvEmpleados.ItemsSource = dataTable1.DefaultView;
                sqlDataAdapter1.Update(dataTable1);

                sqlDataAdapter2.Fill(dataTable2);
                dgvVehiculos.ItemsSource = dataTable2.DefaultView;
                sqlDataAdapter2.Update(dataTable2);
              
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

    }
}
