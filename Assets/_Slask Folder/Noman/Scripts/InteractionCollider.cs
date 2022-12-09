using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// OBS!!!!!! DEETTA ÄR ETT EXTRA SKRIPT, SPARAR DEN FÖR KANSKE ANVÄNDNING I FRAMTIDEN. DEN ANVÄDNER EN RAYCAST SPHERE FÖR ATT BERÄKNA DISTANS MELLAN OBJEKTET DEN SKA INTERGRERA MED OCH SIG SJÄLV
/// TRÄFFAR DEN OBJEKTET SÅ SKRIVER DEN UT MEDDELANDET.
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
