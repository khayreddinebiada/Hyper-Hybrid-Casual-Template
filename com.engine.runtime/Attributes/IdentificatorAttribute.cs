using UnityEngine;

namespace HCEngine
{
    public class IdentificatorAttribute : PropertyAttribute
    {
        public const int AllIndex = -1;
        public int Index;

        public IdentificatorAttribute() : this (AllIndex)
        {

        }

        public IdentificatorAttribute(int index)
        {
            Index = index;
        }
    }
}