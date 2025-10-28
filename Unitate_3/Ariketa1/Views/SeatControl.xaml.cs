using Ariketa1.ViewModels;
using Ariketa1.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Ariketa1.Views
{
    public partial class SeatControl : UserControl
    {
        public SeatViewModel ViewModel => DataContext as SeatViewModel;

        public SeatControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = ViewModel;
            if (vm == null) return;

            if (!vm.IsOccupied)
            {
                var dlg = new ReservaDialog(vm.Id) { Owner = Application.Current.MainWindow };
                if (dlg.ShowDialog() == true)
                {
                    vm.IsOccupied = true;
                    vm.ReservedBy = dlg.Nombre;
                    vm.ReservationDateTime = dlg.FechaReserva;

                    var anim = new DoubleAnimation(1, 0.7, new Duration(TimeSpan.FromMilliseconds(120))) { AutoReverse = true };
                    this.BeginAnimation(OpacityProperty, anim);
                }
            }
            else
            {
                var res = MessageBox.Show($"Erreserba bertan behera utzi {vm.ReservedBy} en {vm.Id}?",
                                          "Erreserba bertan behera utzi", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    vm.IsOccupied = false;
                    vm.ReservedBy = null;
                    vm.ReservationDateTime = null;
                }
            }
        }


        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            var btn = sender as Button;
            var st = new ScaleTransform(1, 1);
            btn.RenderTransform = st;
            btn.RenderTransformOrigin = new Point(0.5, 0.5);
            var anim = new DoubleAnimation(1, 1.06, new Duration(TimeSpan.FromMilliseconds(120)));
            st.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
            st.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            var btn = sender as Button;
            if (btn?.RenderTransform is ScaleTransform st)
            {
                var anim = new DoubleAnimation(st.ScaleX, 1, new Duration(TimeSpan.FromMilliseconds(120)));
                st.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
                st.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
            }
        }
    }
}
