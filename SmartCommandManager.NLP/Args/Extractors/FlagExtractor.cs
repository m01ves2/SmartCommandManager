using SmartCommandManager.Domain.Commands.Models;
using SmartCommandManager.NLP.Args.Models;
using SmartCommandManager.NLP.Shared.Models;

namespace SmartCommandManager.NLP.Command.Extractors
{
    public static class FlagExtractor //: IFlagExtractor
    {
        public static FlagExtractionResult Extract(IReadOnlyList<Token> tokens, FlagDescriptor flag)
        {
            var flagsFound = new List<string>();
            var indexesFound = new List<int>();

            for (int i = 0; i < tokens.Count; i++) {
                if (flag.Aliases.Contains(tokens[i].Value)) {
                    flagsFound.Add(flag.Canonical);
                    indexesFound.Add(i);
                }
            }

            return flagsFound.Count == 0 ? FlagExtractionResult.Empty : new FlagExtractionResult(flagsFound, indexesFound);
        }
    }
}
