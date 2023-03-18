using System;
using UnityEngine.UI;

namespace BattleSystem
{
    public class Unit
    {
        public Unit(GameAcktor owner)
        {
            this.owner = owner;
            _attack = 10;
            _defense = 10;
            _walckSpeed = 1;
            _spawnSpeed = 1;
            _climbSpeed = 0.25f;
            _swimSpeed = 0.25f;

            _poisonResistance = 0.25f;
            _coldResistance= 0.25f;
            _hotResistance = 0.25f;
        }
        /// <summary>
        /// The time it takes for one unit to spawn on a normal cell at 100% spawn rate
        /// </summary>
        public const float DEFAUL_TIME_TO_SPAWN = 3;
        /// <summary>
        /// Time required to move to a normal tile at 100% movement speed
        /// </summary>
        public const float DEFAUL_TIME_TO_WALCK = 4;

        private int _attack;
        private int _defense;
        private float _walckSpeed;
        private float _spawnSpeed;
        private float _swimSpeed;
        private float _climbSpeed;
        private float _coldResistance;
        private float _hotResistance;
        private float _poisonResistance;

        public GameAcktor owner;

        public int attack
        {
            get => _attack > 0 ? _attack : 1;
            set => _attack = value;
        }
        public int defense
        {
            get => _defense > 0 ? _defense : 1;
            set => _defense = value;
        }

        public float walckSpeed
        {
            get => _walckSpeed < 0.1f ? 0.1f : _walckSpeed;
            set => _walckSpeed = value;
        }
        public float spawnSpeed
        {
            get => _spawnSpeed < 0.1f ? 0.1f : _spawnSpeed;
            set => _spawnSpeed = value;
        }

        public float swimSpeed
        {
            get => _swimSpeed < 0 ? 0 : _swimSpeed;
            set => _swimSpeed = value;
        }
        public float climbSpeed
        {
            get => _climbSpeed < 0 ? 0 : _climbSpeed;
            set => _climbSpeed = value;
        }

        public float coldResistance
        {
            get => Math.Clamp(_coldResistance, 0, 1);
            set => _coldResistance = value;
        }
        public float heatResistance
        {
            get => Math.Clamp(_hotResistance, 0, 1);
            set => _hotResistance = value;
        }
        public float poisonResistance
        {
            get => Math.Clamp(_poisonResistance, 0, 1);
            set => _poisonResistance = value;
        }

        public float GetMoveDuration(MoveType type) 
        {
            switch (type)
            {
                case (MoveType.Walcking):
                    return DEFAUL_TIME_TO_WALCK / walckSpeed;
                case (MoveType.Swimming):
                    return DEFAUL_TIME_TO_WALCK / swimSpeed;
                case (MoveType.Climbing):
                    return DEFAUL_TIME_TO_WALCK / climbSpeed;
                default:
                    return DEFAUL_TIME_TO_WALCK / walckSpeed;
            }
        }
    }
}

