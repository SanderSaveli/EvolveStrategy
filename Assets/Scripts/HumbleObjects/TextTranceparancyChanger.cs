using TMPro;
using UnityEngine;

public class TextTranceparancyChanger : MonoBehaviour, ITransparencyChanger
{
    protected TextMeshProUGUI _textField;
    private void Awake()
    {
        _textField = GetComponent<TextMeshProUGUI>();
    }
    public void ChangeTransparency(float alpha)
    {
        _textField.faceColor = new Color(_textField.faceColor.r, _textField.faceColor.g, _textField.faceColor.b, alpha);
    }
}
