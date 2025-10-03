using System.Windows;

namespace Ariketa11
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var formDatos = new VentanaSecundaria();
            formDatos.Show();
        }
    }
}
