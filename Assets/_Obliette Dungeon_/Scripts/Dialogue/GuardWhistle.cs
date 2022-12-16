using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dialogue
{
    [RequireComponent(typeof(AudioSource))]
    public class GuardWhistle: MonoBehaviour
    {
        // Variable for target transform
        [SerializeField]
        private Transform targetTransform;

        // Reference to target vector 3 previous position
        private Vector3 previous;

        // Reference to current position
        private Vector3 current;

        // Variable to get velocity
        private float velocity;

        // Index variable for clip arrays
        private int randomIndex;

        // Variable to set playOnAwake from script
        [SerializeField]
        private bool myPlayOnAwake = false;

        // Variable to set loop condition from script
        [SerializeField]
        private bool myLoop = true;

        // Float to set spatialization options from script
        [SerializeField]
        private float spatialization = 1;

        [SerializeField]
        private float myMaxDistance = 20;

        // Var referring to this object's audio source.
        private AudioSource audioSource;

        // Coroutine to start playback
        private IEnumerator coroutine;

        // Variable to check if playback is occurring
        private bool isPlaying;

        // Variable to allow playback of footsteps sounds
        private bool allowPlayStart;

        // Variable to prevent play at start
        private bool hasStartedOnce = false;

        // Variable to prevent multiple yells
        private int whistleCounter;

        // Start is called before the first frame update
        void Start()
        {
            // Get this object's audio source
            audioSource = GetComponent<AudioSource>();

            // Set audio source settings at start.
            audioSource.playOnAwake = myPlayOnAwake;
            audioSource.spatialBlend = spatialization;
            audioSource.maxDistance = myMaxDistance;
            myLoop = true;

            // Set variable previous equal to target transform start position
            previous = targetTransform.position;

            isPlaying = false;
            allowPlayStart = false;
            hasStartedOnce = false;

            // Set yell counter to 0 to allow first yell
            whistleCounter = 0;
        }

        private void Update()
        {
            // Calculate velocity based on difference between position in previous frame and next frame.
            // Return the magnitude between the current position and previus position (which will equal 0 at start), and divide by time of most recent frame.
            // Thus velocity is equal to displacement / frame in seconds.
            velocity = ((targetTransform.position - previous).magnitude) / Time.deltaTime;
            previous = targetTransform.position;

            //Debug.Log($"velocity = {velocity} and previous = {previous}");
            Debug.Log($"velocity = {velocity} ");


            if (velocity <= 0.5 && allowPlayStart == false && hasStartedOnce == false)
            {
                StopWhistle();
                isPlaying = false;
                hasStartedOnce = true;

            }
            else if (velocity > 0 && allowPlayStart == false && isPlaying == false && hasStartedOnce == true)
            {
                isPlaying = true;
                allowPlayStart = true;
            }
            else if (velocity > 0.5 && velocity <=1.8 && allowPlayStart && isPlaying && hasStartedOnce == true)
            {
                PlayWistle();
                allowPlayStart = false;
                hasStartedOnce = false;
            }
        }

        private void PlayWistle()
        {
            if (whistleCounter == 0)
            {
                Debug.Log($"whistle triggered velocity = {velocity}");
                audioSource.PlayDelayed(5.0f);
                whistleCounter++;
            }
        }

        private void StopWhistle()
        {
            Debug.Log("Stop whistle");
            audioSource.Stop();
            whistleCounter = 0;
        }  
    }
}

