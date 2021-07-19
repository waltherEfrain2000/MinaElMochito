using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Input;
namespace ProyectoMinaELMochito
{
    class Validaciones : Window
    {
        public void validarTxtSinLetras(KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        public void validarTxtSinLetrasConCommas(KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
            { e.Handled = false; }

            else
            { e.Handled = true; }
        }

        public void validarTxtSinNumeros(KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = true;
            else
                e.Handled = false;
        }
        public void ValidarLetras(KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemPeriod || e.Key == Key.Decimal)
                e.Handled = false;
            else
                e.Handled = true;

        }

        public bool VerificarCamposLlenos(string Cantidad, string FechaSalida, string Mineral)
        {
            Double Valor = 0;

            if (Cantidad == string.Empty || FechaSalida == string.Empty )
            {
                MessageBox.Show("Favor no dejar vacío ningún campo...");
                return false;
            }

            else if (Mineral == null)
            {
                MessageBox.Show("Favor, seleccionar un valor mineral!");
                return false;
            }
            else if (Convert.ToDouble(Cantidad) == Valor)
            {
                MessageBoxResult result = MessageBox.Show("La cantidad no puede ser 0",
                  "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

    }

}

