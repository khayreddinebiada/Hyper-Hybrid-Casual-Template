using System.Collections.Generic;
using System;

namespace HCEngine.DI
{
    public static class DIContainer
    {
        internal const int SingleId = int.MaxValue;

        internal static class Container<TValue> where TValue : class
        {
            private static readonly SortedList<int, TValue> _container
                = new SortedList<int, TValue>();

            public static IEnumerable<TValue> Val
            {
                get => _container.Values;
            }

            public static IEnumerable<int> Ids
            {
                get => _container.Keys;
            }

            public static void AddSingleValue(TValue value)
            {
                if (_container.ContainsKey(SingleId))
                    _container[SingleId] = value;
                else
                    _container.Add(SingleId, value);
            }

            public static void Override(int id, TValue value)
            {
                if (!_container.TryAdd(id, value))
                    _container[id] = value;
            }

            public static bool Remove(TValue value)
            {
                foreach (var item in Container<TValue>._container)
                {
                    if (item.Value == value)
                        return _container.Remove(item.Key);
                }

                return false;
            }

            public static bool Remove(int id)
            {
                return _container.Remove(id);
            }

            public static TValue LastNonNull()
            {
                IList<TValue> values = _container.Values;

                for (int i = values.Count - 1; 0 <= i; i--)
                {
                    if (values[i] != null && !values.Equals(null))
                        return values[i];
                }

                return null;
            }

            public static TValue GetValue(int id)
            {
                if (_container.TryGetValue(id, out TValue result))
                {
                    return result;
                }

                return null;
            }

            public static void Clear()
            {
                _container.Clear();
            }


            public static void RemoveAll(Predicate<TValue> predicate)
            {
                List<int> keys = new List<int>();
                foreach (var pair in _container)
                {
                    if (predicate.Invoke(pair.Value))
                        keys.Add(pair.Key);
                }

                for (int i = 0; i < keys.Count; i++)
                {
                    _container.Remove(keys[i]);
                }
            }
        }

        public static void Register<TValue>(int id, TValue value) where TValue : class
        {
            Container<TValue>.Override(id, value);
        }

        public static void RegisterAsSingle<TValue>(TValue value) where TValue : class
        {
            Container<TValue>.AddSingleValue(value);
        }

        /// <summary>
        /// Register range of values in the list with the other values with the same type (TType).
        /// </summary>
        public static void RegisterRange<TValue>(IEnumerable<KeyValuePair<int, TValue>> collection) where TValue : class
        {
            if (collection == null)
                throw new ArgumentNullException();

            foreach (var item in collection)
            {
                Container<TValue>.Override(item.Key, item.Value);
            }
        }

        public static bool Unregister<TValue>(TValue value) where TValue : class
        {
            return Container<TValue>.Remove(value);
        }

        public static bool UnregisterById<TValue>(int id) where TValue : class
        {
            return Container<TValue>.Remove(id);
        }

        public static TValue AsSingle<TValue>() where TValue : class
        {
            return Container<TValue>.LastNonNull();
        }

        public static IEnumerable<TValue> Collect<TValue>() where TValue : class
        {
            return Container<TValue>.Val;
        }

        public static TValue GetValueOfId<TValue>(int id) where TValue : class
        {
            return Container<TValue>.GetValue(id);
        }

        public static void Clear<TValue>() where TValue : class
        {
            Container<TValue>.Clear();
        }

        public static void CleanNull<TValue>() where TValue : class
        {
            Container<TValue>.RemoveAll((value) => value == null || value.Equals(null));
        }
    }
}
