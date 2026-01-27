using BlasII.ModdingAPI;
using HarmonyLib;
using Il2CppTGK.Game.Components.UI;
using UnityEngine;
using UnityEngine.UI;

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

[HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.OnSlotAccept))]
class t3
{
    public static void Postfix(MainMenuWindowLogic __instance, int index, SlotInfo info)
    {
        ModLog.Info($"Accept slot for index {index}");
    }
}

[HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.OnSlotAcceptPressed))]
class t4
{
    public static void Postfix(MainMenuWindowLogic __instance)
    {
        ModLog.Info($"Accept slot pressed");
    }
}

[HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.InitializeAll))]
class t7
{
    public static void Postfix(MainMenuWindowLogic __instance)
    {
        ModLog.Error($"InitializeAll");
    }
}

[HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.InitializeSlots))]
class t8
{
    public static void Postfix(MainMenuWindowLogic __instance)
    {
        ModLog.Error($"InitializeSlots");
    }
}

/// <summary>
/// Scroll the slots menu whenever a new slot is selected
/// </summary>
[HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.OnSlotSelected))]
class MainMenuWindowLogic_OnSlotSelected_Patch
{
    public static void Postfix(MainMenuWindowLogic __instance, ListData data)
    {
        if (TOTAL_SLOTS <= 3)
            return;

        int selected = int.Parse(data.obj.name.Split('_')[1]);
        int ypos;

        if (selected == 0)
            ypos = 0;
        else if (selected == TOTAL_SLOTS - 1)
            ypos = (TOTAL_SLOTS - 3) * 200;
        else
            ypos = (selected - 1) * 200;

        var parent = data.obj.gameObject.transform.parent.Cast<RectTransform>();
        parent.anchoredPosition = new Vector2(parent.anchoredPosition.x, ypos);
    }

    private const int TOTAL_SLOTS = 9;
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

        // Setup references
        var list = __instance.slotsList.elementArray;
        GameObject template = list[0].obj.gameObject;
        Transform parent = list[0].obj.gameObject.transform.parent;

        // Add image mask to parent
        parent.parent.gameObject.AddComponent<Image>();
        var mask = parent.parent.gameObject.AddComponent<Mask>();
        mask.showMaskGraphic = false;

        // Create new UI elements
        for (int i = 3; i < TOTAL_SLOTS; i++)
        {
            GameObject slot = Object.Instantiate(template, parent);
            list.Add(new ListData()
            {
                obj = slot.GetComponent<UISelectableMainMenuSlot>(),
                row = i,
                newSelection = false
            });

            SlotInfo info = __instance.GetSlotInfo(i).Result;
            __instance.slotsInfo.Add(info);

            slot.name = $"Element_{i}";

            UIPixelTextWithShadow text = slot.transform.Find("Number").GetComponent<UIPixelTextWithShadow>();
            text.SetText((i + 1).ToString());
        }

        // Refresh new slots
        for (int i = 3; i < TOTAL_SLOTS; i++)
        {
            __instance.RefreshSlotUI(i);
        }
    }

    private const int TOTAL_SLOTS = 9;

    // TODO
    // Clicking a slot doesnt actually start
    // Always populated with empty
    // Cant scroll down
    // Mask or hide other slots
    // Reset initialized
    // Selected slot doesnt persist across game
}
