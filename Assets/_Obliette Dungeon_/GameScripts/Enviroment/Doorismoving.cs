using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorismoving : MonoBehaviour
{
    [SerializeField]
    private Transform door;
    [SerializeField]
    private AudioClip[] soundsdoor;
    [SerializeField]
    private AudioSource source;
    void Start()
    {
        
    }
        
    // Nollställer nuvarande och pågående värde till 0.
    private int prevalue = 0;
    private int currentvalue = 0;
    void Update()
    {

        
        // Gör om current value till vad dörren har för rotation på Y axlen till INT.

        currentvalue = Mathf.FloorToInt(door.transform.rotation.eulerAngles.y);

        // Om dörren rör sig så händer nedan.
        if (currentvalue != prevalue)
        {
            source.mute = false;
            source.clip = soundsdoor[Random.Range(0, soundsdoor.Length)];
            source.PlayOneShot(source.clip);
            prevalue = currentvalue;
            
        }

       


    }

    public void stopstound()

    {

        source.mute = true;
    }
}
