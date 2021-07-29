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
    /// Lógica de interacción para ReporteInventarioMineral.xaml
    /// </summary>
    public partial class ReporteInventarioMineral : Window
    {
        public ReporteInventarioMineral()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ReporteInventarioMineral_Loaded);
        }

        private void ReporteInventarioMineral_Loaded(object sender, RoutedEventArgs e)
        {
            this.RptInventarioMineral.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Reportes\Reporte InventarioMineral.rdl");
            this.RptInventarioMineral.RefreshReport();
        }
    }
}
