using BlasII.CheatConsole.Attributes;
using BlasII.CheatConsole.Commands;

namespace BlasII.BetterSaves;

internal class BsCommand : ModComplexCommand
{
    public BsCommand() : base("slotname") { }

    [SubCommand]
    private void Set(string name)
    {
        Write($"Setting slot name to '{name}'");
        Main.BetterSaves.UpdateSlotName(name);
    }

    [SubCommand]
    private void Clear()
    {
        Write("Clearing slot name");
        Main.BetterSaves.UpdateSlotName(null);
    }
}
