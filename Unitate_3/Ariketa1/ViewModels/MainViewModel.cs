using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;
using Ariketa1.Models;

namespace Ariketa1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<SeatViewModel> BusSeats { get; set; }
        public ObservableCollection<SeatViewModel> TrainSeats { get; set; }
        public ObservableCollection<SeatViewModel> PlaneSeats { get; set; }

        public ObservableCollection<SeatViewModel> Seats { get; set; }

        public ObservableCollection<Reservation> Reservations { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public string FilePath { get; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Ariketa1", "reservations.json");


        public MainViewModel()
        {
            BusSeats = new ObservableCollection<SeatViewModel>();
            TrainSeats = new ObservableCollection<SeatViewModel>();
            PlaneSeats = new ObservableCollection<SeatViewModel>();
            Seats = new ObservableCollection<SeatViewModel>();
            Reservations = new ObservableCollection<Reservation>();

            LoadReservations();
            LoadSeats();
        }

        public void LoadSeats()
        {
            BusSeats.Clear();
            TrainSeats.Clear();
            PlaneSeats.Clear();
            Seats.Clear();

            GenerateSeats(BusSeats, "Autobusa", 10, 2);

            GenerateSeats(TrainSeats, "Trena", 10, 4);

            GenerateSeats(PlaneSeats, "Hegazkina", 20, 6);

            foreach (var s in BusSeats) Seats.Add(s);
            foreach (var s in TrainSeats) Seats.Add(s);
            foreach (var s in PlaneSeats) Seats.Add(s);

            foreach (var reservation in Reservations)
            {
                var seat = Seats.FirstOrDefault(x => x.Id == reservation.SeatId);
                if (seat != null)
                    seat.IsOccupied = true;
            }
        }


        private void GenerateSeats(ObservableCollection<SeatViewModel> collection, string zone, int rows, int cols)
        {
            for (int r = 1; r <= rows; r++)
            {
                for (int c = 1; c <= cols; c++)
                {
                    string id = $"{zone[0]}{r:D2}{(char)('A' + c - 1)}";
                    collection.Add(new SeatViewModel
                    {
                        Id = id,
                        Zone = zone,
                        IsOccupied = false
                    });
                }
            }
        }

        public void SaveReservations()
        {
            Reservations.Clear();

            foreach (var seat in Seats.Where(s => s.IsOccupied))
            {
                Reservations.Add(new Reservation
                {
                    SeatId = seat.Id,
                    Zone = seat.Zone,
                    Date = seat.ReservationDateTime ?? DateTime.Now,
                    ReservedBy = seat.ReservedBy
                });
            }

            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            var json = JsonConvert.SerializeObject(Reservations, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        public void LoadReservations()
        {
            if (!File.Exists(FilePath)) return;

            var json = File.ReadAllText(FilePath);
            var data = JsonConvert.DeserializeObject<ObservableCollection<Reservation>>(json);
            if (data != null)
            {
                Reservations.Clear();
                foreach (var r in data)
                    Reservations.Add(r);
            }
        }

        public bool ValidateFutureDate(DateTime date)
        {
            return date >= DateTime.Now;
        }
    }
}
