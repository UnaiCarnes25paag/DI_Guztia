using System;
using System.Windows;
using Microsoft.VisualBasic;

namespace Ariketa8
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEjecutar_Click(object sender, RoutedEventArgs e)
        {
            DateTime ahora = DateTime.Now;
            txtAhora.Text = ahora.ToString("dd/MM/yyyy HH:mm:ss");
            txtHoy.Text = ahora.ToString("dd/MM/yyyy");
            txtHoraHoy.Text = ahora.ToString("HH:mm:ss");

            string fechaStr = Interaction.InputBox("Ingrese una fecha en formato dd/mm/yyyy:", "Fecha");
            if (!DateTime.TryParseExact(fechaStr, "dd/MM/yyyy", null,
                                        System.Globalization.DateTimeStyles.None, out DateTime fechaUsuario))
            {
                MessageBox.Show("La fecha no es válida. Usa formato dd/MM/yyyy.");
                return;
            }

            string mesesStr = Interaction.InputBox("Ingrese el número de meses a sumar:", "Meses");
            if (!int.TryParse(mesesStr, out int meses))
            {
                MessageBox.Show("El número de meses no es válido.");
                return;
            }

            DateTime sumaFechas = fechaUsuario.AddMonths(meses);
            txtSumaFechas.Text = sumaFechas.ToString("dd/MM/yyyy");

            int diferenciaDias = Math.Abs((ahora.Date - fechaUsuario.Date).Days);
            txtDiferenciaFechas.Text = $"{diferenciaDias} días";
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtAhora.Clear();
            txtHoy.Clear();
            txtHoraHoy.Clear();
            txtSumaFechas.Clear();
            txtDiferenciaFechas.Clear();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
