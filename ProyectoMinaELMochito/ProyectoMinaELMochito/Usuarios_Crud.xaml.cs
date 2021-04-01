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

namespace ProyectoMinaELMochito
{
    /// <summary>
    /// Lógica de interacción para Usuarios_Crud.xaml
    /// </summary>
    public partial class Usuarios_Crud : Window
    {

        private User user = new User();
        private List<User> users;
        public Usuarios_Crud()
        {
            InitializeComponent();
            ObtenerHabitaciones();
        }

        private void ObtenerHabitaciones()
        {
            users = user.MostrarUsuario();

            dtgriduser.DisplayMemberPath = "id";
            dtgriduser.DisplayMemberPath = "Nombre Completo";
            dtgriduser.DisplayMemberPath = "UserName";
            dtgriduser.DisplayMemberPath = "Password";
            dtgriduser.DisplayMemberPath = "Rol";
            dtgriduser.DisplayMemberPath = "Estado";
            dtgriduser.SelectedValuePath = "id";

            dtgriduser.ItemsSource = users;
         
        }
    }
}
