using BlasII.ModdingAPI;
using BlasII.ModdingAPI.Persistence;
using UnityEngine;

namespace BlasII.BetterSaves;

/// <summary>
/// Allows creating more save files and naming them
/// </summary>
public class BetterSaves : BlasIIMod, ISlotPersistentMod<BatterSavesSlotData>
{
    internal BetterSaves() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    private string _currentSlotName;

    /// <summary>
    /// The name of the current save file
    /// </summary>
    public string CurrentSlotName => _currentSlotName ?? "Unnamed save file";

    /// <summary>
    /// Saves the current slot name
    /// </summary>
    public BatterSavesSlotData SaveSlot()
    {
        return new BatterSavesSlotData()
        {
            SlotName = $"Frame{Time.frameCount}"
            //SlotName = _currentSlotName
        };
    }

    /// <summary>
    /// Loads the current slot name
    /// </summary>
    public void LoadSlot(BatterSavesSlotData data)
    {
        _currentSlotName = data.SlotName;
    }

    /// <summary>
    /// Resets the current slot name
    /// </summary>
    public void ResetSlot()
    {
        _currentSlotName = null;
    }

    /// <summary>
    /// The total number of slots allowed
    /// </summary>
    public const int TOTAL_SLOTS = 10;
}
