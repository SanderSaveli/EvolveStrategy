using EventBusSystem;
using TMPro;
using UnityEngine;

public class PlayerPoints : MonoBehaviour, IEvolvePointsChangeHandler
{
    public TextMeshProUGUI textField;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
        textField.text = Bank.instance.GetAcktorPoints(AcktorList.Player).ToString();
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }
    public void EvolvePointsChanges(AcktorList acktor, int value)
    {
        if(acktor == AcktorList.Player) 
        {
            textField.text = value.ToString();
        }
    }
}
