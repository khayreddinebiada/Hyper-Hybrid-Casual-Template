using Engine.Diagnostics;
using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Editor
{
    public static class SaveHelper
    {
        public static void SaveGame()
        {
            SaveAllActiveScenes();
            AssetDatabase.SaveAssets();
        }

        public static void SaveObject(this Object obj)
        {
            if (obj == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.obj);

            EditorUtility.SetDirty(obj);
            AssetDatabase.SaveAssets();
            EditorUtility.ClearDirty(obj);
        }

        public static void SaveScribtableObjectsOfType(Type type)
        {
            (AssetHelper.FindScribtableObjectsOfType(type) as ScriptableObject[]).SaveObjects();
        }

        public static void SaveObjects(this ScriptableObject[] objects)
        {
            SaveObjects<ScriptableObject>(objects);
        }

        public static void SaveScribtableObjectsOfType<T>() where T : ScriptableObject
        {
            AssetHelper.FindScribtableObjectsOfType<T>().SaveObjects();
        }

        public static void SaveObjects<T>(this T[] objects) where T : ScriptableObject
        {
            foreach (T t in objects)
            {
                EditorUtility.SetDirty(t);
            }

            AssetDatabase.SaveAssets();

            foreach (T t in objects)
            {
                EditorUtility.ClearDirty(t);
            }
        }

        public static void SaveAllActiveScenes()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                EditorSceneManager.SaveScene(SceneManager.GetSceneAt(i));
            }
        }

        public static void SaveSceneAt(int i)
        {
            EditorSceneManager.SaveScene(SceneManager.GetSceneAt(i));
        }
    }
}