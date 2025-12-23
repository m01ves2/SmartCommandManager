using SmartCommandManager.Application.Services;
using SmartCommandManager.Domain.Commands.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartCommandManager.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CommandDispatcher _dispatcher;
        private readonly CommandContext _context;
        private readonly HtmlCommandResultFormatter _formatter;

        private const string InitialHtml = """
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <style>
        body {
            background-color: black;
            color: #e0e0e0;
            font-family: Consolas, monospace;
            margin: 0;
            padding: 10px;
        }
    </style>
</head>
<body>
    <pre>SmartCommandManager WPF</pre>
</body>
</html>
""";

        public MainWindow(CommandDispatcher dispatcher, CommandContext context)
        {
            InitializeComponent();
            OutputBrowser.NavigateToString(InitialHtml);

            //OutputBrowser.Navigate(new Uri("https://google.com/"));
            _dispatcher = dispatcher;
            _context = context;
            _formatter = new HtmlCommandResultFormatter();

            InputTextBox.Focus();
        }
        private void InputTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            var input = InputTextBox.Text;
            InputTextBox.Clear();
            if (string.IsNullOrWhiteSpace(input)) return;

            var result = _dispatcher.Execute(input);
            var html = _formatter.Format(result);

            OutputBrowser.NavigateToString(html);
            OutputBrowser.LoadCompleted += ScrollToEnd;

            if (result.Status == CommandStatus.Exit)
                System.Windows.Application.Current.Shutdown();
        }

        private void WriteLine(string html)
        {
            OutputBrowser.NavigateToString(html);
        }

        private void ScrollToEnd(object? sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            OutputBrowser.InvokeScript( "execScript", new object[] { "window.scrollTo(0, document.body.scrollHeight);" });
            OutputBrowser.LoadCompleted -= ScrollToEnd; // один раз
        }
    }
}