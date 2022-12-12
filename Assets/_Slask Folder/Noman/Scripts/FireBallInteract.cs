using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FireBallInteract : MonoBehaviour
{
    [SerializeField] private GameObject FireOnDoor;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FinalDoor"))
        {
            gameObject.SetActive(false);
            FireOnDoor.SetActive(true);
            Invoke("FireDoor", 5f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    void FireDoor()
    {
        FireOnDoor.SetActive(false);

    }
}
