using EventBusSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour, IGameEndHandler, IPauseMenuEventHandler
{
    private GameStateManager _gameStateManager;
    [SerializeField] private GameObject _darckerPrefab;
    private GameObject _darcker;
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
        _gameStateManager = ServiceLocator.Get<GameStateManager>();
        _darcker = CreateDarcker();
        Debug.Log(_darcker);
        StartCoroutine(AddStartWindowToQueue());
        EventBus.Subscribe(this);
    }

    private GameObject CreateDarcker() 
    {
        GameObject darcker = Instantiate(_darckerPrefab);
        darcker.transform.SetParent(
            _canvas.transform);
        RectTransform rt = darcker.GetComponent<RectTransform>();
        rt.sizeDelta = Vector3.zero;
        rt.localPosition = Vector3.zero;
        darcker.transform.SetAsFirstSibling();
        return darcker;
    } 
    private IEnumerator AddStartWindowToQueue() 
    {
        yield return new WaitForSeconds(2);
        foreach (GuideWindowData window in _startGuideWindows)
        {
            //AddDataToQueue(window);
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
        _darcker.SetActive(true);
        EventBus.RaiseEvent<IWindowOpenHandler>(it => it.WindowOnen());
        ShowWindow(_guideWindows.Dequeue());
    }

    private void StopShowWindows()
    {
        _isWindowShowes = false;
        Time.timeScale = 1;
        _darcker.SetActive(false);
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
        ShowGameEndMenu(true);
        StartCoroutine(WaitAndUnsubscribe());
    }

    public void PlayerLose()
    {
        ShowGameEndMenu(false);
        StartCoroutine(WaitAndUnsubscribe());
    }

    public void BackToMenuGameEnd() 
    {
        Destroy(FindObjectOfType<GameEndWindow>());
        _darcker.SetActive(false);
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneLoader.instance.LoadLevelMenu();
    }
    public void Restart()
    {
        Time.timeScale = 1;
        //Destroy(FindObjectOfType<GameEndWindow>());
        _darcker.SetActive(false);
        SceneLoader.instance.RestartCurrentLevel();
    }
    IEnumerator WaitAndUnsubscribe() 
    { 
        yield return new WaitForSeconds(1);
        EventBus.Unsubscribe(this);
    }

    public void OpenPause()
    {
        _darcker.SetActive(true);
        EventBus.RaiseEvent<IWindowOpenHandler>(it => it.WindowOnen());
        Time.timeScale = 0;
    }

    public void ClousePause()
    {
        _darcker.SetActive(false);
        EventBus.RaiseEvent<IWindowOpenHandler>(it => it.WindowClosed());
        Time.timeScale = 1;
    }

    private GameEndWindow ShowGameEndMenu(bool isPlayerWin) 
    {
        GameEndWindow endWindow =
            Instantiate(_gameEndWindow, _canvas.transform).GetComponent<GameEndWindow>();
        endWindow.transform.SetAsLastSibling();
        _darckerPrefab.SetActive(true);

        if(isPlayerWin) endWindow.header.text = "You win!";
        else endWindow.header.text = "You lose!";

        endWindow.backToMeny.onClick.AddListener(BackToMenuGameEnd);
        endWindow.restart.onClick.AddListener(Restart);

        return endWindow;
    }
}
