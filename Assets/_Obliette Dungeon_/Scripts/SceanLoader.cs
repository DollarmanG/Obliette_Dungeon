using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceanLoader : MonoBehaviour
{
    // This index is youset to change whitch scean to load frelly
    [SerializeField] int indexerNummber;


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
}
