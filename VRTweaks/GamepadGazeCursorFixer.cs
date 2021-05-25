using HarmonyLib;

namespace VRTweaks
{
    [HarmonyPatch(typeof(IngameMenu), "UpdateRaycasterStatus")]
    public static class PauseMenu
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

    [HarmonyPatch(typeof(uGUI_PDA), "UpdateRaycasterStatus")]
    public static class PDAMenu
    {
        [HarmonyPrefix]
        public static bool Prefix(ref uGUI_GraphicRaycaster raycaster, uGUI_PDA __instance)
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


    //TODO fix crafting menu by editing GamepadInputModule
//private void LateUpdate()
//	{
//		this.isControlling = false;
//		bool flag = this.usingController;
//THIS IS THE PROBLEM!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//		this.usingController = (!VROptions.GetUseGazeBasedCursor() && GameInput.GetPrimaryDevice() == GameInput.Device.Controller);
//THIS IS THE PROBLEM!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//		if (!this.usingController)
//		{
//			if (flag && this.currentNavigableGrid != null)
//			{
//				this.currentNavigableGrid.DeselectItem();
//			}
//			return;
//		}
//		if (!flag)
//		{
//			this.OnGroupChanged(this.currentGroup);
//		}
//		if (this.skipOneInputFrame)
//		{
//			this.skipOneInputFrame = false;
//			return;
//		}
//		if (this.IsInputAllowed() && this.currentGroup != null)
//		{
//			this.isControlling = true;
//			this.ProcessInput();
//		}
//		if (Inventory.main)
//		{
//			Inventory.main.UpdateContainers();
//		}
//	}
}
