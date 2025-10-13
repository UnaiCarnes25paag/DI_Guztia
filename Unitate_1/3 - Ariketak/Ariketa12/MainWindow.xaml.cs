using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ariketa12
{
    public partial class MainWindow : Window
    {
        private const double PRECIO_DESAYUNO = 3.0;
        private const double PRECIO_COMIDA = 9.0;
        private const double PRECIO_CENA = 15.5;
        private const double PRECIO_KM = 0.25;
        private const double PRECIO_HORA_VIAJE = 18.0;
        private const double PRECIO_HORA_TRABAJO = 42.0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Recalcular(object sender, RoutedEventArgs e)
        {
            double totalDietas = 0;
            if (chkDesayuno.IsChecked == true) totalDietas += PRECIO_DESAYUNO;
            if (chkComida.IsChecked == true) totalDietas += PRECIO_COMIDA;
            if (chkCena.IsChecked == true) totalDietas += PRECIO_CENA;
            lblTotalDietas.Text = $"Total: {totalDietas:F2} €";

            double km = ParseDouble(txtKm.Text);
            double horasViaje = ParseDouble(txtHorasViaje.Text);
            double totalViajes = km * PRECIO_KM + horasViaje * PRECIO_HORA_VIAJE;
            lblTotalViajes.Text = $"Total: {totalViajes:F2} €";

            double horasTrabajo = ParseDouble(txtHorasTrabajo.Text);
            double totalTrabajo = horasTrabajo * PRECIO_HORA_TRABAJO;
            lblTotalTrabajo.Text = $"Total: {totalTrabajo:F2} €";

            double totalGlobal = totalDietas + totalViajes + totalTrabajo;
            lblTotalGlobal.Text = $"TOTAL: {totalGlobal:F2} €";
        }

        private double ParseDouble(string text)
        {
            if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
                return value;
            return 0;
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            chkDesayuno.IsChecked = false;
            chkComida.IsChecked = false;
            chkCena.IsChecked = false;

            txtKm.Text = "";
            txtHorasViaje.Text = "";
            txtHorasTrabajo.Text = "";

            lblTotalDietas.Text = "Total: 0 €";
            lblTotalViajes.Text = "Total: 0 €";
            lblTotalTrabajo.Text = "Total: 0 €";
            lblTotalGlobal.Text = "TOTAL: 0 €";
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                (Keyboard.FocusedElement as UIElement)?.MoveFocus(request);
            }
        }
    }
}
