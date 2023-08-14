#if UNITY_EDITOR
using HCEngine.Attributes;
using System.Linq;
using UnityEditor;
using UnityEngine;
using HCEngine;
using HCEngine.Data;

namespace HCEditor
{
    public class HeadTemplateEditor
    {
        [MenuItem("Template/Reset Data", false, 0)]
        public static void ResetAllData()
        {
            PlayerPrefs.DeleteAll();
            ObjectSaver.ClearAllFiles();


            IResetData[] reseters = FindHelper.FindAllAssetsOfType<IResetData>();

            int idData = 0;
            foreach (IResetData reset in reseters)
            {
                idData++;
                reset.ResetData(idData);
            }

            Debug.Log("Reset Data assets is finished!...");
        }

        [MenuItem("Template/Unlock Data", false, 0)]
        public static void UnlockAllData()
        {
            PlayerPrefs.DeleteAll();
            ObjectSaver.ClearAllFiles();

            IUnlock[] unlocks = FindHelper.FindAllAssetsOfType<IUnlock>();

            foreach (IUnlock unlock in unlocks)
            {
                unlock.Unlock();
            }

            Debug.Log("Reset Data assets is finished!...");
        }

        [MenuItem("Template/Validate Settings", false, 0)]
        public static void ValidateAll()
        {
            IValidate[] validates = FindHelper.FindAllAssetsOfType<IValidate>();
            for (int i = 0; i < validates.Length; i++)
            {
                validates[i].Validate();
            }

            Debug.Log("Validate assets is finished!...");
        }

        [MenuItem("Template/Save Assets", false, 150)]
        public static void SaveAssets()
        {
            Object[] assets = AssetHelper.GetAssetsOfAttribute(typeof(AssetAttribute), true).ToArray();

            foreach (Object asset in assets)
            {
                asset?.SaveObject();
            }

            Debug.Log("Save assets is finished!...");
        }
    }
}
#endif