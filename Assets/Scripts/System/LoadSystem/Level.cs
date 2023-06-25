using UnityEngine;
using UnityEngine.EventSystems;

public class Level : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _levelNumber;
    public Transform lockBar;
    private bool _locked;
    private void Start()
    {
        if(!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 0);
        }
        if(PlayerPrefs.GetInt("Level") >= _levelNumber - 1) 
        {
            _locked = false;
            lockBar.gameObject.SetActive(false);
        }
        else 
        {
            _locked = true;
            lockBar.gameObject.SetActive(true);
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
