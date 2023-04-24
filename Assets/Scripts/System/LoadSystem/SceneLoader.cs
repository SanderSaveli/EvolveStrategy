using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : DontDestroyOnLoadSingletone<SceneLoader>
{
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("Level");
        Instantiate(Resources.Load(GetLevelPath(level)));
    }

    public void LoadLevelMenu()
    {

    }

    public void LoadMainMenu()
    {

    }

    private string GetLevelPath(int number)
    {
        return "Levels/Level" + number.ToString() + ".prefab";
    }
}
