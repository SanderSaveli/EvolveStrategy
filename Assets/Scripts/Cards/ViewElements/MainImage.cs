using UnityEngine;
using UnityEngine.UI;

public class MainImage : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void ChangeImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
