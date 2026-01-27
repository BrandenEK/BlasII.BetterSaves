using Il2CppTGK.Game.Components.UI;
using HarmonyLib;

namespace BlasII.BetterSaves;

/// <summary>
/// Display the name and time last played on the slot UI
/// </summary>
[HarmonyPatch(typeof(UISelectableMainMenuSlot), nameof(UISelectableMainMenuSlot.SetSlotData))]
class UISelectableMainMenuSlot_SetSlotData_Patch
{
    public static void Postfix(UISelectableMainMenuSlot __instance, SlotInfo info)
    {
        string name = Main.BetterSaves.CurrentSlotName;
        string date = info.dateTime.ToString("MMM d yyyy");
        __instance.zoneName.SetText($"{name}    - {date} -");
    }
}
