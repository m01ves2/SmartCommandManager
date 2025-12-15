namespace SmartCommandManager.NLP.Intent.Exceptions
{
    public class AmbiguousIntentException : Exception
    {
        public AmbiguousIntentException(string msg) :base(msg) { }
    }
}
