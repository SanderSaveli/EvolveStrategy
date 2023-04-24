using TMPro;
using UnityEngine;

public class CardTypeText : MonoBehaviour, ITextChanger
{
    private TextMeshProUGUI textField;
    private void Awake()
    {
        textField = GetComponent<TextMeshProUGUI>();
    }
    public void ChangeText(string text)
    {
        this.textField.text = text;
    }
}
