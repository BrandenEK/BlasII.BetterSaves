using BlasII.CheatConsole;

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
                    if (!ValidateParameterCount(args, 2))
                        return;

                    SetName(args[1]);
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
        Main.BetterSaves.UpdateSlotName(name);
    }

    private void ClearName()
    {
        Main.BetterSaves.UpdateSlotName(null);
    }
}
