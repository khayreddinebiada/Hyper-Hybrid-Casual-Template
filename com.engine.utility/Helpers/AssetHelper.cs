#if UNITY_EDITOR
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Engine.Diagnostics;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace Editor
{
    public static class AssetHelper
    {
        private static string FilePath(string directoryPath, string assetsName) => directoryPath + assetsName;

        public static T[] FindScribtableObjectsOfType<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);

            if (guids == null || guids.Length == 0)
                return null;

            T[] a = new T[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                a[i] = GetAssetOfType<T>(guids[i]);
            }

            return a;
        }

        public static T FindScribtableObjectOfType<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);

            if (guids == null || guids.Length == 0)
                return null;

            return GetAssetOfType<T>(guids[0]);
        }

        private static T GetAssetOfType<T>(string guid) where T : ScriptableObject
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        public static Object[] FindScribtableObjectsOfType(System.Type type)
        {
            string[] guids = AssetDatabase.FindAssets("t:" + type.Name);

            if (guids == null || guids.Length == 0)
                return null;

            Object[] a = new Object[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                a[i] = GetAssetOfType(guids[i], type);
            }

            return a;
        }

        public static Object FindScribtableObjectOfType(System.Type type)
        {
            string[] guids = AssetDatabase.FindAssets("t:" + type.Name);

            if (guids == null || guids.Length == 0)
                return null;

            return GetAssetOfType(guids[0], type);
        }

        private static Object GetAssetOfType(string guid, System.Type type)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAssetAtPath(path, type);
        }

        /// Search for the asset type of T if we find we will return it if no we will create a new one.
        /// 
        public static ScriptableObject GetOrCreateAsset(System.Type type, string directoryPath, string assetsName)
        {
            var settings = AssetDatabase.LoadAssetAtPath(FilePath(directoryPath, assetsName), type);
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance(type).SaveAsset(directoryPath, assetsName);
            }
            return settings as ScriptableObject;
        }

        /// Search for the asset type of T if we find we will return it if no we will create a new one.
        /// 
        public static T GetOrCreateAsset<T>(string directoryPath, string assetsName) where T : ScriptableObject
        {
            var settings = AssetDatabase.LoadAssetAtPath<T>(FilePath(directoryPath, assetsName));
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<T>().SaveAsset(directoryPath, assetsName);
            }
            return settings;
        }

        public static T SaveAsset<T>(this T t, string directoryPath, string assetsName) where T : ScriptableObject
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            AssetDatabase.CreateAsset(t, FilePath(directoryPath, assetsName));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return t;
        }

        public static void PingObject<T>(this T target) where T : Object
        {
            if (target == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.obj);
            Contract.EndContractBlock();

            Selection.activeObject = target;
            EditorGUIUtility.PingObject(target);
        }

        public static IEnumerable<Object> GetAssetsOfAttribute(System.Type attributeType, bool inherit = true)
        {
            return GetAssetsOfAttribute<Object>(attributeType, inherit);
        }

        public static IEnumerable<TObject> GetAssetsOfAttribute<TObject>(System.Type attributeType, bool inherit = true) where TObject : Object
        {
            foreach (var type in ReflectionHelper.GetTypesWithAttribute(attributeType, inherit))
            {
                TObject[] objs = FindScribtableObjectsOfType(type) as TObject[];
                if (objs != null && 0 < objs.Length)
                    foreach (TObject obj in objs)
                        yield return obj;
            }
        }

        public static IEnumerable<TObject> GetAssetsOfAttribute<TObject, TAttribute>(bool inherit = true) where TObject : Object where TAttribute : System.Attribute
        {
            foreach (var type in ReflectionHelper.GetTypesWithAttribute(typeof(TAttribute), inherit))
            {
                TObject[] objs = FindScribtableObjectsOfType(type) as TObject[];
                if (objs != null && 0 < objs.Length)
                    foreach (TObject obj in objs)
                        yield return obj;
            }
        }

    }
}
#endif