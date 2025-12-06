namespace SmartFileManager.App.Interfaces
{
    public interface ICommandDispatcher
    {
        CommandResult Execute(string input);
        string GetPrompt();
    }
}
