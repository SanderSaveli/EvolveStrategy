using UnityEngine;
using UnityEngine.EventSystems;

public class Level : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _levelNumber;
    public Transform lockBar;
    private bool _locked;
    private void Start()
    {
        if(!PlayerPrefs.HasKey("Level" + (_levelNumber-1))) 
        {
            _locked = true;
            lockBar.gameObject.SetActive(true);
        }
        else 
        {
            _locked = false;
            lockBar.gameObject.SetActive(false);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_locked) 
        {
            FindObjectOfType<LevelLoader>().LoadLevel(_levelNumber);
        }
    }
}
