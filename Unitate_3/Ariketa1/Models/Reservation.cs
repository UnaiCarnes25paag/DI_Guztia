using System;

namespace Ariketa1.Models
{
    public class Reservation
    {
        public string SeatId { get; set; } = string.Empty;
        public string Zone { get; set; } = string.Empty;
        public string? ReservedBy { get; set; }
        public DateTime Date { get; set; }
    }
}
