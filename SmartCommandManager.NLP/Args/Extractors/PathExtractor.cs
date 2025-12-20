using SmartCommandManager.NLP.Shared.Models;

namespace SmartCommandManager.NLP.Command.Extractors
{
    public static class PathExtractor
    {
        public static string Extract(IReadOnlyList<Token> tokens, int startIndex, int endIndex)
        { //we are looking tokens with index:  startIndex < index < endIndex
            if ( (startIndex < tokens.Count - 1) && (startIndex + 1 < endIndex ))
                return tokens[startIndex + 1].Value;
            return "";
        }
    }

    //copy from A
}
