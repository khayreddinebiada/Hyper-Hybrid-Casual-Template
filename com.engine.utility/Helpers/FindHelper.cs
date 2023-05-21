#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class FindHelper
    {
        /// <summary>
        /// Find all object of TObject activate and non activate in array.
        /// </summary>
        /// <typeparam name="TObject"> Type of object that you searching for.</typeparam>
        /// <returns> Array of TObjects </returns>
        public static TObject FindScenesComponent<TObject>()
        {
            foreach (Object go in Resources.FindObjectsOfTypeAll(typeof(Object)))
            {
                GameObject cGO = go as GameObject;
                if (cGO != null && !EditorUtility.IsPersistent(cGO.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                {
                    TObject result = cGO.GetComponent<TObject>();
                    if (result != null)
                        return result;
                }
            }

            return default;
        }

        /// <summary>
        /// Find all object of TObject activate and non activate in array.
        /// </summary>
        /// <typeparam name="TObject"> Type of object that you searching for.</typeparam>
        /// <returns> Array of TObjects </returns>
        public static Object FindScenesComponent(System.Type type)
        {
            foreach (Object go in Resources.FindObjectsOfTypeAll(typeof(Object)) as Object[])
            {
                GameObject cGO = go as GameObject;
                if (cGO != null && !EditorUtility.IsPersistent(cGO.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                {
                    Object result = cGO.GetComponent(type);
                    if (result != null) return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Find all object of Object activate and non activate in all activate scenes and put it in array.
        /// </summary>
        /// <returns> Array of Objects </returns>
        public static Object[] FindAllSceneObjects()
        {
            List<Object> objectsInScene = new List<Object>();

            foreach (Object go in Resources.FindObjectsOfTypeAll(typeof(Object)) as Object[])
            {
                GameObject cGO = go as GameObject;
                if (cGO != null && !EditorUtility.IsPersistent(cGO.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                {
                    objectsInScene.Add(go);
                }
            }

            return objectsInScene.ToArray();
        }

        /// <summary>
        /// Find all object of TObject activate and non activate in array.
        /// </summary>
        /// <typeparam name="TObject"> Type of object that you searching for.</typeparam>
        /// <returns> Array of TObjects </returns>
        public static TObject[] FindScenesComponents<TObject>()
        {
            List<TObject> objectsInScene = new List<TObject>();

            foreach (Object go in Resources.FindObjectsOfTypeAll(typeof(Object)) as Object[])
            {
                GameObject cGO = go as GameObject;
                if (cGO != null && !EditorUtility.IsPersistent(cGO.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                {
                    TObject result = cGO.GetComponent<TObject>();
                    if (result != null)
                        objectsInScene.Add(result);
                }
            }

            return objectsInScene.ToArray();
        }

        /// <summary>
        /// Find all object of TObject activate and non activate in array.
        /// </summary>
        /// <typeparam name="TObject"> Type of object that you searching for.</typeparam>
        /// <returns> Array of TObjects </returns>
        public static Object[] FindScenesComponents(System.Type type)
        {
            List<Object> objectsInScene = new List<Object>();

            foreach (Object go in Resources.FindObjectsOfTypeAll(typeof(Object)) as Object[])
            {
                GameObject cGO = go as GameObject;
                if (cGO != null && !EditorUtility.IsPersistent(cGO.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                {
                    Object result = cGO.GetComponent(type);
                    if (result != null) objectsInScene.Add(result);
                }
            }

            return objectsInScene.ToArray();
        }

        /// <summary>
        /// Find any assets in the project with type of TObject or ScriptableObject.
        /// </summary>
        /// <typeparam name="TObject"> Is the type of TObject or ScriptableObject </typeparam>
        /// <returns></returns>
        public static TObject[] FindAllAssetsOfType<TObject>()
        {
            List<TObject> objects = new List<TObject>();
            objects.AddRange(AssetHelper.FindScribtableObjectsOfType<ScriptableObject>().OfType<TObject>());
            objects.AddRange(FindScenesComponents<TObject>());

            return objects.ToArray();
        }
    }
}
#endif