using EFT;
using EFT.EnvironmentEffect;
using System;
using System.Reflection;
using UnityEngine;

namespace QuietRain
{
    public class QuietRainClass : MonoBehaviour
    {

        public static LocalPlayer localPlayer;
        public static EnvironmentManager environmentManager = null;

        public void Start()
        {
            QuietRainPlugin.RainVolume.SettingChanged += SettingsUpdated;
        }

        public void UpdateQuietRain()
        {
            if (environmentManager != null)
            {
                MethodInfo Init = environmentManager.GetType().GetMethod("Init", BindingFlags.Instance | BindingFlags.NonPublic);
                Init.Invoke(environmentManager, null);
                MethodInfo method_4 = environmentManager.GetType().GetMethod("method_4", BindingFlags.Instance | BindingFlags.NonPublic);
                method_4.Invoke(environmentManager, null);
            }
        }

        private void SettingsUpdated(object sender, EventArgs e)
        {
            UpdateQuietRain();
        }

    }
}