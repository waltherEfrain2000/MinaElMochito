using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar los namespaces requeridos
using System.Data.SqlClient;
using System.Configuration;

using System.Windows;


namespace ProyectoMinaELMochito
{

    class Procedimientos : Window
    {

        // conexion con clases
        Empleado empleado = new Empleado();
        Mineralinventario mineralinventario = new Mineralinventario();

        // ----------------------------------    PROCEDIMIENTOS PARA VEHICULOS  -------------------------------- //

        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);
        private Vehiculo elVehiculo = new Vehiculo();

        public void CrearVehiculo(Procedimientos vehiculo)
        {
            try
            {
                string query = @"EXEC InsertarVehiculo @marca,@modelo,@placa,@color,@estado";

                sqlConnection.Open();


                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@marca", elVehiculo.Marca);
                sqlCommand.Parameters.AddWithValue("@modelo", elVehiculo.Modelo);
                sqlCommand.Parameters.AddWithValue("@placa", elVehiculo.Placa);
                sqlCommand.Parameters.AddWithValue("@color", elVehiculo.Color);
                sqlCommand.Parameters.AddWithValue("@estado", elVehiculo.Estado);

                sqlCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                sqlConnection.Close();

            }
        }
        public void ActualizarVehiculo(Procedimientos vehiculo)
        {
            try
            {
                string query = @"EXEC ModificarVehiculo @marca,@modelo,@placa,@color,@estado,@idVehiculo";

                sqlConnection.Open();


                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@idVehiculo", elVehiculo.VehiculoID);
                sqlCommand.Parameters.AddWithValue("@marca", elVehiculo.Marca);
                sqlCommand.Parameters.AddWithValue("@modelo", elVehiculo.Modelo);
                sqlCommand.Parameters.AddWithValue("@placa", elVehiculo.Marca);
                sqlCommand.Parameters.AddWithValue("@color", elVehiculo.Color);
                sqlCommand.Parameters.AddWithValue("@estado", elVehiculo.Estado);

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                sqlConnection.Close();

            }
        }


        public void EliminarVehiculo(Procedimientos vehiculo)
        {
            try
            {
                string query = @"EXEC EliminarVehiculo @estado, @idvehiculo ";

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@idVehiculo", elVehiculo.VehiculoID);
                sqlCommand.Parameters.AddWithValue("@estado", 3);

                sqlCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }





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

        Salida salidas = new Salida();


        public void infoFormulario(int operacion, string IDsalida, string Cantidad, string Total, string FSalida, string Detalle, int cmbIdMineral)
        {

            if (operacion == 1)
            {
                salidas.IDsalida = Convert.ToInt32(IDsalida);
            }

            salidas.Cantidad = Convert.ToDecimal(Cantidad);
            salidas.DetalleSalida = Detalle;

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

        public void txtTotal(string Cantidad, string Precio, string Total)
        {
            try
            {
                if (Cantidad == string.Empty)
                {
                    MessageBox.Show("Favor, ingresar una cantidad a calcular!");
                }
                else
                {

                    decimal precio, cantidad, total;

                    precio = Convert.ToDecimal(Precio, CultureInfo.InvariantCulture);
                    cantidad = Convert.ToDecimal(Cantidad, CultureInfo.InvariantCulture);

                    total = precio * cantidad;
                    Total = total.ToString("0000.00", CultureInfo.InvariantCulture);

                }
            }
            catch (Exception)
            {
                MessageBoxResult result = MessageBox.Show("Error no ingresar mas de 1 punto",
                      "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                Cantidad = "";
            }



        }
        public bool VerificacionDeDatos(string cantidad, string precio, object mineral)
        {

            if (cantidad == string.Empty || precio == string.Empty || mineral == null)
            {
                MessageBoxResult result = MessageBox.Show("Por favor!, Verifique que las casillas" +
                    " contengan la infromación requerida!",
                   "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }



    }
}