namespace SmartCommandManager.Domain.NLP
{
    //public class Token
    //{
    //    public string Value { get; }
    //    public bool IsQuoted { get; } // true если token в кавычках
    //    public char? QuoteChar { get; } // ' " « » и т.д.

    //    public Token(string value, bool isQuoted, char? quoteChar)
    //    {
    //        Value = value;
    //        IsQuoted = isQuoted;
    //        QuoteChar = quoteChar;
    //    }
    //}

    public sealed class Token
    {
        public string Value { get; }
        //public int Index { get; }  // позиция в исходной строке

        public Token(string value /*, int index*/)
        {
            Value = value;
            //Index = index;
        }
    }
}
