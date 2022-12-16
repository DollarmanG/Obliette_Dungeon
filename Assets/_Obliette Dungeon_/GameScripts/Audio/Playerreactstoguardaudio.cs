using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerreactstoguardaudio : MonoBehaviour
{
    [SerializeField] private AudioSource _playerreactstoguard;
    private int counter = 0;


    //Detta skirpt k�r ett ljudklipp n�r man tr�ffar en triggerzon.
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && counter == 0)
        {

            _playerreactstoguard.PlayOneShot(_playerreactstoguard.clip);
            counter++;
        }
    }
}
