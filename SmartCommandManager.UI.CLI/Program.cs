using SmartCommandManager.CompositionRoot;

namespace SmartCommandManager.UI.CLI
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var app = CompositionRoot.CompositionRoot.Build();
            var ui = new ConsoleUI(app.Dispatcher, app.Context);
            ui.Run();
        }
    }
}