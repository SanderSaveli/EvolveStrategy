using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuideWindow : MonoBehaviour
{
    public delegate void PlayerPressNext(GuideWindow window);
    public event PlayerPressNext OnPlayerPressNext;


    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI titleText;

    public void FillWindow(GuideWindowData data)
    {
        description.text = data.Text;
        image.sprite = data.Image;

        titleText.text = data.title;
        buttonText.text = "Next";
    }

    public void Next() 
    { 
        OnPlayerPressNext.Invoke(this);
    }
}
