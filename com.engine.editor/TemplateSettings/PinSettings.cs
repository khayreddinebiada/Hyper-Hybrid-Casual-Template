using Engine.Attribute;
using UnityEngine;

namespace Editor.pin
{
    [TemplateSettings(FilePath, FileName)]
    internal sealed class PinSettings : ScriptableObject
    {
        public const string FilePath = "Assets/Settings/Editor/";
        public const string FileName = "PinSettings";

        public Vector2 ButtonScale = new Vector2(80, 20);
        public PinInfo[] PinInfos;


        public void UpdatePinInfo(PinInfo[] pinInfo)
        {
            PinInfos = pinInfo;
        }

        public void OnValidate()
        {
            PinListInfo.UpdatePinsInfo(PinInfos);
        }
    }
}