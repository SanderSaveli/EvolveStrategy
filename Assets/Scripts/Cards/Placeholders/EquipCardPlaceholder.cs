using UnityEngine;
using EventBusSystem;
using UnityEngine.UI;

namespace CardSystem
{
    public class EquipCardPlaceholder : MonoBehaviour, ICardPlaceholder, IPlayerHoldsCardHandler
    {
        [SerializeField] private Image _typeIcon;
        [SerializeField] private Image _typeIconCirculit;

        private bool _isEmpty = true;
        [SerializeField] private CardType _cardType;
        [SerializeField] private Transform cardPlace;


        public CardType slotType { get => _cardType; private set => _cardType = value; }

        public bool isEmpty { get => _isEmpty; private set => _isEmpty = value; }

        public ICard card { get; private set; }

        private void Start()
        {
            RepaintTypeElements();
            EventBus.Subscribe(this);
        }

        public void RemoveCard(ICard card)
        {
            isEmpty = true;
        }

        public bool TryPlaceCard(ICard card)
        {
            if (slotType == card.cardData.cardType)
            {
                UpdateCardInfo(card);
                PostCard(card);
                return true;
            }
            return false;
        }

        private void UpdateCardInfo(ICard card) 
        {
            EventBus.RaiseEvent<ICardEquipedHandler>(it => it.CardEquiped(card, this.card));
            this.card = card;
        }
        private void PostCard(ICard card) 
        {
            RectTransform cardTransform = card.rectTransform;

            cardTransform.SetParent(cardPlace, false);
            cardTransform.SetAsLastSibling();
            cardTransform.localPosition = Vector3.zero;

            cardTransform.anchorMax = Vector2.one;
            cardTransform.anchorMin = Vector2.zero;
            cardTransform.offsetMin = Vector2.zero;
            cardTransform.offsetMax = Vector2.zero;
        }

        private void RepaintTypeElements()
        {
            TypeToColorConverter palette = Resources.Load<TypeToColorConverter>("Cards/CardPalette");
            _typeIconCirculit.color = palette.GetTypeColor(slotType);
            _typeIcon.sprite = palette.GetTypeSprite(slotType);
        }

        public void ReturnCardBack(ICard card)
        {
            PostCard(card);
        }

        public void PlayerStartHoldCard()
        {
            if(card != null)
                card.rectTransform.GetComponent<Image>().raycastTarget = false;
        }

        public void PlayerStopHoldCard()
        {
            if (card != null)
                card.rectTransform.GetComponent<Image>().raycastTarget = true;
        }
    }
}

