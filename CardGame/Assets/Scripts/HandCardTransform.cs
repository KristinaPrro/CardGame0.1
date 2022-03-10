using Game.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Card
{
    public class HandCardTransform : MonoBehaviour
    {
        [SerializeField] private Transform _rightHandPos;
        [SerializeField] private Transform _pinkyPos;
        [SerializeField] private Transform _thumbPos;


        public void CalculateCardTransform(List<Card> cardList)
        {
            List<GameObject> positionGO = new List<GameObject>();
            CalculateCardPosition(cardList.Count, cardList);
            CalculateCardRotation(cardList.Count, cardList);
        }

        private void CalculateCardPosition(int cardCount, List<Card> cardList)
        {
            float deltaX = (_thumbPos.position.x - _pinkyPos.position.x)/ cardCount;
            float radiusHand = Vector3.Distance(_pinkyPos.position, _rightHandPos.position);
            float sumRX0 = radiusHand + _rightHandPos.position.x;
            float diffRX0 = radiusHand - _rightHandPos.position.x;
            for (int i = 0; i < cardCount; i++)
            {
                Vector3 pos = new Vector3(_pinkyPos.position.x + (i+0.5f) * deltaX, _pinkyPos.position.y);
                pos.y = Mathf.Sqrt((sumRX0 - pos.x) * (diffRX0 + pos.x)) + _pinkyPos.position.y;
                cardList[i].UpdatePosition(pos);
            }
        }

        private void CalculateCardRotation(int cardCount, List<Card> cardList)
        {
            for (int i = 0; i < cardCount; i++)
            {
                Vector3 newDir = Vector3.RotateTowards(transform.forward, cardList[i].transform.position, 0, 0.0F);
                //Debug.DrawRay(cardList[i].transform.position, newDir, Color.red);
                cardList[i].UpdateRotation(Quaternion.LookRotation(newDir));
            }
        }
    }
    
}
