using UnityEngine;
using System.IO;
using System;

namespace HCEngine.Data
{
    [Serializable]
    public class ValueField<T> : IValueField<T>
    {
        [SerializeField, HideInInspector] protected bool _autoSave;
        [SerializeField, HideInInspector] protected string _fileName;

        [SerializeField] protected string _key;
        [SerializeField] protected T _value;

        private bool _isLoaded;

        public string Key => _key;
        public string FileName => _fileName;
        public bool HasValue => ES3.KeyExists(_key, FilePath(_fileName));

        public ValueField(string key, string fileName, T value = default(T), bool autoSave = true)
        {
            _isLoaded = false;
            _autoSave = autoSave;

            _key = key;
            _value = value;

            _fileName = fileName ?? throw new ArgumentNullException("The path file has a null value!.");
        }

        public virtual T Value
        {
            get
            {
                if (_isLoaded == true)
                    return _value;

                return _value = Load();
            }
            set
            {
                if (_value != null && _value.Equals(value))
                    return;

                _value = value;

                if (_autoSave == true)
                    Save();
            }
        }

        public void Save()
        {
            ES3.Save(_key, _value, FilePath(_fileName));
        }

        public void DeleteKey()
        {
            ES3.DeleteKey(_key, FilePath(_fileName));
        }

        public string FilePath(string fileName = "")
        {
            string directoryPath =  $"{Application.persistentDataPath}/data/";

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            return $"{directoryPath}{fileName}.json";
        }

        public T Load()
        {
            _isLoaded = true;
            return ES3.Load(_key, FilePath(_fileName), _value);
        }

        public T Load(T defaultValue)
        {
            _isLoaded = true;
            return ES3.Load(_key, FilePath(_fileName), defaultValue);
        }
    }
}