using TMPro;
using UnityEngine;

public class TextColorChanger : MonoBehaviour, IMainColorChanger
{
    private TextMeshProUGUI _textField;
    private void Awake()
    {
        _textField = GetComponent<TextMeshProUGUI>();
    }
    public void ChangeColorTo(Color color)
    {
        color.a = _textField.faceColor.a;
        _textField.faceColor = color;
    }
}
