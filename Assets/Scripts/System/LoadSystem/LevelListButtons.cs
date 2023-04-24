using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelListButtons : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
