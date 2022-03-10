using DG.Tweening;
using Game.Data;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Game.Card
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Image _artImage;
        
        [SerializeField] private Text _titleText;
        [SerializeField] private Text _descriptionText;

        [SerializeField] private Text _attackText;
        [SerializeField] private Text _hpText;
        [SerializeField] private Text _manaText;
        
        private CardData _cardData;
        private UnityEvent<Card> _cardDeleted;

        public int UpdateHP { get => _cardData.AttackValue; 
            set {
                _cardData.HPValue += value;
                WriteText(_hpText, _cardData.HPValue);
                if (_cardData.HPValue < 1) 
                    DeleteCard();
            }
        }

        public int UpdateAttack { get => _cardData.AttackValue; 
            set {
                _cardData.AttackValue += value;
                WriteText(_attackText, _cardData.AttackValue);
            }
        }

        public int UpdateMana { get => _cardData.AttackValue; 
            set {
                _cardData.HPValue += value;
                WriteText(_manaText, _cardData.ManaValue);
            }
        }

        public void UpdatePosition(Vector3 point) {
            transform.DOMove(point, 1.0f);
        }

        public void UpdateRotation(Quaternion turn) {
            transform.DORotate(turn.eulerAngles, 1.0f); 
        }

        public void LoadData(CardData cardData, UnityEvent<Card> cardDeleted)
        {
            _cardData = cardData;
            StartCoroutine(LoadSpriteFromWeb(_cardData.SpriteURL));
            
            WriteText(_titleText, _cardData.Title);
            WriteText( _descriptionText, _cardData.Description);

            WriteText(_attackText, _cardData.AttackValue);
            WriteText(_hpText, _cardData.HPValue);
            WriteText(_manaText, _cardData.ManaValue);
            _cardDeleted=cardDeleted;
        }

        IEnumerator LoadSpriteFromWeb(string url)
        {
            UnityWebRequest wr = new UnityWebRequest(url);
            DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
            wr.downloadHandler = texDl;
            yield return wr.SendWebRequest();
            if (wr.result!=UnityWebRequest.Result.ConnectionError)
            {
                Texture2D texture2D = texDl.texture;
                _artImage.sprite= Sprite.Create(texture2D, new Rect(0, 0, 150, 150), Vector2.zero, 100f);
            }
        }

        private void WriteText(Text text, string value) {
            text.text = value;

        }

        private void WriteText(Text text, int value) {
            text.text = value.ToString();

        }
        private void DeleteCard()
        {
            _cardDeleted.Invoke(this);
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
