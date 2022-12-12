using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorDissapear : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fireball"))
        {
            Invoke("DoorDissapear", 5f);
        }
    }

    void DoorDissapear()
    {
        gameObject.SetActive(false);
    }
}
