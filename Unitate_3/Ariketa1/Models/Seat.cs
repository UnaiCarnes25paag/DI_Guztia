using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ariketa1.Models
{
    public class Seat
    {
        public string Id { get; set; }
        public ZoneType Zone { get; set; }
        public bool IsOccupied { get; set; }
        public string ReservedBy { get; set; }
        public DateTime? ReservationDateTime { get; set; }
    }
}
