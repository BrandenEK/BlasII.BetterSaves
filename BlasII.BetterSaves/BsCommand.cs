using BlasII.CheatConsole;
using System.Linq;

namespace BlasII.BetterSaves;

internal class BsCommand : ModCommand
{
    public BsCommand() : base("slotname") { }

    public override void Execute(string[] args)
    {
        switch (args[0])
        {
            case "set":
                {
                    SetName(string.Join(' ', args.Skip(1)));
                    break;
                }
            case "clear":
                {
                    if (!ValidateParameterCount(args, 1))
                        return;

                    ClearName();
                    break;
                }
            default:
                {
                    WriteFailure("Unknown subcommand: " + args[0]);
                    break;
                }
        }
    }

    private void SetName(string name)
    {
        Write($"Setting slot name to \"{name}\"");
        Main.BetterSaves.UpdateSlotName(name);
    }

    private void ClearName()
    {
        Write("Clearing slot name");
        Main.BetterSaves.UpdateSlotName(null);
    }
}
