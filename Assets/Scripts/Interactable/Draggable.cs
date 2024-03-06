using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Draggable : Interactable
{
    

    public override void Interact()
    {
        
    }

    public abstract void Grab();
}
