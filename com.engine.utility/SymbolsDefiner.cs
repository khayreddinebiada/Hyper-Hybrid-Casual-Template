using UnityEditor;
using UnityEngine;

namespace Assets._HC_Engine.com.engine.utility
{
    [InitializeOnLoad]
    public class SymbolsDefiner
    {
        private const string Supports = "Support_Template";
        static SymbolsDefiner()
        {
            AddDefineSymbolSDK(BuildTargetGroup.Android);
            AddDefineSymbolSDK(BuildTargetGroup.iOS);
            AddDefineSymbolSDK(BuildTargetGroup.Standalone);
        }

        static void AddDefineSymbolSDK(BuildTargetGroup targetGroup)
        {
            string result = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);

            if (!result.Contains(Supports))
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, result + ";" + Supports);
                Debug.Log($"{Supports} is added!!");
            }
        }
    }
}