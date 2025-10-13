using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TareasWpf.Models;

namespace AtazaKudeatzailea.Infrastructure
{
    public class TaskRepository
    {
        private readonly string folderPath;
        private readonly string filePath;

        public TaskRepository(string baseFolder)
        {
            folderPath = baseFolder;
            filePath = Path.Combine(folderPath, "tareas.xml");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (!File.Exists(filePath))
            {
                var doc = new XDocument(new XElement("Tareas"));
                doc.Save(filePath);
            }
        }

        public ObservableCollection<Tarea> LoadAll()
        {
            var doc = XDocument.Load(filePath);

            var tareas = doc.Root!
                .Elements("Tarea")
                .Select(x => new Tarea
                {
                    Id = (int?)x.Attribute("id") ?? 0,
                    Titulua = (string?)x.Element("Titulua") ?? string.Empty,
                    Lehentasuna = Enum.TryParse((string?)x.Element("Lehentasuna"), ignoreCase: true, out LehentasunaMaila pr) ? pr : LehentasunaMaila.Baxua,
                    AzkenEguna = DateTime.TryParse((string?)x.Element("AzkenEguna"), out var d) ? d : DateTime.Today,
                    Egoera = string.Equals((string?)x.Element("Egoera"), "Eginda", StringComparison.OrdinalIgnoreCase)
                });

            return new ObservableCollection<Tarea>(tareas);
        }

        public void SaveAll(ObservableCollection<Tarea> tareas)
        {
            var doc = new XDocument(
                new XElement("Tareas",
                    tareas.Select(t =>
                        new XElement("Tarea",
                            new XAttribute("id", t.Id),
                            new XElement("Titulua", t.Titulua),
                            new XElement("Lehentasuna", t.Lehentasuna),
                            new XElement("AzkenEguna", t.AzkenEguna.ToString("yyyy-MM-dd")),
                            new XElement("Egoera", t.Egoera ? "Eginda" : "Egin gabe")
                        )
                    )
                )
            );

            doc.Save(filePath);
        }

        public int NextId(ObservableCollection<Tarea> tareas)
        {
            return tareas.Any() ? tareas.Max(t => t.Id) + 1 : 1;
        }
    }
}
