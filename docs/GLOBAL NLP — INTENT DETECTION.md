# GLOBAL NLP — INTENT DETECTION (v1)
## RFC SPECIFICATION

Version: 2.0  
Status: Stable  
Author: SmartCommandManager Architecture Team  
Scope: Global NLP layer for intent resolution  
Audience: Engine developers, module developers, command authors

---

# 1. Purpose

This document defines the architecture, contracts, and processing pipeline of **Global NLP Intent Detection v1** — a domain-agnostic, extensible mechanism for identifying user intent in natural-language-like command input.

Global NLP is responsible for **selecting the correct command**.  
Argument parsing is delegated to **Local NLP Parser** inside each command.

---

# 2. Key Defsetions

### Token  
Atomic unit produced by the Tokenizer.

### Intent Pattern  
A command-provided multi-token or single-token pattern that may represent invocation of a command.

Examples:
- `copy`
- `cp`
- `make a copy`
- `remove`
- `delete`

### Intent Candidate  
A matched instance of a Intent Pattern inside the token stream.

### Intent Validator  
A command or domain-supplied component that may reject an intent candidate based on token context.

### Intent Resolution  
Process of detecting and validating intent candidates.

### NlpIntentResult  
Global NLP output:
- Identified command
- Token list
- Raw input

---

# 3. High-Level Architecture

UserInput
↓
Tokenizer
↓ tokens
GlobalNlpParser
↓
Intent Pattern Matching
↓ candidates
Intent Validators Execution
↓ filtered candidates
Ambiguity Resolution
↓
Single intent?
NO → error
YES → NlpIntentResult
↓
Local NLP (command)
↓
Command.Execute()

# 4. Interfaces (Contracts)

## 4.1 IGlobalNlpParser

```csharp
public interface IGlobalNlpParser
{
    NlpIntentResult Parse(string input, CommandContext context);
}
```

4.2 NlpIntentResult

```csharp
public sealed class NlpIntentResult
{
    public ICommand Command { get; }
    public IReadOnlyList<Token> Tokens { get; }
    public string RawInput { get; }

    public NlpIntentResult(ICommand command, IReadOnlyList<Token> tokens, string rawInput)
    {
        Command = command;
        Tokens = tokens;
        RawInput = rawInput;
    }
}
```

4.3 ICommand (extended)

```csharp
public interface ICommand
{
    string Name { get; }

    IEnumerable<IntentPattern> IntentPatterns { get; }

    IEnumerable<IIntentValidator> GetIntentValidators();

    void Execute(LocalNlpResult args);
}
```

4.4 IntentPattern
Supports multi-token matching.

```csharp
public sealed class IntentPattern
{
    public IReadOnlyList<string> Tokens { get; }

    public IntentPattern(params string[] tokens)
    {
        Tokens = tokens;
    }
}
```

4.5 IntentCandidate

```csharp
public sealed class IntentCandidate
{
    public ICommand Command { get; }
    public IntentPattern Pattern { get; }
    public int StartIndex { get; }
    public int EndIndex => StartIndex + Pattern.Tokens.Count - 1;

    public bool IsValid { get; set; } = true;

    public IntentCandidate(ICommand command, IntentPattern pattern, int startIndex)
    {
        Command = command;
        Pattern = pattern;
        StartIndex = startIndex;
    }
}
```

4.6 IIntentValidator

```csharp
public interface IIntentValidator
{
    bool Reject(IntentCandidate candidate, IReadOnlyList<Token> tokens, CommandContext context);
}
```

5. Algorithm (Step-by-Step)

Step 1 — Tokenize input

Tokenizer runs independently.

Step 2 — Match Intent Patterns

For each command:
foreach pattern → match in tokens → produce IntentCandidate

Step 3 — Collect validators


Validators come from:

-The command
-The domain (base classes)
-Optional global validators

Step 4 — Execute validators
for each candidate:
    for each validator:
        if validator.Reject(candidate):
            mark candidate invalid

Step 5 — Keep only valid candidates
Step 6 — Global validation rules

-0 candidates → UnknownIntent
->1 candidate → AmbiguousIntent
-1 candidate → return result

6. Error Model

|Error              |	Meaning
| ----------------- |:-----------------------------------:|
|UnknownIntent      |	No surviving intent candidates
|AmbiguousIntent    |	More than one surviving candidate
|TokenizerError     |	Tokenizer failed
|ParserpublicError|	public engine bug


7. Sequence Diagram
User Input
    |
    v
Tokenizer -------------------------------+
    | tokens                             |
    v                                    |
Global NLP                               |
  ├─ Find Intent Patterns                  |
  ├─ Build Candidates                    |
  ├─ Collect Validators                  |
  ├─ Run Validators                      |
  ├─ Filter Candidates                   |
  ├─ Ambiguity Resolution                |
  v                                      |
NlpIntentResult                          |
    |                                    |
    v                                    |
Local NLP Parser                         |
    |                                    |
    v                                    |
Command.Execute()                        |


8. Extensibility Model
Global NLP supports:

1. Extending commands

Add new Intent Patterns or new Validators.

2. Adding domain modules

Domain base classes include shared validators.

3. Adding global validators

Used rarely, for cross-domain constraints.

9. FileSystem Domain Example
public abstract class FileSystemCommandBase : ICommand
{
    public virtual IEnumerable<IIntentValidator> GetIntentValidators()
    {
        yield return new BetweenObjectsValidator();
        yield return new WildcardNoiseValidator();
        yield return new ExistingPathValidator();
        yield return new PrepositionConstraintValidator();
    }

    public abstract string Name { get; }
    public abstract IEnumerable<IntentPattern> IntentPatterns { get; }
    public abstract void Execute(LocalNlpResult args);
}


10. Roadmap for v2

Weighted patterns

Context-graph validators

Pattern variables

Debug visualization of validator chain

12. Conclusion

Global NLP v1 is a clean, domain-independent intent system suitable for modular command platforms.

---