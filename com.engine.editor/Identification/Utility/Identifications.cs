using System.Collections.Generic;

namespace HCEditor.Identification
{
    public static class Identifications
    {
        public const string Path = "Assets/Editor/Settings/";
        public const string FileName = "Identifications.asset";

        public static int TotalArrays(this IdentificationsSerialization serialization)
        {
            return serialization.Identifications.Length;
        }

        public static bool IsHasDuplicates()
        {
            IdentificationsSerialization serialization = GetIdentificationsSerialization();
            Dictionary<int, string> dictionary =
                new Dictionary<int, string>(serialization.Identifications.Length);

            bool hasRepeatable = false;

            foreach (var nodes in serialization.Identifications)
            {
                if (CollectValues(nodes.Nodes, ref dictionary))
                    hasRepeatable = true;
            }

            return hasRepeatable;
        }

        public static Dictionary<int, string> GetDictionary(int index)
        {
            IdentificationsSerialization serialization = GetIdentificationsSerialization();
            int containerLength = serialization.Identifications.Length;

            Dictionary<int, string> dictionary = new Dictionary<int, string>(containerLength);

            if (0 < containerLength)
            {
                if ((uint)index < (uint)containerLength)
                    CollectValues(serialization.Identifications[index].Nodes, ref dictionary);
                else
                {
                    foreach (var nodes in serialization.Identifications)
                        CollectValues(nodes.Nodes, ref dictionary);
                }
            }

            return dictionary;
        }

        private static bool CollectValues(
            NodeIdentification[] nodes,
            ref Dictionary<int, string> dictionary)
        {
            bool hasRepeatable = false;

            for (int i = 0; i < nodes.Length; i++)
            {
                if (!dictionary.TryAdd(nodes[i].Id, nodes[i].Key))
                    hasRepeatable = true;
            }

            return hasRepeatable;
        }

        public static IdentificationsSerialization GetIdentificationsSerialization()
        {
            return AssetHelper.GetOrCreateAsset<IdentificationsSerialization>(Path, FileName);
        }
    }
}
