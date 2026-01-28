using BlasII.ModdingAPI;
using HarmonyLib;
using Il2CppSystem.Threading.Tasks;
using Il2CppTGK.Game.Components.UI;
using System.Text;

namespace BlasII.BetterSaves;

/// <summary>
/// Display the name and time last played on the slot UI
/// </summary>
[HarmonyPatch(typeof(UISelectableMainMenuSlot), nameof(UISelectableMainMenuSlot.SetSlotData))]
class UISelectableMainMenuSlot_SetSlotData_Patch
{
    public static void Postfix(UISelectableMainMenuSlot __instance, SlotInfo info)
    {
        var list = info.unlockedWeapons;

        int startIdx = -1;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == -1)
            {
                startIdx = i + 1;
                break;
            }
        }

        if (startIdx == -1)
        {
            ModLog.Error("Failed to find starting index in slot info");
            return;
        }

        var bytes = new byte[list.Count - startIdx];
        for (int i = 0; i < bytes.Length; i++)
            bytes[i] = (byte)list[startIdx + i];

        while (startIdx <= list.Count)
            list.RemoveAt(list.Count - 1);

        string name = Encoding.UTF8.GetString(bytes);
        string date = info.dateTime.ToString("MMM d yyyy");
        __instance.zoneName.SetText($"{name}    - {date} -");
    }
}

/// <summary>
/// Adds the slot name to the SlotInfo
/// </summary>
[HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.GetSlotInfo))]
class MainMenuWindowLogic_GetSlotInfo_Patch
{
    public static void Postfix(int index, Task<SlotInfo> __result)
    {
        ModLog.Info($"Storing slot info for slot {index} ({Main.BetterSaves.CurrentSlotName})");

        var list = __result.Result.unlockedWeapons;
        list.Add(-1);

        foreach (byte b in Encoding.UTF8.GetBytes(Main.BetterSaves.CurrentSlotName))
            list.Add(b);
    }
}
