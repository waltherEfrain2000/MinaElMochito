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
    /// Lógica de interacción para Ayuda.xaml
    /// </summary>
    public partial class Ayuda : Window
    {
        public Ayuda()
        {
            InitializeComponent();
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ListViewItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnVehiculos_Copy_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/II%20PARCIAL/mina/ProyectoMinaELMochito/ProyectoMinaELMochito/Ayuda/Ayuda_Equipamiento/AyudaEquipamiento.html");
                Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/III%20PARCIAL/html2/html/Ayuda_Equipamiento/AyudaEquipamiento.html");
            }
            catch (Exception)
            {

                MessageBox.Show("Error, no se ha encontrado la página...", "Error", MessageBoxButton.OK);
            }
        }

        private void btnProduccion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/III%20PARCIAL/html/Ayuda_Inventario/AyudaIventario.html");
                Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/III%20PARCIAL/html2/html/Ayuda_Inventario/AyudaIventario.html");
            }
            catch (Exception)
            {

                MessageBox.Show("Error, no se ha encontrado la página...", "Error", MessageBoxButton.OK);
            }
        }

        private void btnProduccionMineral_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/III%20PARCIAL/html/Ayuda_Inventario/AyudaEntrada.html");
                Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/III%20PARCIAL/html2/html/Ayuda_Inventario/AyudaEntrada.html");
            }
            catch (Exception)
            {

                MessageBox.Show("Error, no se ha encontrado la página...", "Error", MessageBoxButton.OK);
            }
        }

        private void btnReporteEmpleado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/III%20PARCIAL/html/Ayuda_Inventario/AyudaEmpleados.html");
                Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/III%20PARCIAL/html2/html/Ayuda_Inventario/AyudaEmpleados.html");
            }
            catch (Exception)
            {

                MessageBox.Show("Error, no se ha encontrado la página...", "Error", MessageBoxButton.OK);
            }
        }

        private void btnSalidas_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/III%20PARCIAL/html/Ayuda_Salidas/Salidas/Salidas/AyudaSalidas.html");
                Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/III%20PARCIAL/html2/html/Ayuda_Salidas/Salidas/Salidas/AyudaSalidas.html");
            }
            catch (Exception)
            {

                MessageBox.Show("Error, no se ha encontrado la página...", "Error", MessageBoxButton.OK);
            }
        }

        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/III%20PARCIAL/html/AyudaProduccionMain/VProduccion.html");
                Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/III%20PARCIAL/html2/html/AyudaProduccionMain/VProduccion.html");
            }
            catch (Exception)
            {

                MessageBox.Show("Error, no se ha encontrado la página...", "Error", MessageBoxButton.OK);
            }
        }

        private void btnVehiculos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/III%20PARCIAL/html/AyudaUsuarios/Ayuda%20Usuarios/VProduccion.html");
                Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/III%20PARCIAL/html2/html/AyudaUsuarios/Ayuda%20Usuarios/VProduccion.html");
            }
            catch (Exception)
            {

                MessageBox.Show("Error, no se ha encontrado la página...", "Error", MessageBoxButton.OK);
            }
        }

        private void btnEntrada_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/III%20PARCIAL/html/AyudaCargo/ACargoE.html");
                Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/III%20PARCIAL/html2/html/AyudaCargo/ACargoE.html");
            }
            catch (Exception)
            {

                MessageBox.Show("Error, no se ha encontrado la página...", "Error", MessageBoxButton.OK);
            }
        }

        private void btnInventarioMineral_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/III%20PARCIAL/html/Salidas/AyudaVehiculos.html");
                Process.Start("file:///C:/Users/G7/Desktop/CLASES/2-IMPLEMENTACION%20DE%20SISTEMAS%20DE%20SOFTWARE/III%20PARCIAL/html2/html/AyudaVehiculos/AyudaVehiculos.html");
            }
            catch (Exception)
            {

                MessageBox.Show("Error, no se ha encontrado la página...", "Error", MessageBoxButton.OK);
            }
        }
    }
}
