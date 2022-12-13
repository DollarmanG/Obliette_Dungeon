using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    private bool _fadeIn = false;
    private bool _fadeOut = false;

    public float timeToFade;

   
    // Update is called once per frame
    void Update()
    {
        if (_fadeIn)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += timeToFade * Time.deltaTime;
                if (canvasGroup.alpha >= 1)
                {
                    _fadeIn = false;
                }
            }
        }
        if (_fadeOut)
        {
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha += timeToFade * Time.deltaTime;
                if (canvasGroup.alpha == 0)
                {
                    _fadeIn = false;
                }
            }
        }
    }
    public void FadeIn()
    {
        _fadeIn = true;
    }
    public void FadeOut()
    {
        _fadeOut = true;
    }
}
