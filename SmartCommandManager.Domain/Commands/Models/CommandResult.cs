namespace SmartCommandManager.Domain.Commands.Models
{
    public enum CommandStatus
    {
        Success,
        Failed,
        ValidationError,
        NotFound,
        Forbidden,
        Conflict,
        Exit
    }

    public sealed class CommandResult
    {
        public CommandStatus Status { get; set; }        // Success, Fail, ValidationError, NotFound, Forbidden, etc.
        public string? Message { get; set; }             // Короткое описание результата
        public object? Data { get; set; }                // Любые данные DTO
       // public IReadOnlyList<CommandError>? Errors { get; set; }   // Для валидации
    }
}
