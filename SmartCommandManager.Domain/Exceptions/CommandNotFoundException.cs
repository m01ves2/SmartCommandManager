namespace SmartCommandManager.Application.Exceptions
{
    public class CommandNotFoundException : Exception
    {
        public CommandNotFoundException(string msg) : base(msg) { }
    }
}
