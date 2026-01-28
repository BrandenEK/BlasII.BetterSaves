using BlasII.CheatConsole;
using BlasII.ModdingAPI;
using BlasII.ModdingAPI.Persistence;
using System.Collections.Generic;

namespace BlasII.BetterSaves;

/// <summary>
/// Allows creating more save files and naming them
/// </summary>
public class BetterSaves : BlasIIMod, ISlotPersistentMod<BsSlotData>
{
    internal BetterSaves() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    private readonly Dictionary<int, string> _slotNames = [];

    private string _currentSlotName;
    private int _loadedSlot;

    /// <summary>
    /// The name of the current save file
    /// </summary>
    public string CurrentSlotName => _currentSlotName ?? NO_NAME;

    /// <summary>
    /// The name of the save file that is loaded on the main menu
    /// </summary>
    public string MenuSlotName => _slotNames.TryGetValue(_loadedSlot, out string name) ? name : NO_NAME;

    /// <summary>
    /// Updates the current slot name
    /// </summary>
    public void UpdateSlotName(string name)
    {
        _currentSlotName = name;
    }

    /// <summary>
    /// Updates the loaded slot on the main menu
    /// </summary>
    public void UpdateLoadedSlot(int slot)
    {
        _loadedSlot = slot;
    }

    /// <summary>
    /// Registers the slotname command
    /// </summary>
    protected override void OnRegisterServices(ModServiceProvider provider)
    {
        provider.RegisterCommand(new BsCommand());
    }

    /// <summary>
    /// Saves the current slot name
    /// </summary>
    public BsSlotData SaveSlot()
    {
        return new BsSlotData()
        {
            SlotName = _currentSlotName
        };
    }

    /// <summary>
    /// Loads the current slot name
    /// </summary>
    public void LoadSlot(BsSlotData data)
    {
        ModLog.Warn($"Loaded name {data.SlotName} for slot {_loadedSlot}");
        _currentSlotName = data.SlotName;
        _slotNames[_loadedSlot] = data.SlotName;
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

    private const string NO_NAME = "Unnamed save file";
}
