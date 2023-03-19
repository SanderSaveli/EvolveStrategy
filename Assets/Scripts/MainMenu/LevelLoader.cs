using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public float transmissionTime = 1f;
    private AudioSource audioSource;

    private void Awake()
    {
        MenuAudio audio = FindObjectOfType<MenuAudio>();
        if(audio == null) 
        {
            GameObject go = new GameObject("MenuAudio");
            audioSource = go.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.clip = Resources.Load<AudioClip>("Audio/punk-rock post ussr (bf08aacc25a042dd9f12fccd64b8632b)");
            audioSource.Play();
        }
        else 
        {
            audioSource = audio.GetComponent<AudioSource>();
        }
    }
    public void LoadLevel(int levelNumber) 
    {
        Destroy(audioSource.gameObject);
        StartCoroutine(PlayAnimationAndLoad(levelNumber));
    }

    IEnumerator PlayAnimationAndLoad(int levelNumber) 
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transmissionTime);
        SceneManager.LoadScene("Level" + levelNumber);
    }
}
