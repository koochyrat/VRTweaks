using UnityEngine;
using HarmonyLib;
using UWEXR;

namespace VRTweaks
{
    class CameraPositionFixes
    {

        // In the base game, the camera position is shifted backwards behind the neck while
        // the PDA is not out, and when the PDA is out, it is shifted forwards to the "proper" location.
        // This patch keeps the camera position aligned on the center of the player body always.
        // A side effect is that it also no longer causes the player body to be clipped at the value
        // it was by default, so looking down you can see the body without clipping.
        [HarmonyPatch(typeof(MainCameraControl), nameof(MainCameraControl.LateUpdate))]
        class CameraForwardPosition_Patch
        {
            static bool Prefix(MainCameraControl __instance)
            {
                __instance.cameraUPTransform.localPosition
                    = new Vector3(__instance.cameraUPTransform.localPosition.x, __instance.cameraUPTransform.localPosition.y, 0f);
                return false;
            }
        }

        [HarmonyPatch(typeof(SNCameraRoot), nameof(SNCameraRoot.UpdateVR))]
        public class SNCameraRoot_Patch
        {
            static bool Prefix(SNCameraRoot __instance)
            {
                if (!XRSettings.enabled)
                    return false;

                float num = __instance.mainCamera.stereoSeparation;
                if (Mathf.Abs(__instance.stereoSeparation - num) < 1E-05f)
                {
                    return false;
                }
                float yawAngle = PimaxCullingInit.cantingAngle * Mathf.Rad2Deg; //10 deg
                __instance.stereoSeparation = num;
                __instance.matrixLeftEye = Matrix4x4.TRS(__instance.stereoSeparation * 0.5f * Vector3.right, Quaternion.AngleAxis(-yawAngle, Vector3.up), new Vector3(1, 1, -1));
                __instance.matrixRightEye = Matrix4x4.TRS(-__instance.stereoSeparation * 0.5f * Vector3.right, Quaternion.AngleAxis(yawAngle, Vector3.up), new Vector3(1, 1, -1));

                __instance.guiCamera.SetStereoViewMatrix(Camera.StereoscopicEye.Left, __instance.matrixLeftEye);
                __instance.guiCamera.SetStereoViewMatrix(Camera.StereoscopicEye.Right, __instance.matrixRightEye);
                __instance.imguiCamera.SetStereoViewMatrix(Camera.StereoscopicEye.Left, __instance.matrixLeftEye);
                __instance.imguiCamera.SetStereoViewMatrix(Camera.StereoscopicEye.Right, __instance.matrixRightEye);
                return false;
            }
        }

    }
}

