using System.Windows;

namespace Ariketa11
{
    public partial class VentanaSecundaria : Window
    {
        public VentanaSecundaria()
        {
            InitializeComponent();
        }

        private void GuardarDatos()
        {
            Global.Nombre = txtNombre.Text;
            Global.Apellido1 = txtApellido1.Text;
            Global.Apellido2 = txtApellido2.Text;
            Global.Dni = txtDni.Text;
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            GuardarDatos();
            MessageBox.Show("Datos guardados correctamente.");
            this.Close();
        }

        private void BtnCargarVisualizar_Click(object sender, RoutedEventArgs e)
        {
            GuardarDatos();
            var ventana = new MainWindow();
            ventana.Show();
            this.Close();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
