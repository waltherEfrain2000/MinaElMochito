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

        public bool VerificarCamposLlenos(string Cantidad, string Total, string FechaSalida, string Mineral, string Detalle)
        {
            Double Valor = 0;

            if (Cantidad == string.Empty || Total == string.Empty || FechaSalida == string.Empty || Detalle == string.Empty)
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
        public bool VerificarNegativos(string cantidad)
        {

            try
            {
                double peso = Convert.ToDouble(cantidad);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                cantidad = string.Empty;
                return false;
            }
            finally
            {
            }
            double Cantidad = Convert.ToDouble(cantidad);
            if (Cantidad < 0)
            {
                MessageBoxResult result = MessageBox.Show("Error!" +
                    " Los números no pueden ser negativos!",
                   "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        //--------------------------------------------------Viajes Intenos-----------------------------------------------------------------
        public bool VerificarCamposLlenos(string txtIdEmpleado, string txtIdVehiculo)
        {
            if (txtIdEmpleado == string.Empty || txtIdVehiculo == string.Empty)
            {
                MessageBoxResult result = MessageBox.Show("Por favor, Verifique que las casillas contengan la infromación requerida!",
                    "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            //Si hay valores en las casillas entonces se retornará verdadero
            return true;
        }



        //-----------------------------------------------------------------Produccion Admin---------------------------------------------------------

        public bool VerificacionDedatosRequeridos(string Cantidad, string Precio, string cmbMinerales)
        {
            int Valor = 0;
            if (Cantidad == string.Empty || Precio == string.Empty || cmbMinerales == null)
            {
                MessageBoxResult result = MessageBox.Show("Por favor, Verifique que las casillas contengan la infromación requerida",
                   "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (Cantidad == string.Empty || Precio == string.Empty || cmbMinerales == null)
            {
                return false;
            }
            return true;
        }
    }

}

