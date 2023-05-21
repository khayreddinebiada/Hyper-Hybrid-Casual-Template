using System.Collections.Generic;
using Engine.Attribute;
using UnityEditor;
using UnityEngine;
using System;

namespace Editor
{
    public static class ProjectSettingsTemplate
    {
        public const string Label = "Template Settings";
        public const string Root = "Project/Template";
        public const int SpaceDistance = 30;

        public const string ScriptPropertyField = "m_Script";

        private static readonly string[] Keywords = new string[]
        {
            "Template", "Settings", "Project"
        };

        internal static List<SettingsInfo> TempSettings = new List<SettingsInfo>();

        [InitializeOnLoadMethod]
        public static void Initialize()
        {
            Clear();

            CreateSettings();
        }

        private static void CreateSettings()
        {
            foreach (Type type in ReflectionHelper.GetTypesWithAttribute<TemplateSettingsAttribute>())
            {
                object[] attributes = type.GetCustomAttributes(typeof(TemplateSettingsAttribute), true);

                foreach (var attribute in attributes)
                {
                    AddSettings(attribute, type);
                }
            }
        }

        private static void Clear()
        {
            TempSettings.Clear();
        }

        private static void AddSettings(object attribute, Type type)
        {
            TemplateSettingsAttribute templateSettings = attribute as TemplateSettingsAttribute;

            if (templateSettings != null)
            {
                TempSettings.Add(new SettingsInfo(templateSettings, AssetHelper.GetOrCreateAsset(type, templateSettings.Path, templateSettings.Name + ".asset")));
            }
        }

        [SettingsProvider]
        public static SettingsProvider CreateCustomSettingsProvider()
        {
            SettingsProvider provider = new SettingsProvider(Root, SettingsScope.Project)
            {
                label = Label,

                guiHandler = GuiHandler,

                keywords = new HashSet<string>(Keywords)
            };

            return provider;
        }

        private static void GuiHandler(string searchContext)
        {
            foreach (var settings in TempSettings)
            {
                if (settings.ScriptableObject == null)
                    continue;

                settings.SerializedObject.UpdateIfRequiredOrScript();

                if (GUILayout.Button(settings.Attribute.Name, GUIHeadStyle()))
                    settings.ScriptableObject.PingObject();

                using (var iterator = settings.SerializedObject.GetIterator())
                {
                    if (iterator.NextVisible(true))
                    {
                        do
                        {
                            if (!iterator.name.Equals(ScriptPropertyField))
                                EditorGUILayout.PropertyField(settings.SerializedObject.FindProperty(iterator.name));
                        }
                        while (iterator.NextVisible(false));
                    }
                }

                EditorGUILayout.Space(SpaceDistance);
                settings.SerializedObject.ApplyModifiedProperties();
            }
        }

        private static GUIStyle GUIHeadStyle()
        {
            GUIStyle headButton = new GUIStyle(GUI.skin.button);
            headButton.fontStyle = FontStyle.Bold;

            return headButton;
        }
    }
}