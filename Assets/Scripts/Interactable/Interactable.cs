using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public InteractType type = InteractType.DRAGGABLE;
    public abstract void Interact();
}

public enum InteractType
{
    BUTTON, DRAGGABLE, LINE
}
