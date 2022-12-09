using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Fader : MonoBehaviour
{
    // duration from the start of the scene until this fade begins.
    public float delayBeforeFadingIn;
    // duration of fade, set in inspector
    public float fadeDurationSeconds;
    // event for when the fade is done, can be used in inspector or in code
    public UnityEvent OnFadeComplete;

    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        // start the canvas invisible
        canvasGroup.alpha = 0;
        StartCoroutine(timerBeforeFade(delayBeforeFadingIn));
    }

    public void FadeOut()
    {
        StartCoroutine(fadeCoroutine(0));
    }

    public void FadeIn()
    {
        StartCoroutine(fadeCoroutine(1));
    }

    private IEnumerator fadeCoroutine(float endingAlpha)
    {
        float elapsedTime = 0;
        float startingAlpha = canvasGroup.alpha;

        while (elapsedTime <= fadeDurationSeconds)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startingAlpha, endingAlpha, elapsedTime / fadeDurationSeconds);
            yield return null;
        }

        OnFadeComplete.Invoke();
    }

    private IEnumerator timerBeforeFade(float duration)
    {
        yield return new WaitForSeconds(duration);
        FadeIn();
    }
}
