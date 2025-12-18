using SmartCommandManager.Modules.FileSystem.Services;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.Builder
{
    public class DestinationCandidate
    {
        public string RawPath { get; set; }
        public ItemType ItemType { get; set; }
        public bool Exists { get; set; }
    }
}
