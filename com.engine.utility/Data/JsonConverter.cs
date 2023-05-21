using System;
using UnityEngine;

namespace Engine.utility
{
    public static class JsonConverter
    {
        private const string EmptyJson = "{ }";

        public static bool JsonToObject<TObject>(this TObject obectConvert, string contents)
        {
            try
            {
                JsonUtility.FromJsonOverwrite(contents, obectConvert);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Error: {e.Message}");
            }

            return false;
        }

        public static string ObjectToJson<TObject>(this TObject obectConvert)
        {
            try
            {
                return JsonUtility.ToJson(obectConvert, false);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Error: {e.Message}");
            }

            return EmptyJson;
        }
    }
}