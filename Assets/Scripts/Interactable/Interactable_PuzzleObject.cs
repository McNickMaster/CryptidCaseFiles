using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_PuzzleObject : Draggable
{

    
    public Vector3 normal = Vector3.up;
    private Rigidbody rb;


    void Awake()
    {

        rb = GetComponent<Rigidbody>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(thisEnabled)
        {
            rb.AddForce(25*-normal);
        }
       // rb.velocity = -1 * normal * 25;
    }

    public void Enable()
    {
        thisEnabled = true;
    }

    public void Disable()
    {
        thisEnabled = false;
    }
}
