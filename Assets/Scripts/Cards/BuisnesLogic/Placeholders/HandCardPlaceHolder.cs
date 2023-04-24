using UnityEngine;

namespace CardSystem
{
    public class HandCardPlaceHolder : MonoBehaviour, ICardPlaceholder
    {
        private bool _isEmpty = true;

        public bool isEmpty { get => _isEmpty; private set => _isEmpty = value; }

        public ICard card { get; private set; }

        public void RemoveCard(ICard card)
        {
            Destroy(gameObject);
        }

        public bool TryPlaceCard(ICard card)
        {
            if (isEmpty)
            {
                this.card = card;
                PostCard(card);
                return true;
            }
            return false;
        }

        private void PostCard(ICard card)
        {
            RectTransform cardTransform = card.rectTransform;

            cardTransform.SetParent(transform, false);
            cardTransform.SetAsLastSibling();
            cardTransform.localPosition = Vector3.zero;

            cardTransform.anchorMax = Vector2.one;
            cardTransform.anchorMin = Vector2.zero;
            cardTransform.offsetMin = Vector2.zero;
            cardTransform.offsetMax = Vector2.zero;
        }

        public void ReturnCardBack(ICard card)
        {
            PostCard(card);
        }
    }
}
