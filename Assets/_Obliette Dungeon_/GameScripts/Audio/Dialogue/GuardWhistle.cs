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

            // Start conditions, audio is not playing and audio is not allowed to start.
            isPlaying = false;
            allowPlayStart = false;

            // This condition is used to make sure that only the stop condition is valid at
            // the start of each frame update.
            hasStartedOnce = false;

            // Counter used to prevent triggering audio clip multiple times.
            whistleCounter = 0;
        }

        private void Update()
        {
            // Calculate velocity based on difference between position in previous frame and next frame.
            // Return the magnitude between the current position and previus position (which will equal 0 at start), and divide by time of most recent frame.
            // Thus velocity is equal to displacement / frame in seconds.
            velocity = ((targetTransform.position - previous).magnitude) / Time.deltaTime;
            previous = targetTransform.position;

            // If velocity is less than minimum walk speed, or greater than max walk speed, and playback is not allowed
            // to start, stop whistling, set isPlaying to false (because playback is stopped).
            // hasStarted is set to true so that audio now can play (after making sure that it has stopped at the start of the frame update).
            if ((velocity <= 0.5 || velocity > 1.8f) && allowPlayStart == false && hasStartedOnce == false)
            {
                StopWhistle();
                isPlaying = false;
                hasStartedOnce = true;
            }

            // If velocity is greater than zero, playback is not allowed to start, audio is not playing, 
            // set allowPlayStart to true to reflect that audio can now start playing,
            //  and set isPlaying to true to reflect that audio will start playing.

            else if (velocity > 0 && allowPlayStart == false && isPlaying == false && hasStartedOnce == true)
            {
                isPlaying = true;
                allowPlayStart = true;
            }

            // If velocity is greater than minimum walk speed and less than or equal to max walk speed, and playback is now allowed
            // to start, isPlaying is true (to reflect that audio will now start playing), and hasStartedOnce is true (since audio playback condition
            // has been met), play whistle sound, set allowPlayStart to false (since audio clip should not be triggered again), and
            // set hasStartedOnce to false so that Stop is called at the start of the next frame update where the velocity is less 
            // than the minimum walk speed.

            else if (velocity > 0.5 && velocity <=1.8 && allowPlayStart && isPlaying && hasStartedOnce == true)
            {
                PlayWhistle();
                allowPlayStart = false;
                hasStartedOnce = false;
            }
        }

        
        private void PlayWhistle()
        {
            // If the counter is equal to zero (i.e. sound has not been triggered since the last time
            // the stop condition was met), play the sound after a five second delay, and increase the 
            // counter by one so that the sound is not triggered for each frame update where the target
            // velocity has the correct speed.
            if (whistleCounter == 0)
            {
                audioSource.PlayDelayed(5.0f);
                whistleCounter++;
            }
        }

        private void StopWhistle()
        {
            // Stop playback of the audio source, and set the whistleCounter to 0 so that playback can be
            // triggered again the next time the playback condition is met.
            audioSource.Stop();
            whistleCounter = 0;
        }  
    }
}

