using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DebrisParticleEffectState0 : MonoBehaviour
{

    [SerializeField]
    private Transform rockfallTransform;

    private ParticleSystem particleSystemStateZero;

    Vector3 rockfallScale;

    float scaleTargetX;
    float scaleTargetY;
    float scaleTargetZ;

    // Start is called before the first frame update
    void Start()
    {
        particleSystemStateZero = GetComponent<ParticleSystem>();

        var sh = particleSystemStateZero.shape;

        Debug.Log($"rockfallTransform.localScale.x is {rockfallTransform.localScale.x}");
        Debug.Log($"rockfallTransform.localScale.y is {rockfallTransform.localScale.y}");
        Debug.Log($"rockfallTransform.localScale.z is {rockfallTransform.localScale.z}");

        scaleTargetX = rockfallTransform.localScale.x;
        scaleTargetY = rockfallTransform.localScale.y;
        scaleTargetZ = rockfallTransform.localScale.z;

        sh.scale = new Vector3(scaleTargetX, scaleTargetY, scaleTargetZ);
    }

    // Update is called once per frame
    void Update()
    {
        //particleSystem.shape.scale.Set(rockfallScale.x, rockfallScale.y, rockfallScale.z);

        Debug.Log($"Target x value is {scaleTargetX}");
        Debug.Log($"Target y value is {scaleTargetY}");
        Debug.Log($"Target z value is {scaleTargetZ}");

    }
}
