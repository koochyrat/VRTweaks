using HarmonyLib;

namespace VRTweaks
{
    [HarmonyPatch(typeof(IngameMenu), "UpdateRaycasterStatus")]
    public static class GamepadGazeCursorFixer
    {
        [HarmonyPrefix]
        public static bool Prefix(ref uGUI_GraphicRaycaster raycaster, IngameMenu __instance)
        {
            if (GameInput.IsPrimaryDeviceGamepad() && !VROptions.GetUseGazeBasedCursor())
            {
                raycaster.enabled = false;
            }
            else
            {
                raycaster.enabled = __instance.focused;
            }

            return false;
        }
    }
}
