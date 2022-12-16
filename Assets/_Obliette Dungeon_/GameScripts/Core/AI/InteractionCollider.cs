using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// OBS!!!!!! DEETTA �R ETT EXTRA SKRIPT, SPARAR DEN F�R KANSKE ANV�NDNING I FRAMTIDEN. DEN ANV�DNER EN RAYCAST SPHERE F�R ATT BER�KNA DISTANS MELLAN OBJEKTET DEN SKA INTERGRERA MED OCH SIG SJ�LV
/// TR�FFAR DEN OBJEKTET S� SKRIVER DEN UT MEDDELANDET.
/// </summary>

public class InteractionCollider : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private Vector3 offsetSphere;
    [SerializeField] private LayerMask collisionMask;
    private Ray ray;

    void Update()
    {
        RaycastSphere();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + offsetSphere, interactionDistance);
    }

    public bool RaycastSphere()
    {
        RaycastHit hitInfo;
        ray = new Ray(transform.position + offsetSphere, transform.forward);
        if (Physics.SphereCast(ray, interactionDistance, out hitInfo))
        {
            if (IsInLayerMask(hitInfo.collider.gameObject, collisionMask))
            {
                Debug.Log("Police Hit Player2");
            }
        }

        return false;
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return (layerMask.value & (1 << obj.layer)) > 0;
    }
}
