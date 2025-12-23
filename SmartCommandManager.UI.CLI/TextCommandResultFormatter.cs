using SmartCommandManager.Application.Services;
using SmartCommandManager.Domain.Commands.Models;

namespace SmartCommandManager.UI.CLI
{
    public enum OutputStyle
    {
        OK,
        WARNING,
        ERROR,
        NONE,
    };

    public sealed class FormattedOutput
    {
        public string Content { get; }
        public OutputStyle Style { get; }

        public FormattedOutput(string content, OutputStyle style)
        {
            Content = content;
            Style = style;
        }
    }

    public class TextCommandResultFormatter
    {
        public FormattedOutput Format(CommandResult result)
        {
            string content = RenderStatus(result.Status) + " " + 
                             RenderMessage(result.Message) + 
                             RenderData(result.Data);
            OutputStyle style = OutputStyle.NONE;

            switch (result.Status) {
                case CommandStatus.Success: style = OutputStyle.OK; break;
                case CommandStatus.Exit: style=  OutputStyle.WARNING; break;
                default: style = OutputStyle.ERROR; break;
            }

            return new FormattedOutput(content, style);
        }

        private string RenderMessage(string? message)
        {
            return $"{message ?? ""}";
        }

        private string RenderStatus(CommandStatus status)
        {
            return $"[{status}]";
        }
        private string RenderData(object? data)
        {
            if (data == null)
                return "";
            else
                return "\n" + data.ToString();
        }

    }
}
