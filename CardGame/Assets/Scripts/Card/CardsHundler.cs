using Game.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Card
{
    public class CardsHundler : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private int _minDelta;
        [SerializeField] private int _maxDelta;

        private List<Card> _cardList;
        private int _counter=0;

        public int Counter { get => _counter;
            set {
                if (value < _cardList.Count)
                    _counter = value;
                else
                    _counter = 0;
            }
        }

        public void SetCards(List<Card> cardList)
        {
            _cardList = cardList;
            _button.onClick.AddListener(ChangeValue);
        }

        private void ChangeValue()
        {
            int deltaValue = Random.Range(_minDelta, _maxDelta);
            int numberType = Random.Range(0, 3);
            ChangeValue(deltaValue, numberType);
            Counter++;
        }

        private void ChangeValue(int deltaValue, int numberType)
        {
            switch (numberType)
            {
                case 0:
                    _cardList[Counter].UpdateHP = deltaValue;
                    break;
                case 1:
                    _cardList[Counter].UpdateMana = deltaValue;
                    break;
                case 2:
                    _cardList[Counter].UpdateAttack = deltaValue;
                    break;
            }
        }
    }
}
