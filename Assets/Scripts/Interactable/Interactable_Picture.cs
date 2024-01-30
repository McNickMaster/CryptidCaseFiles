using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Picture : Interactable
{
    public Culprit myCulprit;
    public CauseOfDeath myCauseOfDeath;
    public Vector3 normal = Vector3.up;

    public bool culpritPhoto = true, causeOfDeathPhoto = false;

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
        rb.AddForce(25*normal);
    }

    public override void Interact()
    {
        //nothin
    }
}
