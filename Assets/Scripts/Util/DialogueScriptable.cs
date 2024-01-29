using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/DialogueScriptableObject", order = 1)]
public class DialogueScriptable : ScriptableObject
{

    public string dialogueName;
    public int numPages = 1;
    public string[] dialogueNames;

    public void Init()
    {
        dialogueNames = new string[numPages];
        for(int i = 0; i < numPages; i++)
        {
            dialogueNames[i] = dialogueName + "_" + (i + 1);
        }
    }

    public string GetDialoguePage(int i)
    {
        return dialogueNames[i];
    }
    

}
