using BlasII.ModdingAPI;

namespace BlasII.BetterSaves;

public class BetterSaves : BlasIIMod
{
    internal BetterSaves() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    protected override void OnInitialize()
    {
    }

    /// <summary>
    /// The total number of slots allowed
    /// </summary>
    public const int TOTAL_SLOTS = 10;
}
