using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable_StickyNote : Interactable
{
    public Vector3 normal = Vector3.up;
    public Nick_InputField inputField;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.velocity = normal * 5;
    }

    public override void Interact()
    {
        //nothin
    }

    public void Disable()
    {
        inputField.enabled = false;
    }
    public void Enable()
    {
        inputField.enabled = true;
    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}
