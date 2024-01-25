using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Button : Interactable
{

    public ButtonType btn_type = ButtonType.SPAWN;

    public Color normalTint = Color.white;
    public Color hoverTint = Color.white;
    public Color selectTint = Color.white;
    public SpriteRenderer spriteRenderer;/// <summary>
    /// grrrrr switch this to an image or something, some buttons have images some have sprites, which one is better idek
    /// </summary>


    [Header("Spawn Button Settings")]
    public GameObject noteToSpawn;
    public Transform noteSpawnPoint;
    private GameObject noteSpawned;
    private bool spawnedNote;


    [Header("Page Button Settings")]
    public bool pageButtonLeft = false;

    // Start is called before the first frame update
    void Awake()
    {
        //spriteRenderer.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //this is fine for now, but should be event based later
        if(Input.GetMouseButtonUp(0))
        {
            ResetButtonTint();
        }
    }

//this sucks to implemenet, think about it for a bit before trying
    private void HoverButton()
    {
        spriteRenderer.color = hoverTint;
    }

    private void SelectButton()
    {
        spriteRenderer.color = selectTint;
    }

    private void ResetButtonTint()
    {
        spriteRenderer.color = normalTint;
    }

    public override void Interact()
    {
        SelectButton();


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
                    noteSpawned = Instantiate(noteToSpawn, noteSpawnPoint.position, Quaternion.identity);
                }
                Debug.Log("spawn somethin");
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

        }
    }


}

public enum ButtonType
{
    SPAWN, PAGE, DESTROY_NOTE
}
