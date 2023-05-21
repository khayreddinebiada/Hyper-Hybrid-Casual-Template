using Engine.Attribute;
using UnityEngine;

namespace Editor
{
    [TemplateSettings(FilePath, FileName)]
    public class AssetsSettings : ScriptableObject
    {
        internal const string FilePath = "Assets/Settings/Editor/";
        internal const string FileName = "AssetsSettings";

        [Header("Build Settings")]
        public bool ResetData = true;
        public bool Validate = true;
        public bool SaveAssets = true;
    }
}
