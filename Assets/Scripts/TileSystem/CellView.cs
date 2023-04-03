using TMPro;
using UnityEngine;

public class CellView : MonoBehaviour
{
    private UnitText _unitsNumberTxt;
    private NestView _nestIcon;

    private ViewFaider _faider;

    private void Awake()
    {
        _unitsNumberTxt = GetComponentInChildren<UnitText>();
        _nestIcon = GetComponentInChildren<NestView>();
        _faider = new();
        transform.transform.localScale =
            gameObject.GetComponentInParent<Transform>().localScale;
    }

    public void UpdateUnitView(int newUnitNumber, AcktorList owner, bool isShowen)
    {
        _unitsNumberTxt.ChangeText(newUnitNumber.ToString());
        Color col = new PlayersColors().GetColor(owner);
        _unitsNumberTxt.ChangeColorTo(col);
    }

    public void UpdateNestView(bool isBuilt, bool isShowen)
    {
        _nestIcon.gameObject.SetActive(isBuilt);
        if (isBuilt)
        {
            _nestIcon.ChangeTransparency(isShowen ? 1 : 0);
        }
    }

    public void UpdateFoodView()
    {

    }

    public void HideView()
    {
        _faider.FadeOutAllView(transform);
    }

    public void ShowView(bool isBuilded)
    {
        _faider.FadeInAllView(transform);
    }
}
