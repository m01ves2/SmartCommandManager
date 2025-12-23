using SmartCommandManager.Application.Services;
using SmartCommandManager.Domain.Commands.Models;

namespace SmartCommandManager.UI.WPF
{
    public sealed class HtmlCommandResultFormatter
    {
        public string Format(CommandResult result)
        {
            var style = MapStyle(result.Status);

            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{
            font-family: Consolas, monospace;
            background-color: black;
            color: #d4d4d4;
            padding: 10px;
        }}
        .ok {{ color: #4CAF50; }}
        .warning {{ color: #FFC107; }}
        .error {{ color: #F44336; }}
        .status {{ font-weight: bold; }}
    </style>
</head>
<body>
    <pre>
        <div class='status {style}'>[{result.Status}]</div>
        <div>{Escape(result.Message)}</div>
        {RenderData(result.Data)}
    </pre>
</body>
</html>";
        }

        private static string MapStyle(CommandStatus status) =>
            status switch
            {
                CommandStatus.Success => "ok",
                CommandStatus.Exit => "warning",
                _ => "error"
            };

        private static string RenderData(object? data)
        {
            if (data == null)
                return "";

            return $"{Escape(data.ToString())}";
        }

        private static string Escape(string? text)
        {
            if (text == null)
                return "";
            return System.Net.WebUtility.HtmlEncode(text);
        }
    }
}
