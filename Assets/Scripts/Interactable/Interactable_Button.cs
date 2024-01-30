using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable_Button : Interactable
{

    public ButtonType btn_type = ButtonType.SPAWN;
    public bool is3D = false;
    public Color normalTint = Color.white;
    public Color hoverTint = new Color(0.9f, 0.9f, 0.9f);
    public Color selectTint = new Color(0.8f, 0.8f, 0.8f);
    private Image btn_image;
    private Material mat;
    [HideInInspector]
    public bool hovering = false;
    [HideInInspector]
    public bool selected = false;

    [Header("Travel Button Settings")]
    public GameObject destination;
    public GameObject thisLocation;
    public Cutscene travelCutscene;

    [Header("Spawn Button Settings")]
    public GameObject noteToSpawn;
    public Transform noteSpawnPoint;
    private GameObject noteSpawned;
    private bool spawnedNote;

    [Header("Page Button Settings")]
    public bool pageButtonLeft = false;

    [Header("Dialogue Button Settings")]
    public DialogueScriptable dialogue;

    [Header("Toggle Button Settings")]
    public GameObject objectToToggle;
    public bool state = false;

    private DrawLine lineDrawer;


    // Start is called before the first frame update
    void Awake()
    {
        

        if(is3D)
        {            
            GetMaterialInstance();
        } else 
        {
            btn_image = GetComponent<Image>();
        }


        switch(btn_type)
        {
            case ButtonType.DIALOGUE:
            {
                dialogue.Init();
                break;
            }

            case ButtonType.TOGGLE:
            {
                objectToToggle.SetActive(state);
                break;
            }
        }
        


        ResetButtonTint();
    }

    // Update is called once per frame
    void Update()
    {

        
        //this is fine for now, but should be event based later
        if(Input.GetMouseButtonUp(0))
        {
            ResetButtonTint();
        }

        if(selected)
        {
            SelectButton();
        } else if(hovering)
        {
            HoverButton();
        } else 
        {
            ResetButtonTint();
        }
        
        selected = false;
        hovering = false;

        if(lineDrawer != null)
        {
            lineDrawer.SetPoint(1, PlayerInput.instance.GetConvertedMousePos());
            
            if(Input.GetMouseButtonUp(0))
            {
                lineDrawer.DisableLineDraw();
                lineDrawer = null;
            }
            
        }

        
    }

   

//this sucks to implemenet, think about it for a bit before trying
    public void HoverButton()
    {
        if(is3D)
        {
            mat.SetColor("_TintColor", hoverTint);
        } else 
        {
            btn_image.color = hoverTint;
            
        }
    }
    private void SelectButton()
    {
        if(is3D)
        {
            mat.SetColor("_TintColor", selectTint);
        } else 
        {
            btn_image.color = selectTint;
            
        }
    }
    private void ResetButtonTint()
    {
        if(is3D)
        {
            mat.SetColor("_TintColor", normalTint);
        } else 
        {
            btn_image.color = normalTint;
            
        }
    }

    public override void Interact()
    {


        switch(btn_type)
        {
            case ButtonType.SPAWN:
            {
                spawnedNote = !(noteSpawned == null);
                if(spawnedNote)
                {
                    noteSpawned.GetComponent<Notes>().DisableNote();
                } else 
                {
                    noteSpawned = Instantiate(noteToSpawn, noteSpawnPoint.position + new Vector3(0,0, -0.1f), Quaternion.identity, GetComponentInParent<TempNode>().transform);
                }
                break;
            }
            
            case ButtonType.PAGE:
            {
                Notes note = GetComponentInParent<Notes>();
                
                if(pageButtonLeft)
                {
                    note.PageTurn_Left();
                } else 
                {
                    note.PageTurn_Right();
                }

                Debug.Log("turn page");
                break;
            }

            case ButtonType.DESTROY_NOTE:
            {
                GetComponentInParent<Notes>().DisableNote();

                break;
            }

            case ButtonType.TRAVEL:
            {

                /*
                for(int i = 0; i < TempNode.instance.transform.childCount; i++)
                {
                    Destroy(TempNode.instance.transform.GetChild(i).gameObject);
                }
                */

                GameManager.instance.currentLocation.SetActive(false);
                GameManager.instance.currentLocation = destination;
                travelCutscene.StartCutscene(destination);
                
                break;
            }

            case ButtonType.DIALOGUE:
            {

                DialogueManager.instance.SpawnDialogue(dialogue);

                break;
            }

            case ButtonType.TACK:
            {
                lineDrawer = GameManager.instance.SpawnLineDrawer();

                lineDrawer.SetPoint(0, PlayerInput.instance.GetConvertedMousePos());


                break;
            }

            case ButtonType.TOGGLE:
            {
                state = !state;

                objectToToggle.SetActive(state);

                break;
            }

        }

        
    }


    void GetMaterialInstance()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();

        mesh = GetComponent<MeshRenderer>() != null ? GetComponent<MeshRenderer>() : GetComponentInChildren<MeshRenderer>();


        mat = mesh.material;
        mat.GetColor("_TintColor");
//        Debug.Log(mat.name);
    }

}

public enum ButtonType
{
    SPAWN, PAGE, DESTROY_NOTE, TRAVEL, DIALOGUE, TACK, TOGGLE
}
