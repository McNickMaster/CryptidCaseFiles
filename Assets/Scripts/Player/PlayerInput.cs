using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    public Camera cam;
    public GameObject objectInteracted;
    private GameObject hoveredObject;

    public Interactable interacted;

    public Vector3 mousePosition, objectOffset = Vector3.zero;

    private bool _dragging = false;

    private Vector3 mousePosOnClick = Vector3.zero;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MouseClick();
            _dragging = true;
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

        mousePosition = GetConvertedMousePos();


        SendCastForHover();

    }


    public void MouseClick()
    {
        SendCastForInteract();

        mousePosOnClick = GetConvertedMousePos();
    }

    public void MouseDrag()
    {
        if(objectInteracted != null)
        { 
            objectInteracted.transform.position = GetConvertedMousePos() + objectOffset;

        }
        
    }

    private void StopDrag()
    {
        objectInteracted.GetComponent<Rigidbody>().AddForce(100*Vector3.forward);
        objectInteracted = null;
    }

    private void SendCastForInteract()
    {
        //add a null check to this, it errors when sent out into the void
        interacted = SendCast().transform.GetComponent<Interactable>();

        if(interacted != null)
        {
            switch(interacted.type)
            {
                case (InteractType.DRAGGABLE):
                {
                    objectInteracted = interacted.gameObject.gameObject;
                    objectOffset =  new Vector2(objectInteracted.transform.position.x - GetConvertedMousePos().x, objectInteracted.transform.position.y - GetConvertedMousePos().y);
                    
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

    private void SendCastForHover()
    {
        RaycastHit hit = SendCast();
        if(hit.transform != null)
        {
            interacted = SendCast().transform.GetComponent<Interactable>();

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

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        if(Physics.Raycast(ray, out hitData, 1000))
        {

//            Debug.Log(hitData.transform.gameObject.name);
            
        }

        return hitData;
    }

    public Vector3 GetConvertedMousePos()
    {
        //Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        //Vector2 mousePos = Input.mousePosition;
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
        return new Vector3(mousePos.x, mousePos.y, -0.5f);
    }

    public Vector3 GetConvertedMousePos(float camZ)
    {
        //Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        //Vector2 mousePos = Input.mousePosition;
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camZ));
        return new Vector3(mousePos.x, mousePos.y, -0.5f);
    }







}
