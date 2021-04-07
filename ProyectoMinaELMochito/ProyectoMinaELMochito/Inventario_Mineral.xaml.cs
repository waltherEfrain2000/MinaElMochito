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

using LiveCharts;
using LiveCharts.Wpf;

namespace ProyectoMinaELMochito
{
    /// <summary>
    /// Lógica de interacción para Inventario_Mineral.xaml
    /// </summary>
    public partial class Inventario_Mineral : Window
    {
        private Mineralinventario mineralinventario = new Mineralinventario();
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }

        public Inventario_Mineral()
        {


            InitializeComponent();
            LblDate.Content = $"Hoy es: {DateTime.Now.ToLongDateString()}";
            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            DataContext = this;

            asignarvalororo();
            asignarvalorZinc();
            asignarvalorPlomo();
            asignarvalorPlata();
            asignarvalorCobre();
        }




        public Func<ChartPoint, string> PointLabel { get; set; }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 12;

            //// verificamos el piechar clickeado y le damos sus datos segun el nombre
            if (selectedSeries == pieplata)
            {
                Mineralinventario elmineral = mineralinventario.CargarPlata();
                txtpeso.Text = elmineral.Peso.ToString();
                txtfecha.Text = String.Format("{0}, a las :{1}", elmineral.FechaActualizacion.ToLongDateString(), elmineral.FechaActualizacion.ToLongTimeString());
                txttotal.Text = elmineral.Total.ToString("N");

                Uri uri = new Uri("ImagesMinerales/plata.png", UriKind.Relative);
                imgmineral.Source = new BitmapImage(uri);
                lbdatos.Content = "Datos del mineral de plata ";

            }
            else if (selectedSeries == pieoro)
            {
                Mineralinventario elmineral = mineralinventario.CargarOro();
                txtpeso.Text = elmineral.Peso.ToString();
                txtfecha.Text = String.Format("{0}, a las :{1}", elmineral.FechaActualizacion.ToLongDateString(), elmineral.FechaActualizacion.ToLongTimeString());
                txttotal.Text = elmineral.Total.ToString("N");

                Uri uri = new Uri("ImagesMinerales/oro.png", UriKind.Relative);
                imgmineral.Source = new BitmapImage(uri);
                lbdatos.Content = "Datos del mineral de oro ";
            }
            else if (selectedSeries == pieCobre)
            {
                Mineralinventario elmineral = mineralinventario.CargarCobre();
                txtpeso.Text = elmineral.Peso.ToString();
                txtfecha.Text = String.Format("{0}, a las :{1}", elmineral.FechaActualizacion.ToLongDateString(), elmineral.FechaActualizacion.ToLongTimeString());
                txttotal.Text = elmineral.Total.ToString("N");

                Uri uri = new Uri("ImagesMinerales/cobre.png", UriKind.Relative);
                imgmineral.Source = new BitmapImage(uri);
                lbdatos.Content = "Datos del mineral de Cobre";

            }
            else if (selectedSeries == pieplomo)
            {
                Mineralinventario elmineral = mineralinventario.CargarPlomo();
                txtpeso.Text = elmineral.Peso.ToString();
                txtfecha.Text = String.Format("{0}, a las :{1}", elmineral.FechaActualizacion.ToLongDateString(), elmineral.FechaActualizacion.ToLongTimeString());
                txttotal.Text = elmineral.Total.ToString("N");

                Uri uri = new Uri("ImagesMinerales/plomo.png", UriKind.Relative);
                imgmineral.Source = new BitmapImage(uri);
                lbdatos.Content = "Datos del mineral de Plomo";
            }
            else if (selectedSeries == pieZinc)

            {
                Mineralinventario elmineral = mineralinventario.CargarZinc();
                txtpeso.Text = elmineral.Peso.ToString();
                txtfecha.Text = String.Format("{0}, a las :{1}", elmineral.FechaActualizacion.ToLongDateString(), elmineral.FechaActualizacion.ToLongTimeString());
                txttotal.Text = elmineral.Total.ToString("N");

                Uri uri = new Uri("ImagesMinerales/zinc.png", UriKind.Relative);
                imgmineral.Source = new BitmapImage(uri);
                lbdatos.Content = "Datos del mineral de Zinc";
            }


        }


        private void asignarvalororo()
        {
            try
            {
                Mineralinventario elmineral = mineralinventario.CargarOro();
                pieoro.Values = new ChartValues<double> { elmineral.Peso };
                pieoro.Title = String.Format($"ORO =K {elmineral.Peso}");
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        /// <summary>
        /// aqui le damos el valor al pie chart de cuanto en kilos hay en zinc
        /// </summary>
        private void asignarvalorZinc()
        {
            try
            {
                Mineralinventario elmineral = mineralinventario.CargarZinc();
                pieZinc.Values = new ChartValues<double> { elmineral.Peso };
                pieZinc.Title = String.Format($"ZINC =K {elmineral.Peso}");
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        /// <summary>
        /// asiganamos el valor extraido de plomo al valor del pie chart al que pertenece
        /// </summary>
        private void asignarvalorPlomo()
        {
            try
            {
                Mineralinventario elmineral = mineralinventario.CargarPlomo();
                pieplomo.Values = new ChartValues<double> { elmineral.Peso };
                pieplomo.Title = String.Format($"PLOMO =K {elmineral.Peso}");
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        private void asignarvalorPlata()
        {
            try
            {
                Mineralinventario elmineral = mineralinventario.CargarPlata();
                pieplata.Values = new ChartValues<double> { elmineral.Peso };
                pieplata.Title = String.Format($"PLATA =K {elmineral.Peso}");
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private void asignarvalorCobre()
        {
            try
            {
                Mineralinventario elmineral = mineralinventario.CargarCobre();
                pieCobre.Values = new ChartValues<double> { elmineral.Peso };
                pieCobre.Title = String.Format($"COBRE =K {elmineral.Peso}");
            }
            catch (Exception e)
            {
                throw e;
            }

        }



        private void pieplata_MouseEnter(object sender, MouseEventArgs e)
        {
            Mineralinventario elmineral = mineralinventario.CargarPlata();
            txtpeso.Text = elmineral.Peso.ToString();
        }

        private void pieplata_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Mineralinventario elmineral = mineralinventario.CargarPlata();
            txtpeso.Text = elmineral.Peso.ToString();
        }
    }

}

