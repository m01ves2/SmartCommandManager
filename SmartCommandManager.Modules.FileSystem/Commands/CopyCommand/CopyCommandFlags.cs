using SmartCommandManager.Domain.Commands.Models;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand
{
    public static class CopyCommandFlags
    {
        public static IReadOnlyList<FlagDescriptor> All { get; } =
        [
            new("recursive",
                ["-r", "--recursive"],
                "Include subdirectories."),

            new("overwrite",
                ["-o", "--overwrite"],
                "Allow overwriting an existing destination file."),

            new("skip",
                ["-s", "--skip"],
                "Skip copying if the final destination exists.")
        ];
    }
}
