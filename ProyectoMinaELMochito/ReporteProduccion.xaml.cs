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
    /// Lógica de interacción para ReporteProduccion.xaml
    /// </summary>
    public partial class ReporteProduccion : Window
    {
        public ReporteProduccion()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ReporteEmplado_Loaded);
        }
        private void ReporteEmplado_Loaded(object sender, RoutedEventArgs e)
        {
            this.RptProduccion.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Reportes\Reporte Produccion.rdl");
            this.RptProduccion.RefreshReport();
        }
    }
}
