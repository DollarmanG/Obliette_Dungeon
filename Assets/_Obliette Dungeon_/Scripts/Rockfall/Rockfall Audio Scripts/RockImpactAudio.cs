using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rockfall
{
    public class RockImpactAudio : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] smallRockImpacts;

        [SerializeField]
        private AudioClip[] largeRockImpacts;

        private AudioSource rockImpact;

        public bool playerHasSelectedOnce;


        // Start is called before the first frame update
        void Start()
        {
            //playerHasSelectedOnce = false;
            rockImpact = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (playerHasSelectedOnce == false && collision.relativeVelocity.magnitude > 3)
            {
                playSmallImpactSound();
            }
            else if (playerHasSelectedOnce == true && collision.relativeVelocity.magnitude > 3)
            {
                playLargeImpactSound();
            }
        }

        private void playSmallImpactSound()
        {
            rockImpact.clip = smallRockImpacts[Random.Range(0, smallRockImpacts.Length)];
            rockImpact.PlayOneShot(rockImpact.clip);
            Debug.Log(rockImpact.clip);
        }

        private void playLargeImpactSound()
        {
            rockImpact.clip = largeRockImpacts[Random.Range(0, largeRockImpacts.Length)];
            rockImpact.PlayOneShot(rockImpact.clip);
            Debug.Log(rockImpact.clip);
        }

    }
}

