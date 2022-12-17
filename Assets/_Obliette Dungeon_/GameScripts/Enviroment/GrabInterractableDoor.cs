using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabInterractableDoor : MonoBehaviour
{
    // This gets the positions so that teleport can work
    [SerializeField] private Transform teleportTo;
    [SerializeField] private Transform startPosistion;
    public Rigidbody rb;
    [SerializeField] private GameObject Handler;
    [SerializeField] private Rigidbody HandlerRB;
    // Start is called before the first frame update
    void Start()
    {
        // This gets the Rigid body component from the game objekt the script is attach to
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // This cheks so that if the handler is to far away the deaktivate it and reaktivate it so that the player stops holding it and it teleports back to the door handle.
        float distance = Vector3.Distance(teleportTo.position, startPosistion.position);
        if(distance > 0.4)
        {
            Handler.SetActive(false);
            Handler.SetActive(true);
        }
    }
    // This funstion teleport back to the handle and makes sure anguler velocity and velocity are set to zero.
    public void TeleportBack()
    {
        startPosistion.position = teleportTo.position;
        startPosistion.rotation = teleportTo.rotation;
        //startPosistion.localScale = teleportTo.localScale;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        HandlerRB.velocity = Vector3.zero;
        HandlerRB.angularVelocity = Vector3.zero;
    }
}
