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

        // User controllable variable to set overall speed.
        [SerializeField, Range(1, 2)]
        private float footstepMultiplier = 1;

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

            coroutine = playFootsteps(velocity);

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
        }

        private void Update()
        {
            // Calculate velocity based on difference between position in previous frame and next frame.
            // Return the magnitude between the current position and previus position (which will equal 0 at start), and divide by time of most recent frame.
            // Thus velocity is equal to displacement / frame in seconds.
            velocity = ((targetTransform.position - previous).magnitude) / Time.deltaTime;
            previous = targetTransform.position;

            //Debug.Log($"velocity = {velocity} and previous = {previous}");
            //Debug.Log($"velocity = {velocity} ");
            
            
            if (velocity == 0 && allowPlayStart == false && hasStartedOnce == false)
            {
                Debug.Log("Stop coroutine");
                isPlaying = false;
                //allowPlayStart = true;
                hasStartedOnce = true;
                Debug.Log($"isPlaying = {isPlaying} & allowPlayStart = {allowPlayStart}");
            } else if (velocity > 0 && allowPlayStart == false && isPlaying == false && hasStartedOnce == true)
            {
                isPlaying = true;
                allowPlayStart = true;
                Debug.Log($"isPlaying = {isPlaying} & allowPlayStart = {allowPlayStart}");
                //StartCoroutine(coroutine);
            }
            else if (velocity > 0 && allowPlayStart && isPlaying && hasStartedOnce == true)
            {
                Debug.Log("Start coroutine");
                allowPlayStart = false;
                hasStartedOnce = false;
                
            }


        }

        private IEnumerator playFootsteps(float waitTime)
        {
            resetPitch = 1.0f;
            pitchOffset = Random.Range(-0.2f, 0.2f);
            audioSource.pitch = resetPitch + pitchOffset;
            audioSource.clip = walkClips[Random.Range(0, walkClips.Length)];
            audioSource.PlayOneShot(audioSource.clip);
            Debug.Log("footsteps triggered");
            yield return new WaitForSeconds(waitTime);
        }
    }
}

