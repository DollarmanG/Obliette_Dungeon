using System.Collections;
using System.Collections.Generic;
using ActionHandler;
using UnityEngine;

public class Health : MonoBehaviour
{

    private bool isDead = false;


    public bool IsDead()
    {
        return isDead;
    }


    private void Die()
    {
        if (isDead) return;

        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
        GetComponent<ActionScheduler>().CancelCurrentAction();
    }
}
