using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using HCEngine;
using System;
using HCEditor.Identification;

namespace HCEditor.Drawer
{
    [CustomPropertyDrawer(typeof(IdentificatorAttribute))]
    public class IdentificatorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Integer)
                throw new InvalidOperationException("The id type should be Integer!...");

            Draw(position, property, label);
        }

        private void Draw(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            Rect labelPosition = new Rect(position.x, position.y, position.width / 3, position.height);
            Rect valuePosition = new Rect(position.x + position.width / 3, position.y, position.width / 1.5f, position.height);

            IdentificatorAttribute identificatorAttribute = attribute as IdentificatorAttribute;

            List<string> keys = GetOptions(property, out int selectedIndex, out List<int> ids, identificatorAttribute.Index);

            EditorGUI.LabelField(labelPosition, property.displayName);
            int indexOfId = EditorGUI.Popup(valuePosition, selectedIndex, keys.ToArray());
            if (0 < indexOfId)
                property.intValue = ids[indexOfId];

            EditorGUI.EndProperty();
        }

        private List<string> GetOptions(SerializedProperty property, out int selectedIndex, out List<int> ids, int index)
        {
            Dictionary<int, string> values = Identifications.GetDictionary(index);

            List<string> keys = new List<string>(values.Values);
            ids = new List<int>(values.Keys);

            keys.Insert(0, IdentificationsSerialization.UndefinedKey);
            ids.Insert(0, IdentificationsSerialization.UndefinedId);

            selectedIndex = ids.IndexOf(property.intValue);
            selectedIndex = Math.Max(0, selectedIndex);

            return keys;
        }
    }
}