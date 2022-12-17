using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this interface makes a character to interact with another object.
public interface IInteractable
{
    void Interact(GameObject interactor);
}
