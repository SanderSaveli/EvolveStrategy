using UnityEngine;
using UnityEngine.EventSystems;

namespace TileSystem 
{ 
    public class NestBuildView : MonoBehaviour, IPointerClickHandler
    {
        public delegate void Click();
        public event Click OnClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick.Invoke();
        }
    }
}

