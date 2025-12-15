using SmartCommandManager.NLP.Command.Models;
using SmartCommandManager.NLP.Shared.Models;

namespace SmartCommandManager.NLP.Command.Extractors
{
    public static class FlagExtractor //: IFlagExtractor
    {
        public static FlagExtractionResult Extract(IReadOnlyList<Token> tokens, IReadOnlyCollection<string> flags)
        {
            var flagsFound = new List<string>();

            for (int i = 0; i < tokens.Count; i++) {
                if (flags.Contains(tokens[i].Value))
                    flagsFound.Add(tokens[i].Value);
            }

            return flagsFound.Count == 0 ? FlagExtractionResult.None : new FlagExtractionResult(flagsFound);
        }
    }
}
