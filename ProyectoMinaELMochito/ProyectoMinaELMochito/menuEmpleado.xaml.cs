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
using System.Diagnostics;

namespace ProyectoMinaELMochito
{
    /// <summary>
    /// Lógica de interacción para menuEmpleado.xaml
    /// </summary>
    public partial class menuEmpleado : Window
    {
        public menuEmpleado()
        {
            InitializeComponent();
            botonfecha.Content = string.Format("{0}", DateTime.Now.ToString());
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            menuEmpleado sld = new menuEmpleado();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            ViajeInternoEmp sld = new ViajeInternoEmp();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_2(object sender, RoutedEventArgs e)
        {
            Login sld = new Login();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_3(object sender, RoutedEventArgs e)
        {
            ViajesInternos sld = new ViajesInternos();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            menuEmpleado sld = new menuEmpleado();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_4(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/III%20PARCIAL/html/AyudaProduccionMain/VProduccion.html");

            }
            catch (Exception)
            {

                MessageBox.Show("Error, no se ha encontrado la página...", "Error", MessageBoxButton.OK);
            }
        }
    }
}
