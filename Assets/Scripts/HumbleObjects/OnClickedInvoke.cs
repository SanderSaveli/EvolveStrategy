using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickedInvoke : MonoBehaviour, IPointerClickHandler
{
    public delegate void Click();
    public event Click OnClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke();
    }
}
