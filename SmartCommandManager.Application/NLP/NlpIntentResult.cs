namespace SmartCommandManager.Application.NLP
{
    public class NlpIntentResult
    {
        public string Intent { get; }
        public int IntentIndex { get; }

        public NlpIntentResult(string intent, int index)
        {
            Intent = intent;
            IntentIndex = index;
        }
    }
}
