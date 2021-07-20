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
    /// Lógica de interacción para ReporteSalidas.xaml
    /// </summary>
    public partial class ReporteSalidas : Window
    {
        public ReporteSalidas()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ReporteSalidas_Loaded);
        }

        private void ReporteSalidas_Loaded(object sender, RoutedEventArgs e)
        {
            this.RptSalidas.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Reportes\ReporteSalidas.rdl");
            this.RptSalidas.RefreshReport();
        }
    }
}
