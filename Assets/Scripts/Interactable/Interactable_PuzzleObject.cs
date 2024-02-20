using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_PuzzleObject : Draggable
{

    
    public Vector3 normal = Vector3.up;
    public Vector3 lockPosition = Vector3.zero;
    private Rigidbody rb;
    private Collider boxCollider;
    private Collider meshCollider;

    public bool locked = false;

    void Awake()
    {

        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        meshCollider = GetComponentInChildren<MeshCollider>();

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
            
            if(locked)
            {
                boxCollider.enabled = false;
                rb.isKinematic = true;
            } else {
                boxCollider.enabled = true;
                
                rb.AddForce(35*-normal);


                if(Vector3.Distance(transform.localPosition, lockPosition) < GameData.instance.PUZZLE_AUTO_LOCK)
                {
                    locked = true;
                    transform.localPosition = new Vector3(lockPosition.x, -2.7f, lockPosition.z);
                } else 
                {
                    locked = false;
                }
            }
        }
       // rb.velocity = -1 * normal * 25;
       
        
       
    }

    public override void Grab()
    {
        locked = false;
    }

    public void Enable()
    {
        thisEnabled = true;
        boxCollider.enabled = true;
        //meshCollider.enabled = true;
    }

    public void Disable()
    {
        thisEnabled = false;
        
        boxCollider.enabled = false;
        meshCollider.enabled = false;
    }
}
