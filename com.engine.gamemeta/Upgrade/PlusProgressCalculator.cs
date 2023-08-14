namespace HCEngine.Upgrade
{
    public class PlusProgressCalculator : IProgressCalculator
    {

        private float _initValue;
        private float _currentResult;

        public float CurrentResult
            => _currentResult;

        public float InitValue
        {
            get => _initValue;
            set => _initValue = value;
        }

        public PlusProgressCalculator(float initValue)
        {
            _currentResult = _initValue = initValue;
        }

        public PlusProgressCalculator()
            : this(0)
        {
        }

        public void InsertValue(float value)
        {
            _currentResult += value;
        }

        public void Reset()
        {
            _currentResult = _initValue;
        }
    }
}
