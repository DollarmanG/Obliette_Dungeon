using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayFollowTarget : MonoBehaviour, ICanFollow
{
    //this reads the target that another character follows as its target
    public GameObject FollowTarget()
    {
        return this.gameObject;
    }
}
