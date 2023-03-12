using UnityEngine;
using UnityEngine.UI;

public class ImageTransparencyChanger : MonoBehaviour, ITransparencyChanger
{
    private Image _image;
    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    public void ChangeTransparency(float alpha)
    {
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alpha);
    }
}
