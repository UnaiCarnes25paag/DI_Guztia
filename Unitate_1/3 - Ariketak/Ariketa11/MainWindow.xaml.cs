using System.Windows;

namespace Ariketa11
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MostrarDatos();
        }

        private void MostrarDatos()
        {
            lblNombre.Text = $"Nombre: {Global.Nombre}";
            lblApellido1.Text = $"Apellido1: {Global.Apellido1}";
            lblApellido2.Text = $"Apellido2: {Global.Apellido2}";
            lblDni.Text = $"DNI: {Global.Dni}";
        }
    }
}
