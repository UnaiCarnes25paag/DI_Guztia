using System.Windows;

namespace Ariketa3
{
    public partial class MainWindow : Window
    {
        private double[] numeros = new double[4];
        private int paso = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtNumero.Text, out double valor))
            {
                numeros[paso] = valor;
                paso++;
                txtNumero.Clear();

                if (paso < 4)
                {
                    lblNumero.Content = $"Numero {paso + 1}";
                    txtNumero.Focus();
                }
                else
                {
                    double resultado = (numeros[0] + (numeros[0] * numeros[1]) + (numeros[1] * numeros[2]) + (numeros[2] * numeros[3])) / 4;
                    lblResultado.Content = "Emaitza:";
                    txtResultado.Text = resultado.ToString("F2");
                    lblResultado.Visibility = Visibility.Visible;
                    txtResultado.Visibility = Visibility.Visible;
                    txtNumero.IsEnabled = false;
                    btnSiguiente.IsEnabled = false;
                }
            }
            else
            {
                MessageBox.Show("Sartu balio zuzena.", "Errorea", MessageBoxButton.OK, MessageBoxImage.Error);
                txtNumero.Focus();
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LimpiarFormulario()
        {
            paso = 0;
            lblNumero.Content = "Numero 1";
            txtNumero.Clear();
            txtNumero.IsEnabled = true;
            btnSiguiente.IsEnabled = true;
            lblResultado.Visibility = Visibility.Collapsed;
            txtResultado.Visibility = Visibility.Collapsed;
            txtResultado.Text = "";
            txtNumero.Focus();
        }
    }
}