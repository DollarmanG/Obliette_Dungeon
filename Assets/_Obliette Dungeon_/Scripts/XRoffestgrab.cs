using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRoffestgrab : XRGrabInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        // Nedan g�r s� att om AttachTransform f�ltet i "XR Grab Interactable" �r tomt, s� skapar koden ett objekt som agerar som agerar om attachpunkten
        if (!attachTransform)
        {
            GameObject attachPoint = new GameObject("Offset Grab Pivot");
            attachPoint.transform.SetParent(transform, false);
            attachTransform = attachPoint.transform;
        }
        
    }


    // Nedan kod fixar Offsetgrabing via kod.
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        attachTransform.position = args.interactorObject.transform.position;
        attachTransform.rotation = args.interactorObject.transform.rotation;
    }
}
