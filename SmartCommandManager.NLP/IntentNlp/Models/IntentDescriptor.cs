namespace SmartCommandManager.NLP.IntentNlp.Models
{
    public sealed record IntentDescriptor( string Primary, IReadOnlyList<string> Synonyms );
}
