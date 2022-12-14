using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // This index is youset to change whitch scean to load frelly
    [SerializeField] int indexNumberToChangeScene;

    [SerializeField] int indexNumberForDeathScene;

    [SerializeField] float loadTime;
    // This funstion schanges a scean to a spesific scean 
    public void NewSceneLoader()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + indexNumberToChangeScene);
    }
    // This funstion Exits the aplicastion if playd
    public void Quit()
    {
        Application.Quit();
    }
    // This funstion makes it so that if another scean needs to be loaded then it can be loaded.
    public void LoadDeathScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + indexNumberForDeathScene);
    }

    public void InvokeNewSceneLoader()
    {
        Invoke("NewSceneLoader", loadTime);
    }
    public void InvokeQuit()
    {
        Invoke("Quit", loadTime);
    }
    public void InvokeLoadDeathScene()
    {
        Invoke("LoadDeathScene", loadTime);
    }
}
