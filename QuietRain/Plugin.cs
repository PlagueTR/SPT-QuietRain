using System.Reflection;
using Aki.Reflection.Patching;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using EFT.EnvironmentEffect;

namespace QuietRain
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class QuietRainPlugin : BaseUnityPlugin
    {

        public static GameObject Hook;
        public static QuietRainClass QuietRainClassComponent;

        public static ConfigEntry<float> RainVolume { get; set; }

        private void Awake()
        {
            Debug.LogError("Quiet Rain Awake()");
            Hook = new GameObject();
            QuietRainClassComponent = Hook.AddComponent<QuietRainClass>();
            DontDestroyOnLoad(Hook);
        }

        private void Start()
        {
            RainVolume = Config.Bind("World", "Rain Volume", 1.0f, new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 100, IsAdvanced = false }));

            new QuietRainPatch().Enable();
        }

        public class QuietRainPatch : ModulePatch
        {
            protected override MethodBase GetTargetMethod()
            {
                return typeof(EnvironmentManager).GetMethod("Init", BindingFlags.Instance | BindingFlags.NonPublic);
            }
            [PatchPrefix]
            private static void PatchPreFix(ref EnvironmentManager __instance)
            {
                QuietRainClass.environmentManager = __instance;
                Traverse.Create(__instance).Field("OutdoorRainVolume").SetValue(RainVolume.Value);
                Traverse.Create(__instance).Field("RainVolume").SetValue(RainVolume.Value * 0.7f);
            }
        }

    }
}
