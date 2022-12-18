using System.Collections;
using System.Collections.Generic;
using ActionHandler;
using UnityEngine;

public class Health : MonoBehaviour
{
    /// <summary>
    /// this script is just a future feature that will allow any character to check if its dead or not
    /// also cancels the current action of the character when it dies and plays its animation.
    /// </summary>
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
