namespace SmartCommandManager.NLP.Intent.Models
{
    public class IntentResult
    {
        public IntentDescriptor Intent { get; }
        public int IntentIndex { get; }

        public IntentResult(IntentDescriptor intent, int index)
        {
            Intent = intent;
            IntentIndex = index;
        }
    }
}
