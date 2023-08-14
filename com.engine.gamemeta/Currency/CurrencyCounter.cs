using UnityEngine;
using System.Collections;

namespace HCEngine.Currency
{
    public class CurrencyCounter
    {
        protected TMPro.TextMeshProUGUI _text;
        protected int _totalCoins;
        protected float _timeFinished;
        protected float _deltaTimeUpdate;


        private int nextTarget;
        private int step;

        public bool isCounting { get; private set; }

        public CurrencyCounter(TMPro.TextMeshProUGUI text, int totalCoins, float timeFinished = 3, float deltaTimeUpdate = 0.1f)
        {
            _text = text;
            _totalCoins = totalCoins;
            _timeFinished = timeFinished;
            _deltaTimeUpdate = deltaTimeUpdate;
        }

        public virtual bool UpdateCount(int totalCoins, int amount)
        {
            if (amount < 0)
            {
                _text.StopAllCoroutines();
                _totalCoins = totalCoins;
                _text.text = Normal.Formanization.NormalizeScore(_totalCoins);
                isCounting = false;
                return true;
            }

            isCounting = true;
            nextTarget = totalCoins;
            step = Mathf.FloorToInt((int)((nextTarget - _totalCoins) * _deltaTimeUpdate / _timeFinished)) + 1;
            _text.StartCoroutine(UpdateEvery());
            return true;
        }

        protected virtual IEnumerator UpdateEvery()
        {
            yield return new WaitForSeconds(_deltaTimeUpdate);
            
            _totalCoins += step;

            if (isCounting && _totalCoins < nextTarget)
            {
                _text.StartCoroutine(UpdateEvery());
            }
            else
            {
                _totalCoins = nextTarget;
                isCounting = false;
            }

            _text.text = Normal.Formanization.NormalizeScore(_totalCoins);
        }
    }
}
