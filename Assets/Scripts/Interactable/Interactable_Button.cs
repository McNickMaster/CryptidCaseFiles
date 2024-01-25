using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Button : Interactable
{

    public ButtonType btn_type = ButtonType.SPAWN;

    public bool pageButtonLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void Interact()
    {
        switch(btn_type)
        {
            case ButtonType.SPAWN:
            {
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

        }
    }


}

public enum ButtonType
{
    SPAWN, PAGE
}
