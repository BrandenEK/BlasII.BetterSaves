using BlasII.ModdingAPI;

namespace BlasII.BetterSaves;

public class BetterSaves : BlasIIMod
{
    internal BetterSaves() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    public int CurrentPage { get; private set; }

    protected override void OnInitialize()
    {
        CurrentPage = 1;
    }
}
