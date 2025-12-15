namespace SmartCommandManager.NLP.Intent.Models
{
    public sealed record IntentDescriptor( string Primary, IReadOnlyList<string> Aliases );
}
