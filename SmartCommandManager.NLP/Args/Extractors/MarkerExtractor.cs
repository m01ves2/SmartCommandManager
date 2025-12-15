using SmartCommandManager.NLP.Command.Models;
using SmartCommandManager.NLP.Shared.Models;

namespace SmartCommandManager.NLP.Command.Extractors
{
    public static class MarkerExtractor // : IMarkerExtractor
    {
        public static MarkerExtractionResult Extract(IReadOnlyList<Token> tokens, int intentIndex, IReadOnlyCollection<string> markers)
        {
            var indexes = new List<int>();

            for (int i = 0; i < tokens.Count; i++) {
                if (markers.Contains(tokens[i].Value))
                    indexes.Add(i);
            }

            return indexes.Count == 0 ? MarkerExtractionResult.Empty : new MarkerExtractionResult(indexes);
        }
    }
}
