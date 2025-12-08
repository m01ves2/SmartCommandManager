using SmartCommandManager.Domain.NLP;
using System.Text;

namespace SmartCommandManager.Application.NLP
{
    internal sealed class TokenBuilder
    {
        private StringBuilder _sb = new StringBuilder();
        public bool IsQuoted { get; set; }
        public char? QuoteChar { get; set; }

        public void Append(char c)
        {
            _sb.Append(c);
        }

        public Token Build()
        {
            //string value = IsQuoted ? _sb.ToString() : _sb.ToString().Trim();
            string value = _sb.ToString();
            
            if(!(value.StartsWith('\"') || value.StartsWith('\'')))
                value = value.ToLower();
            
            return new Token(value, IsQuoted, QuoteChar);
        }

        public void Reset()
        {
            _sb.Clear();
            IsQuoted = false;
            QuoteChar = null;
        }

        public bool HasData()
        {
            return _sb.Length > 0;
        }
    }
}
