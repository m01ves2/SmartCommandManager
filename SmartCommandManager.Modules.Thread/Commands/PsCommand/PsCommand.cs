using SmartCommandManager.Domain.Commands.Base;
using SmartCommandManager.Domain.Commands.Models;
using System.Diagnostics;
using System.Text;

namespace SmartCommandManager.Modules.Thread.Commands.PsCommand
{
    public class PsCommand : BaseCommand<Unit>
    {
        public override CommandInfo CommandInfo { get; } = new("ps", "Show processes info", CommandCategory.Thread);

        protected override CommandResult Execute(Unit args)
        {
            // Get all running processes
            Process[] processes = Process.GetProcesses();
            StringBuilder sb = new StringBuilder();
            
            sb.Append("processes: \n");
            sb.Append(" PID    WINDOW    STIME    CPUTIME    MEMORY\n");
            foreach (var process in processes) {
                try {
                    sb.Append($"{process.Id}    {process.MainWindowTitle}    {process.StartTime}    {process.UserProcessorTime}    {process.WorkingSet64 / (1024 * 1024)} MB" + "\n");
                }
                catch {
                    // Handle cases where access to process information is denied
                    //TODO log
                }
            }
            return new CommandResult() { Status = CommandStatus.Success, Message = sb.ToString() };
        }
    }
}
