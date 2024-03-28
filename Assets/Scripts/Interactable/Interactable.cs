using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public InteractType type = InteractType.DRAGGABLE;
    [HideInInspector]
    public bool thisEnabled = true;
    public Tack myTack;
    
    public abstract void Interact();
    public abstract void AltInteract();
}

public enum InteractType
{
    BUTTON, DRAGGABLE, LINE
}
