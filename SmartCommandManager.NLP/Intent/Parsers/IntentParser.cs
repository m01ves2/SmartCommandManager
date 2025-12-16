using SmartCommandManager.NLP.Intent.Exceptions;
using SmartCommandManager.NLP.Intent.Models;
using SmartCommandManager.NLP.Shared.Models;

namespace SmartCommandManager.NLP.Intent.Parsers
{
    public class IntentParser : IIntentParser
    {
        //TODO: «заменить на LINQ после завершения MVP».
        public IntentParseResult Parse(IReadOnlyList<Token> tokens, IEnumerable<IntentDescriptor> intents)
        {

            var candidates = GetCandidates(tokens, intents);

            if (candidates.Count == 0)
                throw new IntentParsingException("no intents found");

            if (candidates.Count > 1)
                throw new IntentParsingException("more than one intent found");

            var first = candidates.First(); //единственный паттерн остался

            if (first.Value.Count > 1)
                throw new IntentParsingException("intent found multiple times");

            var intent = first.Key;
            var index = first.Value[0]; // единственная команда встретилась единственный раз. индекс токена

            return new IntentParseResult( intent, index );
        }

        private Dictionary<IntentDescriptor, List<int>> GetCandidates(IReadOnlyList<Token> tokens, IEnumerable<IntentDescriptor> intents)
        {
            var intentsList = intents.ToList();
            var candidates = new Dictionary<IntentDescriptor, List<int>>(); // паттерн => список индексов токенов, принадлежащих паттерну

            for (int i = 0; i < tokens.Count; i++) {
                string value = tokens[i].Value;

                foreach (var intent in intentsList) {
                    if (intent.Aliases.Contains(value)) {
                        if (!candidates.TryGetValue(intent, out var list)) { //TryGetValue работает быстрее и чище, чем ContainsKey
                            list = new List<int>();
                            candidates[intent] = list;
                        }
                        list.Add(i);
                    }
                }
            }

            return candidates;
        }
    }
}
