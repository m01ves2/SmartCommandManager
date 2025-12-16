namespace SmartCommandManager.Domain.Commands.Models
{
    //public enum CommandStatus
    //{
    //    Success,
    //    Error,
    //    Exit,
    //}

    ////универсальный результат выполнения команды (успех/ошибка, сообщение, данные). Мост между Core и App.
    //public record class CommandResult
    //{
    //    public CommandStatus Status { get; set; } //команда выполнилась или нет.
    //    public string Message { get; set; } = string.Empty; //текстовое сообщение для App (UI).
    //    // при необходимости можно добавить данные, например, список файлов
    //    public object? Data { get; set; } //если команда возвращает что-то, например, список файлов (List<FileItem>).

    //    //private CommandResult(CommandStatus status, string message, object? data)
    //    //{
    //    //    Status = status;
    //    //    Message = message;
    //    //    Data = data;
    //    //}

    //    //public static CommandResult Ok(string msg = "") => new(CommandStatus.Success, msg, null);
    //    //public static CommandResult Fail(string msg) => new(CommandStatus.Error, msg, null);
    //}

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
