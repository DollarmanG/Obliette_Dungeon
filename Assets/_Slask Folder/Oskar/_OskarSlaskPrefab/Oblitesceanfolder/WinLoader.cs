using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoader : MonoBehaviour
{
    // Gets the Scenmaneger and acceses all the SceneLoader script on it
    [SerializeField] GameObject _sceanManeger;
    SceneLoader sceneLoader;

    // Gets the fade scripts from Camra canvas
    [SerializeField] GameObject _fadeOut;
    FadeScript fadeScript;

    // Makes it posible to change the timing in serializedfild
    [SerializeField] float _fadeTime;

    // Makes it posible to change the time for scen schift
    [SerializeField] float _timeUntilSceneShift;

    private void Start()
    {
        // this aplyse the coponent to sceneLoader so that e can acces all the funstions
        sceneLoader = _sceanManeger.GetComponent<SceneLoader>();

        // This aplays the component to fadeScript so that it can acces all of the funstions
        fadeScript = _fadeOut.GetComponent<FadeScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //this refers to a script in fade in script that changes the timmer
        fadeScript.TimeToFadeOut(_fadeTime);
        // Load Load time funstion from sceneLoader
        sceneLoader.LoadTime(_timeUntilSceneShift);
        // Load Win scean 
        sceneLoader.InvokeNewSceneLoader();
        // Load Fade out funstion
        fadeScript.FadeOut();
    }


}
