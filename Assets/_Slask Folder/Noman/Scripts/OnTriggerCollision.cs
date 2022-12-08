using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Police Hit Player");
        }
        
    }
}
