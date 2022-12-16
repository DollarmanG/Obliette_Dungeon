using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


namespace footsteps
{
    [RequireComponent(typeof(AudioSource))]
    public class RunAndWalk : MonoBehaviour
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

        // Arrays to store variations of footsteps, walk and run
        [SerializeField]
        private AudioClip[] walkClips;
        
        [SerializeField]
        private AudioClip[] runClips;

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

        // Var to reset pitch each time new footstep is triggered
        private float resetPitch = 1;

        // Var to randomize pitch with offset
        private float pitchOffset;

        // Var to set time between each footstep
        private float footstepTimer = 1.0f;

        // Variable to set overall speed.
        [SerializeField, Range(1.0f, 2.0f)]
        private float walkingFootstepMultiplier = 1;

        // Variable to set overall speed.
        [SerializeField, Range(0.1f, 2.0f)]
        private float runningFootstepMultiplier = 1;

        // Coroutine to start playback
        private IEnumerator coroutine;

        // Variable to check if playback is occurring
        private bool isPlaying;

        // Variable to allow playback of footsteps sounds
        private bool allowPlayStart;

        // Variable to prevent play at start
        private bool hasStartedOnce = false;

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
        }
            
        private void Update()
        {
            // Calculate velocity based on difference between position in previous frame and next frame.
            // Return the magnitude between the current position and previus position (which will equal 0 at start), and divide by time of most recent frame.
            // Thus velocity is equal to displacement / frame in seconds.
            velocity = ((targetTransform.position - previous).magnitude) / Time.deltaTime;
            previous = targetTransform.position;

            // If velocity is zero (i.e. player is stopped), and playback is not allowed
            // to start, stop coroutine that plays footstep sounds, and set isPlaying to false (because playback is stopped).
            // hasStarted is set to true so that audio now can play (after making sure that it has stopped at the start of the frame update).
            if (velocity == 0 && allowPlayStart == false && hasStartedOnce == false)
            {
                StopCoroutine(playFootsteps(velocity));
                isPlaying = false;
                hasStartedOnce = true;
            }

            // If velocity is greater than zero, playback is not allowed to start, audio is not playing, 
            // set allowPlayStart to true to reflect that audio can now start playing,
            // and set isPlaying to true to reflect that audio will start playing.

            else if (velocity > 0 && allowPlayStart == false && isPlaying == false && hasStartedOnce == true)
            {
                isPlaying = true;
                allowPlayStart = true;
            }

            // If velocity is greater than zero and playback is now allowed to start, isPlaying is true (to reflect that audio will
            // now start playing), and hasStartedOnce is true (since audio playback condition has been met), start coroutine to play sound,
            // set allowPlayStart to false (since audio clip should not be triggered again), and set hasStartedOnce to false so that Stop
            // is called at the start of the next frame update where the velocity is less than the minimum walk speed.

            else if (velocity > 0 && allowPlayStart && isPlaying && hasStartedOnce == true)
            {
                StartCoroutine(playFootsteps(velocity));
                allowPlayStart = false;
                hasStartedOnce = false;
            }
        }

        private IEnumerator playFootsteps(float waitTime)
        {
            // While guard is walking, pull a random walking footstep clip and play it once with a
            // random pitch. This is done at regular intervals based on the formula velocity times
            // a user controlled multiplier (used to match footstep animation to sound).
            while (velocity > 0.01f && velocity < 1.71f )
            {
                resetPitch = 1.0f;
                pitchOffset = Random.Range(-0.2f, 0.2f);
                audioSource.pitch = resetPitch + pitchOffset;
                audioSource.clip = walkClips[Random.Range(0, walkClips.Length)];
                audioSource.PlayOneShot(audioSource.clip);
                yield return new WaitForSeconds(waitTime * walkingFootstepMultiplier);
            }

            // While guard is running, pull a random running footstep clip and play it once with a
            // random pitch. This is done at regular intervals based on the formula velocity times
            // a user controlled multiplier (used to match footstep animation to sound).
            while (velocity >= 1.71f)
            {
                resetPitch = 1.0f;
                pitchOffset = Random.Range(-0.2f, 0.2f);
                audioSource.pitch = resetPitch + pitchOffset;
                audioSource.clip = runClips[Random.Range(0, walkClips.Length)];
                audioSource.PlayOneShot(audioSource.clip);
                yield return new WaitForSeconds(waitTime * runningFootstepMultiplier);
            }
        }
    }
}

