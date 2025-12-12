namespace SmartCommandManager.NLP.Shared.Interfaces
{
    public interface IValidator<T>
    {
        /// <summary>Throw ValidationException on invalid.</summary>
        void Validate(T model);
    }
}
