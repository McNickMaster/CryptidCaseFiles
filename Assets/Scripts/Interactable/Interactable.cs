using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public InteractType type = InteractType.DRAGGABLE;
    [HideInInspector]
    public bool thisEnabled = true;
    
    public abstract void Interact();
}

public enum InteractType
{
    BUTTON, DRAGGABLE, LINE
}
