namespace SmartCommandManager.Domain.Commands
{
    public sealed class Unit
    {
        private Unit() { }
        public static readonly Unit Value = new Unit();
    }
}
