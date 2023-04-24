using UnityEngine;
using UnityEngine.UI;

public class ImageColorChanger : MonoBehaviour, IMainColorChanger
{
    public void ChangeColorTo(Color color)
    {
        GetComponent<Image>().color = color;
    }
}
