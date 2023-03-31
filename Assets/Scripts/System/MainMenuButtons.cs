using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public Credits credits;
    public void ToLevelList()
    {
        SceneManager.LoadScene("LevelsList");
    }
    public void Credits()
    {
        credits.Show();
    }
    public void Exit()
    {
        Application.Quit();
    }
}
