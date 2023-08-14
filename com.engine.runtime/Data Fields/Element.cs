using System.Collections.Generic;
using System;

namespace HCEngine.Data
{
    [Serializable]
    internal struct Element<T> : IComparable<Element<T>>, IEquatable<Element<T>>
    {
        public int Id;
        public ValueField<T> Field;

        internal Element(int key, ValueField<T> value)
        {
            Field = value ?? throw new ArgumentNullException("The FieldKey can't be null!...");
            Id = key;
        }

        public int CompareTo(Element<T> other)
        {
            return Id.CompareTo(other.Id);
        }

        public bool Equals(Element<T> other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            int hashCode = 1363396886;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ValueField<T>>.Default.GetHashCode(Field);
            return hashCode;
        }
    }
}