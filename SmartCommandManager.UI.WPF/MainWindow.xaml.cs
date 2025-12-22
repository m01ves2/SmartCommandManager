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

        //private readonly ICommandManager _commandManager;
        public MainWindow(CommandDispatcher dispatcher, CommandContext context)
        {
            InitializeComponent();
            _dispatcher = dispatcher;
            _context = context;

            WriteLine("SmartCommandManager started.");
            InputTextBox.Focus();
        }
        private void InputTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            var input = InputTextBox.Text;
            InputTextBox.Clear();

            if (string.IsNullOrWhiteSpace(input))
                return;

            WriteLine($"> {input}");

            var result = _dispatcher.Execute(input);

            if (!string.IsNullOrEmpty(result.Message)) {
                WriteLine(result.Message);
            }

            ScrollToEnd();

            if (result.Status == CommandStatus.Exit)
                System.Windows.Application.Current.Shutdown();
        }

        private void WriteLine(string text)
        {
            OutputTextBox.AppendText(text + Environment.NewLine);
        }

        private void ScrollToEnd()
        {
            OutputTextBox.CaretIndex = OutputTextBox.Text.Length;
            OutputTextBox.ScrollToEnd();
        }
    }
}