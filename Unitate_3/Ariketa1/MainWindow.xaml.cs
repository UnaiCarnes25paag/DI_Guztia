using Ariketa1.Models;
using Ariketa1.ViewModels;
using System.IO;
using System.Linq;
using System.Windows;

namespace Ariketa1
{
    public partial class MainWindow : Window
    {
        public MainViewModel Vm => DataContext as MainViewModel;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void AddReservation(SeatViewModel seatVm)
        {
            if (seatVm == null) return;
            var existing = Vm.Reservations.FirstOrDefault(x => x.SeatId == seatVm.Id && x.Zone == seatVm.Zone);
            if (existing != null) Vm.Reservations.Remove(existing);
            Vm.Reservations.Add(new Reservation { SeatId = seatVm.Id, Zone = seatVm.Zone, ReservedBy = seatVm.ReservedBy, Date = seatVm.ReservationDateTime.Value });
            Vm.SaveReservations();
        }

        public void RemoveReservation(SeatViewModel seatVm)
        {
            var existing = Vm.Reservations.FirstOrDefault(x => x.SeatId == seatVm.Id && x.Zone == seatVm.Zone);
            if (existing != null) Vm.Reservations.Remove(existing);
            Vm.SaveReservations();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel vm)
            {
                vm.SaveReservations();
                MessageBox.Show("Erreserbak ongi gorde dira.", "Éxito",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void BtnRecargar_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel vm)
            {
                vm.LoadReservations();
                vm.LoadSeats();
                MessageBox.Show("Erreserbak artxibotik berriro kargatu dira.", "Karga osoa",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnBorrarTodo_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainViewModel;
            if (vm == null) return;

            if (MessageBox.Show("Ziur zaude erreserba guztiak ezabatu nahi dituzula?",
                                "Berretsi",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                vm.Reservations.Clear();
                foreach (var seat in vm.Seats)
                    seat.IsOccupied = false;

                if (File.Exists(vm.FilePath))
                    File.Delete(vm.FilePath);
            }
        }

    }
}
