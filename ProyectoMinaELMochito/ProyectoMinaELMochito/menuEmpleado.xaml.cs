﻿using System;
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
    /// Lógica de interacción para menuEmpleado.xaml
    /// </summary>
    public partial class menuEmpleado : Window
    {
        public menuEmpleado()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            menuEmpleado sld = new menuEmpleado();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            Inventario_Mineral sld = new Inventario_Mineral();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_2(object sender, RoutedEventArgs e)
        {
            Salidas sld = new Salidas();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_3(object sender, RoutedEventArgs e)
        {
            Produccion sld = new Produccion();
            sld.Show();
            this.Close();
        }
    }
}
