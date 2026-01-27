using BlasII.ModdingAPI;
using HarmonyLib;
using Il2CppSystem.Threading.Tasks;
using Il2CppTGK.Game.Components.UI;
using Il2CppTGK.Map;

namespace BlasII.BetterSaves;

/// <summary>
/// Updates the SlotInfo with randomizer data
/// </summary>
[HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.GetSlotInfo))]
class MainMenuWindowLogic_GetSlotInfo_Patch
{
    public static void Postfix(int index, Task<SlotInfo> __result)
    {
        //ModLog.Info($"Replacing slot info for slot {index}");

        //__result.Result.currentZone = new ZoneID()
        //{
        //    id = __result.Result.currentZone.id,
        //    name = Main.BetterSaves.CurrentSlotName,
        //};
    }
}

/// <summary>
/// Display the name and time last played on the slot UI
/// </summary>
[HarmonyPatch(typeof(UISelectableMainMenuSlot), nameof(UISelectableMainMenuSlot.SetSlotData))]
class UISelectableMainMenuSlot_SetSlotData_Patch
{
    public static void Postfix(UISelectableMainMenuSlot __instance, SlotInfo info)
    {
        string name = info.currentZone.name;
        string date = info.dateTime.ToString("MMM d yyyy");
        __instance.zoneName.SetText($"{name}    - {date} -");
    }
}
