using UnityEngine;

namespace CardSystem
{
    public class Card : MonoBehaviour, ICard
    {
        public RectTransform rectTransform { get; protected set; }
        protected CardView _view = new();
        public CardData cardData { get; private set; }

        public bool IsEquiped => throw new System.NotImplementedException();

        protected void Start()
        {
            if (cardData != null)
            {
                InstateCard(cardData);
            }
        }

        public virtual void InstateCard(CardData data)
        {
            rectTransform = GetComponent<RectTransform>();
            _view.ColorCard(rectTransform, data);
            cardData = data;
        }
    }
}


