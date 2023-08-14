using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using UnityEngine;

namespace HCEngine.Data
{
    [System.Serializable]
    public class ListField<T> : IEnumerable<T>, IEnumerable<ValueField<T>>
    {
        [SerializeField] private bool _autoSave;
        [SerializeField] private string _fileName;
        [SerializeField] private List<ValueField<T>> _values;

        public int Count => _values.Count;

        public T this[string key]
        {
            get
            {
                if (!TryGetValueField(key, out ValueField<T> field))
                    throw new System.ArgumentException($"The key '{key}' ItemNotExist");
                Contract.EndContractBlock();

                return field.Value;
            }
            set
            {
                int index = FindIndex(key);

                if (index == -1)
                    throw new System.ArgumentException($"The key '{key}' ItemNotExist");
                Contract.EndContractBlock();

                _values[index].Value = value;
            }
        }

        public ListField(string[] keys, string fileName = null, bool autoSave = true)
        {
            _fileName = fileName;
            _autoSave = autoSave;

            LoadKeys(keys);
        }

        public ListField(int capacity, string fileName = null, bool autoSave = true)
        {
            _fileName = fileName;
            _autoSave = autoSave;
            _values = new List<ValueField<T>>(capacity);
        }

        public void LoadKeys(string[] keys)
        {
            _values = new List<ValueField<T>>(keys.Length);
            foreach (string key in keys)
            {
                ValueField<T> field = new ValueField<T>(key, _fileName, default, _autoSave);
                _values.Add(field);
            }
        }

        private int FindIndex(string key) =>
            _values.FindIndex((e) => e.Key == key);

        public bool TryGetFieldOfValue(T value, out ValueField<T> field)
        {
            bool isFaund = false;
            field = _values.Find((e) =>
            {
                if (e.Value != null && e.Value.Equals(value))
                {
                    return isFaund = true;
                }

                return false;
            });

            return isFaund;
        }

        public bool TryGetValueField(string key, out ValueField<T> value)
        {
            bool isFaund = false;
            value = _values.Find((e) =>
            {
                if (e.Key == key)
                {
                    return isFaund = true;
                }

                return false;
            });

            return isFaund;
        }

        public bool TryGetValueField(string key, out T result)
        {
            if (TryGetValueField(key, out ValueField<T> output))
            {
                result = output.Value;
                return true;
            }

            result = default(T);
            return false;
        }

        public bool TrySetValueField(string key, T value)
        {
            bool isFaund = false;
            _values.Find((e) =>
            {
                if (e.Key == key)
                {
                    e.Value = value;
                    return isFaund = true;
                }

                return false;
            });
            return isFaund;
        }

        public bool ContainsKey(string key)
        {
            return 0 <= _values.FindIndex((e) => e.Key == key);
        }

        public bool ContainsValue(T value)
        {
            return 0 <= _values.FindIndex((e) => e.Value != null && e.Value.Equals(value));
        }

        public void Add(string key, T value)
        {
            if (ContainsKey(key))
                throw new System.ArgumentException($"The key '{key}' AddingDuplicate");
            Contract.EndContractBlock();

            _values.Add(new ValueField<T>(key, _fileName, value, _autoSave));
        }

        public void RemoveAt(string key, bool thenDelete)
        {
            if (thenDelete && TryGetValueField(key, out ValueField<T> value))
                value.DeleteKey();

            _values.RemoveAll(e => e.Key == key);
        }

        public void DeleteKey(string key)
        {
            _values.RemoveAll(e => e.Key == key);
        }

        public void Clear()
        {
            _values.Clear();
        }

        public void DeleteAllKeys()
        {
            foreach (var item in _values)
                item.DeleteKey();
        }

        public void DeleteFile()
        {
            string path = $"{Application.persistentDataPath}/data/{_fileName}.json";

            if (File.Exists(path))
                File.Delete(path);
        }

        public IEnumerator GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            List<T> result = new List<T>(_values.Count);

            foreach (var item in _values)
                result.Add(item.Value);

            Contract.Assert(result.Count == _values.Count);
            return result.GetEnumerator();
        }

        IEnumerator<ValueField<T>> IEnumerable<ValueField<T>>.GetEnumerator()
        {
            return _values.GetEnumerator();
        }
    }
}