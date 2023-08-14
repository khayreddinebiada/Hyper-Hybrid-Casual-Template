using UnityEditor;

namespace HCEditor.pin
{
    [System.Serializable]
    public class PinInfo
    {
        public string NickName;
        public string Path;

        public PinInfo(string nickName, string path)
        {
            NickName = nickName;
            Path = path;
        }

        public PinInfo(UnityEngine.Object obj)
        {
            NickName = obj.name;
            Path = AssetDatabase.GetAssetPath(obj);
        }
    }
}
