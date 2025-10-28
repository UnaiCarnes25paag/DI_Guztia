using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Ariketa1.ViewModels;

namespace Ariketa1.Views
{
    public partial class ReservaDialog : Window
    {
        public string Nombre { get; private set; }
        public DateTime FechaReserva { get; private set; }


        public ReservaDialog(string seatId)
        {
            InitializeComponent();
            this.Title = $"Erreserbatu {seatId}";
            DpFecha.SelectedDate = DateTime.Now.Date;
        }


        private void BtnCancelar_Click(object sender, RoutedEventArgs e) { this.DialogResult = false; }


        private void BtnReservar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNombre.Text)) { MessageBox.Show("Izen bat eman.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning); return; }
            if (!DpFecha.SelectedDate.HasValue) { MessageBox.Show("Hautatu data bat.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning); return; }
            if (!TimeSpan.TryParse(TxtHora.Text, out var hora)) { MessageBox.Show("Ordu baliogabea. Formatua:HH:mm.", "Errorea", MessageBoxButton.OK, MessageBoxImage.Warning); return; }


            var dt = DpFecha.SelectedDate.Value.Date + hora;
            if (dt <= DateTime.Now) { MessageBox.Show("Data/ordua etorkizunean egon behar da.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning); return; }


            Nombre = TxtNombre.Text.Trim();
            FechaReserva = dt;
            this.DialogResult = true;
        }
    }
}