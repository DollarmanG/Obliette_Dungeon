using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using dialogue;
using System.Runtime.CompilerServices;

namespace rockfall
{
    [RequireComponent(typeof(XRSimpleInteractable))]
    public class Rockfall : XRSimpleInteractable
    {
        // Ref to left ray interactor.
        [SerializeField]
        private XRRayInteractor leftHandRayInteractor;

        // Ref to right ray interactor.
        [SerializeField]
        private XRRayInteractor rightHandRayInteractor;

        // References to particle systems
        [SerializeField]
        ParticleSystem debrisParticlesStateZero;

        [SerializeField]
        ParticleSystem debrisParticlesStateOne;

        [SerializeField]
        ParticleSystem dustParticleStateOne;
        
        [SerializeField]
        ParticleSystem dustParticleStateTwo;

        // Used to double hover haptic values only between the first and second time user selects rockfall.

        // Index of three possible states:
        // 1. Player has not yet caused a rockfall
        // 2. Player has caused one small rockfall
        // 3. Player has caused big rockfall
        int[] stateIndex = new int[3] {0,1,2};

        // Int to keep track of current state
        int stateCount = 0;

        // Int to keep track of how many times player has hovered over game object.
        int hoverCount = 0;

        // Declare variable of type mesh renderer.
        // Use this mesh renderer to change the material of this game object
        MeshRenderer meshRenderer;

        // Declare variable of type RockfallMaterials
        // References RockfallMaterials component attached to this game object
        // Use this to pull a new material from array of materials.
        public RockfallMaterials rockfallMaterials;

        // Start is called before the first frame update
        void Start()
        {
            // Set rockfallMeshrenderer variable equal to this game object's mesh renderer
            meshRenderer = GetComponent<MeshRenderer>();

            // Set this game object's first material equal to the first material in
            // rockfallMaterials array attached to this game object.
            meshRenderer.material = rockfallMaterials.setStateMaterial(0);

            // Disable debris particle effects at start
            debrisParticlesStateZero.Stop();
            debrisParticlesStateOne.Stop();
            dustParticleStateOne.Stop();
            dustParticleStateTwo.Stop();
        }

        // Update is called once per frame
        void Update()
        {
            
            
        }

        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);

            // The first time user hovers over rockfall, trigger dialogue and enable state zero particle effect.
            if (hoverCount == 0)
            {
                Debug.Log("Hmmm... the foundation seems cracked here.");

                // Set state zero particle effect to active.
                debrisParticlesStateZero.Play();

                // Raise hover count by one.
                hoverCount++;
            }

            // Every time user hovers, after the first hover and before first select, enable state zero particle effect.
            if (hoverCount > 0 && stateCount == 0)
            {
                debrisParticlesStateZero.Play();
            }

            if (hoverCount > 0 && stateCount == 1)
            {
                debrisParticlesStateOne.Play();
            }

        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);

            // The first time user selects rockfall, raise the value of the selction haptics, go to the next state and
            // allow the doubling of hover haptics.

            if (stateCount == 0)
            {
                Debug.Log("You have selected the item for the first time. Switching to second state");

                debrisParticlesStateZero.Stop();
                debrisParticlesStateOne.Play();

                dustParticleStateOne.Play();

                // Change to state one material.
                meshRenderer.material = rockfallMaterials.setStateMaterial(1);

                //Increase intensity of selection haptics
                leftHandRayInteractor.hapticSelectEnterDuration = 2.0f;
                rightHandRayInteractor.hapticSelectEnterDuration = 2.0f;
                leftHandRayInteractor.hapticSelectEnterIntensity = 1.0f;
                rightHandRayInteractor.hapticSelectEnterIntensity = 1.0f;

                // Double intensity of hover haptics.
                leftHandRayInteractor.hapticHoverEnterDuration = leftHandRayInteractor.hapticHoverEnterDuration * 2;
                rightHandRayInteractor.hapticHoverEnterDuration = rightHandRayInteractor.hapticHoverEnterDuration * 2;
                leftHandRayInteractor.hapticHoverEnterIntensity = leftHandRayInteractor.hapticHoverEnterIntensity * 2;
                rightHandRayInteractor.hapticHoverEnterIntensity = rightHandRayInteractor.hapticHoverEnterIntensity * 2;

                // Raise the count of states by one.
                stateCount++;
            }
            else if (stateCount == 1)
            {
                Debug.Log("You have selected the item for the second time. Switching to third state and removing all selection haptics.");

                debrisParticlesStateOne.Stop();
                dustParticleStateTwo.Play();

                // Change to state one material.
                meshRenderer.material = rockfallMaterials.setStateMaterial(2);

                // Remove all haptic feedback when interacting with rockfall (sequence complete).
                leftHandRayInteractor.hapticHoverEnterDuration = 0.0f;
                rightHandRayInteractor.hapticHoverEnterDuration = 0.0f;
                leftHandRayInteractor.hapticHoverEnterIntensity = 0.0f;
                rightHandRayInteractor.hapticHoverEnterIntensity = 0.0f;

                leftHandRayInteractor.hapticSelectEnterDuration = 0.0f;
                rightHandRayInteractor.hapticSelectEnterDuration = 0.0f;
                leftHandRayInteractor.hapticSelectEnterIntensity = 0.0f;
                rightHandRayInteractor.hapticSelectEnterIntensity = 0.0f;

                // Raise count of state by one.
                stateCount++;
            }
        }
    }
}


