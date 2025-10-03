using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ariketa6
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TextBox1.KeyDown += TextBox_KeyDown;
            TextBox2.KeyDown += TextBox_KeyDown;
            TextBox3.KeyDown += TextBox_KeyDown;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var textBoxes = new[] { TextBox1, TextBox2, TextBox3 };
                var current = sender as TextBox;
                int idx = System.Array.IndexOf(textBoxes, current);
                int nextIdx = (idx + 1) % textBoxes.Length;

                textBoxes[nextIdx].Text = current.Text;
                current.Clear();
                textBoxes[nextIdx].Focus();
                textBoxes[nextIdx].SelectAll();

                e.Handled = true;
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            TextBox1.Clear();
            TextBox2.Clear();
            TextBox3.Clear();
            TextBox1.Focus();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}