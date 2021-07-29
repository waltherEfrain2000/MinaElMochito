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
    /// Lógica de interacción para ReporteVehiculos.xaml
    /// </summary>
    public partial class ReporteVehiculos : Window
    {
        public ReporteVehiculos()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ReporteVehiculos_Loaded);
        }

        private void ReporteVehiculos_Loaded(object sender, RoutedEventArgs e)
        {
            this.RptVehiculos.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Reportes\Reporte Vehiculos.rdl");
            this.RptVehiculos.RefreshReport();
        }
    }
}
