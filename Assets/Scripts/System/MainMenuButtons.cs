using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void ToLevelList()
    {
        SceneManager.LoadScene("LevelsList");
    }
    public void Credits()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }
}
