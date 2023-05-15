using BattleSystem;
using CardSystem;
using EventBusSystem;
using TileSystem;
using UnityEngine;

public class Player : GameAcktor, ISwipeHandler, IClickHandler, ICardEquipedHandler
{
    private BattleService _battleManager;
    private GameStateManager _gameStateManager;
    private NestBuilder _nestBuilder;
    private Region chasenRegion;

    public Player() : 
        base( AcktorList.Player)
    {
        _battleManager = ServiceLocator.Get<BattleService>();
        _gameStateManager = ServiceLocator.Get<GameStateManager>();
        _nestBuilder = NestBuilder.instance;
    }
    public override void OfferToBuildNest(Region region)
    {
        chasenRegion = region;
        region.ShowNestBuildingViewForPlayer();
    }

    public void RightClick(Vector3 position)
    {   }

    public void LeftClick(Vector3 position)
    {
        if (_gameStateManager.currentState == GameStates.NestCellChoses)
        {
            if (_terrainTilemap.ContainTile(position))
            {
                if(_terrainTilemap.GetTile(position).region == chasenRegion) 
                {
                    if (_nestBuilder.TryBuildNest(_terrainTilemap.GetTile(position)))
                    {
                        EventBus.RaiseEvent<IPlayerChoosesNestCellHandler>(it => it.EndChoiseState(_terrainTilemap.GetTile(position).region));
                    }
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
        if (data.walkSpeedBonus != 0)
        {
            unit.walckSpeed += data.walkSpeedBonus;
        }
        if (data.spawnSpeedBonus != 0)
        {
            unit.spawnSpeed += data.spawnSpeedBonus;
        }
        if (data.swimSpeedTimeBonus != 0)
        {
            unit.swimSpeed += data.swimSpeedTimeBonus;
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
