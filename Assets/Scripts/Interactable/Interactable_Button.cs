using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
public class Interactable_Button : Interactable
{

    public ButtonType btn_type = ButtonType.SPAWN;
    public bool is3D = true;
    public AudioClip myClip;
    private MeshRenderer[] meshes;
    //private Material mat;
    private Material[] mats;
    public bool isVisible = true;
    public Color normalTint = Color.white;
    public Color hoverTint = new Color(0.9f, 0.9f, 0.9f);
    public Color selectTint = new Color(0.8f, 0.8f, 0.8f);
    private Image btn_image;
    
    [HideInInspector]
    public bool hovering = false;
    [HideInInspector]
    public bool selected = false;


    [Header("Travel Button Settings")]
    public LocationManager destination;
    public e_Scene destinationScene;
    //public LocationManager thisLocation;
    

    [Header("Spawn Button Settings")]
    public GameObject noteToSpawn;
    public Transform noteSpawnPoint;
    public SimpleTextData textData;
    private GameObject noteSpawned;
    private bool spawnedNote;

    [Header("Page Button Settings")]
    public bool pageButtonLeft = false;

    [Header("Dialogue Button Settings")]
    public DialogueData dialogue;

    [Header("Monologue Button Settings")]
    public SimpleTextData monologue;

    [Header("Toggle Button Settings")]
    public GameObject objectToToggle;
    public bool state = false;

    [Header("Change View Button Settings")]
    public View newView;
    private DrawLine lineDrawer;



    // Start is called before the first frame update
    void Awake()
    {
        
        if(isVisible)
        {
            if(is3D)
            {            
                GetMaterialInstance();
            } else 
            {
                btn_image = GetComponent<Image>();
            }
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
        if(isVisible)
        {

            if(is3D)
            {
                SetAllMats(hoverTint);
            } else 
            {
                btn_image.color = hoverTint;
                
            }
        }
    }
    private void SelectButton()
    {
        
        if(isVisible)
        {
            if(is3D)
            {
                SetAllMats(selectTint);
            } else 
            {
                btn_image.color = selectTint;
                
            }
        }
    }
    private void ResetButtonTint()
    {
        if(isVisible)
        {
                
            if(is3D)
            {
                SetAllMats(normalTint);
            } else 
            {
                btn_image.color = normalTint;
                
            }

        }
    }

    public override void Interact()
    {
        SoundManager.instance.PlaySFX(myClip);

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
                    noteSpawned.GetComponent<Notes>().textData = textData;
                    noteSpawned.GetComponent<Notes>().Init();
                }
                break;
            }

            case ButtonType.SPAWN_STICKY:
            {
                GameObject obj = Instantiate(noteToSpawn, noteSpawnPoint.position + new Vector3(0,0, -0.1f), Quaternion.identity, GetComponentInParent<TempNode>().transform);
                obj.GetComponentInChildren<Canvas>().worldCamera = GameManager.instance.currentView.myCamera;
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
                SoundManager.instance.PlaySFX(GameData.instance.pageTurn);
//                Debug.Log("turn page");
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
                
                GameManager.instance.travelCutscene.StartCutscene();
                GameManager.instance.Travel(destinationScene);
                
                break;
            }

            case ButtonType.DIALOGUE:
            {
  
                DialogueLoader.instance.LoadConversation(dialogue.fileName);
                DialogueLoader.instance.StartConversation();
                

                break;
            }

            case ButtonType.MONOLOGUE:
            {
                DialogueLoader.instance.LoadMonologue(""+monologue.order);

                break;
            }

            case ButtonType.TACK:
            {
                lineDrawer = GameManager.instance.SpawnLineDrawer();

                lineDrawer.SetPoint(0, transform.position);


                break;
            }

            case ButtonType.TOGGLE:
            {
                state = !state;

                objectToToggle.SetActive(state);

                break;
            }

            case ButtonType.SOLVE_CASE:
            {

                GameManager.instance.TrySolveCase();

                break;
            }

            case ButtonType.CHANGE_VIEW:
            {
                GameManager.instance.SetView(newView);

                break;
            }

        }

        
    }

    public void SetGuess()
    {

    }

    void GetMaterialInstance()
    {

        meshes = GetComponentsInChildren<MeshRenderer>();
        mats = new Material[meshes.Length];
        for(int i = 0; i < meshes.Length; i++)
        {
            mats[i] = meshes[i].material;
        }


    }



    void GetMaterialInstance_Old()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();

        mesh = GetComponent<MeshRenderer>() != null ? GetComponent<MeshRenderer>() : GetComponentInChildren<MeshRenderer>();


       // mat = mesh.material;


        
//        Debug.Log(mat.name);
    }

    void SetAllMats(Color c)
    {
        foreach(Material mat in mats)
        {
            mat.SetColor("_TintColor", c);
        }
    }

}

public enum ButtonType
{
    SPAWN, PAGE, DESTROY_NOTE, TRAVEL, DIALOGUE, TACK, TOGGLE, SOLVE_CASE, CHANGE_VIEW, MONOLOGUE, SPAWN_STICKY
}
