using System;
using UnityEngine;

namespace HCEditor.Identification
{
    [CreateAssetMenu(fileName = "New Identifications", menuName = "Add/Identifications", order = 1)]
    public class IdentificationsSerialization : ScriptableObject
    {
        public const int UndefinedId = 0;
        public const string UndefinedKey = "";

        public ArrayIdentification[] Identifications
            = Array.Empty<ArrayIdentification>();
    }
}
