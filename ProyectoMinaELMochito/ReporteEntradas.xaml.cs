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
    /// Lógica de interacción para ReporteEntradas.xaml
    /// </summary>
    public partial class ReporteEntradas : Window
    {
        public ReporteEntradas()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ReporteSalidas_Loaded);
        }

        private void ReporteSalidas_Loaded(object sender, RoutedEventArgs e)
        {
            this.RptEntradas.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Reportes\Reporte Entradas.rdl");
            this.RptEntradas.RefreshReport();
        }
    }
}
