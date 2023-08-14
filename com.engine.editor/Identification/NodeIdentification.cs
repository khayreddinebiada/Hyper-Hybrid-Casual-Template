namespace HCEditor.Identification
{
    [System.Serializable]
    public struct NodeIdentification
    {
        public int Index;
        public string Key;
        public int Id;

        public NodeIdentification(int index, string key, int id)
        {
            Index = index;
            Key = key;
            Id = id;
        }
    }
}
