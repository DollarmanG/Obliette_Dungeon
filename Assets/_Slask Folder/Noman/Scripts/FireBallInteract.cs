using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FireBallInteract : MonoBehaviour
{
    //these variables are being created in inspector and is a reference to what object each of them do. 
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AudioSource audioOnImpact;
    [SerializeField] private AudioClip fireballImpactClip;

    void OnCollisionEnter(Collision collision)
    {
        //this makes sure that the collision between the object and the player does not collide with eachother. if this was not set like this, the object would endlessly do a loop of the code below(respawn)
        if (!collision.gameObject.CompareTag("Player"))
        {
            //sets the velocity of the object (fireball in this case) to 0, when it respawns so that when the player throws the fireball, when it respawns, it doesn't keep moving with the velocity it had before.
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            //respawns the object(fireball) to the reference respawnpoint it is set to.
            gameObject.transform.position = respawnPoint.position;
            gameObject.transform.rotation = respawnPoint.rotation;

            //plays the audio once.
            audioOnImpact.PlayOneShot(fireballImpactClip);
        }
    }
}
