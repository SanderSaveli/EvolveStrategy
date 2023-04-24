using TMPro;
using UnityEngine;

public class UnitText : MonoBehaviour, ITextChanger, IMainColorChanger, ITransparencyChanger
{
    protected TextMeshProUGUI _textField;
    private void Awake()
    {
        _textField = GetComponent<TextMeshProUGUI>();
    }
    public void ChangeTransparency(float alpha)
    {
        Color col = _textField.faceColor;
        col.a = alpha;
        _textField.faceColor = col;
    }
    public void ChangeColorTo(Color color)
    {
        color.a = _textField.faceColor.a;
        _textField.faceColor = color;
    }

    public void ChangeText(string text)
    {
        _textField.text = text;
    }
}
