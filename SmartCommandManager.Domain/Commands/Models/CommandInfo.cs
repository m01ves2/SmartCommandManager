namespace SmartCommandManager.Domain.Commands.Models
{
    public class CommandInfo
    {
        public string Name { get; }
        public string Description { get; }
        public CommandCategory Category { get; }
        public string? Usage { get; }
        public string? Details { get; }
        public IReadOnlyList<FlagDescriptor> Flags { get; }

        public CommandInfo(
               string name,
               string description,
               CommandCategory category,
               string? usage = null,
               string? details = null,
               IReadOnlyList<FlagDescriptor>? flags = null)
        {
            Name = name;
            Description = description;
            Category = category;
            Usage = usage;
            Details = details;
            Flags = flags ?? Array.Empty<FlagDescriptor>();
        }
    }
}
