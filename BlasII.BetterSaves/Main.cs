using MelonLoader;

namespace BlasII.BetterSaves;

internal class Main : MelonMod
{
    public static BetterSaves BetterSaves { get; private set; }

    public override void OnLateInitializeMelon()
    {
        BetterSaves = new BetterSaves();
    }
}