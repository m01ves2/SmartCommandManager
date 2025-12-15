namespace SmartCommandManager.NLP.Intent.Exceptions
{
    public class IntentRepeatedException : Exception
    {
        public IntentRepeatedException(string msg) : base(msg) { }
    }
}
