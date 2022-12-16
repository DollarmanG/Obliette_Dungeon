using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorDissapear : MonoBehaviour
{
    //these variables are being created in inspector and is a reference to what object each of them do. 
    [SerializeField] private LayerMask colliderMask;
    [SerializeField] private GameObject fireOnDoor;
    [SerializeField] private AudioSource doorBurningSound;
    [SerializeField] private AudioSource audioOnImpact;
    [SerializeField] private AudioClip fireballImpactClip;

    
    void OnTriggerEnter(Collider other)
    {
        
        int testMask = 1 << other.gameObject.layer;

        //checks if the object matches the bit value and the flag if it matches the mask, then it continues with the codes below.
        if ((testMask & colliderMask.value) > 0)
        {
            Destroy(other.gameObject);

            fireOnDoor.SetActive(true);
            //each invoke sets a timer when it should activate the method below.
            Invoke("FireDoor", 5f);
            Invoke("DoorDissapear", 5f);
            //plays the audio one time.
            audioOnImpact.PlayOneShot(fireballImpactClip);
            doorBurningSound.PlayOneShot(doorBurningSound.clip);
            doorBurningSound = null;
        }
    }

    //makes the door vanish.
    void DoorDissapear()
    {
        gameObject.SetActive(false);
    }

    //makes the fire on the door vanish.
    void FireDoor()
    {
        fireOnDoor.SetActive(false);

    }
}
