using EventBusSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowManager : MonoBehaviour, IGameEndHandler
{
    private GameStateManager _gameStateManager;
    [SerializeField] private GameObject _darckerPrefab;
    [SerializeField] private Canvas _canvas;

    [Header("Guide Window")]
    [SerializeField] private GameObject _guideWindowPrefab;
    [SerializeField] private GuideWindowData[] _startGuideWindows;

    [Header("Card Window")]
    [SerializeField] private GameObject _cardWindow;

    [Header("End Window")]
    [SerializeField] private GameObject _gameEndWindow;


    private bool _isWindowShowes;

    private Queue<GuideWindowData> _guideWindows = new();

    public void Start()
    {
        _gameStateManager = GetComponent<GameStateManager>();
        foreach(GuideWindowData window in _startGuideWindows) 
        {
            _guideWindows.Enqueue(window);
        }
        EventBus.Subscribe(this);
    }
    public void AddDataToQueue(string name)
    {
        _guideWindows.Enqueue(Resources.Load<GuideWindowData>("GuideWindows/" + name));
        if (!_isWindowShowes)
        {
            StartShowWindows();
        }
    }
    public void AddDataToQueue(GuideWindowData data)
    {
        _guideWindows.Enqueue(data);
        if (!_isWindowShowes)
        {
            StartShowWindows();
        }
    }
    private void StartShowWindows()
    {
        _isWindowShowes = true;
        Time.timeScale = 0;
        _darckerPrefab.SetActive(true);
        EventBus.RaiseEvent<IWindowOpenHandler>(it => it.WindowOnen());
        ShowWindow(_guideWindows.Dequeue());
    }

    private void StopShowWindows()
    {
        _isWindowShowes = false;
        Time.timeScale = 1;
        _darckerPrefab.SetActive(false);
        EventBus.RaiseEvent<IWindowOpenHandler>(it => it.WindowClosed());
    }

    private void ShowWindow(GuideWindowData name)
    {
        _isWindowShowes = true;
        GuideWindow window = Instantiate(_guideWindowPrefab).GetComponent<GuideWindow>();
        window.transform.SetParent(_canvas.transform, false);
        window.transform.SetAsLastSibling();
        window.FillWindow(name);
        window.OnPlayerPressNext += ShowNextWindow;
    }

    private void ShowNextWindow(GuideWindow window)
    {
        Destroy(window.gameObject);
        if (_guideWindows.Count > 0)
        {
            ShowWindow(_guideWindows.Dequeue());
        }
        else
        {
            StopShowWindows();
        }
    }
    public void OpenCardWindow()
    {
        if(_gameStateManager.currentState == GameStates.Battle) 
        {
            _cardWindow.SetActive(true);
            Time.timeScale = 0;
            EventBus.RaiseEvent<IWindowOpenHandler>(it => it.WindowOnen());
        }
    }
    public void CloseCardWindow()
    {
        _cardWindow.SetActive(false);
        Time.timeScale = 1;
        EventBus.RaiseEvent<IWindowOpenHandler>(it => it.WindowClosed());
    }

    public void PlayerWin()
    {
        GameEndWindow endWindow = 
            Instantiate(_gameEndWindow, _canvas.transform).GetComponent<GameEndWindow>();
        endWindow.transform.SetAsLastSibling();
        _darckerPrefab.SetActive(true);
        endWindow.header.text = "You win!";
        endWindow.backToMeny.onClick.AddListener(BackToMenu);
        endWindow.restart.onClick.AddListener(Restart);
        StartCoroutine(WaitAndUnsubscribe());
    }

    public void PlayerLose()
    {
        GameEndWindow endWindow =
        Instantiate(_gameEndWindow, _canvas.transform).GetComponent<GameEndWindow>();
        endWindow.transform.SetAsLastSibling();
        _darckerPrefab.SetActive(true);
        endWindow.header.text = "You lose!";
        endWindow.backToMeny.onClick.AddListener(BackToMenu);
        endWindow.restart.onClick.AddListener(Restart);
        StartCoroutine(WaitAndUnsubscribe());
    }

    public void BackToMenu() 
    {
        Destroy(FindObjectOfType<GameEndWindow>());
        _darckerPrefab.SetActive(false);
        SceneManager.LoadScene("LevelsList");
    }
    public void Restart()
    {
        Destroy(FindObjectOfType<GameEndWindow>());
        _darckerPrefab.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+0);
    }
    IEnumerator WaitAndUnsubscribe() 
    { 
        yield return new WaitForSeconds(1);
        EventBus.Unsubscribe(this);
    }
}
