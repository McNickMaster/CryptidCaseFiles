using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tack : Interactable
{

    private DrawLine lineDrawer;
    [SerializeField]
    private Tack otherTack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(lineDrawer != null)
        {
             if(otherTack != null)
            {
        //            Debug.Log("setting line point");
                lineDrawer.SetPoint(0, transform.position);
                lineDrawer.SetPoint(1, otherTack.transform.position);
            } else if(otherTack == null && (Input.GetMouseButton(1)))
            {
                lineDrawer.SetPoint(0, transform.position);
                lineDrawer.SetPoint(1, PlayerInput.instance.GetConvertedMousePos());
                
            } else {
                lineDrawer.DestroyLine();
                //lineDrawer = null;
            }
        }


       
    }

    public override void Interact()
    {

        //nothin

    }

    public override void AltInteract()
    {
        StartDraw();
    }

    public void StartDraw()
    {
        Debug.Log("starting draw for: " + gameObject.name);
        lineDrawer = GameManager.instance.SpawnLineDrawer();
        lineDrawer.SetPoint(0, transform.position);
    }

    public void StopDrag()
    {
        CastForTackParent();
    }

    public void StopDraw()
    {
        Debug.Log("stopping line draw");
        //lineDrawer.DisableLineDraw();
        
        otherTack = PlayerInput.instance.CastForTack();

        if(otherTack != null && otherTack.gameObject.name.Equals(this.gameObject.name))
        {
            otherTack = null;
        }

    }


    
    public void CastForTackParent()
    {
        Transform parent = null;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.forward, 5);
        Debug.Log("casting for parent: " + hits.Length);

        foreach(RaycastHit hit in hits)
        {
            if(hit.transform != null && hit.transform.gameObject.layer.Equals(6))
            {
                
                parent = hit.transform.gameObject.transform;
                parent.gameObject.GetComponent<Interactable>().myTack = this;
                
                transform.parent = parent;
                transform.localPosition = new Vector3(0, -0.5f, -2.5f);
            
            } else 
            {
                
            }
            Debug.Log(hit.transform.gameObject.name);
        }

        
    }
}
