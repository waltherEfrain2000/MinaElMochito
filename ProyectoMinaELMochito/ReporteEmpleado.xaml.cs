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
    /// Lógica de interacción para ReporteEmpleado.xaml
    /// </summary>
    public partial class ReporteEmpleado : Window
    {
        public ReporteEmpleado()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ReporteEmplado_Loaded);
        }

        private void ReporteEmplado_Loaded (object sender, RoutedEventArgs e)
        {
            this.RptEmpleado.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Reportes\Reporte Empleados.rdl");
            this.RptEmpleado.RefreshReport();
        }
    }
}
