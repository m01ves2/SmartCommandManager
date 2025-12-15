namespace SmartCommandManager.Domain.Commands.Models
{
    public class CommandInfo
    {
        public string Name { get; }
        public string Description { get; }
        public CommandCategory Category { get; }

        public CommandInfo(string name, string description, CommandCategory category) 
        { 
            Name = name; 
            Description = description; 
            Category = category; 
        }
    }
}
