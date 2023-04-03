using System;
using EventBusSystem;

namespace BattleSystem
{
    public class Unit
    {
        private int _attack;
        private int _defense;
        private float _walckSpeed;
        private float _spawnSpeed;
        private float _swimSpeed;
        private float _climbSpeed;
        private float _coldResistance;
        private float _hotResistance;
        private float _poisonResistance;

        public GameAcktor owner { get; private set; }

        public Unit(GameAcktor owner)
        {
            this.owner = owner;
            _attack = BattleConstants.START_UNIT_ATTACK;
            _defense = BattleConstants.START_UNIT_DEFENSE;

            _walckSpeed = BattleConstants.START_UNIT_WALCK_SPEED;
            _climbSpeed = BattleConstants.START_UNIT_CLIMB_SPEED;
            _swimSpeed = BattleConstants.START_UNIT_SWIMMING_SPEED;

            _spawnSpeed = BattleConstants.START_UNIT_SPAWN_SPEED;

            _poisonResistance = BattleConstants.START_UNIT_POISON_RESISTANCE;
            _coldResistance = BattleConstants.START_UNIT_COLD_RESISTANCE;
            _hotResistance = BattleConstants.START_UNIT_HEAT_RESISTANCE;
        }

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
                    return BattleConstants.DEFAUL_TIME_TO_WALCK / walckSpeed;
                case (MoveType.Swimming):
                    return BattleConstants.DEFAUL_TIME_TO_WALCK / swimSpeed;
                case (MoveType.Climbing):
                    return BattleConstants.DEFAUL_TIME_TO_WALCK / climbSpeed;
                default:
                    return BattleConstants.DEFAUL_TIME_TO_WALCK / walckSpeed;
            }
        }
    }
}

