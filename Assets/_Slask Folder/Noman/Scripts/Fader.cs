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

    //coroutine of a fixed time (in inspector) how long time the fadeout should take
    public void FadeOut()
    {
        StartCoroutine(fadeCoroutine(0));
    }
    //coroutine of a fixed time (in inspector) how long time the fadein should take
    public void FadeIn()
    {
        StartCoroutine(fadeCoroutine(1));
    }


    private IEnumerator fadeCoroutine(float endingAlpha)
    {
        //checks the time that has past.
        float elapsedTime = 0;
        //variable that controls the alpha value of the canvas transition
        float startingAlpha = canvasGroup.alpha;

        //checks as long as the time past is less than the fade time, the code inside should keep running
        while (elapsedTime <= fadeDurationSeconds)
        {
            //makes sure the time is according to real time and not frame-wise
            elapsedTime += Time.deltaTime;
            //makes a nice smooth transition
            canvasGroup.alpha = Mathf.Lerp(startingAlpha, endingAlpha, elapsedTime / fadeDurationSeconds);
            yield return null;
        }
        //what it should do when fading is complete
        OnFadeComplete.Invoke();
    }

    //waiting time before it should start the first or next fade.
    private IEnumerator timerBeforeFade(float duration)
    {
        yield return new WaitForSeconds(duration);
        FadeIn();
    }
}
