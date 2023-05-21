using Engine.utility;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace Editor.pin
{
    public static class PinListInfo
    {
        public static List<PinInfo> PinInfo = new List<PinInfo>();

        private const string PinCountKey = "PIN_COUNT_KEY";
        private const string PinItemKey = "PIN_ITEM_KEY";

        private static readonly string EmptyNickName = string.Empty;
        private static readonly string EmptyPath = string.Empty;

        private const int EmptyValueSave = 0;
        private const int OverClearValue = 20;


        private static PinSettings _settings;
        internal static PinSettings Settings
        {
            get
            {
                if (_settings == null) _settings = AssetHelper.GetOrCreateAsset<PinSettings>(PinSettings.FilePath, PinSettings.FileName + ".asset");

                return _settings;
            }
        }

        internal static int Count
        {
            get { return EditorPrefs.GetInt(PinCountKey, EmptyValueSave); }
            set { EditorPrefs.SetInt(PinCountKey, value); }
        }

        public static void OnInitializeProject()
        {
            LoadPaths();

            Settings.UpdatePinInfo(ToArrayPinInfo());
        }

        internal static void LoadPaths()
        {
            int count = Count;
            if (count == 0) return;

            for (int i = 0; i < count; i++)
            {
                if (EditorPrefs.HasKey(PinItemKey + i))
                {
                    PinInfo newInfo = new PinInfo(EmptyNickName, EmptyPath);
                    newInfo.JsonToObject(EditorPrefs.GetString(PinItemKey + i));
                    PinInfo.Add(newInfo);
                }
            }
        }

        internal static void UpdatePinsInfo(PinInfo[] infos)
        {
            Count = infos.Length;
            PinInfo = new List<PinInfo>(infos);

            for (int i = 0; i < PinInfo.Count; i++)
            {
                EditorPrefs.SetString(PinItemKey + i, JsonConverter.ObjectToJson(PinInfo[i]));
            }
        }

        internal static void Save()
        {
            Count = PinInfo.Count;

            for (int i = 0; i < PinInfo.Count; i++)
            {
                EditorPrefs.SetString(PinItemKey + i, JsonConverter.ObjectToJson(PinInfo[i]));
            }
        }

        public static bool AddPinObject(UnityEngine.Object obj)
        {
            if (PinInfo.Exists((pinInfo) =>
            {
                if (pinInfo.Path == AssetDatabase.GetAssetPath(obj)) return true;

                return false;
            }))
                return false;

            PinInfo.Add(new PinInfo(obj));

            Settings.UpdatePinInfo(ToArrayPinInfo());

            Save();
            return true;
        }

        public static void RemovePinObject(UnityEngine.Object obj)
        {
            PinInfo.RemoveAt(PinInfo.FindIndex((path) =>
            {
                if (path.Path == AssetDatabase.GetAssetPath(obj)) return true;
                return false;
            }));

            Settings.UpdatePinInfo(ToArrayPinInfo());

            Save();
        }

        public static void RemoveAllPinObjects()
        {
            PinInfo.Clear();

            Settings.UpdatePinInfo(Array.Empty<PinInfo>());

            int count = Count + OverClearValue;
            for (int i = 0; i < count; i++)
            {
                if (EditorPrefs.HasKey(PinItemKey + i))
                    EditorPrefs.DeleteKey(PinItemKey + i);
            }

            if (EditorPrefs.HasKey(PinCountKey))
                EditorPrefs.DeleteKey(PinCountKey);
        }

        public static UnityEngine.Object GetPinObject(int index)
        {
            if ((uint)Count <= (uint)index) return null;
            return AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(PinInfo[index].Path);
        }

        public static string GetNickName(int index)
        {
            if ((uint)Count <= (uint)index) return null;
            return PinInfo[index].NickName;
        }

        public static void DebugAllPins()
        {
            foreach (PinInfo path in PinInfo)
            {
                UnityEngine.Debug.Log(path.Path);
            }
        }

        internal static PinInfo[] ToArrayPinInfo()
        {
            return PinInfo.ToArray();
        }
    }
}