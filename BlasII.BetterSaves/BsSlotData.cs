using BlasII.ModdingAPI.Persistence;

namespace BlasII.BetterSaves;

/// <summary>
/// Stores slot save data for the Better Saves mod
/// </summary>
public class BsSlotData : SlotSaveData
{
    /// <summary>
    /// The name of the slot
    /// </summary>
    public string SlotName { get; set; }
}
