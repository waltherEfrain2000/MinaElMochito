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
    /// Lógica de interacción para ReporteUsuarios.xaml
    /// </summary>
    public partial class ReporteUsuarios : Window
    {
        public ReporteUsuarios()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ReporteUsuarios_Loaded);
        }

        private void ReporteUsuarios_Loaded(object sender, RoutedEventArgs e)
        {
            this.RptUsuarios.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Reportes\Reporte Usuarios.rdl");
            this.RptUsuarios.RefreshReport();
        }
    }
}
