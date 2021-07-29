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
    /// Lógica de interacción para ReporteProduccionEmpleado.xaml
    /// </summary>
    public partial class ReporteProduccionEmpleado : Window
    {
        public ReporteProduccionEmpleado()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ReporteProduccionEmpleado_Loaded);
        }

        private void ReporteProduccionEmpleado_Loaded(object sender, RoutedEventArgs e)
        {
            this.RptProduccionEmpleado.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Reportes\Reporte Produccion-Empleado.rdl");
            this.RptProduccionEmpleado.RefreshReport();
        }
    }
}
