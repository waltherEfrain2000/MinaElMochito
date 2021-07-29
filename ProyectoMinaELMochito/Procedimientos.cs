using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar los namespaces requeridos
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using System.Windows;


namespace ProyectoMinaELMochito
{

    class Procedimientos : Window
    {

        // conexion con clases
        conexion cn = new conexion();
        Empleado empleado = new Empleado();
        Mineralinventario mineralinventario = new Mineralinventario();




        public void CrearMarca(string Marca)
        {
            conexion cn = new conexion();

            try
            {
                SqlCommand crearMarca = new SqlCommand("AdministrarMarca", cn.Conectarbd);
                crearMarca.CommandType = CommandType.StoredProcedure;

                crearMarca.Parameters.AddWithValue("@TipoConsulta", 1);
                crearMarca.Parameters.AddWithValue("@idMarca", 1);
                crearMarca.Parameters.AddWithValue("@nombreMarca", Marca);

                cn.abrir();

                crearMarca.ExecuteNonQuery();
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


        public void ActualizarMarca(string Marca, int idMarca)
        {
            conexion cn = new conexion();

            try
            {
                SqlCommand crearMarca = new SqlCommand("AdministrarMarca", cn.Conectarbd);
                crearMarca.CommandType = CommandType.StoredProcedure;

                crearMarca.Parameters.AddWithValue("@TipoConsulta", 3);
                crearMarca.Parameters.AddWithValue("@idMarca", idMarca);
                crearMarca.Parameters.AddWithValue("@nombreMarca", Marca);

                cn.abrir();

                crearMarca.ExecuteNonQuery();
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


        public void CrearModelo(string Modelo, int Marca)
        {
            conexion cn = new conexion();

            try
            {
                SqlCommand crearMarca = new SqlCommand("AdministrarModelo", cn.Conectarbd);
                crearMarca.CommandType = CommandType.StoredProcedure;

                crearMarca.Parameters.AddWithValue("@TipoConsulta", 1);
                crearMarca.Parameters.AddWithValue("@idMarca", Marca);
                crearMarca.Parameters.AddWithValue("@idModelo", 1);
                crearMarca.Parameters.AddWithValue("@nombreModelo", Modelo);

                cn.abrir();

                crearMarca.ExecuteNonQuery();
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

        public void ActualizarModelo(int idModelo, int idMarca, string modelo)
        {
            conexion cn = new conexion();

            try
            {
                SqlCommand crearMarca = new SqlCommand("AdministrarModelo", cn.Conectarbd);
                crearMarca.CommandType = CommandType.StoredProcedure;

                crearMarca.Parameters.AddWithValue("@TipoConsulta", 3);
                crearMarca.Parameters.AddWithValue("@idMarca", idMarca);
                crearMarca.Parameters.AddWithValue("@idModelo", idModelo);
                crearMarca.Parameters.AddWithValue("@nombreModelo", modelo);

                cn.abrir();

                crearMarca.ExecuteNonQuery();
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




        // ----------------------------------    PROCEDIMIENTOS PARA VEHICULOS  -------------------------------- //

        //private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        //private SqlConnection sqlConnection = new SqlConnection(connectionString);
        private Vehiculo elVehiculo = new Vehiculo();

        public void CrearVehiculo(int idModelo, int idMarca, string placa, string color, int estado)
        {
            conexion cn = new conexion();

            try
            {
                SqlCommand crearMarca = new SqlCommand("AdministrarVehiculo", cn.Conectarbd);
                crearMarca.CommandType = CommandType.StoredProcedure;

                crearMarca.Parameters.AddWithValue("@TipoConsulta", 2);
                crearMarca.Parameters.AddWithValue("@idVehiculo", 1);
                crearMarca.Parameters.AddWithValue("@idModelo", idModelo);
                crearMarca.Parameters.AddWithValue("@idMarca", idMarca);
                crearMarca.Parameters.AddWithValue("@placa", placa);
                crearMarca.Parameters.AddWithValue("@color", color);
                crearMarca.Parameters.AddWithValue("@estado", estado);

                cn.abrir();

                crearMarca.ExecuteNonQuery();
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
        public void ActualizarVehiculo(int idVehiculo, int idModelo, int idMarca, string placa, string color, int estado)
        {
            conexion cn = new conexion();

            try
            {
                SqlCommand crearMarca = new SqlCommand("AdministrarVehiculo", cn.Conectarbd);
                crearMarca.CommandType = CommandType.StoredProcedure;

                crearMarca.Parameters.AddWithValue("@TipoConsulta", 3);
                crearMarca.Parameters.AddWithValue("@idVehiculo", idVehiculo);
                crearMarca.Parameters.AddWithValue("@idModelo", idModelo);
                crearMarca.Parameters.AddWithValue("@idMarca", idMarca);
                crearMarca.Parameters.AddWithValue("@placa", placa);
                crearMarca.Parameters.AddWithValue("@color", color);
                crearMarca.Parameters.AddWithValue("@estado", estado);

                cn.abrir();

                crearMarca.ExecuteNonQuery();
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


        public void EliminarVehiculo(int idVehiculo)
        {
            conexion cn = new conexion();

            try
            {
                SqlCommand crearMarca = new SqlCommand("AdministrarVehiculo", cn.Conectarbd);
                crearMarca.CommandType = CommandType.StoredProcedure;

                crearMarca.Parameters.AddWithValue("@TipoConsulta", 4);
                crearMarca.Parameters.AddWithValue("@idVehiculo", idVehiculo);
                crearMarca.Parameters.AddWithValue("@idModelo", 1);
                crearMarca.Parameters.AddWithValue("@idMarca", 1);
                crearMarca.Parameters.AddWithValue("@placa", 1);
                crearMarca.Parameters.AddWithValue("@color", 1);
                crearMarca.Parameters.AddWithValue("@estado", 2);

                cn.abrir();

                crearMarca.ExecuteNonQuery();
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
            //salidas.DetalleSalida = Detalle;

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