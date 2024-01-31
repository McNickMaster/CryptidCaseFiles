using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/DialogueScriptableObject", order = 1)]
public class DialogueScriptable : ScriptableObject
{

    public string dialogueName;
    public int numPages = 1;//get rid of
    public string[] dialogueNames;
    public GameObject dialogueObject;
    private bool multipage;

    public void Init()
    {
        multipage = numPages > 1 ? true : false;
        dialogueNames = new string[numPages];

        if(multipage)
        {
            
            for(int i = 0; i < numPages; i++)
            {
                dialogueNames[i] = dialogueName + "_" + (i + 1);
            }


        } else 
        {
            dialogueNames[0] = dialogueName;
        }
    }

    public string GetDialoguePage(int i)
    {
        return dialogueNames[i];
    }
    

}
