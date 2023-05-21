using System.IO;
using UnityEngine;

namespace Data
{
    public static class ObjectSaver
    {
        public static readonly string Path = $"{Application.persistentDataPath}/data/";

        public static string GetSavingPathDirectory()
        {

            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);

            return Path;
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Edit/Clear All Data Files", false, 266)]
        public static void ClearAllFiles()
        {
            string directoryPath = GetSavingPathDirectory();
            if (Directory.Exists(directoryPath))
            {
                foreach (string file in Directory.GetFiles(directoryPath))
                {
                    File.Delete(file);
                }
            }

            Debug.Log($"All Files Deleted path is: {directoryPath}");
        }
#endif
    }
}
