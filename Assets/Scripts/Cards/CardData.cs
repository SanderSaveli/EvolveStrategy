using UnityEngine;
using UnityEngine.Serialization;

namespace CardSystem
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Cards/New CardData")]
    public class CardData : ScriptableObject
    {
        
        //на сколько изменять за уровень
        private const int ATTACK_PER_LEVEL = 5; 
        private const int DEFENSE_PER_LEVEL = 5; 
        private const float SPAWN_PER_LEVEL = 5;
        private const float MOVE_PER_LEVEL = 0.25f;
        private const float RESISTANCE_PER_LEVEL = 0.1f;
        
        [Header("General:")]
        public CardType cardType;
        public string title;
        public int number;
        public Sprite image;
        public int cost;

        [FormerlySerializedAs("_attackBonus")]
        [Header("Bonuses:")]
        
        //уровень
        [SerializeField] private int _attackLevel; 
        [SerializeField] private int _defenseLevel;
        [SerializeField] private float _spawnSpeedevel;//0-1
        [SerializeField] private float _walkSpeedLevel;
        [SerializeField] private float _swimSpeedTimeLevel;
        [SerializeField] private float _climbSpeedLevel;
        [SerializeField] private float _coldResistanceLevel; 
        [SerializeField] private float _heatResistanceLevel;
        [SerializeField] private float _poisonResistanceLevel;

        [Header("Open Cards:")]
        public CardData[] openCards;

        public int attackBonus
        {
            get => _attackLevel * ATTACK_PER_LEVEL;
            set => _attackLevel = value;
        }
        
        public int defenseBonus
        {
            get => _defenseLevel * DEFENSE_PER_LEVEL;
            set => _defenseLevel = value;
        }
        
        public float spawnSpeedBonus
        {
            get => _spawnSpeedevel * SPAWN_PER_LEVEL;
            set => _spawnSpeedevel = value;
        }
        
        public float walkSpeedBonus
        {
            get => _walkSpeedLevel * MOVE_PER_LEVEL;
            set => _walkSpeedLevel = value;
        }
        
        public float swimSpeedTimeBonus
        {
            get => _swimSpeedTimeLevel * MOVE_PER_LEVEL;
            set => _swimSpeedTimeLevel = value;
        }

        public float climbSpeedBonus
        {
            get => _climbSpeedLevel * MOVE_PER_LEVEL;
            set => _climbSpeedLevel = value;
        }
        
        public float coldResistanceBonus
        {
            get => _coldResistanceLevel * RESISTANCE_PER_LEVEL;
            set => _coldResistanceLevel = value;
        }
        
        public float heatResistanceBonus
        {
            get => _heatResistanceLevel * RESISTANCE_PER_LEVEL;
            set => _heatResistanceLevel = value;
        }
        
        public float poisonResistanceBonus
        {
            get => _poisonResistanceLevel * RESISTANCE_PER_LEVEL;
            set => _poisonResistanceLevel = value;
        }

    }
}

