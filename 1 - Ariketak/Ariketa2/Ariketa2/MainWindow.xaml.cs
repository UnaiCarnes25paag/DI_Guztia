using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Ariketa2
{
    public partial class MainWindow : Window
    {
        private List<string> frases = new();

        public MainWindow()
        {
            InitializeComponent();
            ResetUI();
        }

        private void FraseButton_Click(object sender, RoutedEventArgs e)
        {
            frases.Add(FraseTextBox.Text);
            FraseTextBox.Clear();

            if (sender == Frase1Button)
            {
                Frase1Button.Visibility = Visibility.Collapsed;
                Frase2Button.IsEnabled = true;
            }
            else if (sender == Frase2Button)
            {
                Frase2Button.Visibility = Visibility.Collapsed;
                Frase3Button.IsEnabled = true;
            }
            else if (sender == Frase3Button)
            {
                Frase3Button.Visibility = Visibility.Collapsed;
                Frase4Button.IsEnabled = true;
            }
            else if (sender == Frase4Button)
            {
                Frase4Button.Visibility = Visibility.Collapsed;
                Frase5Button.IsEnabled = true;
            }
            else if (sender == Frase5Button)
            {
                Frase5Button.Visibility = Visibility.Collapsed;
                UnirButton.IsEnabled = true;
            }

            FraseTextBox.Focus();
        }

        private void UnirButton_Click(object sender, RoutedEventArgs e)
        {
            FraseTextBox.Text = string.Join(" ", frases);
            UnirButton.IsEnabled = false;
        }

        private void LimpiarButton_Click(object sender, RoutedEventArgs e)
        {
            ResetUI();
        }

        private void ResetUI()
        {
            frases.Clear();
            FraseTextBox.Clear();

            Frase1Button.Visibility = Visibility.Visible;
            Frase2Button.Visibility = Visibility.Visible;
            Frase3Button.Visibility = Visibility.Visible;
            Frase4Button.Visibility = Visibility.Visible;
            Frase5Button.Visibility = Visibility.Visible;

            Frase1Button.IsEnabled = true;
            Frase2Button.IsEnabled = false;
            Frase3Button.IsEnabled = false;
            Frase4Button.IsEnabled = false;
            Frase5Button.IsEnabled = false;
            UnirButton.IsEnabled = false;
            FraseTextBox.Focus();
        }
        private void SalirButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}