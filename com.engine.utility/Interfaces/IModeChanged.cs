using UnityEditor;

namespace HCEngine
{
    public interface IModeChanged
    {
        public void OnModeChanged(PlayModeStateChange state);
    }
}