using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorDissapear : MonoBehaviour
{
    [SerializeField] private LayerMask colliderMask;
    [SerializeField] private GameObject fireOnDoor;
    [SerializeField] private AudioSource doorBurningSound;
    [SerializeField] private AudioSource audioOnImpact;
    [SerializeField] private AudioClip fireballImpactClip;

    void OnTriggerEnter(Collider other)
    {
        int testMask = 1 << other.gameObject.layer;

        if ((testMask & colliderMask.value) > 0)
        {
            Destroy(other.gameObject);

            fireOnDoor.SetActive(true);
            Invoke("FireDoor", 5f);
            Invoke("DoorDissapear", 5f);
            audioOnImpact.PlayOneShot(fireballImpactClip);
            doorBurningSound.PlayOneShot(doorBurningSound.clip);
            doorBurningSound = null;
        }
    }

    void DoorDissapear()
    {
        gameObject.SetActive(false);
    }

    void FireDoor()
    {
        fireOnDoor.SetActive(false);

    }
}
