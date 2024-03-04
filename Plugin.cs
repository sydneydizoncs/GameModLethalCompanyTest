using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalTesting
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class LethalTestingBase : BaseUnityPlugin
    {
        private const string modGUID = "JiRath.LethalTesting";
        private const string modName = "LethalTesting";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);
        private static LethalTestingBase Instance;
        internal ManualLogSource mls;

        void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo("Lethal Testing mod has been loaded.");

            harmony.PatchAll();
        }
    }
}
