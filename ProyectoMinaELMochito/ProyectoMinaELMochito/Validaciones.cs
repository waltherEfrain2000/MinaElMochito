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

        public bool VerificarCamposLlenos(string Cantidad, string Mineral)
        {
           
            if (Cantidad == string.Empty || Mineral == null)
            {
                MessageBoxResult result = MessageBox.Show("Por favor!, Verifique que las casillas" +
                   " contengan la infromación requerida!",
                  "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (Cantidad == string.Empty || Mineral == null)
            {
                return false;
            }

            return true;
        }
    

    }

}

