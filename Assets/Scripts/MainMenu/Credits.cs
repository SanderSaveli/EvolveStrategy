using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Credits : MonoBehaviour
{
    public Animation darcker;
    public AnimationClip darckOn;
    public AnimationClip darckOff;

    public Animation text;

    public void Show()
    {
        darcker.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        darcker.clip = darckOn;
        darcker.Play();
        text.gameObject.SetActive(true);
        text.Play();
        StartCoroutine(waitAndHide(text.clip.length));
    }

    IEnumerator waitAndHide(float duration) 
    { 
        yield return new WaitForSeconds(duration);
        Hide();
    }
    public void Hide()
    {
        darcker.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        darcker.clip = darckOff;
        darcker.Play();
        text.gameObject.SetActive(false);
    }
}
