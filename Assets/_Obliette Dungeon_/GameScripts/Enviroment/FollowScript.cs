using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    //initiate variables
    public Transform target;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // This is called every Physics frames
    private void FixedUpdate()
    {
        //This moves the game objekt towords the targeted game objekt
        rb.MovePosition(target.transform.position);
    }
}
