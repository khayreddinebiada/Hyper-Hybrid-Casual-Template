using HCEditor.Identification;
using UnityEditor;
using UnityEngine;
using System;

namespace HCEditor.Drawer
{
    [CustomPropertyDrawer(typeof(NodeIdentification))]
    internal class NodeIdentificationDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            label.text = label.text.Replace("Element", "Index");

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            float labelKeySize = Math.Min(position.width / 6, 30);
            float shiftingPosition = 0;

            float labelIdSize = Math.Min(position.width / 10, 20);
            float fieldSize = position.width / 2.4f;

            float shiftGroups = 10;

            var keyLabelPosition = new Rect(position.x + shiftingPosition - labelKeySize, position.y, labelKeySize, position.height);
            var keyPosition = new Rect(position.x + shiftingPosition, position.y, fieldSize, position.height);

            shiftingPosition += fieldSize + labelIdSize + shiftGroups;
            var idLabelPosition = new Rect(position.x + shiftingPosition - labelIdSize, position.y, labelIdSize, position.height);
            var idPosition = new Rect(position.x + shiftingPosition, position.y, fieldSize, position.height);

            EditorGUI.LabelField(keyLabelPosition, "Key:");
            EditorGUI.PropertyField(keyPosition, property.FindPropertyRelative("Key"), GUIContent.none);
            EditorGUI.LabelField(idLabelPosition, "ID:");
            EditorGUI.PropertyField(idPosition, property.FindPropertyRelative("Id"), GUIContent.none);

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
