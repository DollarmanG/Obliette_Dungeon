using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioguardreactsandstoneroof : MonoBehaviour
{
    [SerializeField] private AudioSource _guard1reacts;
    [SerializeField] private AudioSource _guard2reacts;
    [SerializeField] private AudioSource _playerreactstoroof;
    private int counter = 0;


    //Detta skirpt k�r ett ljudklipp n�r man tr�ffar en triggerzon.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && counter == 0)
        {
            _playerreactstoroof.PlayOneShot(_playerreactstoroof.clip);
            _guard1reacts.PlayDelayed(3);
            _guard2reacts.PlayDelayed(8);
            counter++;
        }
    }
}
