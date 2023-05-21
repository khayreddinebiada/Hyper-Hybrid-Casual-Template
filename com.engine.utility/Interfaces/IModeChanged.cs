using UnityEditor;

namespace Engine
{
    public interface IModeChanged
    {
        public void OnModeChanged(PlayModeStateChange state);
    }
}