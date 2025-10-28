using System;
using System.ComponentModel;

namespace Ariketa1.ViewModels
{
    public class SeatViewModel : INotifyPropertyChanged
    {
        public string Id { get; set; } = string.Empty;
        public string Zone { get; set; } = string.Empty;

        private bool _isOccupied;
        public bool IsOccupied
        {
            get => _isOccupied;
            set { _isOccupied = value; OnPropertyChanged(nameof(IsOccupied)); OnPropertyChanged(nameof(StatusText)); }
        }

        private string? _reservedBy;
        public string? ReservedBy
        {
            get => _reservedBy;
            set { _reservedBy = value; OnPropertyChanged(nameof(ReservedBy)); }
        }

        private DateTime? _reservationDateTime;
        public DateTime? ReservationDateTime
        {
            get => _reservationDateTime;
            set { _reservationDateTime = value; OnPropertyChanged(nameof(ReservationDateTime)); }
        }

        public string StatusText => IsOccupied ? "Okupatuta" : "Librea";

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
