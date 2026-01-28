using BlasII.ModdingAPI.Persistence;

namespace BlasII.BetterSaves;

/// <summary>
/// Stores global save data for the Better Saves mod
/// </summary>
public class BsGlobalData : GlobalSaveData
{
    /// <summary>
    /// The last slot to be selected
    /// </summary>
    public int SelectedSlot { get; set; }
}
