using SmartCommandManager.Domain.Commands.Models;

namespace SmartCommandManager.Modules.FileSystem.Commands.ListCommand
{
    public static class ListCommandFlags
    {
        public static IReadOnlyList<FlagDescriptor> All { get; } =
        [
            new("longlisting",
                ["-l", "--long", "longlisting"],
                "Show item type, file size and last modification time."),

            new("directoryonly",
                ["-d", "--directoryonly", "--directory"],
                "Show only subdirectories in a directory listing.")
        ];
    }
}
