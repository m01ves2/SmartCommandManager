namespace SmartCommandManager.Application.Dispatcher
{
    /// <summary>
    /// Registry for Commands
    /// </summary>
    public class CommandRegistry
    {
        public IReadOnlyList<ICommand> Commands { get; }

        public CommandRegistry(IEnumerable<ICommand> commands)
        {
            Commands = commands.ToList();
        }

        //public ICommand GetCommand(string name)
        //{
        //    if (_registry.ContainsKey(name))
        //        return _registry[name];
        //    else if (_registry.ContainsKey("unknown"))
        //        return _registry["unknown"];
        //    else
        //        throw new Exception($"Command {name} not registered.");
        //}
    }
}