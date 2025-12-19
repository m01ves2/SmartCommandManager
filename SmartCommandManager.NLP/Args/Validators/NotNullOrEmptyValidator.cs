using System.ComponentModel.DataAnnotations;

namespace SmartCommandManager.NLP.Args.Validators
{
    public static class NotNullOrEmptyValidator
    {
        public static void Validate(string? value, string name = "Value")
        {
            if ( string.IsNullOrEmpty(value) )
                throw new ValidationException($"Validation error: {name} must not be empty");
        }
    }
}
