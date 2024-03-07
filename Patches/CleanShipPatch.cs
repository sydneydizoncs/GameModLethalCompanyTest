using System.Collections;
using HarmonyLib;
using Unity.Netcode;
using UnityEngine;

namespace LethalTesting.Patches
{
    internal class CleanShipPatch
    {
        [HarmonyPatch(typeof(StartOfRound), "Start")]
        [HarmonyPostfix]
        private static void StartPatch()
        {
            LethalTestingBase.Instance.mls.LogInfo((object)"Patching Start in StartOfRound");
            CleanShip();
        }

        [HarmonyPatch(typeof(RoundManager), "GenerateNewLevelClientRpc")]
        [HarmonyPostfix]
        private static void GenerateNewLevelClientRpcPatch(int randomSeed)
        {
            LethalTestingBase.Instance.mls.LogInfo((object)"Patching GenerateNewLevelClientRpc in RoundManager");
            CleanShip();
        }

        private static void CleanShip()
        {
            GameObject.Destroy(GameObject.Find("HangarShip/FileCabinet"));
            GameObject.Destroy(GameObject.Find("HangarShip/ClipboardManual"));
            GameObject.Destroy(GameObject.Find("HangarShip/Bunkbeds"));
            GameObject.Destroy(GameObject.Find("HangarShip/StickyNoteItem"));
            GameObject.Destroy(GameObject.Find("HangarShip/StorageCloset"));

            GameObject Terminal = GameObject.Find("HangarShip/Terminal");
            Vector3 position = default(Vector3);
            position.x = 10.0203f;
            position.y = 1.2506f;
            position.z = -12.0479f;
            
            Vector3 rotation = default(Vector3);
            rotation.x = 270f;
            rotation.y = 338.2519f;
            rotation.z = 0f;

            var TerminalShipObject = Terminal.GetComponentInChildren<PlaceableShipObject>();
            ShipBuildModeManager.Instance.PlaceShipObject(position, rotation, TerminalShipObject);

            /*
            var Bunkbeds = GameObject.Find("HangarShip/Bunkbeds");
            if (Bunkbeds != null)
            {
                LethalTestingBase.Instance.mls.LogInfo((object)"Attempting to put bunkbed into storage...");
                Traverse.Create(ShipBuildModeManager.Instance).Field("timeSincePlacingObject")
                    .SetValue(1f);
                Traverse.Create(ShipBuildModeManager.Instance).Field("InBuildMode")
                    .SetValue(true);
                
                Traverse.Create(ShipBuildModeManager.Instance).Field("placingObject")
                    .SetValue(Bunkbeds.GetComponentInChildren<PlaceableShipObject>());
                ShipBuildModeManager.Instance.StoreObjectLocalClient();
            }
            */
        }
    }
}