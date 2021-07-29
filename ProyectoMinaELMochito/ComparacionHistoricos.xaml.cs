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
    /// Lógica de interacción para Resporte_y_ComparacionHistoricos.xaml
    /// </summary>
    public partial class Resporte_y_ComparacionHistoricos : Window
    {

        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        public Resporte_y_ComparacionHistoricos()
        {
            InitializeComponent();
            ObtenerInventario();
            obtenerEntradas();
            ObtenerSalidas();

        }
        private void ObtenerInventario()
        {


            try
            {
                // El query de consulta a la base de datos
                string query = @"exec ProcedimientoReporteInventarioMineral";

                // Crear un comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Crear el SqlDataAdapter utilizando un SqlCommand
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                using (sqlDataAdapter)
                {
                    // Crear el DataTable que contendrá los datos
                    DataTable tabla = new DataTable();

                    // Llenar el DataTable con los valores del DataAdapter
                    sqlDataAdapter.Fill(tabla);

                    // Llenar el ListBox con los valores del DataTable


                    lbInventario.DisplayMemberPath = "Descripcion";
                    //lbInventario.DisplayMemberPath = "Total";
                    lbInventario.SelectedValuePath = "idMineral";
                    lbInventario.ItemsSource = tabla.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void obtenerEntradas()
        {

            try
            {
                // El query de consulta a la base de datos
                string query = @"exec procedimientoReporteEntrada";


                // Crear un comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Crear el SqlDataAdapter utilizando un SqlCommand
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                using (sqlDataAdapter)
                {
                    // Crear el DataTable que contendrá los datos
                    DataTable tabla = new DataTable();

                    // Llenar el DataTable con los valores del DataAdapter
                    sqlDataAdapter.Fill(tabla);

                    // Llenar el ListBox con los valores del DataTable


                    lbEntradas.DisplayMemberPath = "Descripcion";
                    //lbInventario.DisplayMemberPath = "Total";
                    lbEntradas.SelectedValuePath = "m.idProducto";
                    lbEntradas.ItemsSource = tabla.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ObtenerSalidas()
        {
            try
            {
                // El query de consulta a la base de datos
                string query = @"exec ProcedimientoReporteSalidas";


                // Crear un comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Crear el SqlDataAdapter utilizando un SqlCommand
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                using (sqlDataAdapter)
                {
                    // Crear el DataTable que contendrá los datos
                    DataTable tabla = new DataTable();

                    // Llenar el DataTable con los valores del DataAdapter
                    sqlDataAdapter.Fill(tabla);

                    // Llenar el ListBox con los valores del DataTable


                    lbSalidas.DisplayMemberPath = "Descripcion";
                    //lbInventario.DisplayMemberPath = "Total";
                    lbSalidas.SelectedValuePath = "m.idmineral";
                    lbSalidas.ItemsSource = tabla.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
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

        private void ListViewItem_Selected_9(object sender, RoutedEventArgs e)
        {
            ViajesInternos sld = new ViajesInternos();
            sld.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            menuPrincipal sld = new menuPrincipal();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
