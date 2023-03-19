using UnityEngine;
using UnityEngine.EventSystems;
using EventBusSystem;

namespace CardSystem
{
    public class MovableCard : Card, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private CanvasGroup _canvasGroup;
        private Canvas _canvas;
        private ICardPlaceholder _currentPlaceholder;

        private new void Start()
        {
            base.Start();
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
            _currentPlaceholder = rectTransform.GetComponentInParent<ICardPlaceholder>();
            rectTransform.SetParent(_canvas.transform);
            rectTransform.SetAsLastSibling();
            EventBus.RaiseEvent<IPlayerHoldsCardHandler>(it => it.PlayerStartHoldCard());
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out ICardPlaceholder placeholder))
            {
                if (placeholder.TryPlaceCard(this) && placeholder != _currentPlaceholder)
                {
                    _currentPlaceholder.RemoveCard(this);
                    _currentPlaceholder = placeholder;
                    return;
                }
            }
            _currentPlaceholder.ReturnCardBack(this);
            EventBus.RaiseEvent<IPlayerHoldsCardHandler>(it => it.PlayerStopHoldCard());
        }

        public override void InstateCard(CardData data)
        {
            base.InstateCard(data);
            _canvas = GameObject.FindGameObjectWithTag("CardMenu").GetComponent<Canvas>();
            _canvasGroup = GetComponentInParent<CanvasGroup>();
        }
    }
}

