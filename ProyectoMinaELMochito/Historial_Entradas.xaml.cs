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
            ///cargamos los metodos respectivos por mineral de la clase entrada mineral que los contiene
            EntradaMineral elmineral = entradaMineral.CargarOro();
            EntradaMineral elmineralzinc = entradaMineral.CargarZinc();
            EntradaMineral elmineralplata = entradaMineral.CargarPlata();
            EntradaMineral elmineralcobre = entradaMineral.CargarCobre();
            EntradaMineral elmineralPlomo = entradaMineral.CargarPlomo();
            SeriesCollection = new SeriesCollection
            {
                // cada new pieseries es un pedazo de pastel(parte del grafico de dona)
                new PieSeries
                {
                    //// aqui se le establecen los valores del objeto de cobre 
                    Title = $"Cobre , K:{elmineralcobre.Cantidad}, valor en ${elmineralcobre.Total.ToString("N")}",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble( elmineralcobre.Cantidad)), new  ObservableValue (Convert.ToDouble(elmineralcobre.Total)) },
                    DataLabels = true
                },
                new PieSeries
                {
                    //// aqui se le establecen los valores del objeto de plata
                    Title = $"Plata , K:{elmineralplata.Cantidad}, valor en ${elmineralplata.Total.ToString("N")}",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(elmineralplata.Cantidad)) , new  ObservableValue (Convert.ToDouble(elmineralplata.Total))},
                    DataLabels = true
                },
                new PieSeries
                {
                    //// aqui se le establecen los valores del objeto de oro
                    Title = $"Oro, K:{elmineral.Cantidad}, valor en ${elmineral.Total.ToString("N")}",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(elmineral.Cantidad)) , new  ObservableValue (Convert.ToDouble(elmineral.Total)) },
                    DataLabels = true,
                    ToolTip= elmineral.Total
                },
                new PieSeries
                {
                    //// aqui se le establecen los valores del objeto de sinc
                    Title =$"Zinc, K:{elmineralzinc.Cantidad}, valor en ${elmineralzinc.Total.ToString("N")}",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(elmineralzinc.Cantidad)), new  ObservableValue (Convert.ToDouble(elmineralzinc.Total)) },
                    DataLabels = true
                },
                new PieSeries
                {
                    //// aqui se le establecen los valores del objeto de  plomo
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



    }  
}
