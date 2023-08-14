using UnityEditor;

namespace HCEditor.Identification
{
    [CustomEditor(typeof(IdentificationsSerialization))]
    public class IdentificationsSerializationEditor : Editor
    {
        private SerializedProperty _array;
        private IdentificationsSerialization _identifications;

        private void OnEnable()
        {
            _identifications = (IdentificationsSerialization)target;

            _array = serializedObject.FindProperty(nameof(_identifications.Identifications));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_array);

            if (IsHasDuplicates())
            {
                EditorGUILayout.HelpBox("You have a duplicate Key or Id, They should be uniques!...", MessageType.Error);
            }
            else
            {
                if (HasUndefinedIdOrKey())
                    EditorGUILayout.HelpBox($"You have an empty Key or Undefined Id { IdentificationsSerialization.UndefinedId }, They should be uniques!...", MessageType.Error);
                else
                    EditorGUILayout.HelpBox("You do not have duplicates!...", MessageType.Info);

            }

            serializedObject.ApplyModifiedProperties();
        }

        private bool IsHasDuplicates()
        {
            if (_identifications == null)
                return false;

            return Identifications.IsHasDuplicates();
        }

        private bool HasUndefinedIdOrKey()
        {
            foreach (var array in _identifications.Identifications)
            {
                foreach (var node in array.Nodes)
                {
                    if (node.Id == IdentificationsSerialization.UndefinedId || node.Key == IdentificationsSerialization.UndefinedKey)
                        return true;
                }
            }

            return false;
        }
    }
}
