using BlasII.ModdingAPI;
using HarmonyLib;
using Il2CppTGK.Game.Components.UI;
using UnityEngine;

namespace BlasII.BetterSaves;

//[HarmonyPatch(typeof(UISelectableMainMenuSlot), nameof(UISelectableMainMenuSlot.UpdateSelected))]
//class UISelectableMainMenuSlot_UpdateSelected_Patch
//{
//    public static void Postfix(UISelectableMainMenuSlot __instance)
//    {
//        ModLog.Error(__instance.name + ": update");

//        Transform child = __instance.transform.Find("Number");

//        if (child == null)
//        {
//            ModLog.Error("Failed to find number transform");
//            return;
//        }

//        UIPixelTextWithShadow text = child.GetComponent<UIPixelTextWithShadow>();

//        if (text == null)
//        {
//            ModLog.Error("Failed to find text component");
//            return;
//        }

//        ModLog.Warn("Text is [" + text.normalText.text + "]");
//        text.SetText("5");
//    }
//}

[HarmonyPatch(typeof(UISelectableMainMenuSlot), nameof(UISelectableMainMenuSlot.SetSelected))]
class t
{
    public static void Postfix(UISelectableMainMenuSlot __instance, bool selected)
    {
        ModLog.Info($"{__instance.name} is selected: {selected}");
    }
}

[HarmonyPatch(typeof(UISelectableMainMenuSlot), nameof(UISelectableMainMenuSlot.SetSlotData))]
class t1
{
    public static void Postfix(UISelectableMainMenuSlot __instance, SlotInfo info)
    {
        ModLog.Info($"{__instance.name} is setting slot data: {info.playtimeMinutes}");
    }
}

[HarmonyPatch(typeof(UISelectableMainMenuSlot), nameof(UISelectableMainMenuSlot.SetSlotInfo))]
class t2
{
    public static void Postfix(UISelectableMainMenuSlot __instance, SlotInfo info)
    {
        ModLog.Info($"{__instance.name} is setting slot info: {info.playtimeMinutes}");
    }
}

[HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.OnOpenSlots))]
class MainMenuWindowLogic_OnOpenSlots_Patch
{
    public static void Postfix(MainMenuWindowLogic __instance)
    {
        ModLog.Error("Opening slots menu");

        if (Main.BetterSaves.TempDoneWithInit)
            return;

        Main.BetterSaves.TempDoneWithInit = true;

        int total = 9;

        var list = __instance.slotsList.elementArray;
        Transform parent = list[0].obj.gameObject.transform.parent;

        for (int i = 3; i < total; i++)
        {
            GameObject slot = Object.Instantiate(list[i % 3].obj.gameObject, parent);
            list.Add(new ListData()
            {
                obj = slot.GetComponent<UISelectableMainMenuSlot>(),
                row = i,
                newSelection = false
            });
            __instance.slotsInfo.Add(new SlotInfo());

            slot.name = $"Element_{i}";

            UIPixelTextWithShadow text = slot.transform.Find("Number").GetComponent<UIPixelTextWithShadow>();
            text.SetText((i + 1).ToString());
        }

        for (int i = 3; i < total; i++)
        {
            __instance.RefreshSlotUI(i);
        }
    }
}
