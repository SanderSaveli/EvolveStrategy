using CardSystem;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
using System.Linq;

[RequireComponent(typeof(CardShopView))]
public class CardShop : MonoBehaviour
{
    private List<CardData> _avalibleForSell = new();

    private List<CardData> _currentCards;

    private Bank bank;
    private CardShopView view;

    [SerializeField] private int cardInRoll;
    private const int PRISE_FOR_REROLL = 10;

    private void Start()
    {
        _avalibleForSell = Resources.LoadAll<CardData>("Cards/CardsData/StartCards").ToList();
        bank = Bank.instance;
        view = GetComponent<CardShopView>();
        view.OnTryBuyCard += TryByCard;
        RerollCurrentCards();
    }

    public void TryRerollForPoints() 
    {
        if (bank.TryToBuy(AcktorList.Player, PRISE_FOR_REROLL))
        {
            RerollCurrentCards();
        }
    }
    private void RerollCurrentCards()
    {
        List<CardData> notSelectedCards = _avalibleForSell;
        List<CardData> selectedCards = new();
        int sampleLength = cardInRoll > notSelectedCards.Count ? notSelectedCards.Count : cardInRoll;
        for (int i =0; i < sampleLength; i++) 
        { 
            int index = Random.Range(0, notSelectedCards.Count -1);
            selectedCards.Add(notSelectedCards[index]);
            notSelectedCards.Remove(notSelectedCards[index]);
        }
        _currentCards = selectedCards;
        view.RefillCards(selectedCards);
    }

    public bool TryByCard(CardData card) 
    {
        if (bank.TryToBuy(AcktorList.Player, card.cost)) 
        {
            AddNewCardsToSellPull(card);
            EventBus.RaiseEvent<ICardBoughtHandler>(it => it.CardBought(card));
            return true;
        }
        return false;
    }
    private void AddNewCardsToSellPull(CardData card) 
    { 
        foreach(CardData unlockedCard in card.openCards) 
        {
            if (!_avalibleForSell.Contains(unlockedCard)) 
            {
                _avalibleForSell.Add(unlockedCard);
            }
        }
    }
}
