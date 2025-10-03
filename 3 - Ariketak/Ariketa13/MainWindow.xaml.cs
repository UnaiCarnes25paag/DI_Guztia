using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Ariketa13
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, Cortar_Click));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, Copiar_Click));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, Pegar_Click));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, Eliminar_Click));
        }

        private void Cortar_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtEditor.Text);
            txtEditor.Clear();
        }

        private void Copiar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEditor.Text))
                Clipboard.SetText(txtEditor.Text);
        }

        private void Pegar_Click(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsText())
                txtEditor.Text += Clipboard.GetText();
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            txtEditor.Clear();
        }

        private void Fuente_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                string fuente = menuItem.Header.ToString();
                txtEditor.FontFamily = new FontFamily(fuente);
            }
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
