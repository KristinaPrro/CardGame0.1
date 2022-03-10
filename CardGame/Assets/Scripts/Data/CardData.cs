using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Data
{
    [CreateAssetMenu(fileName = "New CardData", menuName = "Card Data", order = 51)]
    public class CardData : ScriptableObject
    {
        [SerializeField] private string _spriteURL;
        //[SerializeField] private Sprite _uIOverlay;
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        //[SerializeField] private Sprite _attackIcon;
        //[SerializeField] private Sprite _hpIcon;
        //[SerializeField] private Sprite _manaIcon;
        [SerializeField] private int _attackValue;
        [SerializeField] private int _hpValue;
        [SerializeField] private int _manaValue;

        public string SpriteURL { get => _spriteURL;}
        //public Sprite UIOverlay { get => _uIOverlay; }
        public string Title { get => _title; }
        public string Description { get => _description;}
        //public Sprite AttackIcon { get => _attackIcon; }
        //public Sprite HPIcon { get => _hpIcon; }
        //public Sprite ManaIcon { get => _manaIcon; }
        public int AttackValue { get => _attackValue; set => _attackValue = value; }
        public int HPValue { get => _hpValue; set => _hpValue = value; }
        public int ManaValue { get => _manaValue; set => _manaValue = value; }
    }
}

