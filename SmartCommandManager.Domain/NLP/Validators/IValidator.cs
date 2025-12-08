namespace SmartCommandManager.Domain.NLP.Validators
{
    public interface IValidator<T>
    {
        void Validate(T model);
    }
}
