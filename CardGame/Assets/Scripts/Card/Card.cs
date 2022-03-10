using DG.Tweening;
using Game.Data;
using System.Collections;
using UnityEngine;
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

        private Sprite _artSprite;        

        private CardData _cardData;
        public int UpdateAttack { get => _cardData.AttackValue; 
            set {
                _cardData.AttackValue += value;
                WriteText(_attackText, _cardData.AttackValue);
            }
        }

        public int UpdateHP { get => _cardData.AttackValue; 
            set {
                _cardData.HPValue += value;
                WriteText(_hpText, _cardData.HPValue);
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

        public void LoadData(CardData cardData)
        {
            _cardData = cardData;
                StartCoroutine(LoadSpriteFromWeb(_cardData.SpriteURL));
            _artImage.sprite = _artSprite;
            
            WriteText(_titleText, _cardData.Title);
            WriteText( _descriptionText, _cardData.Description);

            WriteText(_attackText, _cardData.AttackValue);
            WriteText(_hpText, _cardData.HPValue);
            WriteText(_manaText, _cardData.ManaValue);
            Debug.Log("LoadData " + cardData);
        }

        IEnumerator LoadSpriteFromWeb(string url)
        {
            UnityWebRequest wr = new UnityWebRequest(url);
            DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
            wr.downloadHandler = texDl;
            yield return wr.SendWebRequest();
            if (wr.result!=UnityWebRequest.Result.ConnectionError)
            {
                Texture2D t = texDl.texture;
                _artSprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), Vector2.zero, 1f);
            }
        }

        private void WriteText(Text text, string value) {
            text.text = value;

        }

        private void WriteText(Text text, int value) {
            text.text = value.ToString();

        }
    }
}
