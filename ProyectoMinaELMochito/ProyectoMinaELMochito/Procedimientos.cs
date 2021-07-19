using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;


namespace ProyectoMinaELMochito
{

    class Procedimientos : Window
    {

        // conexion con clases
        Empleado empleado = new Empleado();
        Mineralinventario mineralinventario = new Mineralinventario();

        // ----------------------------------    PROCEDIMIENTOS PARA EMPLEADOS   -------------------------------- //
        public bool VerificarCamposLlenos(string identidad, string nombre, string edad, string salario, string direccion, object genero, object cargo)
        {
            if (identidad == string.Empty || nombre == string.Empty || edad == string.Empty || salario == string.Empty || direccion == string.Empty)
            {
                MessageBox.Show("Por favor ingresa todos los valores en las cajas de texto");
                return false;
            }
            else if (genero == null)
            {
                MessageBox.Show("Por favor selecciona el Genero del empleado");
                return false;
            }
            else if (cargo == null)
            {
                MessageBox.Show("Por favor selecciona el Cargo del empleado");
                return false;
            }

            else if (Convert.ToInt32(edad) < 18 || Convert.ToInt32(edad) > 100)
            {
                MessageBox.Show("Por favor selecciona una edad valida!");
                return false;
            }
            return true;
        }
        // si no sirve esta parte, cambiar a int genero y cargo
        public void ExtraerInformacionFormulario(int operacion, string empleadoID, string identidad, string nombre, string edad, object genero, object cargo, string salario)
        {
            //entra si va extraer informacion para actualizar
            if (operacion == 1)
            {
                // ojo
                empleado.EmpledoID = Convert.ToInt32(empleadoID);
            }

            empleado.Identidad = identidad;
            empleado.NombreCompleto = nombre;
            empleado.Edad = Convert.ToInt32(edad);
            switch (genero)
            {
                case 0:
                    empleado.Genero = 1;
                    break;
                case 1:
                    empleado.Genero = 2;
                    break;
                default:
                    break;
            }
            switch (cargo)
            {
                case 0:
                    empleado.Cargo = 1;
                    break;
                case 1:
                    empleado.Cargo = 2;
                    break;
                case 2:
                    empleado.Cargo = 3;
                    break;
                case 3:
                    empleado.Cargo = 4;
                    break;
                case 4:
                    empleado.Cargo = 5;
                    break;
                 case 5:
                    empleado.Cargo = 6;
                    break;
                case 6:
                    empleado.Cargo = 7;
                    break;
                default:
                    break;
            }
            decimal monto = Convert.ToDecimal(salario);


            if (!decimal.TryParse(salario, out monto))
            {
                MessageBox.Show("Ingrese un monto válido...");
                return; //Salimos del método o evento
            }

            empleado.Salario = Convert.ToDecimal(salario);
            empleado.Estado = "activo";
            empleado.Direccion = salario;
            //txtSalario.Text = salario.ToString("0000.00", CultureInfo.InvariantCulture);
        }

        //
        Salida salidas = new Salida();
        public void infoFormulario(int operacion, string IDsalida, string Cantidad, string FSalida, int cmbIdMineral, int cmbSalidas)
        {

            if (operacion == 1)
            {
                salidas.IDsalida = Convert.ToInt32(IDsalida);
            }

            salidas.Cantidad = Convert.ToDecimal(Cantidad);
            //salidas.FechaSalida = Convert.ToDateTime(FSalida);
            salidas.DetalleSalida = Convert.ToString(cmbSalidas);
            

            switch (cmbIdMineral)
            {
                case 0:
                    salidas.IDMineral = 1;
                    break;
                case 1:
                    salidas.IDMineral = 2;
                    break;
                case 2:
                    salidas.IDMineral = 3;
                    break;
                case 3:
                    salidas.IDMineral = 4;
                    break;
                case 4:
                    salidas.IDMineral = 5;
                    break;

                default:
                    break;
            }

        }

      

    }
}