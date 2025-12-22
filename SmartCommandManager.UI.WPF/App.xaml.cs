using System.Windows;

namespace SmartCommandManager.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var app = CompositionRoot.CompositionRoot.Build();

            var window = new MainWindow(
                app.Dispatcher,
                app.Context);

            window.Show();
        }
    }

}
