using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rockfall
{
    [RequireComponent(typeof(ParticleSystem))]
    public class DustParticleEffectState2 : MonoBehaviour
    {
        // Reference to the transform of the rockfall game object.
        [SerializeField]
        private Transform rockfallTransform;

        // Variables to hold the z, y, and z scale values of the rockfall transform.
        // These function as a 'target' value to feed into the scale of the particle system.
        float scaleTargetX;
        float scaleTargetY;
        float scaleTargetZ;

        // A particle system variable used to access this game object's particle system.
        private ParticleSystem particleSystemStateOne;

        // Start is called before the first frame update
        void Start()
        {
            // Set the x, y and z target variables equal to the scale of the rockfall game object transform x, y and z values.
            scaleTargetX = rockfallTransform.localScale.x;
            scaleTargetY = rockfallTransform.localScale.y;
            scaleTargetZ = rockfallTransform.localScale.z;

            // Set particle system variable equal to this game object's particle system.
            particleSystemStateOne = GetComponent<ParticleSystem>();

            // Variable referring to the shape module of the particle system attached to this game object.
            var sh = particleSystemStateOne.shape;

            // Set the x, y, and z values of the scale module of the particle system attached to this game object
            // equal to the x, y, and z values of the scale of the rockfall game object transform.
            sh.scale = new Vector3(scaleTargetX, scaleTargetY, scaleTargetZ);
        }
    }
}


