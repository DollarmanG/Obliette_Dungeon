using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabInteractebleOskar : MonoBehaviour
{
    [SerializeField] private Transform teleportTo;
    [SerializeField] private Transform startPosistion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TeleportBack()
    {
        startPosistion.position = teleportTo.position;
        startPosistion.rotation = teleportTo.rotation;
    }
}
