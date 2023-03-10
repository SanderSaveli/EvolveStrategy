using BattleSystem;
using CardSystem;
using EventBusSystem;
using TileSystem;
using UnityEngine;

public class Player : GameAcktor, ISwipeHandler, IClickHandler, ICardEquipedHandler
{
    private BattleManager _battleManager;
    private GameStateManager _gameStateManager;
    private TerrainTilemap _terrainTilemap;
    private NestBuilder _nestBuilder;   
     protected new void Awake()
    {
        base.Awake();
        EventBus.Subscribe(this);
        acktorName = PlayersList.Player;
        _battleManager = BattleManager.instance;
        _gameStateManager = GameStateManager.instance;
        _terrainTilemap = FindObjectOfType<TerrainTilemap>();
        _nestBuilder = NestBuilder.instance;
    }
    public override void OfferToBuildNest(Region region)
    {   }

    public void RightClick(Vector3 position)
    {   }

    public void LeftClick(Vector3 position)
    {
        if (_gameStateManager.currentState == GameStates.NestCellChoses)
        {
            if (_terrainTilemap.ContainTile(position))
            {
                if (_nestBuilder.TryBuildNest(_terrainTilemap.GetTile(position)))
                {
                    EventBus.RaiseEvent<IPlayerChoosesNestCellHandler>(it => it.EndState(_terrainTilemap.GetTile(position).region));
                }
            }
        }
    }

    public void LeftSwipe(Vector3 swipeStartPosition, Vector3 swipeEndPosition)
    {
        if(_gameStateManager.currentState == GameStates.Battle) 
        {
            _battleManager.TryGiveOrderToAttackAllUnit(swipeStartPosition, swipeEndPosition, this);
        }
    }

    public void RightSwipe(Vector3 swipeStartPosition, Vector3 swipeEndPosition)
    {
        if (_gameStateManager.currentState == GameStates.Battle)
        {
            _battleManager.TryGiveOrderToAttackHalfUnit(swipeStartPosition, swipeEndPosition, this);
        }
    }

    public void CardEquiped(ICard card, ICard previousCard)
    {
        CardData data = card.cardData;
        if (data.attackBonus != 0)
        {
            unit.attack += data.attackBonus;
        }
        if (data.defenseBonus != 0)
        {
            unit.defense += data.defenseBonus;
        }
        if (data.moveSpeedBonus != 0)
        {
            unit.moveSpeed += data.moveSpeedBonus;
        }
        if (data.spawnSpeedBonus != 0)
        {
            unit.spawnSpeed += data.spawnSpeedBonus;
        }
        if (data.swimSpeedTimeBonus != 0)
        {
            unit.swimSpeedTime += data.swimSpeedTimeBonus;
        }
        if (data.climbSpeedBonus != 0)
        {
            unit.climbSpeed += data.climbSpeedBonus;
        }
        if (data.coldResistanceBonus != 0)
        {
            unit.coldResistance += data.coldResistanceBonus;
        }
        if (data.heatResistanceBonus != 0)
        {
            unit.heatResistance += data.heatResistanceBonus;
        }
        if (data.poisonResistanceBonus != 0)
        {
            unit.poisonResistance += data.poisonResistanceBonus;
        }
    }
}
