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
        Conexion cn = new Conexion();
        Empleado empleado = new Empleado();
        Mineralinventario mineralinventario = new Mineralinventario();

        // ----------------------------------    PROCEDIMIENTOS PARA VEHICULOS  -------------------------------- //

        //private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        //private SqlConnection sqlConnection = new SqlConnection(connectionString);
        private Vehiculo elVehiculo = new Vehiculo();

        public void CrearVehiculo(Procedimientos vehiculo)
        {
            try
            {
                string query = @"EXEC InsertarVehiculo @marca,@modelo,@placa,@color,@estado";

                cn.abrir();


                SqlCommand sqlCommand = new SqlCommand(query, cn.Conectarbd);

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
                cn.cerrar();
            }
        }
        public void ActualizarVehiculo(Procedimientos vehiculo)
        {
            try
            {
                string query = @"EXEC ModificarVehiculo @marca,@modelo,@placa,@color,@estado,@idVehiculo";

                cn.abrir();


                SqlCommand sqlCommand = new SqlCommand(query, cn.Conectarbd);

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
                cn.cerrar();
            }
        }


        public void EliminarVehiculo(Procedimientos vehiculo)
        {
            try
            {
                string query = @"EXEC EliminarVehiculo @estado, @idvehiculo ";

                cn.abrir();

                SqlCommand sqlCommand = new SqlCommand(query, cn.Conectarbd);

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
                cn.cerrar();
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

        Salida salidas = new Salida();


        public void infoFormulario(int operacion, string IDsalida, string Cantidad, string Total, string FSalida, string Detalle, int cmbIdMineral)
        {

            if (operacion == 1)
            {
                salidas.IDsalida = Convert.ToInt32(IDsalida);
            }

            salidas.Cantidad = Convert.ToDecimal(Cantidad);
            salidas.Total = Convert.ToDecimal(Total);
            salidas.FechaSalida = Convert.ToDateTime(FSalida);
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
        // ----------------------------------    PROCEDIMIENTOS PARA PRODUCCION  -------------------------------- //
        public void VerificarP(string txtCantidad, string txtPrecio, string txtTotal)
        {
            double Valor = 0;
            try
            {
                if (txtCantidad == string.Empty || (Convert.ToDouble(txtCantidad) == Valor))
                {
                    txtCantidad = " ";
                    txtTotal = " ";
                }
                else if (txtPrecio == string.Empty)
                {
                    MessageBoxResult result = MessageBox.Show("Debe seleccionar un mineral",
                      "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtCantidad = "";
                }
                else
                {
                    double Total;
                    double Cantidad, precio;
                    Cantidad = Convert.ToDouble(txtCantidad);
                    precio = Convert.ToDouble(txtPrecio);
                    Total = Cantidad * precio;

                }
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show("No puede ingresar dos puntos deguidos",
                      "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCantidad = "";
            }
        }
    }
}