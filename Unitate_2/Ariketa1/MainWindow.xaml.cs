using Ariketa1.Infrastructure;
using AtazaKudeatzailea.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using TareasWpf.Models;
using TareasWpf.Views;

namespace TareasWpf
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly TaskRepository repo;
        private readonly string dataFolder = "Data";

        public ObservableCollection<Tarea> Tareas { get; } = new ObservableCollection<Tarea>();

        private Tarea? aukeratua;
        public Tarea? Aukeratua
        {
            get => aukeratua;
            set
            {
                if (aukeratua == value) return;
                aukeratua = value;
                OnPropertyChanged(nameof(Aukeratua));
                EditatuCommand.RaiseCanExecuteChanged();
                EzabatuCommand.RaiseCanExecuteChanged();
                ToggleEgoeraCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand GehituCommand { get; }
        public RelayCommand EditatuCommand { get; }
        public RelayCommand EzabatuCommand { get; }
        public RelayCommand ToggleEgoeraCommand { get; }
        public RelayCommand OrdenatuDatazCommand { get; }
        public RelayCommand OrdenatuLehentasunazCommand { get; }

        public MainWindow()
        {
            InitializeComponent();

            repo = new TaskRepository(dataFolder);
            var zerrenda = repo.LoadAll();
            foreach (var t in zerrenda)
                Tareas.Add(t);

            GehituCommand = new RelayCommand(_ => GehituAtaza());
            EditatuCommand = new RelayCommand(_ => EditatuAtaza(), _ => Aukeratua != null);
            EzabatuCommand = new RelayCommand(_ => EzabatuAtaza(), _ => Aukeratua != null);
            ToggleEgoeraCommand = new RelayCommand(_ => AldatuEgoera(), _ => Aukeratua != null);
            OrdenatuDatazCommand = new RelayCommand(_ => Ordenatu("AzkenEguna"));
            OrdenatuLehentasunazCommand = new RelayCommand(_ => Ordenatu("Lehentasuna"));

            DataContext = this;
        }

        private void GehituAtaza()
        {
            var berria = new Tarea
            {
                Id = repo.NextId(Tareas),
                Titulua = "",
                Lehentasuna = LehentasunaMaila.Baxua,
                AzkenEguna = DateTime.Today,
                Egoera = false
            };

            var dialog = new TaskDialog(berria, Tareas) { Owner = this };
            if (dialog.ShowDialog() == true)
            {
                Tareas.Add(dialog.Tarea);
                repo.SaveAll(Tareas);
            }
        }

        private void EditatuAtaza()
        {
            if (Aukeratua == null) return;
            var dialog = new TaskDialog(Aukeratua, Tareas) { Owner = this };
            if (dialog.ShowDialog() == true)
            {
                Aukeratua.Titulua = dialog.Tarea.Titulua;
                Aukeratua.Lehentasuna = dialog.Tarea.Lehentasuna;
                Aukeratua.AzkenEguna = dialog.Tarea.AzkenEguna;
                Aukeratua.Egoera = dialog.Tarea.Egoera;

                repo.SaveAll(Tareas);
                CollectionViewSource.GetDefaultView(Tareas).Refresh();
            }
        }

        private void EzabatuAtaza()
        {
            if (Aukeratua == null) return;
            if (MessageBox.Show("Ziur zaude ataza hau ezabatu nahi duzula?",
                                "Berrespena", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Tareas.Remove(Aukeratua);
                repo.SaveAll(Tareas);
            }
        }

        private void AldatuEgoera()
        {
            if (Aukeratua == null) return;
            Aukeratua.Egoera = !Aukeratua.Egoera;
            repo.SaveAll(Tareas);
            CollectionViewSource.GetDefaultView(Tareas).Refresh();
        }

        private bool ordenaAsc = true;
        private void Ordenatu(string propertyName)
        {
            var view = CollectionViewSource.GetDefaultView(Tareas);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(
                new SortDescription(propertyName, ordenaAsc ? ListSortDirection.Ascending : ListSortDirection.Descending)
            );
            ordenaAsc = !ordenaAsc;
        }

        private void OnItxi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
