using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tack : Interactable
{


    [SerializeField]
    private Tack otherTack;


    List<DrawLine> linesFromThis = new List<DrawLine>();
    public List<DrawLine> linesToThis = new List<DrawLine>();
    private DrawLine currentLineDrawer;
    [SerializeField]
    private bool drawing = false, dragging = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        Debug.Log("i live!");
        dragging = true;
    }

    // Update is called once per frame
    void Update()
    {
/*
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
        */

        if(dragging)
        {
            transform.position = PlayerInput.instance.GetConvertedMousePos() + new Vector3(0,0,-1f);

            if(Input.GetMouseButtonDown(0))
            {
                Interact();
            }

        } else 
        {
            if(drawing)
            {
                currentLineDrawer.SetPoint(0, transform.position);
                currentLineDrawer.SetPoint(1, PlayerInput.instance.GetConvertedMousePos());
            } else 
            {
                
            }
            
            foreach(DrawLine line in linesFromThis)
            {
                if(line != null)
                {
                    line.SetPoint(0, transform.position);
                }
                
            }
            foreach(DrawLine line in linesToThis)
            {
                if(line != null)
                {
                    line.SetPoint(1, transform.position);
                }
            }
        }

        


       
    }

    public override void Interact()
    {

        //nothin
        StopDrag();
        Debug.Log("drop tack");

    }

    public override void AltInteract()
    {
        StartDraw();
    }

    public void StartDraw()
    {
//        Debug.Log("starting draw for: " + gameObject.name);
        currentLineDrawer = GameManager.instance.SpawnLineDrawer();
        currentLineDrawer.SetPoint(0, transform.position);
        drawing = true;
        
    }

    public void StopDraw()
    {
        //Debug.Log("stopping line draw");
        //lineDrawer.DisableLineDraw();
        
        otherTack = PlayerInput.instance.CastForTack();

        if(otherTack != null){
           if(otherTack.gameObject.GetInstanceID().Equals(this.gameObject.GetInstanceID()))
           {  
                otherTack = null;
                currentLineDrawer.DestroyLine();
           } else 
           {
                otherTack.linesToThis.Add(currentLineDrawer);
                linesFromThis.Add(currentLineDrawer);
                currentLineDrawer.DisableLineDraw();
           }
        } else 
        {
            
            currentLineDrawer.DestroyLine();
        }

        
        currentLineDrawer = null;
        drawing = false;

    }
    
    public void StopDrag()
    {
        CastForTackParent();
        
        dragging = false;
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
                if(parent.gameObject.GetComponent<Interactable>().myTack == null)
                {
                    parent.gameObject.GetComponent<Interactable>().myTack = this;
                } else 
                {
                    //dont put anything here
                }
                
                
                transform.parent = parent;
                transform.localPosition = new Vector3(0, -0.5f, -2.5f);
            
            } else 
            {
                
            }
            //Debug.Log(hit.transform.gameObject.name);
        }

        if(parent == null)
        {
            Destroy(this.gameObject);
        }
    }
}
