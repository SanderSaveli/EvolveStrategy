using UnityEngine;

namespace CardSystem
{
    public class ShopElement : MonoBehaviour
    {
        public CardShop shop;
        public ICard card;
        public void TryToBuy()
        {
            shop.TryByCard(card.cardData);
        }
    }

}
