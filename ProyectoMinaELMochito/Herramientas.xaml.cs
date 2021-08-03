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
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ProyectoMinaELMochito
{
    /// <summary>
    /// Lógica de interacción para Herramientas.xaml
    /// </summary>
    public partial class Herramientas : Window
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["ProyectoMinaELMochito.Properties.Settings.MinaConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);
        Herramienta herramienta = new Herramienta();
        Validaciones validaciones = new Validaciones();
        public Herramientas()
        {
            InitializeComponent();
            MostrarHerramienta();
            botonfecha.Content = string.Format("{0}", DateTime.Now.ToString());
        }

        private void MostrarHerramienta()
        {
            try
            {

                string query = @"exec mostrarHerramientas";

                sqlConnection.Open();


                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.ExecuteNonQuery();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable dataTable = new DataTable("Minas.HeramientasyEquipos");
                sqlDataAdapter.Fill(dataTable);
                DgEquipamiento.ItemsSource = dataTable.DefaultView;
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

        private void LimpiarCasillas()
        {
            txtCodigo.Text = string.Empty;
            txtEquipamiento.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtValorUnitario.Text = string.Empty;
            pb1.Value = 0;
            pb3.Value = 0;
        }

        private bool VerificarCamposLlenos()
        {
            if (txtEquipamiento.Text == string.Empty || txtCantidad.Text == string.Empty || txtValorUnitario.Text == string.Empty)
            {
                MessageBox.Show("Por favor ingresa todos los valores en las cajas de texto");
                return false;
            }
            return true;
        }

        private void ExtraerInformacionFormulario(int operacion)
        {
            //entra si va extraer informacion para actualizar
            if (operacion == 1)
            {
                herramienta.ID = Convert.ToInt32(txtCodigo.Text);
            }

            herramienta.NombreHerramienta = txtEquipamiento.Text;
            herramienta.Cantidad = Convert.ToInt32(txtCantidad.Text);
            herramienta.PrecioUnitario = Convert.ToDecimal(txtValorUnitario.Text);
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarCamposLlenos())
            {
                int valor = herramienta.validarHerramienta(txtEquipamiento.Text);

                if (valor == 0)
                {
                    try
                    {
                        //parametro 0 por que no es actualizacion
                        ExtraerInformacionFormulario(0);
                        herramienta.CrearHerramienta(herramienta);

                        // Mensaje de inserción exitosa
                        MessageBox.Show("¡Herramienta insertada correctamente!");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        LimpiarCasillas();
                        MostrarHerramienta();
                    }
                }
                else
                {
                    MessageBox.Show("Esta herramienta ya...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarCamposLlenos())
            {
                if (txtEquipamiento.Text == equip)
                {
                    try
                    {
                        //parametro 1 por que es actualizacion
                        ExtraerInformacionFormulario(1);
                        herramienta.ActualizarHerramienta(herramienta);


                        // Mensaje de inserción exitosa
                        MessageBox.Show("¡Herramienta modificada correctamente!");


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        LimpiarCasillas();
                        MostrarHerramienta();
                    }
                }
                else
                {
                    int valor = herramienta.validarHerramienta(txtEquipamiento.Text);

                    if (valor == 0)
                    {
                        try
                        {
                            //parametro 1 por que es actualizacion
                            ExtraerInformacionFormulario(1);
                            herramienta.ActualizarHerramienta(herramienta);


                            // Mensaje de inserción exitosa
                            MessageBox.Show("¡Herramienta modificada correctamente!");


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);

                        }
                        finally
                        {
                            LimpiarCasillas();
                            MostrarHerramienta();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Esta herramienta ya existe...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }



            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarCamposLlenos())
            {
                try
                {
                    //parametro 1 por que es actualizacion
                    ExtraerInformacionFormulario(1);
                    herramienta.EliminarHerramienta(herramienta);

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
                    MostrarHerramienta();
                }
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCasillas();

        }

        string equip;

        private void DgvHerramientas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView filaSeleccionada = dg.SelectedItem as DataRowView;
            if (filaSeleccionada != null)
            {
                txtCodigo.Text = filaSeleccionada["Código"].ToString();
                txtEquipamiento.Text = filaSeleccionada["Herramienta"].ToString();
                equip = filaSeleccionada["Herramienta"].ToString();
                txtCantidad.Text = filaSeleccionada["Inventario"].ToString();
                txtValorUnitario.Text = filaSeleccionada["Precio de compra"].ToString();
                txtinversion.Text = filaSeleccionada["Herramientas en uso"].ToString();
                pb1.Value = Convert.ToInt32(txtCantidad.Text);
                //pb2.Value = Convert.ToDecimal(txtinversion.Text);
                pb3.Maximum = Convert.ToInt32(txtCantidad.Text);
                pb3.Value = Convert.ToInt32(txtinversion.Text);
            }
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

        private void ListViewItem_Selected_9(object sender, RoutedEventArgs e)
        {
            Herramientas sld = new Herramientas();
            sld.Show();
            this.Close();
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            validaciones.validarTxtSinLetras(e);
        }

        private void txtEquipamiento_KeyDown(object sender, KeyEventArgs e)
        {
            validaciones.validarTxtSinNumeros(e);
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            validaciones.validarTxtSinLetras(e);
        }

        private void txtValorUnitario_KeyDown(object sender, KeyEventArgs e)
        {
            validaciones.validarTxtSinLetrasConCommas(e);
        }

        private void btnAsignar_Click(object sender, RoutedEventArgs e)
        {
            AsignarHerramientas asignarHerramientas = new AsignarHerramientas();
            asignarHerramientas.Show();
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            MostrarHerramienta();
        }

        private void pb1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void DgEquipamiento_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                ((DataGridTextColumn)e.Column).Binding.StringFormat = "dd/MM/yyyy";
            }

            if (e.PropertyType == typeof(Decimal))
            {
                ((DataGridTextColumn)e.Column).Binding.StringFormat = "L00.00";
            }
        }

        private void txtValorUnitario_TextChanged(object sender, TextChangedEventArgs e)
        {
            double Valor = 0;
            try
            {
                if (txtValorUnitario.Text == string.Empty || (Convert.ToDouble(txtValorUnitario.Text) == Valor))
                {
                    txtValorUnitario.Text = "";
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
                txtValorUnitario.Text = "";
            }
        }
    }
}