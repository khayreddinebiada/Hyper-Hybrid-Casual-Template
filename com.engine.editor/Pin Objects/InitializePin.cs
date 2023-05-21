using UnityEditor;

namespace Editor.pin
{
    [InitializeOnLoad]
    sealed class InitializePin : UnityEditor.Editor
    {
        [InitializeOnLoadMethod]
        public static void Initialize()
        {
            PinListInfo.OnInitializeProject();
            PinMaker.OnInitializeProject();
            PinSceneView.OnInitializeProject();
        }
    }
}