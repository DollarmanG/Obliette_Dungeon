using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{   
    //canvas group is neded to get acces to the alpha veriable
    [SerializeField] private CanvasGroup canvasGroup;

    // These are set to false so that the code dose not run whitout input or When its not calld.
    private bool _fadeIn = false;
    private bool _fadeOut = false;

    // These are the time veriable that mekes it posible for adjustment in the fade durastion.
    public float timeToFadeOut;
    public float timeToFadeIn;

    private void Start()
    {
        // This is calld first so a fade in happens when the scean is loaded. 
        FadeIn();
    }
    void Update()
    {   
        if (_fadeIn)
        {
            // This cheks if the alpha of the canvas is fully visible and if it is, then it will fade it out under a durastion. When it is att a desigerd fade then it stops fading it
            if (canvasGroup.alpha <= 1)
            {
                canvasGroup.alpha -= timeToFadeIn * Time.deltaTime;
                if (canvasGroup.alpha >= 1)
                {
                    _fadeIn = false;
                }
            }
        }
        if (_fadeOut)
        {
            // This cheks if the alpha of the canvas is fully invicible and if it is, then it will fade it in under a durastion. When it is att a desigerd fade then it stops defading it
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha += timeToFadeOut * Time.deltaTime;
                if (canvasGroup.alpha == 0)
                {
                    _fadeIn = false;
                }
            }
        }
    }
    // When this is calld then fade in on update vill will start
    public void FadeIn()
    {
        _fadeIn = true;
    }
    // When this is calld then fade out on update vill will start
    public void FadeOut()
    {
        _fadeOut = true;
    }
}
