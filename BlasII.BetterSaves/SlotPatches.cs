using BlasII.ModdingAPI;
using HarmonyLib;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.UI;
using UnityEngine;
using UnityEngine.UI;

namespace BlasII.BetterSaves;

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
        ModLog.Error("slot" + CoreCache.SaveData.CurrentSaveSlot);
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
        if (BetterSaves.TOTAL_SLOTS <= 3)
            return;

        int selected = int.Parse(data.obj.name.Split('_')[1]);
        int ypos;

        if (selected == 0)
            ypos = 0;
        else if (selected == BetterSaves.TOTAL_SLOTS - 1)
            ypos = (BetterSaves.TOTAL_SLOTS - 3) * 200;
        else
            ypos = (selected - 1) * 200;

        var parent = data.obj.gameObject.transform.parent.Cast<RectTransform>();
        parent.anchoredPosition = new Vector2(parent.anchoredPosition.x, ypos);
    }
}

/// <summary>
/// Create the UI and load slot data when menu is opened
/// </summary>
[HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.OnOpenSlots))]
class MainMenuWindowLogic_OnOpenSlots_Patch
{
    public static void Postfix(MainMenuWindowLogic __instance)
    {
        if (__instance.slotsList.elementArray.Count < BetterSaves.TOTAL_SLOTS)
        {
            ModLog.Error("Populating UI for new slots");

            // Setup references
            var list = __instance.slotsList.elementArray;
            GameObject template = list[0].obj.gameObject;
            Transform parent = list[0].obj.gameObject.transform.parent;

            // Add image mask to parent
            __instance.slotsList.gameObject.AddComponent<Image>();
            __instance.slotsList.gameObject.AddComponent<Mask>().showMaskGraphic = false;

            // Remove old UI elements
            Object.Destroy(list[1].obj.gameObject);
            Object.Destroy(list[2].obj.gameObject);
            list.Clear();

            // Create new UI elements
            for (int i = 0; i < BetterSaves.TOTAL_SLOTS; i++)
            {
                GameObject slot = Object.Instantiate(template, parent);
                list.Add(new ListData()
                {
                    obj = slot.GetComponent<UISelectableMainMenuSlot>(),
                    row = i,
                    newSelection = false
                });

                slot.name = $"Element_{i}";

                UIPixelTextWithShadow text = slot.transform.Find("Number").GetComponent<UIPixelTextWithShadow>();
                text.SetText((i + 1).ToString());
            }

            // Remove template
            Object.Destroy(template);
        }
        
        if (__instance.slotsInfo.Count < BetterSaves.TOTAL_SLOTS)
        {
            ModLog.Error("Populating info for new slots");

            // Load slot infos
            __instance.slotsInfo.Clear();
            for (int i = 0; i < BetterSaves.TOTAL_SLOTS; i++)
            {
                SlotInfo info = __instance.GetSlotInfo(i).Result;
                __instance.slotsInfo.Add(info);
                CoreCache.SaveData.ResetPersistence();
            }
        }

        // Refresh new slots
        for (int i = 0; i < BetterSaves.TOTAL_SLOTS; i++)
        {
            __instance.RefreshSlotUI(i);
        }

        // Select last slot
        __instance.slotsList.SelectElement(Main.BetterSaves.SelectedSlot);
    }
}
