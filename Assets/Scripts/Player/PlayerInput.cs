using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    //public Camera cam;
    public GameObject objectInteracted;
    public Transform backPlane;
    private GameObject hoveredObject;
    public GameObject notebookObj, mapObj;

    public Interactable interacted;

    public Vector3 mousePosition3D, objectOffset = Vector3.zero;
    private Vector3 lastMousePos;

    private bool _dragging = false, inUI = false;

    private Vector3 mousePosOnClick = Vector3.zero;
    private Notes noteInteracted;
    private Interactable_PuzzleObject puzzleInteracted;


    [Header("Controls Config")]
    public KeyCode PAUSE_MENU = KeyCode.Escape;
    public KeyCode NOTEBOOK_OPEN = KeyCode.Tab, MAP_OPEN = KeyCode.M;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        UpdateBackplane();
    }

    // Update is called once per frame
    void Update()
    {
        inUI = mapObj.activeSelf || notebookObj.activeSelf;
        if(inUI)
        {

        } else 
        {
            HandleMouse();
        }
        HandleKeys();
    }

    void HandleKeys()
    {
/*
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.instance.SetView(0);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.instance.SetView(1);
        }
        */


        if(Input.GetKeyDown(PAUSE_MENU))
        {
            Application.Quit();
        }

        if(Input.GetKeyDown(NOTEBOOK_OPEN) && !mapObj.activeSelf)
        {
            ToggleNotebook();
        }

        if(Input.GetKeyDown(MAP_OPEN) && !notebookObj.activeSelf)
        {
            ToggleMap();
        }
        
    }

    void HandleMouse()
    {

        if(Input.GetMouseButtonDown(0))
        {
            MouseClick();
            _dragging = true;
        }


        if(Input.GetMouseButtonDown(1))
        {
            MouseRightClick();
        }
        

        if(Input.GetMouseButtonUp(0))
        {
            _dragging = false;
            if(objectInteracted != null)
            {
                StopDrag();
            }
            
            interacted = null;
        }
        
        if(_dragging)
        {
            MouseDrag();
        }



        SendCastForHover();
    }


    public void MouseClick()
    {
        SendCastForInteract();

        mousePosOnClick = GetConvertedMousePos();
    }

    public void MouseRightClick()
    {
        SendCastForAltInteract();

    }

    public void MouseDrag()
    {
        if(objectInteracted != null)
        { 
            
            //objectInteracted.transform.position = mousePosition3D;
            SetObjectToBounds();
            objectInteracted.transform.position = mousePosition3D + objectOffset;

            if(noteInteracted!=null)
            {
                noteInteracted.Disable();
            }
            if(puzzleInteracted!=null)
            {
                puzzleInteracted.Disable();
            }

            objectInteracted.GetComponentInChildren<Collider>().enabled = false;

        }
        
    }

    private void StopDrag()
    {
        SetObjectToBounds();

        //objectInteracted.GetComponent<Rigidbody>().AddForce(100*Vector3.forward);
        objectInteracted.GetComponentInChildren<Collider>().enabled = true;
        if(noteInteracted!=null)
        {
            noteInteracted.Enable();
        }
        if(puzzleInteracted!=null)
        {
            puzzleInteracted.Enable();
        }
        //objectInteracted.layer = LayerMask.GetMask("IgnoreRaycast");
        objectInteracted = null;
    }

    void SetObjectToBounds()
    {
        Vector3 newPos = GameManager.instance.currentView.InBounds(objectInteracted.transform.position);
        if(newPos != Vector3.forward * 7571)
        {
            objectInteracted.transform.position = newPos;
        }
    }

    public void UpdateBackplane()
    {
        backPlane = GameObject.FindWithTag("Backplane").transform;
    }

    void ToggleNotebook()
    {   
        notebookObj.SetActive(!notebookObj.activeSelf);

        //inUI = inUI || notebookObj.active;
    }
    void ToggleMap()
    {   
        mapObj.SetActive(!mapObj.activeSelf);

        //inUI = inUI || mapObj.active;
    }

    private void SendCastForInteract()
    {
        RaycastHit hit = SendCast();
        if(hit.transform != null)
        {
        
            interacted = hit.transform.GetComponent<Interactable>();

            if(interacted != null)
            {
                //Debug.Log(interacted.gameObject.name);

                switch(interacted.type)
                {
                    case (InteractType.DRAGGABLE):
                    {
                        objectInteracted = interacted.gameObject.gameObject;
                        noteInteracted = interacted.GetComponent<Notes>();
                        puzzleInteracted = interacted.GetComponent<Interactable_PuzzleObject>();
                        interacted.GetComponent<Draggable>().Grab();
                        objectOffset = CalcOffset();

                        break;
                    }

                    case (InteractType.BUTTON):
                    {   
                        interacted.Interact();
                        break;
                    }

                    
                }
            }

        }
    }

    private void SendCastForAltInteract()
    {
        RaycastHit hit = SendCast();
        if(hit.transform != null)
        {
        
            interacted = hit.transform.GetComponent<Interactable>();

            if(interacted != null)
            {
                Debug.Log(interacted.gameObject.name);
                switch(interacted.type)
                {

                    case (InteractType.LINE):
                    {
                        interacted.Interact();
                        break;
                    }
                }
            }
        }
    }

    private void SendCastForHover()
    {
        RaycastHit hit = SendCast();
        if(hit.transform != null && !_dragging)
        {
            interacted = hit.transform.GetComponent<Interactable>();

            if(interacted != null)
            {
                switch(interacted.type)
                {
                    case (InteractType.DRAGGABLE):
                    {
                    
                    
                        break;
                    }   
                
                    case (InteractType.BUTTON):
                    {
                        if(Input.GetMouseButton(0))
                        {
                            hoveredObject = interacted.gameObject;
                            interacted.GetComponent<Interactable_Button>().selected = true;
                            interacted.GetComponent<Interactable_Button>().hovering = false;
                        } else {
                            
                            hoveredObject = interacted.gameObject;
                            interacted.GetComponent<Interactable_Button>().selected = false;
                            interacted.GetComponent<Interactable_Button>().hovering = true;
                        }
                        break;
                    }
                }
            } 
            
            if((interacted == null && hoveredObject != null) || (interacted != null && hoveredObject != null && !interacted.gameObject.name.Equals(hoveredObject.name)))
            {  
                hoveredObject.GetComponent<Interactable_Button>().hovering = false;
                hoveredObject = null;

            }
            

        }
        

        


        
    }
        

    private RaycastHit SendCast()
    {

        Ray ray = GameManager.instance.currentView.myCamera.ScreenPointToRay(Input.mousePosition);
        //Vector3 clampedPoint;
        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.green);

        RaycastHit hitData;
        if(Physics.Raycast(ray, out hitData, 1000))
        {
            if(GameManager.instance.currentView.normal == Vector3.up)
            {
                mousePosition3D = hitData.point;
            } else 
            {   
                
                mousePosition3D = GameManager.instance.currentView.InBounds(hitData.point);
            }
            
             
            /*
            
            */
            lastMousePos = mousePosition3D;
            
        } else 
        {
            mousePosition3D = lastMousePos;
        }

        

        return hitData;
    }


    public Vector3 CalcOffset()
    {

        //this works for forward, but not for up
        Vector3 offset;

        offset = new Vector3(objectInteracted.transform.position.x - mousePosition3D.x, objectInteracted.transform.position.y - mousePosition3D.y, objectInteracted.transform.position.z - mousePosition3D.z);
//        Debug.Log(objectInteracted.transform.position.z + " " + mousePosition3D.z + " offset " + offset.z);
//        Debug.DrawLine(objectInteracted.transform.position, mousePosition3D, Color.yellow, 10);



        offset += GameManager.instance.currentView.normal * 0.75f;
        
       // Debug.Log(normalMask);

        //offset = new Vector3(-offset.x * normalMask.x, -offset.y * normalMask.y, offset.z * normalMask.z);

        


        return offset;
    }


    public Vector3 GetConvertedMousePos()
    {
        View myView = GameManager.instance.currentView;
        
        
        Vector3 pos = mousePosition3D;

//        Debug.Log(myView.myCamera.transform.position + " " + pos);
        Debug.DrawLine(myView.myCamera.transform.position, pos, Color.magenta);

        return pos;
    }

     public Vector3 GetConvertedMousePos_Old()
    {
        View myView = GameManager.instance.currentView;
        //Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        //Vector2 mousePos = Input.mousePosition;
        Vector3 mousePos = myView.myCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -myView.myCamera.transform.position.z + myView.myBackPlane.position.z));

        Debug.DrawLine(myView.myCamera.transform.position, mousePos, Color.magenta);
        
        
        Vector3 pos = Vector3.zero;

        if(myView.normal == Vector3.forward)
        {
            pos = new Vector3(mousePos.x, mousePos.y, myView.myBackPlane.position.z - 1.5f);
            Debug.Log("using forward math");
        } else if(myView.normal == Vector3.down)
        {
            pos = new Vector3(mousePos.x, myView.myBackPlane.position.z - 1.5f, mousePos.y);
//            Debug.Log("using down math");
        }

       // mousePosition3D = pos;

        return mousePosition3D;
    }

}
