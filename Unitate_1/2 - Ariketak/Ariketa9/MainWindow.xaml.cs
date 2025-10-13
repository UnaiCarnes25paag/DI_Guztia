using System;
using System.Windows;

namespace Ariketa9
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAmigoNuevo.Text))
            {
                MessageBox.Show("Escribe un nombre antes de añadir.");
                return;
            }

            listBoxAmigos.Items.Add(txtAmigoNuevo.Text);
            txtAmigoNuevo.Clear();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxAmigos.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un elemento para eliminar.");
                return;
            }

            listBoxAmigos.Items.Remove(listBoxAmigos.SelectedItem);
            txtAmigoSeleccionado.Clear();
        }

        private void listBoxAmigos_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (listBoxAmigos.SelectedItem != null)
            {
                txtAmigoSeleccionado.Text = listBoxAmigos.SelectedItem.ToString();
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            listBoxAmigos.Items.Clear();
            txtAmigoNuevo.Clear();
            txtAmigoSeleccionado.Clear();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
