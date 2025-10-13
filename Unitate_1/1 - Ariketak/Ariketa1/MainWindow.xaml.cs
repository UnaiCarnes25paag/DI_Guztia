using System.Windows;

namespace Ariketa1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                MaximizeRestore_Click(sender, e);
            }
            else
            {
                DragMove();
            }
        }

        private void Operar_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtNum1.Text, out double n1) &&
                double.TryParse(txtNum2.Text, out double n2) &&
                double.TryParse(txtNum3.Text, out double n3) &&
                double.TryParse(txtNum4.Text, out double n4))
            {
                double resultado = (n1 + 2*n2 + 3*n3 + 4*n4)/4;
                txtResultado.Text = resultado.ToString();
            }
            else
            {
                MessageBox.Show("Por favor, ingrese solo números válidos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Limpiar_Click(object sender, RoutedEventArgs e)
        {
            txtNum1.Clear();
            txtNum2.Clear();
            txtNum3.Clear();
            txtNum4.Clear();
            txtResultado.Clear();
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}