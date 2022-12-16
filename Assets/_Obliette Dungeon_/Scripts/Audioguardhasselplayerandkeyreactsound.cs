using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioguardhasselplayerandkeyreactsound : MonoBehaviour
{
    [SerializeField] private AudioSource _guard1reacts;
    [SerializeField] private AudioSource _playerreactstokey;
    private int counter = 0;


    //Detta skirpt kör ett ljudklipp när man träffar en triggerzon.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && counter == 0)
        {
            _guard1reacts.PlayOneShot(_guard1reacts.clip);
            _playerreactstokey.PlayDelayed(11);
            counter++;
        }
    }
}
