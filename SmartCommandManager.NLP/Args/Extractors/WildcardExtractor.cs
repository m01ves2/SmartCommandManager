using SmartCommandManager.NLP.Args.Models;
using SmartCommandManager.NLP.Shared.Models;
using System.Collections.ObjectModel;

namespace SmartCommandManager.NLP.Command.Extractors
{
    public static class WildcardExtractor
    {
        public static WildcardExtractionResult Extract(IReadOnlyList<Token> tokens, IReadOnlyCollection<string> wildcardMarkers)
        {
            var indexes = new Collection<int>();
            for (int i = 0; i < tokens.Count; i++) {
                if (wildcardMarkers.Contains(tokens[i].Value))
                    indexes.Add(i);
            }
            if (indexes.Count > 0) {
                return new WildcardExtractionResult(true, indexes);
            }
            return WildcardExtractionResult.None;
        }
    
    }
}
