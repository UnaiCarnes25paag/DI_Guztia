using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ariketa10
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var report = new StringBuilder();
            LoadImageWithFallback("imagenes/imagen1.jpg", img1, 0, report);
            LoadImageWithFallback("imagenes/imagen2.jpg", img2, 1, report);
            LoadImageWithFallback("imagenes/imagen3.jpg", img3, 2, report);
            LoadImageWithFallback("imagenes/imagen4.jpg", img4, 3, report);
            LoadImageWithFallback("imagenes/imagen5.jpg", img5, 4, report);
            LoadImageWithFallback("imagenes/imagen6.jpg", img6, 5, report);

        }

        private void cmbImagenes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;

            img1.Visibility = Visibility.Collapsed;
            img2.Visibility = Visibility.Collapsed;
            img3.Visibility = Visibility.Collapsed;

            if (cmbImagenes.SelectedIndex == 0) img1.Visibility = Visibility.Visible;
            if (cmbImagenes.SelectedIndex == 1) img2.Visibility = Visibility.Visible;
            if (cmbImagenes.SelectedIndex == 2) img3.Visibility = Visibility.Visible;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender == chkImg4) img4.Visibility = Visibility.Visible;
            if (sender == chkImg5) img5.Visibility = Visibility.Visible;
            if (sender == chkImg6) img6.Visibility = Visibility.Visible;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender == chkImg4) img4.Visibility = Visibility.Collapsed;
            if (sender == chkImg5) img5.Visibility = Visibility.Collapsed;
            if (sender == chkImg6) img6.Visibility = Visibility.Collapsed;
        }
        private void LoadImageWithFallback(string relativePath, System.Windows.Controls.Image targetImage, int colorIndex, StringBuilder report)
        {
            string normalized = relativePath.Replace("\\", "/");
            BitmapImage bmp = null;
            string used = null;

            try
            {
                var pack1 = $"pack://application:,,,/{normalized}";
                bmp = TryCreateBitmap(new Uri(pack1, UriKind.Absolute));
                if (bmp != null) used = $"pack1 ({pack1})";
            }
            catch { }

            if (bmp == null)
            {
                try
                {
                    string asm = Assembly.GetExecutingAssembly().GetName().Name;
                    var pack2 = $"pack://application:,,,/{asm};component/{normalized}";
                    bmp = TryCreateBitmap(new Uri(pack2, UriKind.Absolute));
                    if (bmp != null) used = $"pack2 ({pack2})";
                }
                catch { }
            }

            if (bmp == null)
            {
                try
                {
                    var relUri = new Uri(normalized, UriKind.Relative);
                    bmp = TryCreateBitmap(relUri);
                    if (bmp != null) used = $"relative URI ({normalized})";
                }
                catch { }
            }

            if (bmp == null)
            {
                try
                {
                    string abs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, normalized);
                    if (File.Exists(abs))
                    {
                        bmp = TryCreateBitmap(new Uri(abs, UriKind.Absolute));
                        if (bmp != null) used = $"absolute (AppDomain base) ({abs})";
                    }
                }
                catch { }
            }

            if (bmp == null)
            {
                try
                {
                    string alt = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\", normalized));
                    if (File.Exists(alt))
                    {
                        bmp = TryCreateBitmap(new Uri(alt, UriKind.Absolute));
                        if (bmp != null) used = $"project-level fallback ({alt})";
                    }
                }
                catch { }
            }

            if (bmp == null)
            {
                var color = ChooseColor(colorIndex);
                bmp = (BitmapImage?)MakeSolidColorImage(color);
                used = "fallback: color generado (imagen no encontrada)";
            }

            targetImage.Source = bmp;
            targetImage.Visibility = Visibility.Collapsed;

            report.AppendLine($"{relativePath} -> {used}");
        }

        private BitmapImage TryCreateBitmap(Uri uri)
        {
            try
            {
                var bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = uri;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();
                bi.Freeze();
                return bi;
            }
            catch { return null; }
        }

        private BitmapSource MakeSolidColorImage(Color color)
        {
            int width = 400;
            int height = 300;
            int stride = width * 4;
            byte[] pixels = new byte[height * stride];
            for (int i = 0; i < pixels.Length; i += 4)
            {
                pixels[i] = color.B;
                pixels[i + 1] = color.G;
                pixels[i + 2] = color.R;
                pixels[i + 3] = color.A;
            }
            return BitmapSource.Create(width, height, 96, 96, PixelFormats.Bgra32, null, pixels, stride);
        }

        private Color ChooseColor(int idx)
        {
            switch (idx % 6)
            {
                case 0: return Colors.LightCoral;
                case 1: return Colors.LightGreen;
                case 2: return Colors.LightBlue;
                case 3: return Colors.LightGoldenrodYellow;
                case 4: return Colors.Orange;
                default: return Colors.MediumPurple;
            }
        }
    }
}
