using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace CardSystem 
{ 
    public class ShopSlot : MonoBehaviour
    {
        public delegate void TryBuyCard(CardData card);
        public event TryBuyCard OnTryBuyCard;

        public RectTransform CardPlace;
        public TextMeshProUGUI price;
        public Card plasedCard;
        public void FillCard(Card card) 
        {
            plasedCard = card;
            RectTransform cardTransform = card.rectTransform;
            cardTransform.SetParent(CardPlace, false);
            cardTransform.anchorMax = Vector2.one;
            cardTransform.anchorMin = Vector2.zero;
            cardTransform.offsetMin = Vector2.zero;
            cardTransform.offsetMax = Vector2.zero;
            price.text = card.cardData.cost.ToString();
            OnClickedInvoke onClickedInvoke = plasedCard.AddComponent<OnClickedInvoke>();
            onClickedInvoke.OnClick += BuyCard;
        }

        public void ChangeCard(CardData data) 
        { 
            plasedCard.InstateCard(data);
            price.text = data.cost.ToString();
        }

        public void BuyCard() 
        {
            OnTryBuyCard.Invoke(plasedCard.cardData);
        }

    }
}

