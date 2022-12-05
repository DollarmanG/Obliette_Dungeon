using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using dialogue;

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

        bool doubleHoverHaptics;

        // Index of three possible states:
        // 1. Player has not yet caused a rockfall
        // 2. Player has caused one small rockfall
        // 3. Player has caused big rockfall
        int[] stateIndex = new int[3] {0,1,2};

        // Int to keep track of current state
        int stateCount = 0;

        // Int to keep track of how many times player has hovered over game object.
        int hoverCount = 0;

        // Start is called before the first frame update
        void Start()
        {
            doubleHoverHaptics = false;

        }

        // Update is called once per frame
        void Update()
        {
            
            //Debug.Log($"State count = {stateCount}");
        }

        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);

            if (hoverCount == 0 && stateCount < 2)
            {
                Debug.Log("Hmmm... the foundation seems cracked here.");

                Debug.Log("leftHandRayInteractor.hapticHoverEnterDuration = " + leftHandRayInteractor.hapticHoverEnterDuration);
                Debug.Log("rightHandRayInteractor.hapticHoverEnterDuration = " + rightHandRayInteractor.hapticHoverEnterDuration);
                Debug.Log("leftHandRayInteractor.hapticHoverEnterIntensity = " + leftHandRayInteractor.hapticHoverEnterIntensity);
                Debug.Log("rightHandRayInteractor.hapticHoverEnterIntensity = " + rightHandRayInteractor.hapticHoverEnterIntensity);
                hoverCount++;
            }

            if (doubleHoverHaptics && hoverCount > 0 && stateCount > 0 && stateCount < 2)
            {
                leftHandRayInteractor.hapticHoverEnterDuration = leftHandRayInteractor.hapticHoverEnterDuration * 2;
                rightHandRayInteractor.hapticHoverEnterDuration = rightHandRayInteractor.hapticHoverEnterDuration * 2;
                leftHandRayInteractor.hapticHoverEnterIntensity = leftHandRayInteractor.hapticHoverEnterIntensity * 2;
                rightHandRayInteractor.hapticHoverEnterIntensity = rightHandRayInteractor.hapticHoverEnterIntensity * 2;

                doubleHoverHaptics = false;

                Debug.Log("leftHandRayInteractor.hapticHoverEnterDuration = " + leftHandRayInteractor.hapticHoverEnterDuration);
                Debug.Log("rightHandRayInteractor.hapticHoverEnterDuration = " + rightHandRayInteractor.hapticHoverEnterDuration);
                Debug.Log("leftHandRayInteractor.hapticHoverEnterIntensity = " + leftHandRayInteractor.hapticHoverEnterIntensity);
                Debug.Log("rightHandRayInteractor.hapticHoverEnterIntensity = " + rightHandRayInteractor.hapticHoverEnterIntensity);
            }
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);

            if (stateCount == 0)
            {
                Debug.Log("You have selected the item for the first time. Switching to second state");

                leftHandRayInteractor.hapticSelectEnterDuration = 2.0f;
                rightHandRayInteractor.hapticSelectEnterDuration = 2.0f;
                leftHandRayInteractor.hapticSelectEnterIntensity = 1.0f;
                rightHandRayInteractor.hapticSelectEnterIntensity = 1.0f;

                Debug.Log("leftHandRayInteractor.hapticSelectEnterDuration = " + leftHandRayInteractor.hapticSelectEnterDuration);
                Debug.Log("rightHandRayInteractor.hapticSelectEnterDuration = " + rightHandRayInteractor.hapticSelectEnterDuration);
                Debug.Log("leftHandRayInteractor.hapticSelectEnterIntensity = " + leftHandRayInteractor.hapticSelectEnterIntensity);
                Debug.Log("rightHandRayInteractor.hapticSelectEnterIntensity = " + rightHandRayInteractor.hapticSelectEnterIntensity);

                stateCount++;
                doubleHoverHaptics = true;
            }
            else if (stateCount == 1)
            {
                Debug.Log("You have selected the item for the second time. Switching to third state and removing all selection haptics.");

                leftHandRayInteractor.hapticHoverEnterDuration = 0.0f;
                rightHandRayInteractor.hapticHoverEnterDuration = 0.0f;
                leftHandRayInteractor.hapticHoverEnterIntensity = 0.0f;
                rightHandRayInteractor.hapticHoverEnterIntensity = 0.0f;

                leftHandRayInteractor.hapticSelectEnterDuration = 0.0f;
                rightHandRayInteractor.hapticSelectEnterDuration = 0.0f;
                leftHandRayInteractor.hapticSelectEnterIntensity = 0.0f;
                rightHandRayInteractor.hapticSelectEnterIntensity = 0.0f;

                Debug.Log("leftHandRayInteractor.hapticHoverEnterDuration = " + leftHandRayInteractor.hapticHoverEnterDuration);
                Debug.Log("rightHandRayInteractor.hapticHoverEnterDuration = " + rightHandRayInteractor.hapticHoverEnterDuration);
                Debug.Log("leftHandRayInteractor.hapticHoverEnterIntensity = " + leftHandRayInteractor.hapticHoverEnterIntensity);
                Debug.Log("rightHandRayInteractor.hapticHoverEnterIntensity = " + rightHandRayInteractor.hapticHoverEnterIntensity);

                Debug.Log("leftHandRayInteractor.hapticSelectEnterDuration = " + leftHandRayInteractor.hapticSelectEnterDuration);
                Debug.Log("rightHandRayInteractor.hapticSelectEnterDuration = " + rightHandRayInteractor.hapticSelectEnterDuration);
                Debug.Log("leftHandRayInteractor.hapticSelectEnterIntensity = " + leftHandRayInteractor.hapticSelectEnterIntensity);
                Debug.Log("rightHandRayInteractor.hapticSelectEnterIntensity = " + rightHandRayInteractor.hapticSelectEnterIntensity);

                stateCount++;
            }
        }
    }
}


