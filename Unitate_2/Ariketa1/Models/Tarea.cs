using System;
using System.ComponentModel;

namespace TareasWpf.Models
{
    public enum LehentasunaMaila
    {
        Baxua,
        Ertaina,
        Altua
    }

    public class Tarea : INotifyPropertyChanged, IDataErrorInfo
    {
        private int id;
        private string titulua = string.Empty;
        private LehentasunaMaila lehentasuna;
        private DateTime azkenEguna = DateTime.Today;
        private bool egoera;

        public int Id
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public string Titulua
        {
            get => titulua;
            set
            {
                if (titulua != value)
                {
                    titulua = value;
                    OnPropertyChanged(nameof(Titulua));
                }
            }
        }

        public LehentasunaMaila Lehentasuna
        {
            get => lehentasuna;
            set
            {
                if (lehentasuna != value)
                {
                    lehentasuna = value;
                    OnPropertyChanged(nameof(Lehentasuna));
                }
            }
        }

        public DateTime AzkenEguna
        {
            get => azkenEguna;
            set
            {
                if (azkenEguna != value)
                {
                    azkenEguna = value;
                    OnPropertyChanged(nameof(AzkenEguna));
                }
            }
        }

        public bool Egoera
        {
            get => egoera;
            set
            {
                if (egoera != value)
                {
                    egoera = value;
                    OnPropertyChanged(nameof(Egoera));
                }
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Titulua):
                        if (string.IsNullOrWhiteSpace(Titulua))
                            return "Titulua ezin da hutsik egon.";
                        break;

                    case nameof(AzkenEguna):
                        if (AzkenEguna.Date < DateTime.Today)
                            return "Azken eguna ezin da gaur baino lehenagokoa izan.";
                        break;
                }

                return string.Empty;
            }
        }

        public string Error => null;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
