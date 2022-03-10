using Game.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Card
{
    public class GeneratorCard : MonoBehaviour
    {
        [SerializeField] private GameObject _cardPrefabGO;
        [SerializeField] private int _maxCountCard=9;
        [SerializeField] private HandCardTransform _hand;
        [SerializeField] private List<CardData> _cardDataList;
        [SerializeField] private GameObject _parentGO;
        
        [SerializeField] private CardsHundler _cardsHundler;

        private List<Card> _cardList;
        private UnityEvent<Card> _cardDeleted;
        private void Awake()
        {
            _cardDeleted = new UnityEvent<Card>();
            GenerateCardInHand();
            _hand.StartGame(_cardDeleted);
        }

        private void GenerateCardInHand()
        {
            _cardList = new List<Card>();
            int count = Random.Range(1, _maxCountCard);
            Debug.Log("count "+ count);
            for (int i = 0; i < count; i++)
            {
                _cardList.Add(
                    Instantiate(_cardPrefabGO, _parentGO.transform).GetComponent<Card>());
                _cardList[i].LoadData(_cardDataList[i], _cardDeleted);
                _cardList[i].transform.position= _parentGO.transform.position;

            }
            PlaceCardInHand(_cardList);

        }

        public void PlaceCardInHand(List<Card> cardList)
        {
            _hand.CalculateCardTransform(cardList);
            _cardsHundler.SetCards(cardList);
        }
    }
}
