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


using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
namespace ProyectoMinaELMochito
{
    /// <summary>
    /// Lógica de interacción para EntradasHistoricas.xaml
    /// </summary>
    public partial class EntradasHistoricas : Window
    {
        private EntradaMineral entradaMineral = new EntradaMineral();
        public EntradasHistoricas()
        {

            InitializeComponent();
            botonfecha.Content = string.Format("{0}", DateTime.Now.ToString());
            EntradaMineral elmineral = entradaMineral.CargarOro();
            EntradaMineral elmineralzinc = entradaMineral.CargarZinc();
            EntradaMineral elmineralplata = entradaMineral.CargarPlata();
            EntradaMineral elmineralcobre = entradaMineral.CargarCobre();
            EntradaMineral elmineralPlomo = entradaMineral.CargarPlomo();
            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {

                    Title = $"Cobre , K:{elmineralcobre.Cantidad}, valor en ${elmineralcobre.Total.ToString("N")}",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble( elmineralcobre.Cantidad)), new  ObservableValue (Convert.ToDouble(elmineralcobre.Total)) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = $"Plata , K:{elmineralplata.Cantidad}, valor en ${elmineralplata.Total.ToString("N")}",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(elmineralplata.Cantidad)) , new  ObservableValue (Convert.ToDouble(elmineralplata.Total))},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = $"Oro, K:{elmineral.Cantidad}, valor en ${elmineral.Total.ToString("N")}",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(elmineral.Cantidad)) , new  ObservableValue (Convert.ToDouble(elmineral.Total)) },
                    DataLabels = true,
                    ToolTip= elmineral.Total
                },
                new PieSeries
                {
                    Title =$"Zinc, K:{elmineralzinc.Cantidad}, valor en ${elmineralzinc.Total.ToString("N")}",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(elmineralzinc.Cantidad)), new  ObservableValue (Convert.ToDouble(elmineralzinc.Total)) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title =$"Plomo , K:{elmineralPlomo.Cantidad}, valor en ${elmineralPlomo.Total.ToString("N")}",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(elmineralPlomo.Cantidad)), new  ObservableValue (Convert.ToDouble(elmineralPlomo.Total)) },
                    DataLabels = true
                }
            };

            //adding values or series will update and animate the chart automatically
            //SeriesCollection.Add(new PieSeries());
            //SeriesCollection[0].Values.Add(5);

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }

        private void UpdateAllOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();

            foreach (var series in SeriesCollection)
            {
                foreach (var observable in series.Values.Cast<ObservableValue>())
                {
                    observable.Value = r.Next(0, 10);
                }
            }
        }

        private void AddSeriesOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();
            var c = SeriesCollection.Count > 0 ? SeriesCollection[0].Values.Count : 5;

            var vals = new ChartValues<ObservableValue>();

            for (var i = 0; i < c; i++)
            {
                vals.Add(new ObservableValue(r.Next(0, 10)));
            }

            SeriesCollection.Add(new PieSeries
            {
                Values = vals
            });
        }

        private void RemoveSeriesOnClick(object sender, RoutedEventArgs e)
        {
            if (SeriesCollection.Count > 0)
                SeriesCollection.RemoveAt(0);
        }

        private void AddValueOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();
            foreach (var series in SeriesCollection)
            {
                series.Values.Add(new ObservableValue(r.Next(0, 10)));
            }
        }

        private void RemoveValueOnClick(object sender, RoutedEventArgs e)
        {
            foreach (var series in SeriesCollection)
            {
                if (series.Values.Count > 0)
                    series.Values.RemoveAt(0);
            }
        }

        private void RestartOnClick(object sender, RoutedEventArgs e)
        {
            Chart.Update(true, true);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Resporte_y_ComparacionHistoricos sld = new Resporte_y_ComparacionHistoricos();
            sld.Show();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            Empleados sld = new Empleados();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            Vehiculos sld = new Vehiculos();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_2(object sender, RoutedEventArgs e)
        {
            Inventario_Mineral sld = new Inventario_Mineral();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_3(object sender, RoutedEventArgs e)
        {
            EntradasHistoricas sld = new EntradasHistoricas();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_4(object sender, RoutedEventArgs e)
        {
            Salidas sld = new Salidas();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_5(object sender, RoutedEventArgs e)
        {
            ViajesInternos sld = new ViajesInternos();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_6(object sender, RoutedEventArgs e)
        {
            Usuarios_Crud sld = new Usuarios_Crud();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            menuPrincipal sld = new menuPrincipal();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_7(object sender, RoutedEventArgs e)
        {
            menuPrincipal sld = new menuPrincipal();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_8(object sender, RoutedEventArgs e)
        {
            Login sld = new Login();
            sld.Show();
            this.Close();
        }
    }
    }
