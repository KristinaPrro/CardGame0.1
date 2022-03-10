using Game.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Card
{
    public class HandCardTransform : MonoBehaviour
    {
        [SerializeField] private Transform _rightHandPos;
        [SerializeField] private Transform _pinkyPos;
        [SerializeField] private Transform _thumbPos;

        private UnityEvent<Card> _cardDeleted;
        private List<Card> _cardList;
        public void StartGame(UnityEvent<Card> cardDeleted)
        {
            _cardDeleted=cardDeleted;
            _cardDeleted.AddListener(DeleteCard);
        }
        
        public void CalculateCardTransform(List<Card> cardList)
        {
            _cardList = cardList;
            CalculateCardPosition(cardList.Count);
            CalculateCardRotation(cardList.Count);
        }

        private void DeleteCard(Card card)
        {
            _cardList.Remove(card);
            CalculateCardTransform(_cardList);
        }

        private void CalculateCardPosition(int cardCount)
        {
            float deltaX = (_thumbPos.position.x - _pinkyPos.position.x)/ cardCount;
            float radiusHand = Vector3.Distance(_pinkyPos.position, _rightHandPos.position);
            float sumRX0 = radiusHand + _rightHandPos.position.x;
            float diffRX0 = radiusHand - _rightHandPos.position.x;
            for (int i = 0; i < cardCount; i++)
            {
                Vector3 pos = new Vector3(_pinkyPos.position.x + (i+0.5f) * deltaX, _pinkyPos.position.y);
                pos.y = Mathf.Sqrt((sumRX0 - pos.x) * (diffRX0 + pos.x)) + _pinkyPos.position.y;
                _cardList[i].UpdatePosition(pos);
            }
        }

        private void CalculateCardRotation(int cardCount)
        {
            for (int i = 0; i < cardCount; i++)
            {
                Vector3 lookPointPos = _cardList[i].transform.position;
                Vector3 V0 = lookPointPos - _rightHandPos.position;
                Vector3 V1 = new Vector3(0, 1, 0);

                float znak = (lookPointPos.x - _rightHandPos.position.x)< 0 ? 1 : -1;
                Vector3 rot = new Vector3(0, 0,Vector3.Angle(V0, V1)* znak); 
                _cardList[i].UpdateRotation(rot);
            }
        }
    }
    
}
