using UnityEngine;
using UnityEngine.UI;

public class NestView : MonoBehaviour, ITransparencyChanger, IMainColorChanger
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _background;
    [SerializeField] private Image _circuit;

    public void ChangeColorTo(Color color)
    {
        _background.color = color;
    }

    public void ChangeTransparency(float alpha)
    {
        if(_icon != null) 
        {
            if (_icon.enabled == true)
            {
                _icon.color = new Color(_icon.color.r, _icon.color.g, _icon.color.b, alpha);
                _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, alpha);
                _circuit.color = new Color(_circuit.color.r, _circuit.color.g, _circuit.color.b, alpha);
            }
        }
    }
}
