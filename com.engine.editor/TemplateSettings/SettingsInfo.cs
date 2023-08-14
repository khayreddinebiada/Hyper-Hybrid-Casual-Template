﻿using HCEngine.Attributes;
using UnityEditor;
using UnityEngine;

namespace HCEditor
{
    internal class SettingsInfo
    {
        internal TemplateSettingsAttribute Attribute;
        internal ScriptableObject ScriptableObject;
        internal SerializedObject SerializedObject;

        internal SettingsInfo(TemplateSettingsAttribute attribute, ScriptableObject scriptableObject)
        {
            Attribute = attribute;
            ScriptableObject = scriptableObject;
            SerializedObject = new SerializedObject(scriptableObject);
        }
    }
}
