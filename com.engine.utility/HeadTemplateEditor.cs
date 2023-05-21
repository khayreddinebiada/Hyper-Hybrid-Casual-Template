#if UNITY_EDITOR
using Engine.Attributes;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Engine;

namespace Editor
{
    public class HeadTemplateEditor
    {
        [MenuItem("Tempate/Reset Data", false, 0)]
        public static void ResetAllData()
        {
            PlayerPrefs.DeleteAll();
            Data.ObjectSaver.ClearAllFiles();


            IResetData[] reseters = FindHelper.FindAllAssetsOfType<IResetData>();

            int idData = 0;
            foreach (IResetData reset in reseters)
            {
                idData++;
                reset.ResetData(idData);
            }

            Debug.Log("Reset Data assets is finished!...");
        }

        [MenuItem("Tempate/Unlock Data", false, 0)]
        public static void UnlockAllData()
        {
            PlayerPrefs.DeleteAll();
            Data.ObjectSaver.ClearAllFiles();

            IUnlock[] unlocks = FindHelper.FindAllAssetsOfType<IUnlock>();

            foreach (IUnlock unlock in unlocks)
            {
                unlock.Unlock();
            }

            Debug.Log("Reset Data assets is finished!...");
        }

        [MenuItem("Tempate/Validate Settings", false, 0)]
        public static void ValidateAll()
        {
            IValidate[] validates = FindHelper.FindAllAssetsOfType<IValidate>();
            for (int i = 0; i < validates.Length; i++)
            {
                validates[i].Validate();
            }

            Debug.Log("Validate assets is finished!...");
        }

        [MenuItem("Tempate/Save Assets", false, 150)]
        public static void SaveAssets()
        {
            Object[] assets = AssetHelper.GetAssetsOfAttribute(typeof(AssetAttribute), true).ToArray();

            foreach (Object asset in assets)
            {
                asset?.SaveObject();
                Debug.Log(asset.name);
            }

            Debug.Log("Save assets is finished!...");
        }
    }
}
#endif