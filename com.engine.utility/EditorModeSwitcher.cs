#if UNITY_EDITOR
using HCEngine;
using UnityEditor;

namespace HCEditor
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