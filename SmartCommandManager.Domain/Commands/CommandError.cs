namespace SmartCommandManager.Domain.Commands
{
    public sealed class CommandError
    {
        public string Field { get; set; }        // например, "source", "destination"
        public string Message { get; set; }      // например, "Path does not exist"
    }
}
