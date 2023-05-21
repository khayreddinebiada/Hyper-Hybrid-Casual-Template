#if UNITY_EDITOR
using Engine;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [InitializeOnLoad]
    public class EditorModeSwitcher
    {
        static EditorModeSwitcher()
        {
            EditorApplication.playModeStateChanged += ModeChanged;
        }

        private static void ModeChanged(PlayModeStateChange state)
        {
            Debug.Log("ModeChanged: " + state);
            IModeChanged[] modes = FindHelper.FindAllAssetsOfType<IModeChanged>();
            foreach (IModeChanged mode in modes)
            {
                if (mode != null)
                    mode.OnModeChanged(state);
            }
        }
    }
}
#endif