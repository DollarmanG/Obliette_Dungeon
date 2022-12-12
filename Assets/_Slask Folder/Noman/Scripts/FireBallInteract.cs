using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FireBallInteract : MonoBehaviour
{
    [SerializeField] private GameObject fireOnDoor;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Rigidbody rb;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FinalDoor"))
        {
            gameObject.SetActive(false);
            fireOnDoor.SetActive(true);
            Invoke("FireDoor", 5f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            gameObject.transform.position = respawnPoint.position;
            rb.constraints = RigidbodyConstraints.None;

        }
    }

    void FireDoor()
    {
        fireOnDoor.SetActive(false);

    }
}
