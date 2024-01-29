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


    // Start is called before the first frame update
    void Awake()
    {
        btn_image = GetComponent<Image>();

        if(is3D)
        {            
            GetMaterialInstance();
        } else 
        {

        }

        if(btn_type == ButtonType.DIALOGUE)
        {
            dialogue.Init();
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
                    noteSpawned = Instantiate(noteToSpawn, noteSpawnPoint.position, Quaternion.identity, GetComponentInParent<TempNode>().transform);
                }
//                Debug.Log("spawn somethin");
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

                thisLocation.SetActive(false);
                travelCutscene.StartCutscene(destination);
                break;
            }

            case ButtonType.DIALOGUE:
            {

                DialogueManager.instance.SpawnDialogue(dialogue);

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
    SPAWN, PAGE, DESTROY_NOTE, TRAVEL, DIALOGUE
}
