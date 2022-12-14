using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceanLoader : MonoBehaviour
{
    // This index is youset to change whitch scean to load frelly
    [SerializeField] int indexerNummber;

    [SerializeField] int indexerNummberForDeathScean;

    [SerializeField] float loadTime;
    // This funstion schanges a scean to a spesific scean 
    public void NewSceanLoader()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + indexerNummber);
    }
    // This funstion Exits the aplicastion if playd
    public void Quit()
    {
        Application.Quit();
    }
    // This funstion makes it so that if another scean needs to be loaded then it can be loaded.
    public void LoadDeathScean()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + indexerNummberForDeathScean);
    }

    public void InvokeNewSceanLoader()
    {
        Invoke("NewSceanLoader", loadTime);
    }
    public void InvokeQuit()
    {
        Invoke("Quit", loadTime);
    }
    public void InvokeLoadDeathScean()
    {
        Invoke("LoadDeathScean", loadTime);
    }
}
