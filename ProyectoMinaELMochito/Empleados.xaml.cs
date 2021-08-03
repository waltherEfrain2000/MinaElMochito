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
// Agregar los namespaces requeridos
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;


namespace ProyectoMinaELMochito
{
    /// <summary>
    /// Lógica de interacción para Empleados.xaml
    /// </summary>

    public partial class Empleados : Window
    {

        // Variables miembros
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);
        //Hola

        private Empleado empleado = new Empleado();
        public Empleados()
        {
            InitializeComponent();
            MostrarEmpleado();
            MostrarCargos();
            MostrarGeneros();
            botonfecha.Content = string.Format("{0}", DateTime.Now.ToString());
            //cmbGenero.Items.Add("F");
            //cmbGenero.Items.Add("M");
            //cmbCargo.Items.Add("Gerente");
        }
        private void LimpiarCasillas()
        {
            txtEmpleadoID.Text = string.Empty;
            txtIdentidad.Text = string.Empty;
            txtNombreCompleto.Text = string.Empty;
            txtEdad.Text = string.Empty;
            txtSalario.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            dpFechaNacimiento.Text = string.Empty;

            cmbGenero.SelectedValue = null;
            cmbCargo.SelectedValue = null;
            edicionDeCasillas(false, 0);
        }

        private bool VerificarCamposLlenos()
        {
            if (txtIdentidad.Text == string.Empty || txtNombreCompleto.Text == string.Empty || txtSalario.Text == string.Empty || txtDireccion.Text == string.Empty)
            {
                MessageBox.Show("Por favor ingresa todos los valores en las cajas de texto");
                return false;
            }
            else if (dpFechaNacimiento.SelectedDate == null)
            {
                MessageBox.Show("Por favor, ingresar su fecha de nacimiento");
                return false;
            }
            else if (cmbGenero.SelectedValue == null)
            {
                MessageBox.Show("Por favor selecciona el Genero del empleado");
                return false;
            }
            else if (cmbCargo.SelectedValue == null)
            {
                MessageBox.Show("Por favor selecciona el Cargo del empleado");
                return false;
            }

            //else if (Convert.ToInt32(txtEdad.Text) < 18 || Convert.ToInt32(txtEdad.Text) > 100)
            //{
            //    MessageBox.Show("Por favor selecciona una edad valida!");
            //    return false;
            //}

            return true;

        }
        private void ExtraerInformacionFormulario(int operacion)
        {
            //entra si va extraer informacion para actualizar
            if (operacion == 1)
            {
                empleado.EmpledoID = Convert.ToInt32(txtEmpleadoID.Text);
            }

            empleado.Identidad = txtIdentidad.Text;

            char[] delimitador = { ' ' };
            string[] trozos = txtNombreCompleto.Text.Split(delimitador);

            empleado.PrimerNombre = trozos[0];
            empleado.SegundoNombre = trozos[1];
            empleado.PrimerApellido = trozos[2];
            empleado.SegundoApellido = trozos[3];

            empleado.FechaNacimiento = dpFechaNacimiento.Text;

            switch (cmbGenero.SelectedIndex)
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

            empleado.Direccion = txtDireccion.Text;

            switch (cmbCargo.SelectedIndex)
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


            decimal monto = Convert.ToDecimal(txtSalario.Text);


            if (!decimal.TryParse(txtSalario.Text, out monto))
            {
                MessageBox.Show("Ingrese un monto válido...");
                return; //Salimos del método o evento
            }

            empleado.Salario = Convert.ToDecimal(txtSalario.Text);
            //empleado.Estado = "activo";

            //txtSalario.Text = salario.ToString("0000.00", CultureInfo.InvariantCulture);

        }


        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarCamposLlenos())
            {
                int valor = empleado.validarIdentidad(txtIdentidad.Text);

                if (valor == 0)
                {
                    try
                    {
                        //parametro 0 por que no es actualizacion
                        ExtraerInformacionFormulario(0);
                        empleado.CrearEmpleado(empleado);

                        // Mensaje de inserción exitosa
                        MessageBox.Show("¡Empleado insertado correctamente!");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        LimpiarCasillas();
                        MostrarEmpleado();
                    }
                }
                else
                {
                    MessageBox.Show("Esta identidad ya existe...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void MostrarEmpleado()
        {
            try
            {

                string query = @"exec mostrarEmpleado";

                sqlConnection.Open();


                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.ExecuteNonQuery();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable dataTable = new DataTable("Empleados.Empleado");
                sqlDataAdapter.Fill(dataTable);
                DgvEmpleados.ItemsSource = dataTable.DefaultView;
                sqlDataAdapter.Update(dataTable);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        private void edicionDeCasillas(bool opcion, int operacion)
        {
            //operacion sirve para distingir entre actualizar y activar o inabilitar todas las casillas
            if (operacion == 0)
            {
                //txtIdentidad.IsReadOnly = opcion;
                txtNombreCompleto.IsReadOnly = opcion;
                txtEdad.IsReadOnly = opcion;
                cmbGenero.IsReadOnly = opcion;
                cmbCargo.IsReadOnly = opcion;
                txtSalario.IsReadOnly = opcion;
                txtDireccion.IsReadOnly = opcion;
            }
            else
            {
                txtNombreCompleto.IsReadOnly = opcion;
                txtEdad.IsReadOnly = opcion;
                cmbCargo.IsReadOnly = opcion;
                txtSalario.IsReadOnly = opcion;
                txtDireccion.IsReadOnly = opcion;
            }

        }

        string validarIde;
        private void DgvEmpleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView filaSeleccionada = dg.SelectedItem as DataRowView;
            if (filaSeleccionada != null)
            {
                txtEmpleadoID.Text = filaSeleccionada["Código"].ToString();
                txtIdentidad.Text = filaSeleccionada["Identidad"].ToString();
                validarIde = filaSeleccionada["Identidad"].ToString();
                txtNombreCompleto.Text = filaSeleccionada["Nombre"].ToString();
                dpFechaNacimiento.Text = filaSeleccionada["FechaNacimiento"].ToString();
                cmbGenero.Text = filaSeleccionada["Género"].ToString();
                txtDireccion.Text = filaSeleccionada["Dirección"].ToString();
                cmbCargo.Text = filaSeleccionada["Cargo"].ToString();
                txtSalario.Text = filaSeleccionada["Salario"].ToString();

                edicionDeCasillas(true, 0);
            }
        }

        private void txtIdentidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtEdad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtSalario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
            { e.Handled = false; }

            else
            { e.Handled = true; }
        }


        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarCamposLlenos())
            {
                try
                {
                    //parametro 1 por que es actualizacion
                    edicionDeCasillas(false, 1);
                    //ocultar todos los otones inecesarios
                    btnModificar.Visibility = Visibility.Hidden;
                    btnAgregar.Visibility = Visibility.Hidden;
                    btnEliminar.Visibility = Visibility.Hidden;
                    btnAceptarModificacion.Visibility = Visibility.Visible;
                    btnCancelarModificacion.Visibility = Visibility.Visible;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void btnAceptarModificacion_Click(object sender, RoutedEventArgs e)
        {

            if (VerificarCamposLlenos())
            {
                btnModificar.Visibility = Visibility.Visible;
                btnAgregar.Visibility = Visibility.Visible;
                btnEliminar.Visibility = Visibility.Visible;
                btnAceptarModificacion.Visibility = Visibility.Hidden;
                btnCancelarModificacion.Visibility = Visibility.Hidden;

                if (txtIdentidad.Text == validarIde)
                {
                    try
                    {
                        //parametro 1 por que es actualizacion
                        ExtraerInformacionFormulario(1);
                        empleado.ActualizarEmpleado(empleado);


                        // Mensaje de inserción exitosa
                        MessageBox.Show("¡Empleado modificado correctamente!");


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        LimpiarCasillas();
                        MostrarEmpleado();
                    }
                }
                else
                {
                    int valor = empleado.validarIdentidad(txtIdentidad.Text);

                    if (valor == 0)
                    {
                        try
                        {
                            //parametro 1 por que es actualizacion
                            ExtraerInformacionFormulario(1);
                            empleado.ActualizarEmpleado(empleado);


                            // Mensaje de inserción exitosa
                            MessageBox.Show("¡Empleado modificado correctamente!");


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);

                        }
                        finally
                        {
                            LimpiarCasillas();
                            MostrarEmpleado();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Esta identidad ya existe...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

            }
        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarCamposLlenos())
            {
                try
                {
                    //parametro 1 por que es actualizacion
                    edicionDeCasillas(true, 0);
                    //ocultar todos los otones inecesarios
                    btnModificar.Visibility = Visibility.Hidden;
                    btnAgregar.Visibility = Visibility.Hidden;
                    btnEliminar.Visibility = Visibility.Hidden;
                    btnAceptarEliminacion.Visibility = Visibility.Visible;
                    btnCancelarEliminacion.Visibility = Visibility.Visible;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void btnAceptarEliminacion_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarCamposLlenos())
            {
                //ocultar todos los otones inecesarios
                btnModificar.Visibility = Visibility.Visible;
                btnAgregar.Visibility = Visibility.Visible;
                btnEliminar.Visibility = Visibility.Visible;
                btnAceptarEliminacion.Visibility = Visibility.Hidden;
                btnCancelarEliminacion.Visibility = Visibility.Hidden;
                try
                {
                    //parametro 1 por que es actualizacion
                    ExtraerInformacionFormulario(1);
                    empleado.EliminarEmpleado(empleado);

                    // Mensaje de inserción exitosa
                    MessageBox.Show("¡Empleado Eliminado correctamente!");


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    LimpiarCasillas();
                    MostrarEmpleado();
                }
            }
        }
        private void MostrarBotonesPrincipales()
        {
            btnModificar.Visibility = Visibility.Visible;
            btnAgregar.Visibility = Visibility.Visible;
            btnEliminar.Visibility = Visibility.Visible;
            btnAceptarModificacion.Visibility = Visibility.Hidden;
            btnCancelarModificacion.Visibility = Visibility.Hidden;
            btnAceptarEliminacion.Visibility = Visibility.Hidden;
            btnCancelarEliminacion.Visibility = Visibility.Hidden;
            LimpiarCasillas();
            edicionDeCasillas(false, 0);
        }

        private void btnCancelarModificacion_Click(object sender, RoutedEventArgs e)
        {
            MostrarBotonesPrincipales();
        }

        private void btnCancelarEliminacion_Click(object sender, RoutedEventArgs e)
        {
            MostrarBotonesPrincipales();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCasillas();
        }



        private void MostrarCargos()
        {
            cmbCargo.ItemsSource = empleado.LlenarComboBoxCargo();
            cmbCargo.DisplayMemberPath = "NombreCargo";
            cmbCargo.SelectedValuePath = "Cargo";

        }

        private void MostrarGeneros()
        {
            cmbGenero.ItemsSource = empleado.LlenarComboBoxGenero();
            cmbGenero.DisplayMemberPath = "NombreGenero";
            cmbGenero.SelectedValuePath = "Genero";

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            Empleados sld = new Empleados();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            Vehiculos sld = new Vehiculos();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_2(object sender, RoutedEventArgs e)
        {
            Inventario_Mineral sld = new Inventario_Mineral();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_3(object sender, RoutedEventArgs e)
        {
            EntradasHistoricas sld = new EntradasHistoricas();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_4(object sender, RoutedEventArgs e)
        {
            Salidas sld = new Salidas();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_5(object sender, RoutedEventArgs e)
        {
            ViajesInternos sld = new ViajesInternos();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_6(object sender, RoutedEventArgs e)
        {
            Usuarios_Crud sld = new Usuarios_Crud();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_7(object sender, RoutedEventArgs e)
        {
            menuPrincipal sld = new menuPrincipal();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            menuPrincipal sld = new menuPrincipal();
            sld.Show();
            this.Close();
        }

        private void ListViewItem_Selected_8(object sender, RoutedEventArgs e)
        {
            Login sld = new Login();
            sld.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtNombreCompleto_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtNombreCompleto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void cmbGenero_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtSalario_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtSalario.MaxLength = 12;
            double Valor = 0;
            try
            {
                if (txtSalario.Text == string.Empty || (Convert.ToDouble(txtSalario.Text) == Valor))
                {
                    txtSalario.Text = "";
                }
                //else if (txtSalario.Text == string.Empty)
                //{
                //    MessageBoxResult result = MessageBox.Show("Debe seleccionar un mineral",
                //      "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    txtSalario.Text = "";
                //}
                else
                {
                    //double Total;
                    //double Cantidad, precio;
                    //Cantidad = Convert.ToDouble(txtSalario.Text);
                    //precio = Convert.ToDouble(txtSalario.Text);
                    //Total = Cantidad * precio;
                    //txtSalario.Text = Cantidad.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show("No puede ingresar dos puntos deguidos",
                      "Confirmar", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtSalario.Text = "";
            }
        }

        private void DgvEmpleados_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            // referencia: https://social.msdn.microsoft.com/Forums/vstudio/en-US/a5f90126-d509-46c2-93b6-2affd09b13c4/wpf-format-datagrid-column-as-currency-wpf?forum=wpf

            if (e.PropertyType == typeof(DateTime))
            {
                ((DataGridTextColumn)e.Column).Binding.StringFormat = "dd/MM/yyyy";
            }

            if (e.PropertyType == typeof(Decimal))
            {
                ((DataGridTextColumn)e.Column).Binding.StringFormat = "L00.00";
            }
        }
    }
}