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

namespace ProyectoMinaELMochito
{
    /// <summary>
    /// Lógica de interacción para Reportes.xaml
    /// </summary>
    public partial class Reportes : Window
    {
        public Reportes()
        {
            InitializeComponent();
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void ListViewItem_MouseDoubleClick (object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnReporteEmpleado_Click(object sender, RoutedEventArgs e)
        {
            ReporteEmpleado reporteEmpleado = new ReporteEmpleado();
            reporteEmpleado.Show();
        }

        private void btnEntrada_Click(object sender, RoutedEventArgs e)
        {
            ReporteEntradas reporte = new ReporteEntradas();
            reporte.Show();
        }

        private void btnInventarioMineral_Click(object sender, RoutedEventArgs e)
        {
            ReporteInventarioMineral reporte = new ReporteInventarioMineral();
            reporte.Show();
        }

        private void btnProduccion_Click(object sender, RoutedEventArgs e)
        {
            ReporteProduccion reporte = new ReporteProduccion();
            reporte.Show();
        }

        private void btnProduccionMineral_Click(object sender, RoutedEventArgs e)
        {
            ReporteProduccionEmpleado reporte = new ReporteProduccionEmpleado();
            reporte.Show();
        }

        private void btnSalidas_Click(object sender, RoutedEventArgs e)
        {
            ReporteSalidas reporte = new ReporteSalidas();
            reporte.Show();
        }

        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            ReporteUsuarios reporte = new ReporteUsuarios();
            reporte.Show();
        }

        private void btnVehiculos_Click(object sender, RoutedEventArgs e)
        {
            ReporteVehiculos reporte = new ReporteVehiculos();
            reporte.Show();
        }
    }
}
