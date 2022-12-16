using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace audioCollisions 
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioCollisionsSingle : MonoBehaviour
    {
        private AudioSource audioSource;
        
        
        private float randomPitch;


        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.spatialBlend = 1.0f;
            audioSource.maxDistance = 10.0f;
            audioSource.playOnAwake = false;

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.relativeVelocity.magnitude > 2)
            {
                audioSource.pitch = 1.0f;
                randomPitch = Random.Range(-0.5f, 0.5f);
                audioSource.pitch = audioSource.pitch + randomPitch;
                audioSource.PlayOneShot(audioSource.clip);
            }

            
        }
    }
}

