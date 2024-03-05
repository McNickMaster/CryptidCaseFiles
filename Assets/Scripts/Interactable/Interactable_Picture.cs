using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Picture : Interactable
{
    public Culprit myCulprit;
    public CauseOfDeath myCauseOfDeath;
    public Victim myVictim;
    public Vector3 normal = Vector3.up;

//    [HideInInspector]
    public bool culpritPhoto = false, causeOfDeathPhoto = false, victimPhoto = false;

    private Rigidbody rb;

    [ExecuteInEditMode]
    void SetBools()
    {
        
        culpritPhoto = (myCulprit != Culprit.NULL);
        causeOfDeathPhoto = (myCauseOfDeath != CauseOfDeath.NULL);
        victimPhoto = (myVictim != Victim.NULL);
    }

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
}
