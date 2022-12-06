using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DebrisParticleEffectState0 : MonoBehaviour
{

    [SerializeField]
    private Transform rockfallTransform;

    private ParticleSystem particleSystem;

    Vector3 rockfallScale;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();

        

        rockfallScale.x = rockfallTransform.localScale.x;
        rockfallScale.y = rockfallTransform.localScale.y;
        rockfallScale.z = rockfallTransform.localScale.z;

        particleSystem.shape.scale.Equals(rockfallTransform.localScale);
    }

    // Update is called once per frame
    void Update()
    {
        particleSystem.shape.scale.Set(rockfallScale.x, rockfallScale.y, rockfallScale.z);

        Debug.Log(rockfallTransform.localScale.x);
        Debug.Log(rockfallTransform.localScale.y);
        Debug.Log(rockfallTransform.localScale.z);
    }
}
