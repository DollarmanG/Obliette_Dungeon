using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FireBallInteract : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Rigidbody rb;

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            gameObject.transform.position = respawnPoint.position;
            gameObject.transform.rotation = respawnPoint.rotation;
        }
    }
}
