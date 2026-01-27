using BlasII.ModdingAPI;
using BlasII.ModdingAPI.Persistence;

namespace BlasII.BetterSaves;

/// <summary>
/// Allows creating more save files and naming them
/// </summary>
public class BetterSaves : BlasIIMod, ISlotPersistentMod<BatterSavesSlotData>
{
    internal BetterSaves() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    /// <summary>
    /// The name of the current save file
    /// </summary>
    public string CurrentSlotName { get; private set; }

    /// <summary>
    /// Saves the current slot name
    /// </summary>
    public BatterSavesSlotData SaveSlot()
    {
        return new BatterSavesSlotData()
        {
            SlotName = CurrentSlotName
        };
    }

    /// <summary>
    /// Loads the current slot name
    /// </summary>
    public void LoadSlot(BatterSavesSlotData data)
    {
        CurrentSlotName = data.SlotName;
    }

    /// <summary>
    /// Resets the current slot name
    /// </summary>
    public void ResetSlot()
    {
        CurrentSlotName = null;
    }

    /// <summary>
    /// The total number of slots allowed
    /// </summary>
    public const int TOTAL_SLOTS = 10;
}
