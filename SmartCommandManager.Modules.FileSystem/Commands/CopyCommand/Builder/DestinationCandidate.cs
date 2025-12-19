using SmartCommandManager.Modules.FileSystem.Services;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.Builder
{
    public class DestinationCandidate
    {
        public string Path { get; set; }
        public ItemType ItemType { get; set; }
        public bool Exists { get; set; }

        public DestinationCandidate(string path, ItemType itemType, bool exists )
        {
            Path = path;
            ItemType = itemType;
            Exists = exists;
        }
    }
}
