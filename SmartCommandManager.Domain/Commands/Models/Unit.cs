namespace SmartCommandManager.Domain.Commands.Models
{
    public sealed class Unit
    {
        private Unit() { }
        public static readonly Unit Value = new Unit();
    }
}
