using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rockfall
{
    [RequireComponent(typeof(AudioSource))]
    public class RockfallHoverCrackingStone : MonoBehaviour
    {
        private AudioSource crackingRocks;

        [SerializeField]
        private AudioClip stateOneClip;

        [SerializeField]
        private AudioClip stateTwoClip;

        // Start is called before the first frame update
        void Start()
        {
            crackingRocks = GetComponent<AudioSource>();
        }

        public void playStateOneClip()
        {
            crackingRocks.clip = stateOneClip;
            crackingRocks.Play();
        }

        public void playStateTwoClip()
        {
            crackingRocks.clip = stateTwoClip;
            crackingRocks.Play();
        }

        public void stop()
        {
            crackingRocks.Stop();
        }
    }
}

