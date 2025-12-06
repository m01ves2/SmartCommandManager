namespace SmartCommandManager.UI.CLI
{
    static void Main()
    {
        var provider = Bootstrap.Build();
        var dispatcher = provider.GetRequiredService<ICommandDispatcher>();
        var loop = provider.GetRequiredService<CommandLoop>();

        loop.Run();
    }
}