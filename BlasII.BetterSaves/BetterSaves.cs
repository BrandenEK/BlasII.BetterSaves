using BlasII.CheatConsole;
using BlasII.ModdingAPI;
using BlasII.ModdingAPI.Persistence;

namespace BlasII.BetterSaves;

/// <summary>
/// Allows creating more save files and naming them
/// </summary>
public class BetterSaves : BlasIIMod, ISlotPersistentMod<BsSlotData>, IGlobalPersistentMod<BsGlobalData>
{
    internal BetterSaves() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    private string _currentSlotName;
    private int _selectedSlot;

    /// <summary>
    /// The name of the current save file
    /// </summary>
    public string CurrentSlotName => string.IsNullOrEmpty(_currentSlotName) ? NO_NAME : _currentSlotName;

    /// <summary>
    /// The index of the slot that should be selected when loaded the menu
    /// </summary>
    public int SelectedSlot => _selectedSlot >= 0 && _selectedSlot < TOTAL_SLOTS ? _selectedSlot : 0;

    /// <summary>
    /// Updates the current slot name
    /// </summary>
    public void UpdateSlotName(string name)
    {
        _currentSlotName = name;
    }

    /// <summary>
    /// Updates the selected slot
    /// </summary>
    public void UpdateSelectedSlot(int slot)
    {
        _selectedSlot = slot;
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
    /// Saves the selected slot
    /// </summary>
    public BsGlobalData SaveGlobal()
    {
        return new BsGlobalData()
        {
            SelectedSlot = _selectedSlot
        };
    }

    /// <summary>
    /// Loads the selected slot
    /// </summary>
    public void LoadGlobal(BsGlobalData data)
    {
        _selectedSlot = data.SelectedSlot;
    }

    /// <summary>
    /// The total number of slots allowed
    /// </summary>
    public const int TOTAL_SLOTS = 10;

    /// <summary>
    /// The display name for a save file without a name
    /// </summary>
    private const string NO_NAME = "Unnamed save file";
}
