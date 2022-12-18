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

        // Variable to reset pitch of this game object
        private float pitchReset;

        // Variable to alter pitch by a random amount
        private float pitchRandomizer;

        // Variables to reset volume;
        private float stateOneVolumeReset;
        private float stateTwoVolumeReset;

        // Variable to randomize volume
        private float randomizeVolume;


        // Start is called before the first frame update
        void Start()
        {

            //playerHasSelectedOnce = false;
            rockImpact = GetComponent<AudioSource>();

        }

        private void OnCollisionEnter(Collision collision)
        {

            if (collision.relativeVelocity.magnitude > 0.5)
            {
                playSmallImpactSound();
                playLargeImpactSound();
            }
            //else if (collision.relativeVelocity.magnitude >= 0.1)
            //{
            //    playLargeImpactSound();
            //}
        }

        private void playSmallImpactSound()
        {
            rockImpact.clip = smallRockImpacts[Random.Range(0, smallRockImpacts.Length)];
            // Set the variable pitch equal to the start pitch of this audio source
            pitchReset = 1.0f;
            pitchRandomizer = Random.Range(-0.5f, 0.5f);
            rockImpact.pitch = pitchReset + pitchRandomizer;
            stateOneVolumeReset = 0.2f;
            randomizeVolume = Random.Range(-0.199f, -0.05f);
            rockImpact.volume = stateOneVolumeReset + randomizeVolume;
            rockImpact.PlayOneShot(rockImpact.clip);
        }

        private void playLargeImpactSound()
        {
            rockImpact.clip = largeRockImpacts[Random.Range(0, largeRockImpacts.Length)];
            // Set the variable pitch equal to the start pitch of this audio source
            pitchReset = 1.0f;
            pitchRandomizer = Random.Range(-0.5f, 0.5f);
            rockImpact.pitch = pitchReset + pitchRandomizer;
            stateOneVolumeReset = 1.0f;
            randomizeVolume = Random.Range(-0.3f, 0.1f);
            rockImpact.volume = stateOneVolumeReset + randomizeVolume;
            rockImpact.PlayOneShot(rockImpact.clip);
        }

    }
}

