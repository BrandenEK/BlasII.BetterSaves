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

[HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.OnOpenSlots))]
class MainMenuWindowLogic_OnOpenSlots_Patch
{
    public static void Postfix(MainMenuWindowLogic __instance)
    {
        ModLog.Error("Opening main menu");

        var list = __instance.slotsList.elementArray;

        for (int i = 0; i < list.Count; i++)
        {
            int idx = Main.BetterSaves.CurrentPage * 3 + i + 1;

            UIPixelTextWithShadow text = list[i].obj.transform.Find("Number").GetComponent<UIPixelTextWithShadow>();
            text.SetText(idx.ToString());
        }
    }
}
