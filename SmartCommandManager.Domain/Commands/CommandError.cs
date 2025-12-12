namespace SmartCommandManager.Domain.Commands
{
    public sealed class CommandError
    {
        public string Field { get; init; }        // например, "source", "destination"
        public string Message { get; init; }      // например, "Path does not exist"
    }
}
