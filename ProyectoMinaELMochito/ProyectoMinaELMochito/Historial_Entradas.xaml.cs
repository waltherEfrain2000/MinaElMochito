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
    /// Lógica de interacción para Historial_Entradas.xaml
    /// </summary>
    public partial class Historial_Entradas : Window
    {
        private EntradaMineral entradaMineral = new EntradaMineral();
        public Historial_Entradas()
        {
            InitializeComponent();
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
    }
}
