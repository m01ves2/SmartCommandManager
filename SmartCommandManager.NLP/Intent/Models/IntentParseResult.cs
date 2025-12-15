namespace SmartCommandManager.NLP.Intent.Models
{
    public class IntentParseResult
    {
        public IntentDescriptor Intent { get; }
        public int IntentIndex { get; }

        public IntentParseResult(IntentDescriptor intent, int index)
        {
            Intent = intent;
            IntentIndex = index;
        }
    }
}
