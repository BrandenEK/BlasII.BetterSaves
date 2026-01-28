using BlasII.ModdingAPI;
using HarmonyLib;
using Il2CppSystem.Threading.Tasks;
using Il2CppTGK.Game.Components.UI;

namespace BlasII.BetterSaves;

/// <summary>
/// Display the name and time last played on the slot UI
/// </summary>
[HarmonyPatch(typeof(UISelectableMainMenuSlot), nameof(UISelectableMainMenuSlot.SetSlotData))]
class UISelectableMainMenuSlot_SetSlotData_Patch
{
    public static void Postfix(UISelectableMainMenuSlot __instance, SlotInfo info)
    {
        //NewSlotInfo newInfo = info.Cast<NewSlotInfo>();

        if (info is not NewSlotInfo)
        {
            ModLog.Error("slot info was not overwritten");
            return;
        }

        NewSlotInfo newInfo = (NewSlotInfo)info;

        string name = newInfo.SlotName;
        string date = info.dateTime.ToString("MMM d yyyy");
        __instance.zoneName.SetText($"{name}    - {date} -");
    }
}

/// <summary>
/// 
/// </summary>
[HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.GetSlotInfo))]
class MainMenuWindowLogic_GetSlotInfo_Patch
{
    public static void Postfix(int index, Task<SlotInfo> __result)
    {
        ModLog.Info($"Replacing slot info for slot {index}");

        //var list = __result.Result.unlockedWeapons;

        __result.m_result = new NewSlotInfo(__result.Result, Main.BetterSaves.CurrentSlotName);

    }
}

public class NewSlotInfo : SlotInfo
{
    public string SlotName { get; }

    public NewSlotInfo(SlotInfo info, string name)
    {
        canConvertNGPlus = info.canConvertNGPlus;
        challengerTier = info.challengerTier;
        currentZone = info.currentZone;
        dateTime = info.dateTime;
        doves = info.doves;
        error = info.error;
        hasChallengesActive = info.hasChallengesActive;
        isNGPlus = info.isNGPlus;
        percentValue = info.percentValue;
        playtimeHours = info.playtimeHours;
        playtimeMinutes = info.playtimeMinutes;
        state = info.state;
        unlockedWeapons = info.unlockedWeapons;
        SlotName = name;
    }
}
