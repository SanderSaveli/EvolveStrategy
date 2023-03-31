using UnityEngine;

[CreateAssetMenu(fileName = "GuideWindowData", menuName = "GUI/New GuideWiondow")]
public class GuideWindowData : ScriptableObject
{
    public Sprite Image;
    public string title;
    public string Text;
}