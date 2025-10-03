using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ariketa5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FontButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string fontName)
            {
                InputTextBox.FontFamily = new FontFamily(fontName);
            }
        }

        private void IncreaseFontSize_Click(object sender, RoutedEventArgs e)
        {
            InputTextBox.FontSize += 2;
        }

        private void DecreaseFontSize_Click(object sender, RoutedEventArgs e)
        {
            if (InputTextBox.FontSize > 2)
                InputTextBox.FontSize -= 2;
        }

        private void Seleccionar_Click(object sender, RoutedEventArgs e)
        {
            int totalChars = InputTextBox.Text.Length;
            string selectedText = InputTextBox.SelectedText;
            if (!string.IsNullOrEmpty(selectedText))
            {
                ResultTextBlock.Text = $"Total characters: {totalChars}\nSelected: \"{selectedText}\" ({selectedText.Length} chars)";
            }
            else
            {
                ResultTextBlock.Text = $"Total characters: {totalChars}\nNo text selected.";
            }
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}