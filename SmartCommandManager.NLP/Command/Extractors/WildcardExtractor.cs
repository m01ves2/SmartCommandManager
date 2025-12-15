using SmartCommandManager.NLP.Command.Models;
using SmartCommandManager.NLP.Intent.Models;

namespace SmartCommandManager.NLP.Command.Extractors
{
    public static class WildcardExtractor
    {
        public static WildcardExtractionResult Extract(IReadOnlyList<Token> tokens, IReadOnlyCollection<string> wildcardMarkers)
        { 
            for (int i = 0; i < tokens.Count; i++) {
                if (wildcardMarkers.Contains(tokens[i].Value))
                    return new WildcardExtractionResult(true);
            }
            return WildcardExtractionResult.None;
        }
    
    }
}
