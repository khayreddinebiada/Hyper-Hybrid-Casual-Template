using UnityEditor;
using UnityEngine;

namespace Editor.pin
{
    public class PinSceneView : UnityEditor.Editor
    {
        private const int Padding = 5;

        private static bool IsInited = false;

        internal static void OnInitializeProject()
        {
            if (!IsInited)
            {
                SceneView.duringSceneGui += UpdateSceneView;
                IsInited = true;
            }
        }

        private static void UpdateSceneView(SceneView sceneView)
        {
            Handles.BeginGUI();

            int j = 0;
            for (int i = 0; i < PinListInfo.Count; i++)
            {
                string nickName = PinListInfo.GetNickName(i);
                Object obj = PinListInfo.GetPinObject(i);
                if (obj == null) continue;

                if (GUI.Button(
                    new Rect(
                    sceneView.camera.pixelWidth - PinListInfo.Settings.ButtonScale.x - 15,
                    120 + ((Padding + PinListInfo.Settings.ButtonScale.y) * j),
                    PinListInfo.Settings.ButtonScale.x,
                    PinListInfo.Settings.ButtonScale.y),
                    nickName, GetButtonStyle()))
                {
                    AssetHelper.PingObject(obj);
                }

                if (GUI.Button(new Rect(sceneView.camera.pixelWidth - PinListInfo.Settings.ButtonScale.x - 40,
                    120 + ((Padding + PinListInfo.Settings.ButtonScale.y) * j), 20f, 20f), "X", GetButtonStyle()))
                {
                    PinListInfo.RemovePinObject(obj);
                }
                j++;
            }

            Handles.EndGUI();
        }

        private static GUIStyle GetButtonStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.alignment = TextAnchor.MiddleLeft;
            return style;
        }
    }

}