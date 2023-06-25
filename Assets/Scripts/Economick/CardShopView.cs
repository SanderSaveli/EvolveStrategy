using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

namespace CardSystem
{
    public class CardShopView : MonoBehaviour, ICardBoughtHandler
    {
        public delegate bool TryBuyCard(CardData card);
        public event TryBuyCard OnTryBuyCard;

        [SerializeField] private Transform cardZome;
        private GameObject cardAsset;
        private GameObject shopSlot;
        private List<ShopSlot> _activeSlots = new();
        private void Awake()
        {
            cardAsset = (GameObject)Resources.Load("Cards/CardPrefab/CardAsset");
            shopSlot = (GameObject)Resources.Load("Cards/CardPrefab/ShopSlot");
            EventBus.Subscribe(this);
        }

        private void BuyCard(CardData card) 
        { 
            OnTryBuyCard.Invoke(card);
        }

        public void RefillCards(List<CardData> cardsData)
        {
            for(int i =0; i < cardsData.Count; i++) 
            { 
                if(_activeSlots.Count - 1> i) 
                {
                    RefillExistingCard(i, cardsData[i]);
                }
                else 
                {
                    CreateNewCard(cardsData[i]);
                }
            }
            if(_activeSlots.Count > cardsData.Count) 
            {
                DeleteAllActiveCardAfterIndex(_activeSlots.Count);
            }
        }

        private void RefillExistingCard(int index, CardData data) 
        {
            _activeSlots[index].ChangeCard(data);
        }
        private void CreateNewCard(CardData data)
        {
            ShopSlot newSlot = Instantiate(shopSlot, cardZome).GetComponent<ShopSlot>();
            Card newCard = Instantiate(cardAsset).GetComponent<Card>();
            newCard.InstateCard(data);
            newSlot.FillCard(newCard);
            _activeSlots.Add(newSlot);
            newSlot.OnTryBuyCard += BuyCard;
        }

        private void DeleteAllActiveCardAfterIndex(int index) 
        { 
            for(int i = index; i < _activeSlots.Count; i++) 
            {
                DeleteSlot(_activeSlots[i]);
            }
        }

        private void DeleteSlot(ShopSlot slot) 
        {
            slot.OnTryBuyCard -= BuyCard;
            Destroy(slot.gameObject);
        }

        public void CardBought(CardData card)
        {
            ShopSlot removableSlot = null;
            foreach(ShopSlot slot in _activeSlots) 
            { 
                if(slot.plasedCard.cardData == card) 
                {
                    removableSlot = slot;
                    break;
                }
            }
            DeleteSlot(removableSlot);
        }
    }

}

