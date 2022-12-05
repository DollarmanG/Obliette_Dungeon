using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDialogue : MonoBehaviour
{
    int count = 0;

    public void printDialogue1()
    {
        if (count == 0)
        {
            Debug.Log("Hmmm, the foundation seems cracked here.");
            count++;
        }
    }
}
