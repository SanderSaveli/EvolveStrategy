using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ZoomInOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler   
{
    private RectTransform _rectTransform;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeMaskableParametr(false, _rectTransform);
        _rectTransform.localScale = new Vector3(1.5f, 1.5f, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeMaskableParametr(true, _rectTransform);
        _rectTransform.localScale = Vector3.one;
    }

    private void ChangeMaskableParametr(bool isOn, Transform transform) 
    {
        if(transform.TryGetComponent(out Image img)) 
        {
            img.maskable = isOn;
        }
        if (transform.TryGetComponent(out TextMeshProUGUI txt))
        {
            txt.maskable = isOn;
        }
        for(int i =0; i < transform.childCount; i++) 
        {
            ChangeMaskableParametr(isOn, transform.GetChild(i));
        }
    }
}
