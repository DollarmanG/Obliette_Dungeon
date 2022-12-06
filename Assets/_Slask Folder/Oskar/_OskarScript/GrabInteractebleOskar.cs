using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabInteractebleOskar : MonoBehaviour
{
    [SerializeField] private Transform teleportTo;
    [SerializeField] private Transform startPosistion;
    public Rigidbody rb;
    [SerializeField] private GameObject Handler;
    [SerializeField] private Rigidbody HandlerRB;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(teleportTo.position, startPosistion.position);
        if(distance > 0.4)
        {
            Handler.SetActive(false);
            Handler.SetActive(true);
        }
    }
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
