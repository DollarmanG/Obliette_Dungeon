using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayFollowTarget : MonoBehaviour, ICanFollow
{
    public GameObject FollowTarget()
    {
        return this.gameObject;
    }
}
