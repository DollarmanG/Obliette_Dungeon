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

        }

        // Update is called once per frame
        void Update()
        {
            
            
        }

        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);

            // The first time user hovers over rockfall, trigger dialogue.
            if (hoverCount == 0)
            {
                Debug.Log("Hmmm... the foundation seems cracked here.");

                hoverCount++;
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

                meshRenderer.material = rockfallMaterials.setStateMaterial(1);

                leftHandRayInteractor.hapticSelectEnterDuration = 2.0f;
                rightHandRayInteractor.hapticSelectEnterDuration = 2.0f;
                leftHandRayInteractor.hapticSelectEnterIntensity = 1.0f;
                rightHandRayInteractor.hapticSelectEnterIntensity = 1.0f;

                leftHandRayInteractor.hapticHoverEnterDuration = leftHandRayInteractor.hapticHoverEnterDuration * 2;
                rightHandRayInteractor.hapticHoverEnterDuration = rightHandRayInteractor.hapticHoverEnterDuration * 2;
                leftHandRayInteractor.hapticHoverEnterIntensity = leftHandRayInteractor.hapticHoverEnterIntensity * 2;
                rightHandRayInteractor.hapticHoverEnterIntensity = rightHandRayInteractor.hapticHoverEnterIntensity * 2;

                stateCount++;
            }
            else if (stateCount == 1)
            {
                Debug.Log("You have selected the item for the second time. Switching to third state and removing all selection haptics.");

                meshRenderer.material = rockfallMaterials.setStateMaterial(2);

                leftHandRayInteractor.hapticHoverEnterDuration = 0.0f;
                rightHandRayInteractor.hapticHoverEnterDuration = 0.0f;
                leftHandRayInteractor.hapticHoverEnterIntensity = 0.0f;
                rightHandRayInteractor.hapticHoverEnterIntensity = 0.0f;

                leftHandRayInteractor.hapticSelectEnterDuration = 0.0f;
                rightHandRayInteractor.hapticSelectEnterDuration = 0.0f;
                leftHandRayInteractor.hapticSelectEnterIntensity = 0.0f;
                rightHandRayInteractor.hapticSelectEnterIntensity = 0.0f;

                stateCount++;
            }
        }
    }
}


