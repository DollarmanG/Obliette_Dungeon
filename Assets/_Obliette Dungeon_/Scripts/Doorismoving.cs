using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorismoving : MonoBehaviour
{
    [SerializeField]
    private Transform door;
    [SerializeField]
    private AudioClip[] soundsdoor;
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
        
    // Nollst�ller nuvarande och p�g�ende v�rde till 0.
    private int prevalue = 0;
    private int currentvalue = 0;
    void Update()
    {
        // G�r om current value till vad d�rren har f�r rotation p� Y axlen till INT.
        
        currentvalue = Mathf.FloorToInt(door.transform.rotation.eulerAngles.y);

        // Om d�rren r�r sig s� h�nder nedan.
        if (currentvalue != prevalue)
        {
            source.clip = soundsdoor[Random.Range(0, soundsdoor.Length)];
            source.PlayOneShot(source.clip);
            prevalue = currentvalue;
        }

        else if (currentvalue == prevalue)
        { Debug.Log("jag passerade"); }


    }

    public void stopstound()

    {

        source.Stop();

    }
}
