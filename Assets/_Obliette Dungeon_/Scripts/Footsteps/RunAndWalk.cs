using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace footsteps
{
    [RequireComponent(typeof(AudioSource))]
    public class RunAndWalk : MonoBehaviour
    {
        // Arrays to store variations of footsteps, walk and run
        [SerializeField]
        private AudioClip[] walkClips;
        
        [SerializeField]
        private AudioClip[] runClips;

        // Variable to set playOnAwake from script
        [SerializeField]
        private bool myPlayOnAwake = false;

        // Float to set spatialization options from script
        [SerializeField]
        private float spatialization = 1;

        [SerializeField]
        private float myMaxDistance = 20;

        // Var referring to this object's audio source.
        private AudioSource audioSource;

        //Var referring to target rigidbody
        [SerializeField]
        private Rigidbody rb;

        // Start is called before the first frame update
        void Start()
        {
            // Get this object's audio source
            audioSource = GetComponent<AudioSource>();

            audioSource.playOnAwake = myPlayOnAwake;
            audioSource.spatialBlend = spatialization;
            audioSource.maxDistance = myMaxDistance;
        }

        private void FixedUpdate()
        {
            Debug.Log(rb.velocity);
        }
    }
}

