using SmartCommandManager.NLP.Command.Models;
using SmartCommandManager.NLP.Intent.Models;

namespace SmartCommandManager.NLP.Command.Extractors
{
    public interface INoiseExtractor
    {
        NoiseExtractionResult Extract(IReadOnlyList<Token> tokens, int intentIndex);
    }
}
