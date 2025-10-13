using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using TareasWpf.Models;

namespace TareasWpf.Views
{
    public partial class TaskDialog : Window
    {
        public Tarea Tarea { get; private set; }
        public List<LehentasunaMaila> Lehentasunak { get; } =
            Enum.GetValues(typeof(LehentasunaMaila)).Cast<LehentasunaMaila>().ToList();

        private readonly IEnumerable<Tarea> _existingTasks;

        public TaskDialog(Tarea tarea, IEnumerable<Tarea> existingTasks)
        {
            InitializeComponent();

            _existingTasks = existingTasks;

            Tarea = new Tarea
            {
                Id = tarea.Id,
                Titulua = tarea.Titulua,
                Lehentasuna = tarea.Lehentasuna,
                AzkenEguna = tarea.AzkenEguna,
                Egoera = tarea.Egoera
            };

            DataContext = this;
        }

        private void OnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (!IsValid(Tarea))
            {
                MessageBox.Show("Datu batzuk ez dira zuzenak.\n\nEgiaztatu titulua eta data.",
                                "Errorea", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_existingTasks.Any(t =>
                t.Titulua.Equals(Tarea.Titulua, StringComparison.OrdinalIgnoreCase)
                && t.Id != Tarea.Id))
            {
                MessageBox.Show("Titulua errepikatuta dago. Aukeratu beste bat.",
                                "Errorea", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
        }

        private bool IsValid(object data)
        {
            if (data is IDataErrorInfo info)
            {
                foreach (var prop in data.GetType().GetProperties())
                {
                    string error = info[prop.Name];
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show($"Errorea '{prop.Name}' propietatean: {error}");
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
