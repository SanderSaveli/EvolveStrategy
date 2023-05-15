using UnityEngine.SceneManagement;
using System;

public class SceneLoader : DontDestroyOnLoadSingletone<SceneLoader>
{
    public GameLoadData gameLoadData { get; private set; }
    public MenuLoadData menuLoadData { get; private set; }

    public void RestartCurrentLevel() 
    {
        if (gameLoadData != null)
            SceneManager.LoadScene("Level", LoadSceneMode.Single);
        else
            throw new Exception("There is no gameLoadData to restart level");
    }
    public void LoadLevel(int level)
    {
        gameLoadData = new GameLoadData(level);
        SceneManager.LoadScene("Level");
    }

    public void LoadLevelMenu()
    {
        menuLoadData = new MenuLoadData();
        SceneManager.LoadScene("LevelMenu");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
