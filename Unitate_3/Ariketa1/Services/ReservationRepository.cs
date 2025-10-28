using Ariketa1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Ariketa1.Services
{
    public class ReservationRepository
    {
        private readonly string _filePath;
        public ReservationRepository(string filePath = null)
        {
            _filePath = filePath ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SistemaReservaAsientos", "reservations.json");
            var dir = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        }


        public List<Reservation> Load()
        {
            try
            {
                if (!File.Exists(_filePath)) return new List<Reservation>();
                var txt = File.ReadAllText(_filePath);
                return JsonConvert.DeserializeObject<List<Reservation>>(txt) ?? new List<Reservation>();
            }
            catch
            {
                return new List<Reservation>();
            }
        }


        public void Save(List<Reservation> reservations)
        {
            var txt = JsonConvert.SerializeObject(reservations, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_filePath, txt);
        }
    }
}
